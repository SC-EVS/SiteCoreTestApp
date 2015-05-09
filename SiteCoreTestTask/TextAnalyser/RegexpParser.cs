using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace SiteCoreTestTask.TextAnalyser
{
    public class RegexpParser
    {
        public static List<string> stopWords = new List<string> { "word1", "word2", "word3", " or ", " and ", " if ", " very " , " simple "};
        //pattern for hrefs
        private static string HRefPattern = "href\\s*=\\s*(?:[\"'](?<1>[^\"']*)[\"']|(?<1>\\S+))";
        //pattern for urls
        private static string urlPattern = @"^(http|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?$";
        //meta regex
        private static string metaPattern = @"<meta\s*(?:(?:\b(\w|-)+\b\s*(?:=\s*(?:""[^""]*""|'" + @"[^']*'|[^""'<> ]+)\s*)?)*)/?\s*>";


        //here we parse hrefs and meta-tags data
        public static IList<string> ParseTags(string bareText, bool ifMetaTag = false)
        {
            List<string> List = new List<string>();
            Match m;
            try
            {
                m = Regex.Match(bareText,ifMetaTag==false?HRefPattern:metaPattern,
                                RegexOptions.IgnoreCase);
                while (m.Success)
                {
                    List.Add(ifMetaTag == false ? m.Groups[1].ToString() : m.Groups[0].ToString());
                    m = m.NextMatch();
                }
                return List;
            }
            catch
            {
                return null;
            }

        }

        //find out whether it's an Url
        public static bool IsUrl(string Url)
        {
            try
            {
                if (Regex.IsMatch(Url, urlPattern,
                                 RegexOptions.IgnoreCase))
                {
                    return true;
                }
                else
                {
                    //In case we put localhost link in textarea
                    if (Regex.IsMatch(Url, @"^(http|https):\/\/localhost"))
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public static IList<string> FindStopWords(string bareText)
        {
            List<string> hrefList = new List<string>();
            Match m;
            foreach (string stopWord in stopWords)
            {
                try
                {
                    m = Regex.Match(bareText, stopWord,
                                    RegexOptions.IgnoreCase);
                    while (m.Success)
                    {
                        hrefList.Add(m.Groups[0].ToString());
                        m = m.NextMatch();
                    }

                }
                catch
                {
                    return null;
                }
            }

            return hrefList;
        }

      

    }
}