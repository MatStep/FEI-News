using FEI_News.MenuItems;
using FEI_News.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace FEI_News
{
    public partial class MainPage : MasterDetailPage
    {
        public List<MasterPageItem> menuList { get; set; }

        public MainPage()
        {

            InitializeComponent();

            menuList = new List<MasterPageItem>();

            // Creating our pages for menu navigation
            // Here you can define title for item, 
            // icon on the left side, and page that you want to open after selection
            var page1 = new MasterPageItem() { Title = "Články", Icon = "ic_shortcut_format_color_text.png", TargetType = typeof(Articles) };
            var page2 = new MasterPageItem() { Title = "Šablóny", Icon = "itemIcon2.png", TargetType = typeof(Templates) };
            var page3 = new MasterPageItem() { Title = "Otváracie hodiny", Icon = "itemIcon3.png", TargetType = typeof(OpeningHours) };
            var page4 = new MasterPageItem() { Title = "Zdroje informácií", Icon = "itemIcon4.png", TargetType = typeof(InformationSources) };
            var page5 = new MasterPageItem() { Title = "Kalendár udalostí", Icon = "itemIcon5.png", TargetType = typeof(Calendar) };
            var page6 = new MasterPageItem() { Title = "Softvér pre študentov", Icon = "itemIcon6.png", TargetType = typeof(Software) };
            var page7 = new MasterPageItem() { Title = "Harmonogram štúdia", Icon = "itemIcon7.png", TargetType = typeof(StudySchedule) };
            var page8 = new MasterPageItem() { Title = "Rozvrh skúšok", Icon = "itemIcon8.png", TargetType = typeof(Exams) };

            // Adding menu items to menuList
            menuList.Add(page1);
            menuList.Add(page2);
            menuList.Add(page3);
            menuList.Add(page4);
            menuList.Add(page5);
            menuList.Add(page6);
            menuList.Add(page7);
            menuList.Add(page8);

            // Setting our list to be ItemSource for ListView in MainPage.xaml
            navigationDrawerList.ItemsSource = menuList;

            // Initial navigation, this can be used for our home page
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(Articles)));
        }

        // Event for Menu Item selection, here we are going to handle navigation based
        // on user selection in menu ListView
        private void OnMenuItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            var item = (MasterPageItem)e.SelectedItem;
            Type page = item.TargetType;

            Detail = new NavigationPage((Page)Activator.CreateInstance(page));
            IsPresented = false;
        }
    }
}
