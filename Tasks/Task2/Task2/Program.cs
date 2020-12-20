// Copyright (c) 2012-2019 FuryLion Group. All Rights Reserved.

using System;
using System.Collections.Generic;

namespace Task2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int selectMenu;
            Human human;
            var listPeople = new List<Human> {
                new Student("Игорь", "Слепухин", "Александрович", "18.09.1999", "ФИТ", 2, "17-ИТ-4"),
                new Student("Александр", "Шершень", "Яковлевич", "28.09.2000", "ФИТ", 2, "17-ИТ-4"),
                new Employee("Вася", "Пупкин", "Александрович", "12.01.1975", "FuryLion", 1000, 2)
            };

            do
            {
                Console.Clear();
                Console.WriteLine(  "1. Добавить информацию о новом человеке.\n" +
                                    "2. Редактировать поля уже имеющейся записи о человеке.\n" +
                                    "3. Удалить информацию о человеке.\n" +
                                    "4. Вывести информацию о человеке\n" +
                                    "5. Вывести информацию о всех людях\n" +
                                    "0. Выход.");
                selectMenu = ReadInt();
                while(selectMenu < 0 || selectMenu > 5)
                {
                    Console.Write("Ошибка ввода. Попробуйте еще раз: ");
                    selectMenu = ReadInt();
                }

                switch (selectMenu)
                {
                    case 1:
                        {
                            Console.Clear();
                            Console.WriteLine(  "1. Добавить информацию о новом студенте.\n" +
                                                "2. Добавить информацию о новом работнике.\n" +
                                                "3. Добавить информацию о новом водителе.\n");
                            selectMenu = ReadInt();
                            while (selectMenu < 1 || selectMenu > 3)
                            {
                                Console.Write("Ошибка ввода. Попробуйте еще раз: ");
                                selectMenu = ReadInt();
                            }

                            switch (selectMenu)
                            {
                                case 1:
                                    {
                                        Console.Clear();
                                        human = new Student();
                                        listPeople = InputPeople(listPeople, human);
                                        Console.ReadLine();
                                        break;
                                    }
                                case 2:
                                    {
                                        Console.Clear();
                                        human = new Employee();
                                        listPeople = InputPeople(listPeople, human);
                                        Console.ReadLine();
                                        break;
                                    }
                                case 3:
                                    {
                                        Console.Clear();
                                        human = new Driver();
                                        listPeople = InputPeople(listPeople, human);
                                        Console.ReadLine();
                                        break;
                                    }
                            }
                            break;
                        }
                    case 2:
                        {
                            Console.Clear();
                            OutputAllPeople(listPeople);
                            Console.Write("Укажите номер записи, которую хотите отредактировать: ");
                            var count = CheckCount(listPeople);
                            listPeople[count - 1].EditFields();
                            break;
                        }
                    case 3:
                        {
                            Console.Clear();
                            Console.Write("Укажите номер записи, которую хотите удалить: ");
                            var count = CheckCount(listPeople);
                            listPeople.RemoveAt(count - 1);
                            break;
                        }
                    case 4:
                        {
                            Console.Clear();
                            Console.Write("Укажите номер записи, которую хотите вывести: ");
                            var count = CheckCount(listPeople);

                            Console.WriteLine($" ___________________________________\n" +
                                   $"|Запись №{count}                          |" +
                                   $"\n{listPeople[count - 1]}");

                            Console.ReadLine();
                            break;
                        }
                    case 5:
                        {
                            Console.Clear();
                            OutputAllPeople(listPeople);
                            Console.ReadLine();
                            break;
                        }
                }
            } while (selectMenu != 0);
        }

        private static List<Human> InputPeople(List<Human> listPeople, Human human)
        {
            human.EditFields();
            listPeople.Add(human);

            return listPeople;
        }

        private static int CheckCount(List<Human> listPeople)
        {
            var count = ReadInt();
            while (count < 0 || count > listPeople.Count)
            {
                Console.Write("Ошибка ввода. Попробуйте еще раз: ");
                count = ReadInt();
            }

            return count;
        }
        
        private static void OutputAllPeople(List<Human> listPeople)
        {
            for (int i = 0; i < listPeople.Count; i++)
            {
                Console.WriteLine($" ___________________________________\n" +
                                  $"|Запись №{i + 1}                          |" +
                                  $"\n{listPeople[i]}");
                listPeople[i].FullYears();
            }
        }

        private static int ReadInt()
        {
            int value;

            while (!int.TryParse(Console.ReadLine(), out value))
                Console.Write("Ошибка ввода. Попробуйте еще раз: ");

            return value;
        }
    }
}
