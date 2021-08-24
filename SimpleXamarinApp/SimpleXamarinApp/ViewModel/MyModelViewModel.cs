using SimpleXamarinApp.Model;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SimpleXamarinApp.ViewModel
{
    public class MyModelViewModel : BindableObject
    {
        public ICommand AddCommand { get; }
        public ICommand RemoveCommand { get; }
        public ICommand ConstantAddCommand { get; }

        public People People { get; }

        Thread t;

        bool isRunning = false;

        string currentName;
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

        Account selectedPerson;
        public Account SelectedPerson
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
            People = new People();
            People.Persons.Add(new Account() { Name = "bob" });
            People.Persons.Add(new Account() { Name = "steve" });
        }

        void AddName()
        {
            People.Persons.Add(new Account() { Name = CurrentName });
        }

        void RemoveName()
        {
            People.Persons.Remove(SelectedPerson);
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
                t = new Thread(threadedAdding);
                t.Start();
                isRunning = true;
            }
        }

        void threadedAdding()
        {
            int count = 0;
            while (true)
            {
                People.Persons.Add(new Account() { Name = "jack" + count++ });
                Thread.Sleep(5000);
            }
        }
    }
}
