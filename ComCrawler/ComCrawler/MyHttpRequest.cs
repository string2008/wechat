using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Threading;
using System.Diagnostics;

namespace ComCrawler
{
    public class MyHttpRequest
    {
        /// <summary>
        /// 获取字符流
        /// </summary>
        /// <param name="url"></param>
        /// <param name="cookieContainer"></param>
        /// <returns></returns>
        public static Stream GetStream(string url, ref CookieContainer cookieContainer, bool autoRedirect=false)
        {
            HttpWebRequest httpWebRequest = null;
            HttpWebResponse httpWebResponse = null;

            try
            {
                httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
                httpWebRequest.CookieContainer = cookieContainer;
                //httpWebRequest.ContentType = contentType;
                httpWebRequest.Referer = "https://uac.10010.com/portal/homeLogin";
                httpWebRequest.Accept = "text/javascript, application/javascript, application/ecmascript, application/x-ecmascript, */*; q=0.01";
                httpWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/39.0.2171.71 Safari/537.36";
                httpWebRequest.Method = "GET";
                httpWebRequest.ServicePoint.ConnectionLimit = int.MaxValue;
                httpWebRequest.AllowAutoRedirect=autoRedirect;

                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                Stream responseStream = httpWebResponse.GetResponseStream();

                return responseStream;
            }
            catch (Exception)
            {
                return null;
            }

        }

        /// <summary>
        /// 获取HTML
        /// </summary>
        /// <param name="url"></param>
        /// <param name="cookieContainer"></param>
        /// <returns></returns>
        public static string GetHtml(string url, ref CookieContainer cookieContainer)
        {
            Thread.Sleep(1000);
            HttpWebRequest httpWebRequest = null;
            HttpWebResponse httpWebResponse = null;
            try
            {
                httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.CookieContainer = cookieContainer;
                //httpWebRequest.ec = contentType;
                httpWebRequest.Referer = "https://uac.10010.com/portal/homeLogin";
                httpWebRequest.Accept = "text/javascript, application/javascript, application/ecmascript, application/x-ecmascript, */*; q=0.01";
                httpWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/39.0.2171.71 Safari/537.36";
                httpWebRequest.Method = "GET";
                httpWebRequest.ServicePoint.ConnectionLimit = int.MaxValue;
                httpWebRequest.KeepAlive = true;

                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                Stream responseStream = httpWebResponse.GetResponseStream();
                StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8);
                string html = streamReader.ReadToEnd();

                streamReader.Close();
                responseStream.Close();

                httpWebRequest.Abort();
                httpWebResponse.Close();

                //cookieContainer = httpWebRequest.CookieContainer;
                //string cookiesstr = cookieContainer.GetCookieHeader(httpWebRequest.RequestUri);
                //Trace.Write(cookiesstr);
                return html;
            }
            catch (WebException ex)
            {
               // var pageContent = new StreamReader(ex.Response.GetResponseStream())
               //          .ReadToEnd();
                return ex.Message;
            }

        }

        /// <summary>
        /// Post request
        /// </summary>
        /// <param name="url"></param>
        /// <param name="cookieContainer"></param>
        /// <returns></returns>
        public static string PostRequest(string url, string postString,CookieContainer cookieContainer)
        {
            string cookiesstr = cookieContainer.GetCookieHeader(new System.Uri("https://uac.10010.com/portal/Service/MallLogin?callback=jQuery17205931004374288023_1426145732103&redirectURL=http%3A%2F%2Fwww.10010.com&userName=15618603702&password=811006&pwdType=01&productType=01&redirectType=01&rememberMe=1&_=1426145797041"));
            Trace.Write(cookiesstr);

            Thread.Sleep(1000);
            HttpWebRequest httpWebRequest = null;
            HttpWebResponse httpWebResponse = null;
            try
            {
                httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.CookieContainer = cookieContainer;
                httpWebRequest.ContentType = "application/x-www-form-urlencoded";
                httpWebRequest.Accept = "application/json, text/javascript, */*; q=0.01";
                httpWebRequest.Method = "Post";
                httpWebRequest.KeepAlive = true;
                //httpWebRequest.Host = "iservice.10010.com";
                httpWebRequest.Referer = "http://iservice.10010.com/e3/query/call_dan.html";
                httpWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/39.0.2171.71 Safari/537.36";


                Encoding encoding = Encoding.UTF8;//根据网站的编码自定义  
                byte[] postData = encoding.GetBytes(postString);//postDataStr即为发送的数据，格式还是和上次说的一样  
                httpWebRequest.ContentLength = postData.Length;
                Stream requestStream = httpWebRequest.GetRequestStream();
                requestStream.Write(postData, 0, postData.Length);
                requestStream.Close();
                
                httpWebRequest.Headers.Add("Accept-Encoding", "gzip, deflate");
                httpWebRequest.Headers.Add("Accept-Language", "zh-CN,zh;q=0.8,en;q=0.6");
                httpWebRequest.Headers.Add("Cache-Control", "no-cache");
                httpWebRequest.Headers.Add("Origin", @"http://iservice.10010.com");
                httpWebRequest.Headers.Add("Pragma", "no-cache");
                httpWebRequest.Headers.Add("X-Requested-With", "XMLHttpRequest");
                //

                httpWebRequest.ServicePoint.ConnectionLimit = 60;
                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                Stream responseStream = httpWebResponse.GetResponseStream();
                StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8);
                string html = streamReader.ReadToEnd();

                streamReader.Close();
                responseStream.Close();

                httpWebRequest.Abort();
                httpWebResponse.Close();

                return html;
            }
            catch (WebException ex)
            {
                var pageContent = new StreamReader(ex.Response.GetResponseStream())
                         .ReadToEnd();
                return ex.Message;
            }

        }

        /// <summary>
        /// getCurrentTimeTick
        /// </summary>
        /// <returns></returns>
        public static int GetCurrentTimeTick()
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan diff = DateTime.Now - origin;
            int timestamp = (int)Math.Floor(diff.TotalSeconds);
            return timestamp;
        }

        /// <summary>
        /// getCurrentTimeTick
        /// </summary>
        /// <returns></returns>
        public static void AddStringToCookie(string strCookie, ref CookieContainer cookies, Uri uri)
        {
            string[] strs = strCookie.Split(';');

            cookies.PerDomainCapacity = 50;
            Cookie cookie = null;
            foreach (string str in strs)
            {

                int sep = str.IndexOf("=");
                string cName = str.Substring(0, sep).Trim();
                string cValue = str.Substring(sep + 1, (str.Length - sep - 1)).Trim();
                cookie = new Cookie(cName, System.Web.HttpUtility.UrlEncode(cValue));
                //cookie=new Cookie("piw", "%7B%22login_name%22%3A%22156****3702%22%2C%22nickName%22%3A%22%E7%BC%AA%E5%AE%B6%E7%BA%A2%22%2C%22rme%22%3A%7B%22ac%22%3A%22%22%2C%22at%22%3A%22%22%2C%22pt%22%3A%2201%22%2C%22u%22%3A%2215618603702%22%7D%2C%22verifyState%22%3A%22%22%7D");

                //cookie.Domain = "10010.com";
                cookies.Add(uri, cookie);
                //cookie = null;
            }
            //return cookie;
        }

        /// <summary>
        /// getCurrentTimeTick
        /// </summary>
        /// <returns></returns>
    }
}
