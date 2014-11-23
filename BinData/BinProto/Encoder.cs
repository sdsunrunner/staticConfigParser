using System;
using System.Reflection;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Text;
using System.Collections.Generic;
using System.IO;

namespace BinProto
{
    class Encoder
    {
        // 生成的解析函数名
        public static Dictionary<string, string> dicClientFunc = new Dictionary<string, string>();
        public static void AddClientFunc(string fileName, string funcName)
        { dicClientFunc.Add(fileName, funcName); }

        public static string GetClientFunc(string fileName)
        { return dicClientFunc[fileName]; }

        public static void RemoveClientFunc(string fileName)
        { dicClientFunc.Remove(fileName); }

        public static Dictionary<string, string> dicServerFunc = new Dictionary<string, string>();
        public static void AddServerFunc(string fileName, string funcName)
        { dicServerFunc.Add(fileName, funcName); }

        public static string GetServerFunc(string fileName)
        { return dicServerFunc[fileName]; }

        public static void RemoveServerFunc(string fileName)
        { dicServerFunc.Remove(fileName); }

        // 生成解析代码
        public static string codeClient;
        public static string codeServer;

        public static void SetClient(string strCode)
        { codeClient = strCode; }

        public static void SetServer(string strCode)
        { codeServer = strCode; }

        public static void AddClient(string strCode)
        { codeClient += strCode + Common.csRet; }

        public static void AddServer(string strCode)
        { codeServer += strCode + Common.csRet; }

        public static void AddCode(string strCode)
        {
            AddClient(strCode);
            AddServer(strCode);
        }

        public static void AddCode(string strCode, string user)
        {
            switch (user)
            {
                case Common.userAll:
                    { AddCode(strCode); }
                    break;
                case Common.userClient:
                    { AddClient(strCode); }
                    break;
                case Common.userServer:
                    { AddServer(strCode); }
                    break;
                default:
                    break;
            }
        }

        public static void AddClientReturn()
        { codeClient += Common.csRet; }

        public static void AddServerReturn()
        { codeServer += Common.csRet; }

        public static void AddReturn()
        {
            AddClientReturn();
            AddServerReturn();
        }

        public static void AddReturn(string user)
        {
            switch (user)
            {
                case Common.userAll:
                    { AddReturn(); }
                    break;
                case Common.userClient:
                    { AddClientReturn(); }
                    break;
                case Common.userServer:
                    { AddServerReturn(); }
                    break;
                default:
                    break;
            }
        }

        // 字段解析编码
        public static void EncodeFiled(string user, string type, string name, bool bConst)
        {
            if (Common.userNull == user)
            { return; }

            string owner;
            if (true == bConst)
            { owner = "xList."; }
            else
            { owner = "xData."; }

            string strCode = "";
            switch (type)
            {
                case "int":
                    {
                        if (true == bConst)
                        { strCode = owner + name + " = ReadInt32( 5,  x++ );"; }
                        else
                        { strCode = owner + name + " = ReadInt32( i,  x++ );"; }
                    }
                    break;
                case "intarray":
                    {
                        if (true == bConst)
                        { strCode = "sArray = SubString(GetString(xSheet.Cells[5, x++]), \';\');"; }
                        else
                        { strCode = "sArray = SubString(GetString(xSheet.Cells[i, x++]), \';\');"; }

                        strCode += "foreach (string sData in sArray)";
                        strCode += "{";
                        strCode += owner + name + ".Add(System.Convert.ToInt32(sData));";
                        strCode += "}";
                    }
                    break;
                case "int64":
                    {
                        if (true == bConst)
                        { strCode = owner + name + " = ReadInt64( 5,  x++ );"; }
                        else
                        { strCode = owner + name + " = ReadInt64( i,  x++ );"; }

                    }
                    break;
                case "int64array":
                    {
                        if (true == bConst)
                        { strCode = "sArray = SubString(GetString(xSheet.Cells[5, x++]), \';\');"; }
                        else
                        { strCode = "sArray = SubString(GetString(xSheet.Cells[i, x++]), \';\');"; }

                        strCode += "foreach (string sData in sArray)";
                        strCode += "{";
                        strCode += owner + name + ".Add(System.Convert.ToInt64(sData));";
                        strCode += "}";

                    }
                    break;
                case "float":
                    {
                        if (true == bConst)
                        { strCode = owner + name + " = ReadFloat( 5,  x++ );"; }
                        else
                        { strCode = owner + name + " = ReadFloat( i,  x++ );"; }
                    }
                    break;
                case "floatarray":
                    {
                        if (true == bConst)
                        { strCode = "sArray = SubString(GetString(xSheet.Cells[5, x++]), \';\');"; }
                        else
                        { strCode = "sArray = SubString(GetString(xSheet.Cells[i, x++]), \';\');"; }

                        strCode += "foreach (string sData in sArray)";
                        strCode += "{";
                        strCode += owner + name + ".Add(System.Convert.ToSingle(sData));";
                        strCode += "}";
                    }
                    break;
                case "string":
                    {
                        if (true == bConst)
                        { strCode = owner + name + " = ReadString( 5,  x++ );"; }
                        else
                        { strCode = owner + name + " = ReadString( i,  x++ );"; }
                    }
                    break;
                case "stringarray":
                    {
                        if (true == bConst)
                        { strCode = "sArray = SubString(GetString(xSheet.Cells[5, x++]), \';\');"; }
                        else
                        { strCode = "sArray = SubString(GetString(xSheet.Cells[i, x++]), \';\');"; }

                        strCode += "foreach (string sData in sArray)";
                        strCode += "{";
                        strCode += owner + name + ".Add(Encoding.UTF8.GetBytes(sData));";
                        strCode += "}";
                    }
                    break;
            }
            switch (user)
            {
                case Common.userAll:
                    { AddCode(strCode); }
                    break;
                case Common.userClient:
                    {
                        AddClient(strCode);
                        AddServer("x++;");
                    }
                    break;
                case Common.userServer:
                    {
                        AddServer(strCode);
                        AddClient("x++;");
                    }
                    break;
            }
        }

