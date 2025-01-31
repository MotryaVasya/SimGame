using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    interface IVirus
    {
        string Code { get; }
        bool Reinfection { get; }
        float Infection { get; }
        float Lethality { get; }
        void Infect(Person person);
        bool Death(Person person);
    }
}
