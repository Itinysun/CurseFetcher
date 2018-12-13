using HtmlAgilityPack;

namespace CurseFunction
{
    public class CurseDownloadLink
    {
        public string href;
        public string version;
        public CurseDownloadLink(string h,string v)
        {
            href = h;
            version = v;
        }
        public static CurseDownloadLink TryPrase(string html)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);
            var dh =doc.DocumentNode.SelectSingleNode("//a[@class='download__link']");
            string h = null == dh ? null : dh.GetAttributeValue("href", null);
            var dv = doc.DocumentNode.SelectSingleNode("//abbr[contains(@class,'standard-date')]");
            string v = null == dv ? null : dv.GetAttributeValue("data-epoch", null);
            return new CurseDownloadLink(h,v);
        }
    }
}
