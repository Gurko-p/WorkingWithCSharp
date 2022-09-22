using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timers = System.Timers;

namespace TimerApp
{
    /// <summary>
    /// Создаем таймер, который с интервалом в 2 секунды 
    /// проверяет системное время и в зависимости от этого выполняет необходимые действия
    /// </summary>
    class Program
    {
        public delegate void AccountHandler(string message);
        public static event AccountHandler Notify;
        public static event AccountHandler Notify1;

        static void Main(string[] args)
        {

            Timers.Timer timer = new Timers.Timer(2000);
            timer.AutoReset = true;
            timer.Enabled = true;
            timer.Elapsed += OnTimedEvent;
            Notify += delegate (string mes) { Console.WriteLine(mes); }; // можно остановить или запустить таймер timer.Stop(), timer.Start()
            Notify1 += delegate (string mes) { Console.WriteLine(mes); };

            Console.Read();
        }
        private static void OnTimedEvent(object source, Timers.ElapsedEventArgs e)
        {
            if (e.SignalTime < new DateTime(2022, 9, 22, 13, 5, 0))
            {
                Console.WriteLine("Событие таймера сработало в {0}", e.SignalTime);
            }
            else if (e.SignalTime > new DateTime(2022, 9, 22, 13, 7, 0) && e.SignalTime < new DateTime(2022, 9, 22, 13, 8, 0))
            {
                Notify.Invoke($"Таймер продолжает работу, запрос не отправляется! {e.SignalTime}");
            }
            else
            {
                Notify1.Invoke($"Таймер продолжает работу, запрос отправлен! {e.SignalTime}");
            }
        }
    }
}
