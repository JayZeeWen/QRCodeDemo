using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Drawing;
using System.Net;
using ZXing.Common;
using ZXing.QrCode;
using Newtonsoft;

namespace QRCodeDemo
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class GetEWM : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {


            ZXing.BarcodeWriter writer = new ZXing.BarcodeWriter();
            writer.Format = ZXing.BarcodeFormat.QR_CODE;
            QrCodeEncodingOptions options = new QrCodeEncodingOptions();
            options.DisableECI = true;
            options.CharacterSet = "UTF-8";
            options.Width = 150;
            options.Height = 150;
            writer.Options = options;
            string tree_id = context.Request.QueryString["tree_id"];

            #region get URL  
            //string AbsoluteUri = context.Request.Url.AbsoluteUri;
            //Uri uri = new Uri(AbsoluteUri);
            //string url = "http://" + uri.Host + "/" + uri.Segments[1] + "mobile/szum/TreeMessage.aspx?id=" + tree_id;
            //if (context.Request.IsLocal)
            //{
            //    IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
            //    IPAddress ipAddr = ipHost.AddressList[1];
            //    //url = url.Replace("localhost", ipAddr.ToString());
            //    url = url.Replace("localhost","192.168.3.27");
            //} 
            //string result = url;
            #endregion

            #region get json 
            Person p = new Person() { Name = "zhangsan", Age = 1 };
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(p);
            string result = json;
            #endregion

            Bitmap b = writer.Write(result);
            //将图片验证码保存为流Stream返回
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            b.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            context.Response.BinaryWrite(ms.ToArray());
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

    }
    #region for test , delete this part when you use it 

    public class Person
    {
        public int Age
        {
            get; set;
        }
        public string Name
        {
            get; set;
        }
    }
    #endregion
}