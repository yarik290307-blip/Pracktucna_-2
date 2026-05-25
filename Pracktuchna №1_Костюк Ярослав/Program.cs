using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1;

namespace ConsoleApp1.Pracktuchna__1_Костюк_Ярослав
{
    class Program
    {
        static void OldMain(string[] args)
        {
            StudentGroup myGroup = new StudentGroup();
            myGroup.GroupName = "К-320";
            myGroup.Specialty = "Інженерія програмного забезпечення";
            myGroup.CourseNumber = 3;

            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine("\n=== Меню управління групою ===");
                Console.WriteLine("1. Додати студента");
                Console.WriteLine("2. Видалити студента");
                Console.WriteLine("3. Вивести всіх студентів ");
                Console.WriteLine("4. Пошук студента (за ПІБ або номером залікової)");
                Console.WriteLine("5. Редагування даних студента");
                Console.WriteLine("6. Вивести відмінників / тих, хто має < 60 балів");
                Console.WriteLine("7. Вивести статистику групи");
                Console.WriteLine("8. Зберегти / Завантажити дані групи з файлу");
                Console.WriteLine("0. Вийти");
                Console.Write("Оберіть дію: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddStudentUI(myGroup);
                        break;
                    case "2":
                        RemoveStudentUI(myGroup);
                        break;
                    case "3":
                        ShowAllStudentsUI(myGroup);
                        break;
                    case "4":
                        SearchStudentUI(myGroup);
                        break;
                    case "5":
                        EditStudentUI(myGroup);
                        break;
                    case "6":
                        ShowFilteredStudentsUI(myGroup);
                        break;
                    case "7":
                        ShowStatisticsUI(myGroup);
                        break;
                    case "8":
                        SaveLoadUI(myGroup);
                        break;
                    case "0":
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Невідома команда. Спробуйте ще раз.");
                        break;
                }
            }
        }

        static void AddStudentUI(StudentGroup group)
        {
            Console.WriteLine("\n--- Додавання студента ---");
            try
            {
                Student newStudent = new Student()
                {
                    Specialty = group.Specialty, // Required властивість
                    EnrollmentDate = DateTime.Now // Init-only властивість
                };

                Console.Write("Введіть ПІБ: ");
                newStudent.FullName = Console.ReadLine();

                Console.Write("Введіть номер залікової (8 символів): ");
                newStudent.RecordBookNumber = Console.ReadLine();

                Console.Write("Введіть email: ");
                newStudent.PersonalEmail = Console.ReadLine();

                Console.Write("Рік народження: ");
                int year = int.Parse(Console.ReadLine());
                Console.Write("Місяць народження: ");
                int month = int.Parse(Console.ReadLine());
                Console.Write("День народження: ");
                int day = int.Parse(Console.ReadLine());
                newStudent.DateOfBirth = new DateTime(year, month, day);

                newStudent.Status = StudentStatus.Active;

                group.AddStudent(newStudent);
                Console.WriteLine("Студента успішно додано!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка при додаванні: {ex.Message}");
            }
        }

        static void RemoveStudentUI(StudentGroup group)
        {
            Console.WriteLine("\n--- Видалення студента ---");
            Console.Write("Введіть номер залікової студента: ");
            string recordBook = Console.ReadLine();

            Student found = group.FindStudent(recordBook);
            if (found != null)
            {
                group.RemoveStudent(recordBook);
                Console.WriteLine("Студента видалено.");
            }
            else
            {
                Console.WriteLine("Студента з такою заліковою не знайдено.");
            }
        }

        static void ShowAllStudentsUI(StudentGroup group)
        {
            Console.WriteLine("\n--- Список студентів ---");
            List<Student> students = group.GetAllStudents();

            if (students.Count == 0)
            {
                Console.WriteLine("Група порожня.");
                return;
            }

            for (int i = 0; i < students.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {students[i].FullName} - Залікова: {students[i].RecordBookNumber}");

                if ((i + 1) % 10 == 0 && (i + 1) < students.Count)
                {
                    Console.WriteLine("Натисніть Enter для продовження...");
                    Console.ReadLine();
                }
            }
        }

