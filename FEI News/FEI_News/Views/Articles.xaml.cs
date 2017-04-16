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

namespace FEI_News.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Articles : ContentPage
    {
        private const string Url = "http://mechatronika.cool/noviny/wp-json/wp/v2/posts";
        private HttpClient client = new HttpClient();
        private ObservableCollection<Post> _posts;

        public Articles()
        {
            InitializeComponent();

            Title = "Články";
        }

        public static string StripTags(string source)
        {
            return Regex.Replace(source, "<.*?>", string.Empty);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var content = await client.GetStringAsync(Url);
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
                _posts[i].ContentString = StripTags(WebUtility.HtmlDecode(_posts[i].Content.ContentText));
            }
            PostsListView.ItemsSource = _posts;
        }

        private async void listView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var post = e.Item as Post;
            var article = new Article();
            article.BindingContext = post.Id;
            await Navigation.PushAsync(article);
        }

    }
}
