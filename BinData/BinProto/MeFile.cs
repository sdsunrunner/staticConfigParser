using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BinProto
{
    class MeFile
    {
        public static Dictionary<string, string> dicAllFile = new Dictionary<string, string>();
        // 获取当前目录(含子目录)所有扩展名为fileEx的文件名
        public static void InitFileList(string exName)
        {
            dicAllFile.Clear();
            // 遍历当前目录
            string[] curFiles = Directory.GetFiles(System.Environment.CurrentDirectory);

            //获得所有子目录
            string[] subDics = Directory.GetDirectories(System.Environment.CurrentDirectory);

            foreach (string file in curFiles)
            {
                FileInfo finfo = new FileInfo(file);
                if (finfo.Extension != exName)
                { continue; }

                string name = Path.GetFileNameWithoutExtension(file);
                if (!name.Contains("$"))
                { dicAllFile.Add(name, file); }
            }

            foreach (string subDic in subDics)
            {
                string[] files = Directory.GetFiles(subDic);

                foreach ( string file in files )
                {
                    FileInfo finfo = new FileInfo(file);
                    if (finfo.Extension != exName)
                    { continue; }

                    string name = Path.GetFileNameWithoutExtension(file);
                    if (!name.Contains("$"))
                    { dicAllFile.Add(name, file); }
                }
            }
        }

        // 获取所有文件名
        public static string[] GetNameList()
        {
            string[] nameList = new string[ dicAllFile.Count ];
            int i = 0;
            foreach (KeyValuePair<string, string> kvp in dicAllFile)
            { nameList[i++] = kvp.Key; }
            return nameList;

        }

        // 获得文件全路径
        public static string GetFilePath(string fileName)
        {
            if ( !dicAllFile.ContainsKey( fileName ) )
            { return null; }

            return dicAllFile[fileName];
        }

    }
}
