using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WeatherApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        const string url = "http://dataservice.accuweather.com/forecasts/v1/daily/5day/322722?apikey=UDWRwa1QGHyvfKFGDTUZzA1xImcT3FrC";
        public MainWindow()
        {
            InitializeComponent();
            Button[] days = { Day1, Day2, Day3, Day4, Day5, Day6, Day7, Day8, Day9, Day10 };
            for (int i = 0; i < days.Length; i++)
            {
                DateTime currentDate = date.AddDays(i);
                int dayOfWeekIndex = ((int)currentDate.DayOfWeek + i) % 7;
                string dayName = currentDate.ToString("dddd");
                string capitalizedDayName = char.ToUpper(dayName[0]) + dayName.Substring(1);
                days[i].Content = $"{capitalizedDayName}, {currentDate.Day}";
                getWeather();
            }
            

        }
        private async void getWeather()
        {
            HttpClient httpClient = new HttpClient();
            string response = await httpClient.GetStringAsync(url);
            JObject jsonObject = JObject.Parse(response);
            double temperatureMiminum = (double)jsonObject["DailyForecasts"][0]["Temperature"]["Minimum"]["Value"];
            Day1.Content = (temperatureMiminum - 32) * 5/9;
        }
        static DateTime date = DateTime.Now;
        
    }
    

}
