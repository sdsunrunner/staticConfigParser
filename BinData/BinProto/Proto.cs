using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BinProto
{
    class Proto
    {
        // proto文件输出
        public static string strServer;
        public static string strClient;

        public static void SetServer(string strData)
        { strServer = strData; }
        public static void AddServer(string strData)
        { strServer += strData; }

        public static void SetClient(string strData)
        { strClient = strData; }
        public static void AddClient(string strData)
        { strClient += strData; }

        public static void Init()
        {
            strServer = "";
            strClient = "";
        }

        // 字段定义
        public static string GetTypeDefine(string type)
        {
            string typeDeine = "";
            switch (type)
            {
                case "int":
                    { typeDeine = Common.csTab + Common.csReq + "sint32 "; }
                    break;
                case "intarray":
                    { typeDeine = Common.csTab + Common.csRpt + "sint32 "; }
                    break;
                case "int64":
                    { typeDeine = Common.csTab + Common.csReq + "sint64 "; }
                    break;
                case "int64array":
                    { typeDeine = Common.csTab + Common.csRpt + "sint64 "; }
                    break;
                case "float":
                    { typeDeine = Common.csTab + Common.csReq + "float "; }
                    break;
                case "floatarray":
                    { typeDeine = Common.csTab + Common.csRpt + "float "; }
                    break;
                case "string":
                    { typeDeine = Common.csTab + Common.csReq + "bytes "; }
                    break;
                case "stringarray":
                    { typeDeine = Common.csTab + Common.csRpt + "bytes "; }
                    break;
            }
            return typeDeine;
        }

        public static void SaveProto()
        {
            // 生成serer proto文件
            byte[] sData = new UTF8Encoding().GetBytes(Proto.strServer);

            string path = "E:\\workspace\\trunk\\protodef\\" + "serverdata" + Common.csPro;
            FileStream fileServer = new FileStream(path, FileMode.Create, FileAccess.Write);
            fileServer.Write(sData, 0, sData.Length);
            fileServer.Close();

            // 生成client proto文件
            byte[] cData = new UTF8Encoding().GetBytes(Proto.strClient);
            path = "E:\\workspace\\trunk\\protodef\\" + "clientdata" + Common.csPro;
            FileStream fileClient = new FileStream(path, FileMode.Create, FileAccess.Write);
            fileClient.Write(cData, 0, cData.Length);
            fileClient.Close();
        }
    }
}