        public static void EncodeSaveFile(string listName)
        {
            AddReturn();
            AddClient("string fPath = System.Environment.CurrentDirectory + @\"\\..\\..\\DataClient\\\";");
            AddServer("string fPath = System.Environment.CurrentDirectory + @\"\\..\\..\\DataServer\\\";");
            AddCode("FileStream wFile = new FileStream(fPath + fileName + \".dat\", FileMode.Create, FileAccess.Write);");
            AddCode("Serializer.Serialize<" + listName + ">(wFile, xList);");
            AddCode("wFile.Close();");
            AddReturn();

            AddCode("FileStream rFile = new FileStream(fPath + fileName + \".dat\", FileMode.Open, FileAccess.Read);");
            AddCode(listName + " readList = Serializer.Deserialize<" + listName + ">(rFile);");
            AddCode("rFile.Close();");
            AddCode("EndParse();");
            AddCode("}"); // parsefunction
            AddReturn();
        }

        // 输出公共代码
        public static void StartCode()
        {
            // 输出代码
            codeClient = "";

            AddCode( "/**************************************************************************" );
            AddCode( " *" );
            AddCode( " *" );
            AddCode(" *\t\t\t\t\t此文件为自动生成 不要自行更改!!!");
            AddCode( " *" );
            AddCode( " *" );
            AddCode( " *************************************************************************/" );

            // 头文件
            AddCode( "using System;" );
            AddCode("using System.Reflection;");
            AddCode( "using Excel = Microsoft.Office.Interop.Excel;" );
            AddCode( "using System.Text;" );
            AddCode( "using System.IO;" );
            AddCode( "using ProtoBuf;" );
            AddClient( "using clientdata;" );
            AddServer("using serverdata;");
            AddReturn();

            AddCode( "namespace BinData" );
            AddCode( "{" );// namespace BinData
            AddClient( "class ClientParser" );
            AddServer( "class ServerParser" );
            AddCode( "{" );// class Parser
            AddCode( "// const字符串" );
            AddCode( "public static string strEnd = \" \\r\\n\";" );
            AddCode( "public static string strXlsx = \".xlsx\";" );
            AddCode( "public static string strDat = \".dat\";" );
            AddCode( "public static string NowTime() { return DateTime.Now.ToString() + \" \"; }" );
            AddReturn();
            
            AddCode( "// Excel相关对象" );
            AddCode( "public static Excel.Application xApp;" );
            AddCode( "public static Excel.Workbook xBook;" );
            AddCode( "public static Excel.Worksheet xSheet;" );
            AddCode( "public static int nSheetIndex;" );
            AddReturn();

            AddCode( "// 打开工作薄" );
            AddCode( "public static void GetBook(string szName)" );
            AddCode( "{" ); // GetBook
            AddCode( "xBook = xApp.Workbooks.Open(szName, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);" );
            AddCode( "}" ); // GetBook
            AddReturn();

            AddCode( "// 打开指定Worksheet" );
            AddCode( "public static void GetSheet()" );
            AddCode( "{ xSheet = (Excel.Worksheet)xBook.Sheets[nSheetIndex]; }" );
            AddReturn();

            AddCode( "private static void NextSheet()" );
            AddCode( "{" ); // NextSheet
            AddCode( "++nSheetIndex;" );
            AddCode( "GetSheet();" );
            AddCode( "}" );
            AddReturn();

            AddCode( "// 行数" );
            AddCode( "public static int Rows()" );
            AddCode( "{ return xSheet.UsedRange.Cells.Rows.Count; }" );
            AddReturn();

            AddCode( "// 列数" );
            AddCode( "public static int Cols()" );
            AddCode( "{ return xSheet.UsedRange.Cells.Columns.Count; }" );
            AddReturn();

            AddCode( "// 分割字符串" );
            AddCode( "public static String[] SubString(String strData, char separator)" );
            AddCode( "{" ); // SubString
            AddCode( "String[] strArray = strData.Split(separator);" );
            AddCode( "return strArray;" );
            AddCode( "}" ); // SubString
            AddReturn();

            AddCode( "public static string GetString(Excel.Range range)" );
            AddCode( "{" ); // GetString
            AddCode( "if (null == range.Value2)" );
            AddCode( "{ return \"\"; }" );
            AddCode( "return range.Value2.ToString();" );
            AddCode( "}" ); // GetString
            AddReturn();

            AddCode( "// 解析整型字段" );
            AddCode( "public static Int32 ReadInt32(int i, int j)" );
            AddCode( "{" ); // ReadInt32
            AddCode( "Excel.Range range = xSheet.Cells[i, j];" );
            AddCode( "if (null == range.Value2)" );
            AddCode( "{ return 0; }" );
            AddCode( "return System.Convert.ToInt32(range.Value2.ToString());" );
            AddCode( "}" ); // ReadInt32
            AddReturn();

            AddCode( "public static Int64 ReadInt64(int i, int j)" );
            AddCode( "{" ); // Readint64
            AddCode( "Excel.Range range = xSheet.Cells[i, j];" );
            AddCode( "if (null == range.Value2)" );
            AddCode( "{ return 0; }" );
            AddCode( "return System.Convert.ToInt64(range.Value2.ToString());" );
            AddCode( "}" ); // ReadInt64
            AddReturn();

            AddCode( "// 解析float" );
            AddCode( "public static float ReadFloat(int i, int j)" );
            AddCode( "{" ); // ReadFloat
            AddCode( "Excel.Range range = xSheet.Cells[i, j];" );
            AddCode( "if (null == range.Value2)" );
            AddCode( "{ return 0; }" );
            AddCode( "return System.Convert.ToSingle(range.Value2.ToString());" );
            AddCode( "}" ); // ReadFloat
            AddReturn();

            AddCode( "// 解析字符串" );
            AddCode( "public static byte[] ReadString(int i, int j)" );
            AddCode( "{" ); // ReadString
            AddCode( "Excel.Range range = xSheet.Cells[i, j];" );
            AddCode( "if (null == range.Value2)" );
            AddCode( "{ return Encoding.UTF8.GetBytes(\"\"); }" );
            AddCode( "return Encoding.UTF8.GetBytes(range.Value2.ToString());" );
            AddCode( "}" ); // ReadString
            AddReturn();

            AddCode( "// 开始解析Excel" );
            AddCode( "private static void StartParse(string path)" );
            AddCode( "{" ); // StartParse
            AddCode( "nSheetIndex = 1;" );
            AddCode( "string strPath = MeFile.GetFilaPath(path);" );
            AddCode( "xApp = new Excel.Application();" );
            AddCode( "GetBook(strPath);" );
            AddCode( "GetSheet();" );
            AddCode( "}" ); // StartParse
            AddReturn();

            AddCode("// 结束解析Excel");
            AddCode( "public static void EndParse()" );
            AddCode( "{" ); // EndParse
            AddCode( "nSheetIndex = 1;" );
            AddCode( "xSheet = null;" );
            AddCode( "xBook = null;" );
            AddCode( "if (null != xApp)" );
            AddCode( "{ xApp.Quit(); }" );
            AddCode( "xApp = null;" );
            AddCode( "}" ); // EndParse
            AddReturn();
        }

