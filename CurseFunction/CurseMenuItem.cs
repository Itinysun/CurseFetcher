using HtmlAgilityPack;
using System.Collections.Generic;

namespace CurseFunction
{
    public class CurseMenuItem
    {
        public string path;
        public string title;
        public string thumb;
        public List<CurseMenuItem> childs;
        public CurseMenuItem(string n,string p,string s)
        {
            title = n;
            thumb = s;
            if (p.Contains("/"))
            {
                var c = p.Split('/');
                p = c[c.Length-1];
            }
            path = "/" + p;
            childs = new List<CurseMenuItem>();
        }
        public void addChild(CurseMenuItem c)
        {
            c.path = this.path + "/" + c.path;
            childs.Add(c);
        }
        public static CurseMenuItem TryPrase(string html)
        {
            try
            {
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml("<html>" + html + "</html>");
                string title = doc.DocumentNode.SelectSingleNode("//p").InnerText.Replace("&amp;", "&");
                string path = doc.DocumentNode.SelectSingleNode("//a").GetAttributeValue("href", null);
                string thumb = doc.DocumentNode.SelectSingleNode("//img").GetAttributeValue("src", null);
                var m = new CurseMenuItem(title, path, thumb);
                return m;
            }
            catch
            {
                return null;
            }
        }
    }
}
