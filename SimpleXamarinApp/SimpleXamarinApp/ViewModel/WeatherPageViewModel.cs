using Newtonsoft.Json;
using SimpleXamarinApp.Model;
using System.Net.Http;
using System.Windows.Input;
using Xamarin.Forms;

namespace SimpleXamarinApp.ViewModel
{
    public class WeatherPageViewModel : BindableObject
    {
        public ICommand GetWeatherCommand { get; }

        private int zipCode = 0;

        public int ZipCode
        {
            get => zipCode;
            set
            {
                if (zipCode == value)
                    return;
                zipCode = value;
                OnPropertyChanged();
            }
        }

        private Weather currentWeather;

        public Weather CurrentWeather
        {
            get => currentWeather;
            set
            {
                if (currentWeather == value)
                    return;
                currentWeather = value;
                OnPropertyChanged();
            }
        }

        public WeatherPageViewModel()
        {
            GetWeatherCommand = new Command(GetWeather);
        }

        private async void GetWeather()
        {
            if (zipCode < 10000)
                return;

            var httpClient = new HttpClient();
            var uri = string.Format("https://api.openweathermap.org/data/2.5/weather?zip={0},us&appid=b3cfe3e8bd07c9bef41276df2b8df9bb&units=imperial", ZipCode);
            var resultJson = await httpClient.GetStringAsync(uri);
            var results = JsonConvert.DeserializeObject<Weather>(resultJson);
            CurrentWeather = results;
        }
    }
}