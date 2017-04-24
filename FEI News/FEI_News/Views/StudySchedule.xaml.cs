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
    public partial class StudySchedule : ContentPage
    {
        private HttpManager httpManager;
        private ObservableCollection<Schedule> scheduleList;

        public StudySchedule()
        {
            InitializeComponent();

            httpManager = HttpManager.Instance;

            Title = "Harmonogram štúdia";
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var stream = Android.App.Application.Context.Assets.Open("harmonogram_studia.csv");
            scheduleList = new ObservableCollection<Schedule>();

            using (var streamReader = new StreamReader(stream, Encoding.GetEncoding("Windows-1250"), true))
            {
                // Ignore header
                streamReader.ReadLine();

                while (!streamReader.EndOfStream)
                {
                    var line = streamReader.ReadLine();
                    var values = line.Split(';');
                    scheduleList.Add(new Schedule
                    {
                        Event = values[0],
                        Date = values[1],
                        Type = values[2],
                    });
                }
            }
            ScheduleListView.ItemsSource = scheduleList;
        }
    }
}
