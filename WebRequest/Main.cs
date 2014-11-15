using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Spike.Core.Net;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Runtime.Serialization;
using log4net;
//using Newtonsoft.Json;
//using System.Web.Script.Serialization;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]  
namespace XXX
{
    public partial class Main : Form
    {
        private HttpClient hc;

        private StringBuilder result = new StringBuilder();

        private static readonly ILog log = LogHelper.GetInstance();

        public Main()
        {
            InitializeComponent();
            hc = new HttpClient();
        }

        private void logWrite(string strLog)
        {  
            log.Info(strLog);
            result.AppendLine(strLog);
            this.txt_Output.Text = result.ToString();
        }

        private void btn_ValidatePhoneNumber_Click(object sender, EventArgs e)
        {
            string strResult = hc.GetString("https://ac.wxcs.cn/user.do?do=Vali.checkPhoneNo&phoneNO=18081907231");

            logWrite(strResult);
        }

        string requestStr = string.Empty;

        private void btn_GetImg_Click(object sender, EventArgs e)
        {
            string requestStr = "https://ac.wxcs.cn/portal.do?do=SecurityCode.ajaxGetCode&ajaxTimestamp=" + Common.ConvertDateTimeInt(DateTime.Now).ToString();


            logWrite(requestStr);


            string strResult = hc.GetString(requestStr);

            logWrite(strResult);

            strResult = "[" + strResult + "]";

            var results = JSON.parse<List<Result>>(strResult);

            logWrite(string.Format("state={0},data={1}", results[0].state, results[0].data));

            requestStr = "https://ac.wxcs.cn/vcode?vkey=" + results[0].data;


            logWrite(requestStr);

            string filename = hc.GetImg(requestStr);

            logWrite(filename);

            ///////////////////////////////////////////////
            //string filename = this.pic_ValidateImg.Tag.ToString();
            //////////////////////////////////////////////
            logWrite(filename);

            this.pic_ValidateImg.Image = Image.FromFile(filename);

            strResult = ImageVerify.GetStrFromBmp(filename);

            logWrite(strResult);

            strResult = ImageVerify.imageVerify(filename);

            logWrite(strResult);

            this.lbl_result.Text = strResult;
        }

        private void btn_Browser_Click(object sender, EventArgs e)
        {
            //this.openFileDialog1.ShowDialog.RootFolder = Environment.SpecialFolder.MyComputer;

            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog1.FileName;
                if (!string.IsNullOrEmpty(filePath))
                {
                    this.pic_ValidateImg.Image = Image.FromFile(filePath);
                    this.pic_ValidateImg.Tag = filePath;
                }
            }
        }

        private void btn_PwdGen_Click(object sender, EventArgs e)
        {
            logWrite(RandomPassword.GetRandomPWD());
        }
    }
}
