using FEI_News.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FEI_News.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Article : ContentPage
    {
        private const string Url = "http://mechatronika.cool/noviny/wp-json/wp/v2/posts";
        private HttpClient client = new HttpClient();
        private Post post;
        private int id;

        public Article(Post post)
        {
            InitializeComponent();

            this.post = post;

            Title = post.TitleString;

            id = post.Id;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var content = await client.GetStringAsync(Url + "/" + id);
            //var post = JsonConvert.DeserializeObject<Post>(content);
            post.ContentString = post.Content.ContentText;

            var htmlSource = new HtmlWebViewSource();
            var stream = Android.App.Application.Context.Assets.Open("styles.css");
            using (var streamReader = new StreamReader(stream))
            {
                String css = streamReader.ReadToEnd();
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("<HTML><HEAD>" +
                "<meta name = \"viewport\" content = \"width=device-width, initial-scale=1\">" +
                "<style> embed, iframe, object {margin-bottom: 1.5em; max-width: 100%;} </style>" +
                "</HEAD><body>");
            sb.Append(post.ContentString);
            sb.Append("</body></HTML>");
            htmlSource.Html = sb.ToString();
            Browser.Source = htmlSource;
        }
    }
}
