using System.Collections.ObjectModel;

namespace SimpleXamarinApp.Model
{
    public class People
    {
        public ObservableCollection<Account> Persons { get; }

        public People()
        {
            Persons = new ObservableCollection<Account>();
        }
    }

    public class Account
    {
        public string Name { get; set; }
    }
}
