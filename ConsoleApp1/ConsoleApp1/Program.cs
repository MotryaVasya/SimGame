using ConsoleApp1.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                (int, int) YAndE;
                Console.WriteLine("Введите сколько народа будет в вашем городе");
                YAndE.Item1 = int.Parse(Console.ReadLine());
                Console.WriteLine("Введите сколько дней будет длится эпидемия");
                YAndE.Item2 = int.Parse(Console.ReadLine());

                Simulator sim = new Simulator(YAndE.Item1, YAndE.Item2, new Killar("asdf", false, 0.3f, 0.5f));
                Observer observer = new Observer(ref sim);
                observer.Start();
                Results(sim);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        private static void Results(Simulator simulator)
        {
            /*
                 * Сколько человек было заражено;
                 * Сколько человек погибло;
                 * Сколько человек вылечилось;
                 * Сколько человек в популяции;
                 * Сколько дней длилась симуляция (необходимо, если симуляция была досрочно остановлена).
             */


            Console.WriteLine(simulator.Days==simulator.MaxDays?
                $"Заражено: {simulator.Illed}\n" +
                $"Умершие: {simulator.Dead.Count}\n" +
                $"Вылечилось: {simulator.Recovered}\n" +
                $"Живые: {simulator.Alive.Count}"
                :
                $"Заражено: {simulator.Illed}\n" +
                $"Умершие: {simulator.Dead.Count}\n" +
                $"Вылечилось: {simulator.Recovered}\n" +
                $"Живые: {simulator.Alive.Count}\n" +
                $"Дней прошло: {simulator.Days}"
                );
        }
    }
}
