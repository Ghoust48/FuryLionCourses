// Copyright (c) 2012-2019 FuryLion Group. All Rights Reserved.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Task2
{
    abstract class Human
    {
        private string _dateOfBirth;

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public string DateOfBirth
        {
            set
            {
                while (!DateTime.TryParseExact(value, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateTime))
                {
                    Console.Write("Ошибка ввода. Попробуйте еще раз: ");
                    value = Console.ReadLine();
                }
                    _dateOfBirth = value;
            }
            get
            {
                return _dateOfBirth;
            }
        }

        public Human()
        {
            Console.WriteLine("Вызов коструктора без параметров класса Human");
        }

        public Human (string firstName, string lastName, string middleName, string dateOfBirth)
        {
            Console.WriteLine("Вызов коструктора с параметрами класса Human");
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
            DateOfBirth = dateOfBirth;
        }

        public Human(Human human)
        {
            Console.WriteLine("Вызов коструктора копирования класса Human");
            FirstName = human.FirstName;
            LastName = human.LastName;
            MiddleName = human.MiddleName;
            DateOfBirth = human.DateOfBirth;
        }

        ~Human()
        {
            Console.WriteLine($"Вызов деструктора класса Human, объекты: {FirstName}, {LastName}, {MiddleName}, {DateOfBirth} были уничтожены:");
        }

        public virtual void EditFields()
        {
            Console.Write("Имя: ");
            FirstName = Console.ReadLine();

            Console.Write("Фамилия: ");
            LastName = Console.ReadLine();

            Console.Write("Отчество: ");
            MiddleName = Console.ReadLine();

            Console.Write("Дата рождения: ");
            DateOfBirth = Console.ReadLine();
        }

        public override string ToString()
        {
            return $"|        Имя        |{FirstName,15}|\n" +
                   $"|___________________|_______________|\n" +
                   $"|      Фамилия      |{LastName,15}|\n" +
                   $"|___________________|_______________|\n" +
                   $"|      Отчество     |{MiddleName,15}|\n" +
                   $"|___________________|_______________|\n" +
                   $"|   Дата рождения   |{DateOfBirth,15}|\n" +
                   $"|___________________|_______________|\n";
        }

        public virtual void FullYears()
        {
            var day = DateTime.Now.Day;
            var month = DateTime.Now.Month;
            var year = DateTime.Now.Year;
            var words = DateOfBirth.Split('.');

            if (int.Parse(words[1]) < month || (int.Parse(words[1]) == month && int.Parse(words[0]) <= day))
                Console.WriteLine($"|Количество полных лет: {year - int.Parse(words[2])}          |"+
                    "\n|___________________________________|");
            else if (int.Parse(words[1]) > month || (int.Parse(words[1]) == month && int.Parse(words[0]) > day))
               Console.WriteLine($"|Количество полных лет: {year - int.Parse(words[2]) - 1}          |" +
                    "\n|___________________________________|");
        }

        protected virtual int ReadInt()
        {
            int value;

            while (!int.TryParse(Console.ReadLine(), out value))
                Console.Write("Ошибка ввода. Попробуйте еще раз: ");

            return value;
        }

    }
}
