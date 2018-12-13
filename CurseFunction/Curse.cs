using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurseFunction
{
    public class Curse
    {
        /// <summary>
        /// 解析插件列表页插件项目
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static List<CurseListItem> PraseList(string html)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);
            var res = new List<CurseListItem>();
            HtmlNodeCollection lis = doc.DocumentNode.SelectNodes("//li[@class='project-list-item']");
            if (null == lis)
                return null;
            if (lis.Count > 0)
            {
                for (int i = 0; i < lis.Count; i++)
                {
                    CurseListItem it = CurseListItem.TryPrase(lis[i].InnerHtml);
                    if (null != it)
                    {
                        it.isInstall = Settings.db.CheckInstalled(it.id);
                        res.Add(it);
                    }
                        
                }
            }
            return res;
        }

        /// <summary>
        /// 解析插件栏目
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static List<CurseMenuItem> PraseCategory(string html)
        {
            var mls = new List<CurseMenuItem>();
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(html);
                var parent = doc.DocumentNode.SelectSingleNode("//ul[contains(@class,'categories-tier')]");
            HtmlDocument xdoc = new HtmlDocument();
            xdoc.LoadHtml(parent.InnerHtml);
            mls.Add(new CurseMenuItem("ALL", "", "https://media.forgecdn.net/avatars/thumbnails/54/513/64/64/636135265289061589.png"));
            foreach (var n in xdoc.DocumentNode.SelectNodes("li"))
                {
                    string ty = n.GetAttributeValue("data-root", "sub").ToLower();
                    if (ty.Equals("false"))
                    {
                        var cmi = CurseMenuItem.TryPrase(n.InnerHtml);
                        mls.Add(cmi);
                    }
                    else if(ty.Equals("true"))
                    {
                        var cmi = CurseMenuItem.TryPrase(n.InnerHtml);
                        mls.Add(cmi);
                    }
                    else
                    {
                        var cdoc = new HtmlDocument();
                        cdoc.LoadHtml(n.InnerHtml);
                        foreach(var cn in cdoc.DocumentNode.SelectNodes("//li"))
                        {
                        var cld = CurseMenuItem.TryPrase(cn.InnerHtml);
                            mls.Last().addChild(cld);
                        }
                    }
                }
            return mls;
        }

        /// <summary>
        /// 解析分页
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static List<CursePageLink> PrasePages(string html)
        {
            try
            {
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(html);
                var pl = doc.DocumentNode.SelectSingleNode("//ul[@class='b-pagination-list paging-list j-tablesorter-pager j-listing-pagination']");
                HtmlDocument lis = new HtmlDocument();
                lis.LoadHtml(pl.InnerHtml);
                var pls = lis.DocumentNode.SelectNodes("//li");
                List<CursePageLink> pages = new List<CursePageLink>();
                foreach (var li in pls)
                {
                    if (li.HasClass("b-pagination-item"))
                    {
                        CursePageLink l = CursePageLink.TryPraseLink(li.InnerHtml);
                        if (null == l)
                            l = CursePageLink.TryPraseSpan(li.InnerHtml);
                        pages.Add(l);
                    }
                    else
                    {
                        CursePageLink dot = new CursePageLink("...", null);
                        dot.isdot = true;
                        pages.Add(dot);
                    }

                }
                return pages;
            }
            catch
            {
                return null;
            }

        }

        /// <summary>
        /// 解析插件详情页
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static CurseDetail PraseDetail(string html)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);
            var node = doc.DocumentNode.SelectSingleNode("//main[contains(@class,'project-details__main')]");
            if (null != node)
            {
                var cd= CurseDetail.TryPrase(node.InnerHtml);
                return cd;
            }
            else
                return null;
        }

        /// <summary>
        /// 解析下载链接
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static CurseDownloadLink PraseDownload(string html)
        {
            try
            {
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(html);
                var node = doc.DocumentNode.SelectSingleNode("//div[contains(@class,'download-container')]");
                if (null == node) return null;
                return CurseDownloadLink.TryPrase(node.InnerHtml);
            }catch(Exception e)
            {
                Settings.LogException(e);
                return null;
            }

        }

        /// <summary>
        /// 字符串转Int
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int GetIntFromStr(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return 0;
            }
            int t = int.Parse(str);
            return t;
        }

        /// <summary>        
        /// 时间戳转为C#格式时间        
        /// </summary>        
        /// <param name=”timeStamp”></param>        
        /// <returns></returns>        
        public static DateTime ConvertStringToDateTime(string timeStamp)
        {
            DateTime dtStart = new DateTime(1970, 1, 1);
            try
            { 
                long lTime = long.Parse(timeStamp + "0000000");
                TimeSpan toNow = new TimeSpan(lTime);
                return dtStart.Add(toNow);
            }
            catch
            {
                return dtStart;
            }
            
        }
    }
}