        public static void EndCode()
        {
            AddClient("public static void ParseClient(string fileName)");
            AddServer("public static void ParseServer(string fileName)");
            AddCode("{"); // Parse
            AddCode("if ( fileName.Contains( \"$\" ) )");
            AddCode("{ return; }");

            AddCode("switch (fileName)");
            AddCode("{"); // switch 

            foreach (KeyValuePair<string, string> kvp in dicClientFunc)
            {
                AddClient("case \"" + kvp.Key + "\":");
                AddClient("{" + kvp.Value + "( fileName ); }");
                AddClient("break;");
            }

            foreach (KeyValuePair<string, string> kvp in dicServerFunc)
            {
                AddServer("case \"" + kvp.Key + "\":");
                AddServer("{" + kvp.Value + "( fileName ); }");
                AddServer("break;");
            }

            AddCode("default:");
            AddCode("break;");
            AddCode("}"); // switch 
            AddCode("}"); // Parse

            AddCode("}"); // class Parser
            AddCode("}"); // namespace BinData

            string path = "E:\\workspace\\trunk\\tools\\BinData\\BinData\\" + "ClientParser.cs";

            byte[] cData = Encoding.UTF8.GetBytes(codeClient);
            FileStream cFile = new FileStream(path, FileMode.Create, FileAccess.Write);
            cFile.Write(cData, 0, cData.Length);
            cFile.Close();

            path = "E:\\workspace\\trunk\\tools\\BinData\\BinData\\" + "ServerParser.cs";

            byte[] sData = Encoding.UTF8.GetBytes(codeServer);
            FileStream sFile = new FileStream(path, FileMode.Create, FileAccess.Write);
            sFile.Write(sData, 0, sData.Length);
            sFile.Close();
        }
    }
}
