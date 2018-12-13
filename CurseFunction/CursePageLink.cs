using HtmlAgilityPack;

namespace CurseFunction
{
    public class CursePageLink
    {
        public string href;
        public string title;
        public bool iscurrent=false;
        public bool ispre = false;
        public bool islast = false;
        public bool isdot = false;
        public CursePageLink(string t,string h)
        {
            href = h;
            title = t;
        }
        public CursePageLink()
        {

        }
        public static CursePageLink TryPraseLink(string html)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml("<html>" + html + "</html>");
            var a = doc.DocumentNode.SelectSingleNode("//a");
            CursePageLink l = new CursePageLink();
            if (null != a)
            {

                l.href = a.GetAttributeValue("href", "");
                l.title = a.InnerText;
                if (l.title.ToLower().Equals("prev"))
                    l.ispre = true;
                if (l.title.ToLower().Equals("next"))
                    l.islast = true;
                return l;
            }
            return null;
        }
        public static CursePageLink TryPraseSpan(string html)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml("<html>" + html + "</html>");
            var a = doc.DocumentNode.SelectSingleNode("//span");
            CursePageLink l = new CursePageLink();
            if (null != a)
            {

                l.href = null;
                l.title = a.InnerText;
                l.iscurrent = true;
                return l;
            }
            return null;
        }
    }
}