        static void SearchStudentUI(StudentGroup group)
        {
            Console.WriteLine("\n--- Пошук студента ---");
            Console.Write("Введіть ПІБ або номер залікової: ");
            string query = Console.ReadLine();

            bool foundAny = false;
            foreach (Student s in group.GetAllStudents())
            {
                if (s.FullName.Contains(query) || s.RecordBookNumber == query)
                {
                    s.ShowDetailedInfo();
                    foundAny = true;
                }
            }

            if (!foundAny)
            {
                Console.WriteLine("Студентів не знайдено.");
            }
        }

        static void EditStudentUI(StudentGroup group)
        {
            Console.WriteLine("\n--- Редагування даних ---");
            Console.Write("Введіть номер залікової студента: ");
            string recordBook = Console.ReadLine();

            Student student = group.FindStudent(recordBook);
            if (student == null)
            {
                Console.WriteLine("Студента не знайдено.");
                return;
            }

            Console.WriteLine("1. Оновити середній бал");
            Console.WriteLine("2. Змінити статус");
            Console.WriteLine("3. Додати нотатку");
            Console.Write("Оберіть дію: ");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.Write("Введіть новий бал (0-100): ");
                if (double.TryParse(Console.ReadLine(), out double newGrade))
                {
                    student.UpdateAverageGrade(newGrade);
                    Console.WriteLine("Бал оновлено.");
                }
            }
            else if (choice == "2")
            {
                Console.WriteLine("0 - Active, 1 - AcademicLeave, 2 - Expelled, 3 - Graduated");
                Console.Write("Введіть номер статусу: ");
                if (int.TryParse(Console.ReadLine(), out int statusNum))
                {
                    student.Status = (StudentStatus)statusNum;
                    Console.WriteLine("Статус оновлено.");
                }
            }
            else if (choice == "3")
            {
                Console.Write("Введіть нотатку: ");
                student.Notes = Console.ReadLine();
                Console.WriteLine("Нотатку збережено.");
            }
        }

        static void ShowFilteredStudentsUI(StudentGroup group)
        {
            Console.WriteLine("\n--- Відмінники та боржники ---");
            Console.WriteLine("1. Відмінники (>= 90)");
            Console.WriteLine("2. Боржники (< 60)");
            Console.Write("Оберіть варіант: ");
            string choice = Console.ReadLine();

            foreach (Student s in group.GetAllStudents())
            {
                if (choice == "1" && s.IsExcellent())
                {
                    Console.WriteLine($"{s.FullName} - {s.AverageGrade} балів");
                }
                else if (choice == "2" && s.IsFailing())
                {
                    Console.WriteLine($"{s.FullName} - {s.AverageGrade} балів");
                }
            }
        }

        static void ShowStatisticsUI(StudentGroup group)
        {
            Console.WriteLine("\n--- Статистика групи ---");
            Console.WriteLine($"Кількість студентів: {group.GroupSize}");
            Console.WriteLine($"Середній бал групи: {group.AverageGroupGrade}");

            int excellentCount = 0;
            foreach (Student s in group.GetAllStudents())
            {
                if (s.IsExcellent())
                {
                    excellentCount++;
                }
            }

            if (group.GroupSize > 0)
            {
                double percent = (double)excellentCount / group.GroupSize * 100;
                Console.WriteLine($"Відсоток відмінників: {Math.Round(percent, 2)}%");
            }
        }

        static void SaveLoadUI(StudentGroup group)
        {
            Console.WriteLine("\n--- Робота з файлом ---");
            Console.WriteLine("1. Зберегти дані");
            Console.WriteLine("2. Завантажити дані");
            Console.Write("Оберіть дію: ");
            string choice = Console.ReadLine();

            string filePath = "group_data.json";

            if (choice == "1")
            {
                group.SaveToFile(filePath);
                Console.WriteLine("Дані успішно збережено у " + filePath);
            }
            else if (choice == "2")
            {
                group.LoadFromFile(filePath);
                Console.WriteLine("Дані завантажено з " + filePath);
            }
        }
    }
}