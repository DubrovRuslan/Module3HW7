using System;
using System.Threading.Tasks;

namespace Module3HW7
{
    public class MessageBox
    {
        private const int Delay = 3000;
        public event Action<State> Message;
        public async void Open()
        {
            Console.WriteLine("Окно открыто");
            await Task.Delay(Delay);
            Console.WriteLine("Окно закрыто");
            Message?.Invoke((State)(new Random().Next(2)));
        }
    }
}
