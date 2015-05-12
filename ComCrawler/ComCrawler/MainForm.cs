using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;


namespace ComCrawler
{
    public partial class MainForm : Form
    {
        CookieContainer Cookie10086;
        CookieContainer Cookie10010;
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            /*
            string username = "";
            string password = "";
            GetDataFrom10010(username,password);*/

            PrepareFor10086();
        }

        /// <summary>
        /// GetDataFrom10010
        /// </summary>
        private string GetDataFrom10010(string username, string password)
        {
            string url = "https://uac.10010.com/portal/Service/MallLogin?callback=jQuery172049633088964037597_1426232251540&redirectURL=http%3A%2F%2Fiservice.10010.com%2Fe3%2Findex.html&userName="+username+"&password="+password+"&pwdType=01&productType=01&redirectType=01&rememberMe=1&_=1426232261354" + MyHttpRequest.GetCurrentTimeTick().ToString();
            CookieContainer cookies = new CookieContainer();
            MyHttpRequest.GetHtml(url, ref cookies);

            string urlPost = "http://iservice.10010.com/e3/static/check/checklogin/?_=" + MyHttpRequest.GetCurrentTimeTick().ToString() + @"&menuid=000100030001";
            string postString = "";//这里即为传递的参数，可以用工具抓包分析，也可以自己分析，主要是form里面每一个name都要加进来  

            string result = MyHttpRequest.PostRequest(urlPost, postString, cookies);

            urlPost = "http://iservice.10010.com/e3/static/query/callDetail?_=" + MyHttpRequest.GetCurrentTimeTick().ToString() + @"&menuid=000100030001";
            postString = "pageNo=1&pageSize=20&beginDate=2015-03-01&endDate=2015-03-31";//这里即为传递的参数，可以用工具抓包分析，也可以自己分析，主要是form里面每一个name都要加进来  
            //byte[] postData = Encoding.UTF8.GetBytes(postString);//编码，尤其是汉字，事先要看下抓取网页的编码方式 

            //string strCookieCall = "BIGipServerPOOL_W2PORTAL_WEB=269421066.23040.0000; BIGipServerPOOL_WSYYT2_4GqiantaiAPP=688591882.17695.0000; nickName=; noPayOrderNum=; ang_sessionid=370062722815719221; ang_catchyou=1; ang_seqid=12; mallcity=31|310; e3=ksK9VDML5Qz4sVjysp2d2v1VsC3d41GHvRK8nLrGyl22Mzq60QKL!-1432640836!-45521404; _n3fa_cid=2acf0fb433f84abfadb53c0a58a7b9b5; _n3fa_ext=ft=1416798288; _n3fa_lvt_a9e72dfe4a54a20c3d6e671b3bad01d9=1426224802,1426231113,1426239676,1426255216; _n3fa_lpvt_a9e72dfe4a54a20c3d6e671b3bad01d9=1426255216; __utma=231252639.922540477.1426216378.1426238280.1426255185.5; __utmb=231252639.8.10.1426255185; __utmc=231252639; __utmz=231252639.1426255185.5.4.utmcsr=iservice.10010.com|utmccn=(referral)|utmcmd=referral|utmcct=/e3/query/call_dan.html; __utmv=231252639.Shanghai; WT_FPC=id=2d3c774f9c4d32fffb31426216377715:lv=1426255343236:ss=1426255182156; MENUURL=%2Fe3%2Fnavhtml3%2FWT3%2FWT_MENU_3_001%2F031%2F012.html%3F_%3D1426255342496; MIE=00090001; Hm_lvt_9208c8c641bfb0560ce7884c36938d9d=1425449508,1426145706; Hm_lpvt_9208c8c641bfb0560ce7884c36938d9d=1426255344; MII=000100030001";
            //MyHttpRequest.AddStringToCookie(strCookieCall, ref cookies, new Uri(urlPost));
            result = MyHttpRequest.PostRequest(urlPost, postString, cookies);
            ShowResult(result);
            return result;
        }

