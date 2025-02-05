using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Person
    {
        private string _gender;
        private float _immunity;
        private int _age;
        private float _initialImmunity;
        private bool _totalImmunity;
        private const float _coefLostImmunity = 0.000017f;


        public int Age => _age;
        public string Gender => _gender; 
        public float Immunity => _immunity; 
        public bool Status { get; set; }
        private bool TotalImmunity => _totalImmunity;
        public Person(string Gender, int Age, float Immunity)
        {
            _gender = Gender;
            _age = Age;
            _initialImmunity = Immunity;

            Status = false;
            UpdateImmunity();
        }
        public void UpdateAge()
        {
            _age++;
            UpdateImmunity();
        }

        private void UpdateImmunity()
        {
            if (Age >= 29200)
                return;
            _immunity = _initialImmunity - _coefLostImmunity * Age;
        }
    }
}
