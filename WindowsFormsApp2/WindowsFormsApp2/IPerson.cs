using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
    internal interface IPerson
    {
        int _cardNumber { get; }
        string _name { get; }
        DateTime _birthday { get; }
        int CalcAge(DateTime date);
    }
}
