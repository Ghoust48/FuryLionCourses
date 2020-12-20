// Copyright (c) 2012-2019 FuryLion Group. All Rights Reserved.

using System;
using System.Collections.Generic;
using System.Text;

namespace Task2
{
    internal class Student : Human
    {
        private int _course;

        public string Faculty { get; set; }

        public string Group { get; set; }

        public int Course
        {
            set
            {
                while (value < 1 || value > 5)
                {
                    Console.Write("Ошибка ввода. Попробуйте еще раз: ");
                    value = ReadInt();
                }

                _course = value;
            }
            get
            {
                return _course;
            }
        }

        public Student()
        {
            Console.WriteLine("Вызов коструктора без параметров класса Student");
        }

        public Student(string firstName, string lastName, string middleName, string dateOfBirth, string faculty, int course, string group)
            : base(firstName, lastName, middleName, dateOfBirth)
        {
            Console.WriteLine("Вызов коструктора с параметрами класса Student");
            Faculty = faculty;
            Course = course;
            Group = group;
        }

        public Student(Student student)
        {
            Console.WriteLine("Вызов коструктора копирования класса Student");
            Course = student.Course;
            Faculty = student.Faculty;
            Group = student.Group;
        }

        ~Student()
        {
            Console.WriteLine($"Вызов деструктора класса Student, объекты: {Course}, {Faculty}, {Group} были уничтожены:");
        }

        public override void EditFields()
        {
            base.EditFields();

            Console.Write("Факультет: ");
            Faculty = Console.ReadLine();

            Console.Write("Курс: ");
            Course = ReadInt();

            Console.Write("Группа: ");
            Group = Console.ReadLine(); ;
        }

        public override string ToString()
        {
            return $"|                Студент            |\n" +
                   $"|___________________________________|\n" + 
                   base.ToString() + 
                   $"|     Факультет     |{Faculty,15}|\n" +
                   $"|___________________|_______________|\n" +
                   $"|       Курс        |{Course,15}|\n" +
                   $"|___________________|_______________|\n" +
                   $"|      Группа       |{Group,15}|\n" +
                   $"|___________________|_______________|";
        }

    }
}
