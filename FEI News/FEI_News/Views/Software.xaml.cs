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
        private int idInformatics = 79;
        private int idHardware = 83;
        private int idElectro = 85;

        public Software()
        {
            InitializeComponent();

            httpManager = HttpManager.Instance;

            Title = "Softvér pre študentov";
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            String finalString = "";
            String contentString = "";

            // Informatics
            // =================
            var content = await httpManager.Client.GetStringAsync(HttpManager.PageUrl + "/" + idInformatics);
            var post = JsonConvert.DeserializeObject<Post>(content);

            post.TitleString = WebUtility.HtmlDecode(post.Title.TitleText);
            post.ContentString = post.Content.ContentText;

            contentString = "<h2>" + post.Title.TitleText + "</h2>" + post.ContentString.Replace("https://", "<a href='https://");
            finalString = finalString + contentString;

            // Hardware
            // =================
            content = await httpManager.Client.GetStringAsync(HttpManager.PageUrl + "/" + idHardware);
            post = JsonConvert.DeserializeObject<Post>(content);

            post.TitleString = WebUtility.HtmlDecode(post.Title.TitleText);
            post.ContentString = post.Content.ContentText;

            contentString = "<h2>" + post.Title.TitleText + "</h2>" + post.ContentString.Replace("https://", "<a href='https://");
            finalString = finalString + contentString;

            // Electro
            // =================
            content = await httpManager.Client.GetStringAsync(HttpManager.PageUrl + "/" + idElectro);
            post = JsonConvert.DeserializeObject<Post>(content);

            post.TitleString = WebUtility.HtmlDecode(post.Title.TitleText);
            post.ContentString = post.Content.ContentText;

            contentString = "<h2>" + post.Title.TitleText + "</h2>" + post.ContentString.Replace("https://", "<a href='https://");
            finalString = finalString + contentString;
            // Ad hoc solution...
            finalString = finalString.Replace("http://", "<a href='http://");
            finalString = finalString.Replace("<br />", "'>link</a><br />");
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
