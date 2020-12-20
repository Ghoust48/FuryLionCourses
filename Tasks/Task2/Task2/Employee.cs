// Copyright (c) 2012-2019 FuryLion Group. All Rights Reserved.

using System;
using System.Collections.Generic;
using System.Text;

namespace Task2
{
    internal class Employee : Human
    {
        private int _pay;
        private float _experience;

        public string Organization { get; set; }

        public int Pay
        {
            set
            {
                while (value <= 0)
                {
                    Console.Write("Ошибка ввода. Попробуйте еще раз: ");
                    value = ReadInt();
                }
                _pay = value;
            }
            get
            {
                return _pay;
            }
        }

        public float Experience
        {
            set
            {
                while (value <= 0)
                {
                    Console.Write("Ошибка ввода. Попробуйте еще раз: ");
                    value = ReadInt();
                }

                _experience = value;
                
            }
            get
            {
                return _experience;
            }
        }

        public Employee()
        {
            Console.WriteLine("Вызов коструктора без параметров класса Employee");
        }

        public Employee(string firstName, string lastName, string middleName, string dateOfBirth, 
            string organization, int pay, float experience)
            : base(firstName, lastName, middleName, dateOfBirth)
        {
            Console.WriteLine("Вызов коструктора с параметрами класса Employee");
            Organization = organization;
            Pay = pay;
            Experience = experience;
        }

        public Employee(Employee employee)
        {
            Console.WriteLine("Вызов коструктора копирования класса Employee");
            Organization = employee.Organization;
            Pay = employee.Pay;
            Experience = employee.Experience;
        }

        ~Employee()
        {
            Console.WriteLine($"Вызов деструктора класса Employee, объекты: {Organization}, {Pay}, {Experience} были уничтожены:");
        }

        public override void EditFields()
        {
            base.EditFields();

            Console.Write("Организация: ");
            Organization = Console.ReadLine();

            Console.Write("Зарплата: ");
            Pay = ReadInt();

            Console.Write("Стаж: ");
            Experience = float.Parse(Console.ReadLine());
        }

        public override string ToString()
        {
            return $"|               Работник            |\n" +
                   $"|___________________________________|\n" + 
                   base.ToString() +
                   $"|    Организация    |{Organization,15}|\n" +
                   $"|___________________|_______________|\n" +
                   $"|      Зарплата     |{Pay,15}|\n" +
                   $"|___________________|_______________|\n" +
                   $"|       Стаж        |{Experience,15}|\n" +
                   $"|___________________|_______________|";
        }

        
    }
}
