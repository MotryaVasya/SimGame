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

                Simulator sim = new Simulator(YAndE.Item1, YAndE.Item2, VirusCollection.ChickenpoxVirus);
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


            Console.WriteLine(simulator.Days == simulator.MaxDays ?
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

    public static class VirusCollection
    {
        public static Virus ChickenpoxVirus => new Chickenpox("Chickenpox", false, 0.7f, 0.0001f);
    }
    class Chickenpox : Virus
    {
        private static Random _rand = new Random();

        public Chickenpox(string Code, bool Reinfection, float InfectionCoef, float LethalityCoef) : base(Code, Reinfection, InfectionCoef, LethalityCoef)
        {
            _letality = LethalityCoef + (float)_rand.Next(-30, 30) / 100;
        }
        public override int AgeToInfect => 3;

        public override int DayToRecover => 14;

        public override bool Death(Person person)
        {
            if (_rand.NextDouble() <= Lethality)
            {
                person.Detach();
                return true;
            }
            return false;
        }

        public override void Infect(Person person)
        {
            if (person.Immunity <= Infection)
                person.Infect();
        }
    }
}
