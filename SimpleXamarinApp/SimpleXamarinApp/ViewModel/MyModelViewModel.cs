using Newtonsoft.Json;
using SimpleXamarinApp.Model;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading;
using System.Windows.Input;
using Xamarin.Forms;

namespace SimpleXamarinApp.ViewModel
{
    public class MyModelViewModel : BindableObject
    {
        public ICommand AddCommand { get; }
        public ICommand RemoveCommand { get; }
        public ICommand ConstantAddCommand { get; }
        public ICommand GetWeatherCommand { get; }

        public ObservableCollection<Person> People { get; }

        int zipCode = 0;
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

        Weather currentWeather;
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

        Thread t;

        bool isRunning = false;

        string currentName = string.Empty;
        public string CurrentName
        {
            get => currentName;
            set
            {
                if (currentName == value)
                    return;
                currentName = value;
                OnPropertyChanged();
            }
        }

        Person selectedPerson = null;
        public Person SelectedPerson
        {
            get => selectedPerson;
            set
            {
                if (selectedPerson == value)
                    return;
                selectedPerson = value;
                CurrentName = value.Name;
                OnPropertyChanged();
            }
        }

        public MyModelViewModel()
        {
            AddCommand = new Command(AddName);
            RemoveCommand = new Command(RemoveName);
            ConstantAddCommand = new Command(ConstantAddName);
            GetWeatherCommand = new Command(GetWeather);
            People = new ObservableCollection<Person>
            {
                new Person() { Name = "bob" },
                new Person() { Name = "steve" }
            };
        }

        async void GetWeather()
        {
            if (zipCode < 10000)
                return;

            var httpClient = new HttpClient();
            var uri = string.Format("https://api.openweathermap.org/data/2.5/weather?zip={0},us&appid=b3cfe3e8bd07c9bef41276df2b8df9bb&units=imperial", ZipCode);
            var resultJson = await httpClient.GetStringAsync(uri);
            var results = JsonConvert.DeserializeObject<Weather>(resultJson);
            CurrentWeather = results;
        }

        void AddName()
        {
            if (string.IsNullOrEmpty(CurrentName))
                return;
            System.Console.WriteLine("adding "+ CurrentName);
            People.Add(new Person() { Name = CurrentName });
        }

        void RemoveName()
        {
            if (SelectedPerson == null)
                return;
            System.Console.WriteLine("removing " + CurrentName);
            People.Remove(SelectedPerson);
        }

        void ConstantAddName()
        {
            if (isRunning)
            {
                t.Abort();
                isRunning = false;
            }
            else
            {
                t = new Thread(ThreadedAdding);
                t.Start();
                isRunning = true;
            }
        }

        void ThreadedAdding()
        {
            int count = 0;
            while (true)
            {
                People.Add(new Person() { Name = "jack" + count++ });
                Thread.Sleep(5000);
            }
        }
    }
}
