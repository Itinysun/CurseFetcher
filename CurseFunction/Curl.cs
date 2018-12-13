using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System;

namespace CurseFunction
{
    public class Curl
    {
        /// <summary>
        /// 上次访问URL，用以设置Referer
        /// </summary>
        private static string lasturl;

        /// <summary>
        /// cookier容器
        /// </summary>
        private static CookieContainer cookieJar = new CookieContainer();

        /// <summary>
        /// Web代理
        /// </summary>
        private static WebProxy proxy;

        /// <summary>
        /// 启用代理开关
        /// </summary>
        private static bool useProxy = false;

        /// <summary>
        /// GET抓取Html
        /// </summary>
        /// <param name="url">完整链接</param>
        /// <returns></returns>
        public static string GetHtmlAsync(string url)
        {
            try
            {
                var res = GetResponse(url);
                var bts = res.Content.ReadAsByteArrayAsync();
                var con = System.Text.Encoding.Default.GetString(bts.Result);
                return con;
            }
            catch(Exception e)
            {
                Settings.LogException(e);
                return null;
            }
            
            
        }
        /// <summary>
        /// 下载文件到本地
        /// </summary>
        /// <param name="url">完整的下载链接</param>
        /// <param name="target">完整的本地路径</param>
        /// <returns></returns>
        public static bool Download(string url , string target)
        {
            var res = GetResponse(url);
            if (null == res)
                return false;
            try
            {
                using (var file = new FileStream(target, FileMode.OpenOrCreate))
                {
                    var bytes = res.Content.ReadAsByteArrayAsync().Result;
                    file.Write(bytes, 0, bytes.Length);
                }
            }
            catch(Exception e)
            {
                Settings.LogException(e);
                return false;
            }

            return true;
        }

        /// <summary>
        /// 获取Response实例
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static HttpResponseMessage GetResponse(string url)
        {
            try
            {
                var client = BuildClient();
                var request = BuildRequest(url);
                var response = client.SendAsync(request).Result;
                if (response.StatusCode.Equals(HttpStatusCode.OK))
                {
                    return response;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception e)
            {
                Settings.LogException(e);
                return null;
            }

        }

        /// <summary>
        /// 构建通用Request
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static HttpRequestMessage BuildRequest(string url)
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            httpRequestMessage.Headers.Add("Connection", "keep-alive");
            httpRequestMessage.Headers.Add("Origin", "test.com");
            httpRequestMessage.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/69.0.3497.100 Safari/537.36");
            httpRequestMessage.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8");
            httpRequestMessage.Headers.Add("Referer", lasturl ?? url);
            httpRequestMessage.Headers.Add("Accept-Encoding", "gzip");
            httpRequestMessage.Headers.Add("Accept-Language", "en-GB,en-US;q=0.9,en;q=0.8");
            httpRequestMessage.Headers.Add("Upgrade-Insecure-Requests", "1");
            lasturl = url;
            return httpRequestMessage;
        }

        /// <summary>
        /// 构建通用HttpClient
        /// </summary>
        /// <returns></returns>
        private static HttpClient BuildClient()
        {
            var handler = new HttpClientHandler
            {
                CookieContainer = cookieJar,
                UseCookies = true,
                UseDefaultCredentials = false,
                AutomaticDecompression = DecompressionMethods.GZip
        };
            if (useProxy)
            {
                handler.Proxy = proxy;
                
            }
            var httpClient = new HttpClient(handler);
            return httpClient;
        }

        /// <summary>
        /// 初始化网络访问代理配置
        /// </summary>
        public static void InitProxy()
        {
            if (Settings.sc.proxy_enable)
            {
                SetProxy(Settings.sc.proxy_ip, Settings.sc.proxy_port, Settings.sc.proxy_username, Settings.sc.proxy_password);
            }
            else
            {
                EnableProxy(false);
            }
        }

        /// <summary>
        /// 设置代理并启用
        /// </summary>
        /// <param name="host">IP</param>
        /// <param name="port">端口</param>
        /// <param name="un">用户名（可选）</param>
        /// <param name="pwd">密码（可选）</param>
        private static void SetProxy(string host,string port,string un,string pwd)
        {
            if (int.TryParse(port.Trim(), out int p))
            {
                proxy = new WebProxy(host, p);
                if (!string.IsNullOrEmpty(un))
                {
                    pwd = string.IsNullOrEmpty(pwd) ? "" : pwd;
                    proxy.Credentials = new NetworkCredential(un, pwd);
                }
                EnableProxy(true);
            }
            else
            {
                Settings.LogException(new System.Exception("proxy 端口解析错误" + port));
            }
        }

       /// <summary>
       /// 启用或关闭代理
       /// </summary>
       /// <param name="sw">开关</param>
       private static void EnableProxy(bool sw)
        {
            useProxy = sw;
        }
    }
}
