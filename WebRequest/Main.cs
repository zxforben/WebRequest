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
//using Newtonsoft.Json;
//using System.Web.Script.Serialization;

namespace XXX
{
     public partial class Main : Form
    {
        HttpClient hc;

        StringBuilder result = new StringBuilder();

        public Main()
        {
            InitializeComponent();
            hc = new HttpClient();
        }

        private void btn_ValidatePhoneNumber_Click(object sender, EventArgs e)
        {
            result.Append(hc.GetString("https://ac.wxcs.cn/user.do?do=Vali.checkPhoneNo&phoneNO=18081907231"));

            //hc.Get("https://ac.wxcs.cn/user.do?do=Vali.checkPhoneNo&phoneNO=18081907231", Output);

            txt_Output.Text = result.ToString();


        }

        private void Output(string text)
        {
            result.Append(text);
            //txt_Output.Text = result.ToString();
        }

        string requestStr = string.Empty;

        private void btn_GetImg_Click(object sender, EventArgs e)
        {
            requestStr = "https://ac.wxcs.cn/portal.do?do=SecurityCode.ajaxGetCode&ajaxTimestamp=" + Common.ConvertDateTimeInt(DateTime.Now).ToString();
            result.Append(requestStr);
            requestStr = hc.GetString(requestStr);
            result.Append(requestStr);

            requestStr = "[" + requestStr + "]";

            var results = JSON.parse<List<Result>>(requestStr);

            result.Append(string.Format("state={0},data={1}", results[0].state, results[0].data));

            requestStr = "https://ac.wxcs.cn/vcode?vkey=" + results[0].data;

            result.Append(requestStr);
            string filename = hc.GetImg(requestStr);

            this.pic_ValidateImg.Image = Image.FromFile(filename);

            result.Append(ImageVerify.GetStrFromBmp(filename));

            result.Append(ImageVerify.imageVerify(filename));

            txt_Output.Text = result.ToString();

        }


        // using System.Runtime.Serialization.Json;

        
    }



}
