using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace BinProto
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void BinProto_Click(object sender, EventArgs e)
        {
            try
            {
                this.OutPut.Text = Common.NowTime() + "开始检查文件列表\r\n";

                MeFile.InitFileList(".xlsx");
                string[] allFileName = MeFile.GetNameList();

                Proto.Init();
                Encoder.StartCode();
                foreach (string fileName in allFileName)
                {
                    int nRes = Common.ParseExcel(fileName);
                    if (0 != nRes)
                    {
                        this.OutPut.Text += Common.NowTime() + " .." + fileName;
                        this.OutPut.Text += ".xlsx..[" + Common.nSheetIndex.ToString() + "]分页 [";
                        this.OutPut.Text += nRes.ToString() + "]列解析出错\r\n";
                        Common.EndParse();
                        return;
                    }
                    this.OutPut.Text += "解析" + fileName + ".xlsx 完成\r\n";
                }
                Encoder.EndCode();

                // 保存proto文件
                Proto.SaveProto();

                this.OutPut.Text += "全部解析完成 \r\n";
            }
            catch (System.Exception E)
            {
                this.OutPut.Text += E.ToString();
                Common.EndParse();
            }
            finally
            {
                Common.EndParse();
            }
        }
    }
}
