using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Pracktuchna__1_Костюк_Ярослав
{
    public enum StudentStatus
    {
        Active,
        AcademicLeave,
        Expelled,
        Graduated
    }

    public class Student
    {
        private string _fullName;
        private string _recordBookNumber;
        private string _personalEmail;
        private double _averageGrade;

        public required string Specialty { get; set; }
        public string Notes { get; set; }
        public DateTime EnrollmentDate { get; init; }
        public DateTime DateOfBirth { get; set; }
        public StudentStatus Status { get; set; }

        public string FullName
        {
            get { return _fullName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 5)
                {
                    throw new ArgumentException("ПІБ не може бути порожнім і має містити мінімум 5 символів.");
                }
                _fullName = value;
            }
        }

        public string RecordBookNumber
        {
            get { return _recordBookNumber; }
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length != 8)
                {
                    throw new ArgumentException("Номер залікової книжки має містити рівно 8 символів.");
                }
                _recordBookNumber = value;
            }
        }

        public string PersonalEmail
        {
            get { return _personalEmail; }
            set
            {
                if (!value.Contains("@") || !value.Contains("."))
                {
                    throw new ArgumentException("Невірний формат email.");
                }
                _personalEmail = value;
            }
        }

        public double AverageGrade
        {
            get { return _averageGrade; }
            private set { _averageGrade = Math.Round(value, 2); }
        }

        public int Age
        {
            get
            {
                int age = DateTime.Now.Year - DateOfBirth.Year;
                if (DateTime.Now.DayOfYear < DateOfBirth.DayOfYear)
                {
                    age--;
                }
                return age;
            }
        }

        public GradeJournal Journal { get; set; }

        public Student()
        {
            Journal = new GradeJournal();
        }

        public void ShowDetailedInfo()
        {
            Console.WriteLine($"Студент: {FullName}");
            Console.WriteLine($"Вік: {Age}, Залікова: {RecordBookNumber}");
            Console.WriteLine($"Середній бал: {AverageGrade}, Статус: {Status}");
            Console.WriteLine($"Дата зарахування: {EnrollmentDate.ToShortDateString()}");
        }

        public void UpdateAverageGrade(double newGrade)
        {
            if (newGrade >= 0 && newGrade <= 100)
            {
                AverageGrade = newGrade;
            }
        }

        public bool IsExcellent()
        {
            return AverageGrade >= 90;
        }

        public bool IsFailing()
        {
            return AverageGrade < 60;
        }

        public int GetYearsToGraduation()
        {
            int yearsStudied = DateTime.Now.Year - EnrollmentDate.Year;
            int yearsLeft = 4 - yearsStudied;
            if (yearsLeft < 0)
            {
                return 0;
            }
            return yearsLeft;
        }
    }
}
