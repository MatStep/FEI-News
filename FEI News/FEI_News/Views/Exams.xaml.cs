using FEI_News.Managers;
using FEI_News.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FEI_News.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Exams : ContentPage
    {
        private HttpManager httpManager;
        private ObservableCollection<Exam> examList;

        public Exams()
        {
            InitializeComponent();

            httpManager = HttpManager.Instance;

            Title = "Rozvrh skúšok";
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var stream = Android.App.Application.Context.Assets.Open("terminy_skusok.csv");
            examList = new ObservableCollection<Exam>();

            using (var streamReader = new StreamReader(stream, Encoding.GetEncoding("Windows-1250"), true))
            {
                // Ignore header
                streamReader.ReadLine();

                while ( !streamReader.EndOfStream)
                {
                    var line = streamReader.ReadLine();
                    var values = line.Split(';');
                    //examList.Add(new Exam(values[0], values[1], values[2], values[3], values[4], values[5], values[6], values[7], values[8]));
                    examList.Add(new Exam { Place=values[0],
                        Code = values[1],
                        Subject = values[2],
                        F_Date = values[3],
                        F_Time = values[4],
                        F_Room = values[5],
                        S_Date = values[6],
                        S_Time = values[7],
                        S_Room = values[8]
                    });
                }
            }
            ExamsListView.ItemsSource = examList;
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            ExamsListView.BeginRefresh();
            if (string.IsNullOrWhiteSpace(e.NewTextValue))
                ExamsListView.ItemsSource = examList;
            else
                ExamsListView.ItemsSource = examList.Where(i => i.Subject.IndexOf(e.NewTextValue, 0, StringComparison.CurrentCultureIgnoreCase) != -1);
            ExamsListView.EndRefresh();
        }

        private void ExamsListView_Refreshing(object sender, EventArgs e)
        {

        }
    }
}
