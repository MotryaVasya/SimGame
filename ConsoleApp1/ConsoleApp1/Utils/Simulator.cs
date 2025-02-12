using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Utils
{
    public class Simulator
    {
        private const double _mortality = (double)14 / 1000;
        private const double _birthrate = (double)8 / 1000;

        private static Random _random = new Random();
        private List<Person> _alive;
        private List<Person> _dead;
        private int _maxDays;
        private int _day;
        private Virus _virus;
        private int _illed;
        private int _recovered;

        public int Days => _day;
        public int MaxDays => _maxDays;
        public int Illed => _illed; // больные
        public int Recovered => _recovered;

        public List<Person> Alive => _alive;
        public List<Person> Dead => _dead;
        public Simulator(int countPopulation, int maxDays, Virus virus)
        {
            _day = 1;
            _maxDays = maxDays;
            _alive = new List<Person>();
            _dead = new List<Person>();
            _virus = virus;
            Population(countPopulation);
        }
        public void RunSimmulation()
        {
            StartInfection();
            for (int i = 1; i <= _maxDays; i++)
            {
                _day = i;
                _alive.RemoveAll((p) =>
                {
                    if (!p.IsAlive)
                    {
                        _dead.Add(p);
                        return true;
                    }
                    return false;
                });
                if (i % 365 == 0)
                {
                    _alive.RemoveAll((p) =>
                    {
                        p.UpdateAge();
                        if (p.Age >= p.MaxAge)
                        {
                            _dead.Add(p);
                            return true;
                        }
                        return false;
                    });
                }
                Infection();
                Mortaliti();
                Birth();
            }
        }
        private void Mortaliti()
        {
            int range = (int)Math.Round(_alive.Count * _mortality / 365);
            List<Person> a = _alive.GetRange(0, range);
            _alive.RemoveRange(0, range);
            _dead.AddRange(a);

        }
        private void Birth()
        {
            int range = (int)Math.Round(_alive.Count * _birthrate / 365);
            for (int i = 0; i < range; i++)
            {
                Person person = new Person(
                    _random.Next(0, 2) == 0 ? "Male" : "Famale", 0,
                    (float)_random.Next(70, 76) / 100);
                _alive.Add(person);
            }
        }
        private void StartInfection()
        {
            for (int i = 0; i < Math.Round(_alive.Count * 0.02); i++)
            {
                _alive.Find((p) => (p.Age >= _virus.AgeToInfect) && (!p.Status)).Infect();
                _illed++;
            }
            _alive = _alive.OrderBy(_ => _random.Next()).ToList();
        }
        private void Infection()
        {
            var allInfected = _alive.FindAll((p) => p.Status);
            foreach (var p in allInfected)
            {
                if (_virus.Death(p)) continue;

                if (p.UpdateInfection() == _virus.DayToRecover)
                {
                    if (!_virus.Reinfection)
                    {
                        p.CreateTotalImmunity();
                    }
                    p.Recover();
                    _recovered++;
                    continue;
                }
                if (_random.Next(101) >= 28)
                {
                    for (int i = 0; i < p.Friends / 2; i++)
                    {
                        Person meeting = _alive[_random.Next(0, _alive.Count)];
                        if (!meeting.Status && meeting.Age >= _virus.AgeToInfect && !meeting.TotalImmunity)
                        {
                            _virus.Infect(meeting);
                            _illed++;
                        }
                    }
                }
            }
        }
        private void Population(int countPopulation)
        {
            string[] gender = new string[2] { "Male", "Famale" };

            for (int i = 0; i < countPopulation; i++)
            {
                Person person = new Person(
                    gender[_random.Next(0, 2)],
                    _random.Next(0, 81),
                    (float)_random.Next(70, 76) / 100);
                if (person.Age >= person.MaxAge)
                    _dead.Add(person);
                else
                    _alive.Add(person);
            }

        }
    }
}
