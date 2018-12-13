using HtmlAgilityPack;
using System.Collections.Generic;

namespace CurseFunction
{
    public class CurseDetailPage
    {
        public string title;
        public string href;
        public bool isActive = false;
        public CurseDetailPage(string t,string h , bool a)
        {
            title = t;
            href = h;
            isActive = a;
        }
        public static CurseDetailPage TryPrase(string html)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);
            var nav = doc.DocumentNode.SelectSingleNode("//a");
            if (null == nav)
                return null;
            string t = nav.InnerText;
            string h = nav.GetAttributeValue("href",null);
            bool a = false;
            return new CurseDetailPage(t,h,a);
        }
        public static List<CurseDetailPage> TryPraseList(string html)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);
            var nav = doc.DocumentNode.SelectSingleNode("//nav[contains(@class,'detail-navigation')]");
            List<CurseDetailPage> pages = new List<CurseDetailPage>();
            if(null!=nav && nav.ChildNodes.Count > 0)
            {
                foreach(var node in nav.ChildNodes)
                {
                    CurseDetailPage p = CurseDetailPage.TryPrase(node.InnerHtml);
                    if (null != p)
                        pages.Add(p);
                }
            }
            else
            {
                return null;
            }
            if (pages.Count > 0)
                return pages;
            else
                return null;
        }
    }
}
