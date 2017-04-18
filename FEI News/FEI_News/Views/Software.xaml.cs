using FEI_News.Managers;
using FEI_News.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FEI_News.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Software : ContentPage
    {
        private HttpManager httpManager;
        private int id = 79;

        public Software()
        {
            InitializeComponent();

            httpManager = HttpManager.Instance;

            Title = "Softvér pre študentov";
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var content = await httpManager.Client.GetStringAsync(HttpManager.PageUrl + "/" + id);
            var post = JsonConvert.DeserializeObject<Post>(content);

            post.TitleString = WebUtility.HtmlDecode(post.Title.TitleText);
            post.ContentString = post.Content.ContentText;

            String finalString = post.ContentString.Replace("https://", "<a href='https://");
            // Ad hoc solution...
            finalString = finalString.Replace("/<br />", "/'>link</a><br />");
            finalString = finalString.Replace("/</p>", "/'>link</a></p>");

            Browser.Source = httpManager.CreateHtmlView(finalString);

            Browser.Navigating += (s, e) =>
            {
                if (e.Url.StartsWith("http"))
                {
                    try
                    {
                        var uri = new Uri(e.Url);
                        Device.OpenUri(uri);
                    }
                    catch (Exception)
                    {
                    }

                    e.Cancel = true;
                }
            };
        }
    }
}
