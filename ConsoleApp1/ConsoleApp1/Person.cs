using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Person
    {
        private int _id;
        private string _gender;
        private int Age { get; set; }
        private float _immunity;
        private float _initialImmunity;
        private bool _totalImmunity;
        private const float _coefLostImmunity = 0.000017f;


        public string Gender => _gender; 
        public float Immunity => _immunity; 
        public bool Status { get; set; }
        private bool TotalImmunity => _totalImmunity;
        public Person(int Id, string Gender, int Age, float Immunity)
        {
             _id = Id;
            _gender = Gender;
            this.Age = Age;
            _initialImmunity = Immunity;

            Status = false;
            UpdateImmunity();
        }
        private void UpdateAge()
        {
            Age++;
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
