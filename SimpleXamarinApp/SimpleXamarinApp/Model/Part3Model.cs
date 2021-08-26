using System.ComponentModel;

namespace SimpleXamarinApp.Model
{
    public class Part3Model : INotifyPropertyChanged
    {
        private int number = 0;

        public int Number
        {
            get => number;
            set
            {
                if (number == value)
                    return;
                number = value;
                OnPropertyChange(nameof(Number));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChange(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}