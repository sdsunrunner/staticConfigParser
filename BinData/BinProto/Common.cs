using System;
using System.Reflection;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Text;
using System.Collections.Generic;

namespace BinProto
{
    class Common
    {
        // const字符串
        public const string csTab = "\t";
        public const string csRet = "\r\n";
        public const string csSem = ";";
        public const string csCut = ",";
        public const string csPro = ".proto";

        public const string csMsg = "message ";
        public const string csBeg = "{" + csRet;
        public const string csEnd = "}";

        public const string csReq = "required ";
        public const string csOpt = "optional ";
        public const string csRpt = "repeated ";
        
        public const string userAll = "a";
        public const string userClient = "c";
        public const string userServer = "s";
        public const string userNull = "#";

        public const string spaceClient = "clientdata.";
        public const string spaceServer = "serverdata.";

        // const数值
        public const int nUser = 1;    // 第1行 字段用户是客户端or服务器or公用
        public const int nType = 2;    // 第2行 字段数据类型
        public const int nNote = 3;    // 第3行 注释

        public static string NowTime() { return DateTime.Now.ToString(); }

        // Excel相关对象
        public static Excel.Application xApp;
        public static Excel.Workbook xBook;
        public static Excel.Worksheet xSheet;
        public static int nSheetIndex;

