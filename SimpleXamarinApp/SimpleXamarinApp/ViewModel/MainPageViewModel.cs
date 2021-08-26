using SimpleXamarinApp.Model;
using SimpleXamarinApp.View;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows.Input;
using Xamarin.Forms;

namespace SimpleXamarinApp.ViewModel
{
    public class MainPageViewModel : BindableObject
    {
        public ICommand AddCommand { get; }
        public ICommand RemoveCommand { get; }
        public ICommand ConstantAddCommand { get; }
        public ICommand WeatherPageCommand { get; }
        public ICommand Page3Command { get; }

        public ObservableCollection<Person> People { get; }

        private Thread t;

        private bool isRunning = false;

        private string currentName = string.Empty;

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

        private Person selectedPerson = null;

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

        public MainPageViewModel()
        {
            AddCommand = new Command(AddName);
            RemoveCommand = new Command(RemoveName);
            ConstantAddCommand = new Command(ConstantAddName);
            WeatherPageCommand = new Command(GoToWeatherPage);
            Page3Command = new Command(GoToPart3Page);
            People = new ObservableCollection<Person>
            {
                new Person() { Name = "bob" },
                new Person() { Name = "steve" }
            };
        }

        private async void GoToPart3Page()
        {
            var part3PageVM = new Part3PageViewModel();
            var part3Page = new Part3Page();
            part3Page.BindingContext = part3PageVM;
            await Application.Current.MainPage.Navigation.PushAsync(part3Page);
        }

        private async void GoToWeatherPage()
        {
            var weatherPageVM = new WeatherPageViewModel();
            var weatherPage = new WeatherPage();
            weatherPage.BindingContext = weatherPageVM;
            await Application.Current.MainPage.Navigation.PushAsync(weatherPage);
        }

        private void AddName()
        {
            if (string.IsNullOrEmpty(CurrentName))
                return;
            System.Console.WriteLine("adding " + CurrentName);
            People.Add(new Person() { Name = CurrentName });
        }

        private void RemoveName()
        {
            if (SelectedPerson == null)
                return;
            System.Console.WriteLine("removing " + CurrentName);
            People.Remove(SelectedPerson);
        }

        private void ConstantAddName()
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

        private void ThreadedAdding()
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