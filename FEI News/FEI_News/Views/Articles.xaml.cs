using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;

using FEI_News.Model;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net;
using System.Text.RegularExpressions;
using FEI_News.Managers;

namespace FEI_News.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Articles : ContentPage
    {
        private HttpManager httpManager;
        private ObservableCollection<Post> _posts;

        public Articles()
        {
            InitializeComponent();

            httpManager = HttpManager.Instance;

            Title = "FEI Noviny";
        }

        public static string StripTags(string source)
        {
            return Regex.Replace(source, "<.*?>", string.Empty);
        }

        public static string Truncate(string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength) + "...";
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var content = await httpManager.Client.GetStringAsync(HttpManager.PostsUrl);
            var posts = JsonConvert.DeserializeObject<List<Post>>(content);
            _posts = new ObservableCollection<Post>(posts);
            for (int i = 0; i < _posts.Count; i++)
            {
                _posts[i].TitleString = WebUtility.HtmlDecode(_posts[i].Title.TitleText);
                var contentText = _posts[i].Content.ContentText;
                string regexImgSrc = @"<?src\s*=\s*[""']?([^'"" >]+?)[ '""][^>]*?>";
                MatchCollection matchesImgSrc = Regex.Matches(contentText, regexImgSrc, RegexOptions.IgnoreCase | RegexOptions.Singleline);

                foreach (Match m in matchesImgSrc)
                {
                    string href = m.Groups[1].Value;
                    posts[i].ImgUrl = href;
                }
                _posts[i].ContentString = Truncate(StripTags(WebUtility.HtmlDecode(_posts[i].Content.ContentText)), 150);
            }
            PostsListView.ItemsSource = _posts;
        }

        private async void listView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var post = e.Item as Post;
            var article = new Article(post);
            article.BindingContext = post;
            await Navigation.PushAsync(article);
        }

    }
}
