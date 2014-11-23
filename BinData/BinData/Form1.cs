using System;
using System.Reflection;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Text;
using System.IO;
using System.Collections.Generic;

namespace BinData
{
    public partial class Form1 : Form
    {
        public Form1()
        { InitializeComponent(); }

        private void BinData_Click(object sender, EventArgs e)
        {
            string currentfileName = "";
            try
            {
                MeFile.InitFileList(".xlsx");
                string[] allFileName = MeFile.GetNameList();

                Dictionary<string, string> parseFuncs = new Dictionary<string, string>();

                //this.OutPut.Text = Utility.NowTime() + "开始解析Excel表" + Utility.strEnd;

                foreach (string fileName in allFileName)
                {
                    //ServerParser.ParseServer( fileName );
                    currentfileName = fileName;
                    ClientParser.ParseClient( fileName );
                    this.OutPut.Text +=  fileName + ".xlsx\t\t解析完成" + ServerParser.strEnd;
                }

                // 完成
                this.OutPut.Text += ServerParser.NowTime() + "表格全部解析完成" + ServerParser.strEnd;
            }
            catch (System.Exception E)
            {
                this.OutPut.Text += E.Message;
                this.OutPut.Text += currentfileName;
                ServerParser.EndParse();
                ClientParser.EndParse();
            }
            finally
            {
                ServerParser.EndParse();
                ClientParser.EndParse();
            }
        }
    }
}
