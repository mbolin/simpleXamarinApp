using SimpleXamarinApp.Model;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SimpleXamarinApp.ViewModel
{
    public class Part3PageViewModel : BindableObject
    {
        public Part3Model MyModel { get; set; }
        public ICommand Button1 { get; }
        public ICommand Button2 { get; }
        public ICommand Button3 { get; }
        public ICommand Button4 { get; }

        public Part3PageViewModel()
        {
            Button1 = new Command(Button1Action);
            Button2 = new Command(Button2Action);
            Button3 = new Command(Button3ActionAsync);
            Button4 = new Command(Button4Action);
            MyModel = new Part3Model();
        }

        private void Button1Action()
        {
            TestSynchronous();
            TestSynchronous();
            TestSynchronous();
        }

        private void Button2Action()
        {
            TestASynchronous();
            TestASynchronous();
            TestASynchronous();
        }

        private async void Button3ActionAsync()
        {
            await TestASynchronous();
            await TestASynchronous();
            await TestASynchronous();
        }

        private void Button4Action()
        {
            //Task t = TestASynchronous();
            //TaskFactory tf = new TaskFactory();
            //tf.StartNew(TestASynchronous).Unwrap().GetAwaiter().GetResult();
            //tf.StartNew(TestASynchronous).Unwrap().GetAwaiter().GetResult();
            //tf.StartNew(TestASynchronous).Unwrap().GetAwaiter().GetResult();

            TestASynchronous().GetAwaiter().GetResult();
            TestASynchronous().GetAwaiter().GetResult();
            TestASynchronous().GetAwaiter().GetResult();
        }

        void TestSynchronous()
        {
            MyModel.Number++;
            Thread.Sleep(500);
        }

        async Task TestASynchronous()
        {
            MyModel.Number++;
            //configure await false is specifically for button4
            await Task.Delay(500).ConfigureAwait(false);
        }
    }
}