// Copyright (c) 2012-2019 FuryLion Group. All Rights Reserved.

using System;
using System.Collections.Generic;
using System.Text;

namespace Task2
{
    internal class Driver : Employee
    {
        public string CarBrand { get; set; }
        public string CarModel { get; set; }

        public Driver()
        {
            Console.WriteLine("Вызов коструктора без параметров класса Driver");
        }

        public Driver(string firstName, string lastName, string middleName, string dateOfBirth, 
            string organization, int pay, float experience, string carBrand, string carModel)
            : base(firstName, lastName, middleName, dateOfBirth, organization, pay, experience)
        {
            Console.WriteLine("Вызов коструктора с параметрами класса Driver");
            CarBrand = carBrand;
            CarModel = carModel;
        }

        public Driver(Driver driver)
        {
            Console.WriteLine("Вызов коструктора копирования класса Driver");
            CarBrand = driver.CarBrand;
            CarModel = driver.CarModel;
        }

        ~Driver()
        {
            Console.WriteLine($"Вызов деструктора класса Employee, объекты: {CarBrand}, {CarModel}, были уничтожены:");
        }

        public override void EditFields()
        {
            base.EditFields();

            Console.Write("Марка автомобиля: ");
            CarBrand = Console.ReadLine();
            
            Console.Write("Модель автомобиля: ");
            CarModel = Console.ReadLine();
        }

        public override string ToString()
        {
            return $"|               Водитель            |\n" +
                   $"|___________________________________|\n" + 
                   base.ToString() +    
                   $"\n|  Марка автомобиля |{CarBrand,15}|\n" +
                   $"|___________________|_______________|\n" +
                   $"| Модель автомобиля |{CarModel,15}|\n" +
                   $"|___________________|_______________|";
        }
    }
}
