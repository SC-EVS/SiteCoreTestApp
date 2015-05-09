using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Text;

namespace SiteCoreTestTask.TextAnalyser
{
    public interface IGetText
    {
        string GetText(string text);
    }


    public class SimpleText : IGetText
    {

        public string GetText(string text)
        {
            return text;
        }
    }

    public class TextFromUrl : IGetText
    {

        public string GetText(string Url)
        {
            try
            {
                HttpWebRequest proxy_request = (HttpWebRequest)WebRequest.Create(Url);
                proxy_request.Method = "GET";
                //proxy_request.ContentType = "application/x-www-form-urlencoded";
                proxy_request.KeepAlive = true;
                HttpWebResponse resp = proxy_request.GetResponse() as HttpWebResponse;
                string html = "";
                using (StreamReader sr = new StreamReader(resp.GetResponseStream(), Encoding.GetEncoding(1251)))
                {
                    if (resp.StatusCode != HttpStatusCode.OK)
                    {
                        throw new WebException(resp.StatusCode.ToString());
                    }
                    html = sr.ReadToEnd();
                    html = html.Trim();
                    return html;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            //  Console.WriteLine(html);
        }
    }
}