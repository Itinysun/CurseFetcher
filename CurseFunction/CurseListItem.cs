using HtmlAgilityPack;

namespace CurseFunction
{
    public  class CurseListItem
    {
        public string title;
        public string thumbnails;
        public string href;
        public string description;
        public string id;
        public string download;
        public string update;
        public bool isInstall = false;
        public static CurseListItem TryPrase(string html)
        {
            try
            {
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml("<html>" + html.Replace("\n", "") + "</html>");
                HtmlNode title = doc.DocumentNode.SelectSingleNode("//h2");
                CurseListItem it = new CurseListItem();
                HtmlNode thumb = doc.DocumentNode.SelectSingleNode("//img");
                it.title = title.InnerText.Trim();
                it.thumbnails = thumb.GetAttributeValue("src", null);
                it.href = title.ParentNode.GetAttributeValue("href", null);
                it.id = it.href.Substring(it.href.LastIndexOf("/")+1);
                HtmlNode meta = doc.DocumentNode.SelectSingleNode("//span[contains(@class,'count--download')]");
                it.download = meta.InnerText.Trim();
                string up = doc.DocumentNode.SelectSingleNode("//abbr").GetAttributeValue("data-epoch", null).Trim();
                if (!string.IsNullOrEmpty(up))
                {
                    it.update = Curse.ConvertStringToDateTime(up).ToString();
                }
                it.description = doc.DocumentNode.SelectSingleNode("//div[@class='list-item__description']").InnerText.Trim();
                return it;
            }
            catch
            {
                return null;
            }
        }
    }
}
