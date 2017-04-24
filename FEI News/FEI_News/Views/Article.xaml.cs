using FEI_News.Managers;
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
        private HttpManager httpManager;
        private Post post;
        private int id;

        public Article(Post post)
        {
            InitializeComponent();

            httpManager = HttpManager.Instance;

            this.post = post;

            Title = "FEI Noviny";

            id = post.Id;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var content = await httpManager.Client.GetStringAsync(HttpManager.PostsUrl + "/" + id);
            //var post = JsonConvert.DeserializeObject<Post>(content);
            post.ContentString = post.Content.ContentText;

            var htmlSource = new HtmlWebViewSource();
            var stream = Android.App.Application.Context.Assets.Open("styles.css");
            using (var streamReader = new StreamReader(stream))
            {
                String css = streamReader.ReadToEnd();
            }

            title.Text = post.TitleString;

            Browser.Source = httpManager.CreateHtmlView(post.ContentString);

        }
    }
}
