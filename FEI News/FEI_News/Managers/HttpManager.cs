using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FEI_News.Managers
{
    public class HttpManager
    {
        public static string BaseUrl = "http://mechatronika.cool/noviny/wp-json/wp/v2";
        public static string PostsUrl = BaseUrl + "/posts";
        public static string PageUrl = BaseUrl + "/pages";
        private static HttpManager instance;
        private static HttpClient client;

        private HttpManager() { }

        public static HttpManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new HttpManager();
                    client = new HttpClient();
                }
                return instance;
            }
        }

        public HttpClient Client { get
            {
                return client;
            }
            protected set {}
        }

        public HtmlWebViewSource CreateHtmlView(String body)
        {
            var htmlSource = new HtmlWebViewSource();

            String css = "";
            var stream = Android.App.Application.Context.Assets.Open("styles.css");
            using (var streamReader = new StreamReader(stream))
            {
                css = streamReader.ReadToEnd();
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("<HTML><HEAD>" +
                "<meta name = \"viewport\" content = \"width=device-width, initial-scale=1\">" +
                "<style> " + css + " </style>" +
                "</HEAD><body>");
            sb.Append(body);
            sb.Append("</body></HTML>");

            htmlSource.Html = sb.ToString();

            return htmlSource;
        }
    }
}
