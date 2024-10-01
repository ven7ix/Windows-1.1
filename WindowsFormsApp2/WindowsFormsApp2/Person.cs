using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
    internal class Person : IPerson
    {
        public int _cardNumber { get; set; }
        public string _name { get; set; }
        public DateTime _birthday { get; set; }
        
        public Person(int cardNumber, string name, DateTime birthday)
        {
            _cardNumber = cardNumber;
            _name = name;
            _birthday = birthday;
        }

        public int CalcAge(DateTime date)
        {
            int age = DateTime.Now.Year - date.Year;
            if (DateTime.Now.Month < date.Month || (DateTime.Now.Month < date.Month && DateTime.Now.Day < date.Day)) age--;
            return age;
        }
    }
}