        // 打开工作薄
        public static void GetBook(string szName)
        {
            xBook = xApp.Workbooks.Open(szName, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
        }

        // 打开指定Worksheet
        public static void GetSheet(Excel.Workbook xBook)
        { xSheet = (Excel.Worksheet)xBook.Sheets[nSheetIndex]; }

        // 行数
        public static int Rows()
        { return xSheet.UsedRange.Cells.Rows.Count; }

        // 列数
        public static int Cols()
        { return xSheet.UsedRange.Cells.Columns.Count; }

        // 分割字符串
        public static String[] SubString(String strData, char separator)
        {
            String[] strArray = strData.Split(separator);
            return strArray;
        }
        public static string GetString(Excel.Range range)
        {
            if (null == range.Value2)
            { return ""; }
            return range.Value2.ToString();
        }

        // 解析整型字段
        public static Int32 ReadInt32(int i, int j)
        {
            Excel.Range range = xSheet.Cells[i, j];
            if (null == range.Value2)
            { return 0; }
            return System.Convert.ToInt32(range.Value2.ToString());
        }

        public static Int64 ReadInt64(int i, int j)
        {
            Excel.Range range = xSheet.Cells[i, j];
            if (null == range.Value2)
            { return 0; }
            return System.Convert.ToInt64(range.Value2.ToString());
        }

        // 解析float
        public static float ReadFloat(int i, int j)
        {
            Excel.Range range = xSheet.Cells[i, j];
            if (null == range.Value2)
            { return 0; }
            return System.Convert.ToSingle(range.Value2.ToString());
        }
        
        // 开始解析Excel
        private static void StartParse(string fileName)
        {
            nSheetIndex = 1;
            xApp = new Excel.Application();
            GetBook(MeFile.GetFilePath(fileName));
            GetSheet(xBook);
        }

        // 下一页
        private static void NextSheet()
        {
            ++nSheetIndex;
            GetSheet(xBook);
        }

        // 完成Excel解析
        public static void EndParse()
        {
            nSheetIndex = 1;
            xSheet = null;
            xBook = null;
            if ( null != xApp )
            { xApp.Quit(); }
            xApp = null;
        }

        

        // 解析Excel
        public static int ParseExcel(string fileName)
        {
            if ( fileName.Contains( "$" ) )
            { return 0; }

            StartParse(fileName);

            // 保存当前proto和code
            string protoServer = Proto.strServer;
            string protoClient = Proto.strClient;
            string codeServer = Encoder.codeServer;
            string codeClient = Encoder.codeClient;
            
            string className = fileName + "Data";
            string listName = fileName + "List";

            string clientfunc = "ParseClient" + fileName;
            string serverfunc = "ParseServer" + fileName;
            Encoder.AddClientFunc(fileName, clientfunc);
            Encoder.AddServerFunc(fileName, serverfunc);

            // 
            Encoder.AddClient("public static void " + clientfunc + "( string fileName )");
            Encoder.AddServer("public static void " + serverfunc + "( string fileName )");

            Encoder.AddCode("{");  // parsefunction
            Encoder.AddCode("StartParse(fileName);");

            Encoder.AddClient(spaceClient + listName + " xList = new " + spaceClient + listName + "();");
            Encoder.AddServer(spaceServer + listName + " xList = new " + spaceServer + listName + "();");

            Encoder.AddCode("int x = 1;");
            Encoder.AddCode("string[] sArray;");
            Encoder.AddCode("// 数据从第5行开始");
            Encoder.AddCode("for (int i = 5; i <= Rows(); ++i)");
            Encoder.AddCode("{"); // forfiled


            // 读取表头
            string[] strUser = new string[ Cols() ];
            string[] strType = new string[ Cols() ];
            string[] strName = new string[ Cols() ];
            for (int i = 1; i <= Cols(); ++i)
            {
                Excel.Range range = xSheet.Cells[1,i];
                if (null == range.Value2)
                { return i; }
                strUser[i - 1] = range.Value2.ToString();

                if (userNull == strUser[i - 1])
                {
                    strType[i - 1] = "";
                    strName[i - 1] = "";
                }
                else
                {
                    range = xSheet.Cells[2, i];
                    if (null == range.Value2)
                    { return i; }
                    strType[i - 1] = range.Value2.ToString();

                    range = xSheet.Cells[4, i];
                    if (null == range.Value2)
                    { return i; }
                    strName[i - 1] = range.Value2.ToString();
                }                
            }

            // 是否某端专用表格
            bool serverOnly = true; // 没有客户端数据
            bool clientOnly = true; // 没有服务器数据

            foreach (string user in strUser)
            {
                switch (user)
                {
                    case userAll:
                        {
                            serverOnly = false;
                            clientOnly = false;
                        }
                        break;
                    case userClient: // 有客户端数据
                        { serverOnly = false; }
                        break;
                    case userServer: // 有服务器数据
                        { clientOnly = false; }
                        break;
                    default:
                        break;
                }
            }

            string strProto = "";

            // Proto类名
            strProto = csMsg + className + csRet + csBeg;
            Proto.AddServer( strProto );
            Proto.AddClient( strProto );

            // 解析代码
            Encoder.AddClient(spaceClient + className + " xData = new " + spaceClient + className + "();");
            Encoder.AddServer(spaceServer + className + " xData = new " + spaceServer + className + "();");

            int cIndex = 1;
            int sIndex = 1;
            for (int i = 1; i <= Cols(); ++i)
            {
                // 读取字段代码
                Encoder.EncodeFiled(strUser[i - 1], strType[i - 1], strName[i - 1], false);

                // 跳过注释行
                if (userNull == strUser[i - 1])
                { continue; }

                // proto字段定义
                strProto = Proto.GetTypeDefine(strType[i - 1]);

                switch (strUser[i - 1])
                {
                    case userAll:
                        {
                            Proto.AddServer(strProto + strName[i - 1] + " = " + sIndex.ToString() + csSem + csRet);
                            Proto.AddClient(strProto + strName[i - 1] + " = " + cIndex.ToString() + csSem + csRet);
                            ++sIndex;
                            ++cIndex;
                        }
                        break;
                    case userServer:
                        {
                            Proto.AddServer(strProto + strName[i - 1] + " = " + sIndex.ToString() + csSem + csRet);
                            ++sIndex;
                        }
                        break;
                    case userClient:
                        {
                            Proto.AddClient(strProto + strName[i - 1] + " = " + cIndex.ToString() + csSem + csRet);
                            ++cIndex;
                        }
                        break;
                    default:
                        return i;
                }                
            }

            // ProtoList定义
            strProto = csEnd + csRet + csRet;
            strProto += csMsg + " " + fileName + "List" + csRet;
            strProto += csBeg + csTab + csRpt + className + " data = 1;" + csRet;
            Proto.AddServer(strProto);
            Proto.AddClient(strProto);

            // list.add代码
            Encoder.AddCode("x = 1;");
            Encoder.AddCode("xList.data.Add(xData);");
            Encoder.AddCode("}"); // forfiled
            Encoder.AddCode("NextSheet();");
            Encoder.AddCode("x = 1;");

            // 解析第二页
            NextSheet();

            // 判断有没有数据
            Excel.Range xRag = xSheet.Cells[1, 1];
            if (null == xRag.Value2)
            {
                Encoder.EncodeSaveFile( listName );

                strProto = csEnd + csRet + csRet;
                Proto.AddServer(strProto);
                Proto.AddClient(strProto);

                if (serverOnly)
                {
                    Proto.SetClient(protoClient);
                    Encoder.SetClient(codeClient);
                    Encoder.RemoveClientFunc(fileName);
                }

                if (clientOnly)
                {
                    Proto.SetServer(protoServer);
                    Encoder.SetServer(codeServer);
                    Encoder.RemoveServerFunc(fileName);
                }
                
                EndParse();
                return 0;
            }

            strUser = new string[Cols()];
            strType = new string[Cols()];
            strName = new string[Cols()];
            for (int i = 1; i <= Cols(); ++i)
            {
                Excel.Range range = xSheet.Cells[1, i];
                if (null == range.Value2)
                { return i; }
                strUser[i - 1] = range.Value2.ToString();

                if (userNull == strUser[i - 1])
                {
                    strType[i - 1] = "";
                    strName[i - 1] = "";
                }
                else
                {
                    range = xSheet.Cells[2, i];
                    if (null == range.Value2)
                    { return i; }
                    strType[i - 1] = range.Value2.ToString();

                    range = xSheet.Cells[4, i];
                    if (null == range.Value2)
                    { return i; }
                    strName[i - 1] = range.Value2.ToString();
                }
            }

            cIndex = 2;
            sIndex = 2;
            for (int i = 1; i <= Cols(); ++i)
            {
                // 读取字段代码
                Encoder.EncodeFiled(strUser[i - 1], strType[i - 1], strName[i - 1], true);

                // 跳过注释行
                if (userNull == strUser[i - 1])
                { continue; }

                // 字段定义
                strProto = Proto.GetTypeDefine(strType[i - 1]);

                switch (strUser[i - 1])
                {
                    case userAll:
                        {
                            Proto.AddServer(strProto + strName[i - 1] + " = " + sIndex.ToString() + csSem + csRet);
                            Proto.AddClient(strProto + strName[i - 1] + " = " + cIndex.ToString() + csSem + csRet);
                            ++sIndex;
                            ++cIndex;
                        }
                        break;
                    case userServer:
                        {
                            Proto.AddServer(strProto + strName[i - 1] + " = " + sIndex.ToString() + csSem + csRet);
                            ++sIndex;
                        }
                        break;
                    case userClient:
                        {
                            Proto.AddClient(strProto + strName[i - 1] + " = " + cIndex.ToString() + csSem + csRet);
                            ++cIndex;
                        }
                        break;
                    default:
                        return i;
                }
            }

            strProto = csEnd + csRet + csRet;
            Proto.AddServer(strProto);
            Proto.AddClient(strProto);

            Encoder.EncodeSaveFile( listName );

            if ( serverOnly )
            {
                Proto.SetClient( protoClient );
                Encoder.SetClient( codeClient );
                Encoder.RemoveClientFunc(fileName);
            }

            if ( clientOnly )
            {
                Proto.SetServer( protoServer );
                Encoder.SetServer( codeServer );
                Encoder.RemoveServerFunc(fileName);
            }
            EndParse();
            return 0;
        }
    }
}