        /// <summary>
        /// GetDataFrom10010
        /// </summary>
        private string GetDataFrom10086(string username, string password)
        {
            //login page
            string url = "https://sh.ac.10086.cn/prx/000/http/localhost/login";
            CookieContainer cookies = new CookieContainer();
            MyHttpRequest.GetHtml(url, ref cookies);

            //get valid code
            url = "https://sh.ac.10086.cn/validationCode?rnd=" + MyHttpRequest.GetCurrentTimeTick().ToString() + @"&menuid=000100030001";
            MyHttpRequest.GetHtml(url, ref cookies);

            //get message
            url = "https://sh.ac.10086.cn/loginex?iscb=1&act=1&telno=&t=" + MyHttpRequest.GetCurrentTimeTick().ToString() + @"&menuid=000100030001";
            MyHttpRequest.GetHtml(url, ref cookies);

            string postString = "";//这里即为传递的参数，可以用工具抓包分析，也可以自己分析，主要是form里面每一个name都要加进来  

            string result = MyHttpRequest.PostRequest(url, postString, cookies);

            url = "http://iservice.10010.com/e3/static/query/callDetail?_=" + MyHttpRequest.GetCurrentTimeTick().ToString() + @"&menuid=000100030001";
            postString = "pageNo=1&pageSize=20&beginDate=2015-02-01&endDate=2015-02-28";//这里即为传递的参数，可以用工具抓包分析，也可以自己分析，主要是form里面每一个name都要加进来  
            //byte[] postData = Encoding.UTF8.GetBytes(postString);//编码，尤其是汉字，事先要看下抓取网页的编码方式 

            //string strCookieCall = "BIGipServerPOOL_W2PORTAL_WEB=269421066.23040.0000; BIGipServerPOOL_WSYYT2_4GqiantaiAPP=688591882.17695.0000; nickName=; noPayOrderNum=; ang_sessionid=370062722815719221; ang_catchyou=1; ang_seqid=12; mallcity=31|310; e3=ksK9VDML5Qz4sVjysp2d2v1VsC3d41GHvRK8nLrGyl22Mzq60QKL!-1432640836!-45521404; _n3fa_cid=2acf0fb433f84abfadb53c0a58a7b9b5; _n3fa_ext=ft=1416798288; _n3fa_lvt_a9e72dfe4a54a20c3d6e671b3bad01d9=1426224802,1426231113,1426239676,1426255216; _n3fa_lpvt_a9e72dfe4a54a20c3d6e671b3bad01d9=1426255216; __utma=231252639.922540477.1426216378.1426238280.1426255185.5; __utmb=231252639.8.10.1426255185; __utmc=231252639; __utmz=231252639.1426255185.5.4.utmcsr=iservice.10010.com|utmccn=(referral)|utmcmd=referral|utmcct=/e3/query/call_dan.html; __utmv=231252639.Shanghai; WT_FPC=id=2d3c774f9c4d32fffb31426216377715:lv=1426255343236:ss=1426255182156; MENUURL=%2Fe3%2Fnavhtml3%2FWT3%2FWT_MENU_3_001%2F031%2F012.html%3F_%3D1426255342496; MIE=00090001; Hm_lvt_9208c8c641bfb0560ce7884c36938d9d=1425449508,1426145706; Hm_lpvt_9208c8c641bfb0560ce7884c36938d9d=1426255344; MII=000100030001";
            //MyHttpRequest.AddStringToCookie(strCookieCall, ref cookies, new Uri(urlPost));
            result = MyHttpRequest.PostRequest(url, postString, cookies);
            return result;
        }

        /// <summary>
        /// ShowResult
        /// </summary>
        private void ShowResult(string result)
        {
            //result = @"";
            result = "[" + result + "]";
            JArray jsonObj = JArray.Parse(result);
            string ts=(string)jsonObj[0]["pafeMap"];

            DataTable dt = new DataTable("call_record");
            dt.Columns.Add("column0", System.Type.GetType("System.String"));
            dt.Columns.Add("column1", System.Type.GetType("System.String"));
            dt.Columns.Add("column2", System.Type.GetType("System.String"));
            dt.Columns.Add("column3", System.Type.GetType("System.String"));
            dt.Columns.Add("column4", System.Type.GetType("System.String"));

            foreach(var item in jsonObj[0]["pageMap"]["result"] )
            {
                //Console.WriteLine(item["othernum"]);
                //Console.WriteLine(item["calltypeName"]);
                //Console.WriteLine(item["calldate"]);
                //Console.WriteLine(item["calltime"]);
               // Console.WriteLine(item["calllonghour"]);

                DataRow dr = dt.NewRow();
                dr[0] = item["othernum"];
                dr[1] = item["calltypeName"];
                dr[2] = item["calldate"];
                dr[3] = item["calltime"];
                dr[4] = item["calllonghour"];
                dt.Rows.Add(dr);

            }

           // var array=[{"inf_temp":null,"inf_owner":null,"id":2,"inf_date":null,"inf_name":"方式","inf_txt":"出租"}];
            //JavascriptSerilization jss=new JavascriptSerilization();
            //var json=jss.Seriliza(array);



            dataGridView1.DataSource = dt;
            
        }

        /// <summary>
        /// PrepareFor10086
        /// </summary>
        private void PrepareFor10086()
        {
                Cookie10010=new CookieContainer();

                //homepage
                string url = "http://www.sh.10086.cn/sh/wsyyt/busi/2002_14.jsp";
                var result = MyHttpRequest.GetStream(url, ref Cookie10086);
            
                //Login page
                url = "https://sh.ac.10086.cn/prx/000/http/localhost/login";
                result = MyHttpRequest.GetStream(url, ref Cookie10086);

                //Login page 2
                url = "https://sh.ac.10086.cn/loginex?iscb=1&act=6&t="+MyHttpRequest.GetCurrentTimeTick().ToString();
                result = MyHttpRequest.GetStream(url, ref Cookie10086);

                ValidationCodeFrom10086();
        }

        /// <summary>
        /// PrepareFor10086
        /// </summary>
        private void ValidationCodeFrom10086()
        {
            //Login page 2
            string url = "https://sh.ac.10086.cn/validationCode?rnd=" + MyHttpRequest.GetCurrentTimeTick().ToString();
            Stream result = MyHttpRequest.GetStream(url, ref Cookie10086);
            validateImg.Image = Image.FromStream(result);
        }

        private void btnDyCode_Click(object sender, EventArgs e)
        {
            string username = "";
            string url = "https://sh.ac.10086.cn/loginex?iscb=1&act=1&telno=" +username + "&t=" + MyHttpRequest.GetCurrentTimeTick().ToString();
            Stream result = MyHttpRequest.GetStream(url, ref Cookie10086);
            validateImg.Image = Image.FromStream(result);

        }

        private void btnLogin1_Click(object sender, EventArgs e)
        {
            string username = System.Web.HttpUtility.UrlEncode("", System.Text.Encoding.UTF8);
            string password = System.Web.HttpUtility.UrlEncode("811006", System.Text.Encoding.UTF8); 
            string url = "https://sh.ac.10086.cn/loginex?iscb=1&act=2&telno=" + username + "=&password=" + password + "&authLevel=2&validcode=" + textValidCode.Text + "&decode=1&t=" + MyHttpRequest.GetCurrentTimeTick().ToString();
            string result = MyHttpRequest.GetHtml(url, ref Cookie10086);
           // validateImg.Image = Image.FromStream(result);
        }


    }
}
