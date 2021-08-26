using System.ComponentModel;

namespace SimpleXamarinApp.Model
{
    public class Person : INotifyPropertyChanged
    {
        public string Name { get; set; }
        private int number = 0;

        private int Number
        {
            get => number;
            set
            {
                if (number == value)
                    return;
                number = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}