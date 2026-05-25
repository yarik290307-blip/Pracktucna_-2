using System;
using System.Collections.Generic;
using System.Text;
using ConsoleApp1;

namespace StudentGroupManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            StudentGroup group = new StudentGroup();
            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine("\n=== РОЗШИРЕНЕ МЕНЮ СИСТЕМИ (16x16 Порти) ===");
                Console.WriteLine("1. Додати студента");
                Console.WriteLine("2. Видалити студента");
                Console.WriteLine("3. Вивести студентів (оцінки лабораторних)");
                Console.WriteLine("4. Відкрити порт");
                Console.WriteLine("5. Закрити порт");
                Console.WriteLine("6. Записати дані в порт");
                Console.WriteLine("7. Прочитати дані з порту");
                Console.WriteLine("8. Вивести повний стан матриці портів");
                Console.WriteLine("9. Прив'язати студента до порту");
                Console.WriteLine("10. Симулювати лабораторну роботу");
                Console.WriteLine("11. Переглянути лог операцій (StringBuilder)");
                Console.WriteLine("12. Вийти з програми");
                Console.Write("Оберіть дію (1-12): ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Введіть ім'я студента: ");
                        string name = Console.ReadLine();
                        Student newStudent = new Student()
                        {
                            FullName = name,
                            Specialty = "К-320"
                        };
                        group.AddStudent(newStudent);
                        Console.WriteLine("Студента додано.");
                        break;
                    case "2":
                        Console.Write("Введіть ім'я студента для видалення: ");
                        string nameToRemove = Console.ReadLine();
                        Student studentToRemove = group.FindStudentByName(nameToRemove);
                        if (studentToRemove != null)
                        {
                            group.RemoveStudent(studentToRemove);
                            Console.WriteLine("Студента видалено.");
                        }
                        else
                        {
                            Console.WriteLine("Студента не знайдено.");
                        }
                        break;
                    case "3":
                        List<Student> allStudents = group.GetAllStudents();

                        // Використання StringBuilder за вимогами завдання
                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine("--- Список студентів ---");
                        foreach (Student s in allStudents)
                        {
                            sb.Append("Студент: ");
                            sb.Append(s.FullName);
                            sb.Append(" | Сер. бал лаб: ");
                            sb.Append(s.GetAverageLabGrade());
                            sb.Append(" | Порт: ");
                            if (s.AssignedPortRow != -1)
                            {
                                sb.Append("[");
                                sb.Append(s.AssignedPortRow);
                                sb.Append(",");
                                sb.Append(s.AssignedPortCol);
                                sb.Append("]");
                            }
                            else
                            {
                                sb.Append("Не призначено");
                            }
                            sb.AppendLine();
                        }
                        Console.WriteLine(sb.ToString());
                        break;
                    case "4":
                        Console.Write("Рядок порту (0-15): ");
                        int rOpen = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Стовпець порту (0-15): ");
                        int cOpen = Convert.ToInt32(Console.ReadLine());
                        group.Matrix.OpenPort(rOpen, cOpen);
                        Console.WriteLine("Команду на відкриття надіслано.");
                        break;
                    case "5":
                        Console.WriteLine("Порт успішно закрито та очищено.");
                        break;
                    case "6":
                        Console.Write("Рядок порту (0-15): ");
                        int rWrite = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Стовпець порту (0-15): ");
                        int cWrite = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Введіть число (0-255) для запису: ");
                        byte bWrite = Convert.ToByte(Console.ReadLine());
                        byte[] dataWrite = new byte[] { bWrite };
                        group.Matrix.WriteToPort(rWrite, cWrite, dataWrite);
                        Console.WriteLine("Дані записано в буфер.");
                        break;
                    case "7":
                        Console.Write("Рядок порту (0-15): ");
                        int rRead = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Стовпець порту (0-15): ");
                        int cRead = Convert.ToInt32(Console.ReadLine());
                        byte[] dataRead = group.Matrix.ReadFromPort(rRead, cRead);
                        if (dataRead.Length > 0)
                        {
                            Console.WriteLine("Прочитано перший байт з буфера: " + dataRead[0]);
                        }
                        break;
                    case "8":
                        group.Matrix.ScanMatrix();
                        break;
                    case "9":
                        Console.Write("Введіть ім'я студента: ");
                        string sName = Console.ReadLine();
                        Student sAssign = group.FindStudentByName(sName);
                        if (sAssign != null)
                        {
                            Console.Write("Рядок порту (0-15): ");
                            int pRow = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Стовпець порту (0-15): ");
                            int pCol = Convert.ToInt32(Console.ReadLine());
                            group.AssignStudentToPort(sAssign, pRow, pCol);
                        }
                        else
                        {
                            Console.WriteLine("Студента не знайдено.");
                        }
                        break;
                    case "10":
                        Console.Write("Введіть ім'я студента: ");
                        string simName = Console.ReadLine();
                        Student simStudent = group.FindStudentByName(simName);
                        if (simStudent != null)
                        {
                            Console.Write("Номер лабораторної (1-10): ");
                            int labNum = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Оцінка (0-100): ");
                            byte labGrade = Convert.ToByte(Console.ReadLine());

                            byte[] labData = new byte[] { labGrade };
                            group.SimulateLabWork(simStudent, labNum, labGrade, labData);
                        }
                        else
                        {
                            Console.WriteLine("Студента не знайдено.");
                        }
                        break;
                    case "11":
                        Console.WriteLine(group.Logger.GetFullLog());
                        break;
                    case "12":
                        isRunning = false;
                        Console.WriteLine("Роботу завершено. Успіхів!");
                        break;
                    default:
                        Console.WriteLine("Невідома команда. Спробуйте ще раз.");
                        break;
                }
            }
        }
    }
}