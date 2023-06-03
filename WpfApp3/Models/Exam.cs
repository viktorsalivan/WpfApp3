using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp3.Models
{
    public class Exam
    {
        //Название предмета
        public string Name { get; set; }

        //Фаммлия, имя и очество
        public string SurnameNP { get; set; }

        //Паспорт
        public string Passport { get; set; }

        //Дата
        public DateTime Date { get; set; }
        // Оцентка
        public int Rating { get; set; }


        // конструкторы
        public Exam() { }
        public Exam(string name, string surnameNP, string passport, DateTime date, int rating)
        {
            Name = name;
            SurnameNP = surnameNP;
            Passport = passport;
            Date = date;
            Rating = rating;
        } // Exam

    }
}
