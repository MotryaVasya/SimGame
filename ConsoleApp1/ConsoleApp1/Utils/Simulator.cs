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
    class Simulator
    {
        private const double _mortality = (double)16 / 1000;
        private const double _birthrate = (double)8 / 1000;

        private static Random _random = new Random();
        private List<Person> _alive;
        private List<Person> _dead;
        private int _maxDays;
        private int _days;
        public int MaxDays => _maxDays;
        public Simulator(int countPopulation, int maxDays)
        {
            _days = 0;
            _maxDays = maxDays;
            _alive = new List<Person>();
            _dead = new List<Person>();
            Population(countPopulation);
        }
        public void RunSimmulation()
        {
            for (int i = 0; i < _maxDays; i++)
            {
                _days = i;
                _alive.RemoveAll((p) =>
                {
                    p.UpdateAge();
                    if (p.Age>= 29200)
                    {
                        _dead.Add(p);
                        return true;
                    }
                    return false;
                });
            }
        }

        private void UpdatePopulation(int Start, int Count)
        {
            List<Person> list = _alive.GetRange(Start, Count);
            foreach (Person person in list)
            {
                _dead.Add(person);
                _alive.Remove(person);
            }
        }
        private void UpdatePopulation()
        {
            List<Person> a = _alive.GetRange(0, (int)Math.Round(_alive.Count / _mortality));
            _alive.RemoveRange(0, (int)Math.Round(_alive.Count / _mortality));
            for (int i = 0; i < a.Count; i++)
            {
                _dead.Add(a[i]);
            }
            
            string[] gender = new string[2] { "Male", "Famale" };
            for (int i = 0; i < Math.Round(_alive.Count / _birthrate); i++)
            {
                Person person = new Person(gender[_random.Next(0, 2)],
                    0,
                    _random.Next(65, 76) / 100);
                _alive.Add(person);
            }
        }

        private void Population(int countPopulation)
        {
            string[] gender = new string[2] { "Male", "Famale" };
            int maxAge = 29201;
            double maxImmunity = 0.75;

            for (int i = 0; i < countPopulation; i++)
            {
                Person person = new Person(gender[_random.Next(0, 2)],
                    _random.Next(maxAge),
                    _random.Next(65, 76) / 100);
                if (person.Age >= 29200)
                    _dead.Add(person);
                else
                    _alive.Add(person);
            }

        }
    }
}
