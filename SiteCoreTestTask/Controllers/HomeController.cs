using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SiteCoreTestTask.TextAnalyser;

namespace SiteCoreTestTask.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {


        //collections save their state between requests
        static List<string> outerLinks = null;
        static Dictionary<string, int> stopWordsInMetaDict = null;
        static Dictionary<string, int> stopWordsDict = null;

        public ActionResult Index(IList<string> stopWordsOnPage, IList<string> stopWordsInMeta, List<string> externalLinks)
        {
            ViewData["Message"] = "My rocking test app!";
            ViewData["StopWords"] = RegexpParser.stopWords;
            return View();
        }

        public ActionResult About()
        {
            return View();
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SeoAnalysis(string textOrUrl,
            bool eachWordOnPage = false,
            bool eachWordOnPageInMetaTags = false,
            bool externalLinks = false)
        {
            IGetText getText;
            string currentDomain;
            string text = "";
            //getText instance depends upon the text we entered
            if (RegexpParser.IsUrl(textOrUrl.Trim()))
            {
                getText = new TextFromUrl();
                Uri uri = new Uri(textOrUrl.Trim());
                currentDomain = uri.Host.ToString();
            }
            else
            {
                currentDomain = "localhost";
                getText = new SimpleText();
            }
            //get full bare text
            try
            {
                text = getText.GetText(textOrUrl.Trim());
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", new { error = ex.Message });
            }


            #region Find all external links
            //find all hrefs
            IList<string> listHrefs = RegexpParser.ParseTags(text);
            List<string> innerLinks = null;
            outerLinks = null;
            if (externalLinks)
            {
                innerLinks = new List<string>();
                outerLinks = new List<string>();
                foreach (string link in listHrefs)
                {
                    // if our href is fully qualified url
                    if (RegexpParser.IsUrl(link.Trim()))
                    {
                        Uri uri = new Uri(link.Trim());
                        string host = uri.Host.ToString();
                        if (host.Equals(currentDomain))
                        {
                            // adding absolute inner link
                            innerLinks.Add(link);
                        }
                        else
                        {
                            // oops, this link's domain name doesn't equal our domain name
                            outerLinks.Add(link);
                        }

                    }
                    else
                    {
                        // adding  inner link
                        innerLinks.Add(link);
                    }

                }
            }

            ViewData["ExternalLinks"] = outerLinks;
            #endregion


            #region Find all stop-words matches in meta-tags
            IList<string> metaTagsContent;
            IList<string> stopWordsInMetaList = null;
            if (eachWordOnPageInMetaTags)
            {
                metaTagsContent = RegexpParser.ParseTags(text, true);
                string stopWordsInMetaTags = "";
                foreach (string val in metaTagsContent)
                {
                    stopWordsInMetaTags += val;
                }
                stopWordsInMetaList = RegexpParser.FindStopWords(stopWordsInMetaTags);
            }

            stopWordsInMetaDict = new Dictionary<string, int>();
            if (stopWordsInMetaList != null)
            {

                var result = stopWordsInMetaList.GroupBy(wrd => wrd.ToLower());
                foreach (var item in result)
                {
                    stopWordsInMetaDict.Add(item.Key, item.Count());
                }
            }
            ViewData["StopWordsInMetaTags"] = stopWordsInMetaDict;

            #endregion


            #region Find all stop-words matches in text

            IList<string> stopWords = null;
            if (eachWordOnPage)
            {
                stopWords = RegexpParser.FindStopWords(text);
            }

            stopWordsDict = new Dictionary<string, int>();
            if (stopWords != null)
            {

                var result = stopWords.GroupBy(wrd => wrd.ToLower());
                foreach (var item in result)
                {
                    //stop-word/entries count
                    stopWordsDict.Add(item.Key.ToLower(), item.Count());
                }
                // if (!stopWordsDict.ContainsKey(word))
                //stopWordsDict.Add(word, count);

            }
            ViewData["StopWordsOnPage"] = stopWordsDict;
            #endregion


            // return RedirectToAction("Index", new { stopWordsOnPage = stopWords });
            return View("Index");
        }


        public ActionResult Error(string error)
        {
            ViewData["Error"] = error;
            return View("Error");
        }


        public ActionResult SortCollection(string collname, bool? sort = false)
        {

                switch (collname)
                {
                    case "stopWordsInMetaDict":
                        {
                            if (stopWordsInMetaDict != null)
                            {
                                List<KeyValuePair<string, int>> listedDict = stopWordsInMetaDict.ToList();
                                //false -- ascending/ true -- descending sort
                                return View("SortedCollection", sort == false ? listedDict.OrderBy(val => val.Value) : listedDict.OrderByDescending(val => val.Value));
                            }
                            break;
                        }
                    case "StopWordsOnPage":
                        {
                            if (stopWordsDict != null)
                            {
                                List<KeyValuePair<string, int>> listedDict = stopWordsDict.ToList();
                                return View("SortedCollection", sort == false ? listedDict.OrderBy(val => val.Value) : listedDict.OrderByDescending(val => val.Value));
                            }
                            break;
                        }
                }
                return View("SortedCollection");
        }
    }
}
