using HtmlAgilityPack;
using System.Collections.Generic;

namespace CurseFunction
{
    public class CurseDetail
    {
        public string version;
        public string content;
        public List<CurseDetailPage> pages;
        public static CurseDetail TryPrase(string html)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);
            var v = doc.DocumentNode.SelectSingleNode("//span[@class='stats--game-version']");
            CurseDetail cd = new CurseDetail();
            if (null != v)
            {
                cd.version = v.InnerText;
            }
            cd.pages = CurseDetailPage.TryPraseList(html);
            var c = doc.DocumentNode.SelectSingleNode("//section[contains(@class,'project-content')]");
            if (null != c)
                cd.content = c.InnerText;
            return cd;
        }
    }
}
