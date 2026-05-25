using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class StudentGroup
    {
        public PortMatrix Matrix { get; set; } = new PortMatrix();
        public PortLogger Logger { get; set; } = new PortLogger();
        public string GroupName { get; set; }
        public string Specialty { get; set; }
        public int CourseNumber { get; set; }

        private List<Student> _students;

        public StudentGroup()
        {
            _students = new List<Student>();
        }

        public int GroupSize
        {
            get { return _students.Count; }
        }

        public double AverageGroupGrade
        {
            get
            {
                if (_students.Count == 0) return 0;
                double sum = 0;
                foreach (Student s in _students)
                {
                    sum += s.AverageGrade;
                }
                return Math.Round(sum / _students.Count, 2);
            }
        }

        public void AddStudent(Student s)
        {
            _students.Add(s);
        }

        public void RemoveStudent(string recordBookNumber)
        {
            Student toRemove = null;
            foreach (Student s in _students)
            {
                if (s.RecordBookNumber == recordBookNumber)
                {
                    toRemove = s;
                    break;
                }
            }
            if (toRemove != null)
            {
                _students.Remove(toRemove);
            }
        }

        public Student FindStudent(string recordBookNumber)
        {
            foreach (Student s in _students)
            {
                if (s.RecordBookNumber == recordBookNumber)
                {
                    return s;
                }
            }
            return null;
        }

        public List<Student> GetExcellentStudents()
        {
            List<Student> excellent = new List<Student>();
            foreach (Student s in _students)
            {
                if (s.IsExcellent())
                {
                    excellent.Add(s);
                }
            }
            return excellent;
        }

        public void SaveToFile(string filePath)
        {
            string json = JsonSerializer.Serialize(_students);
            File.WriteAllText(filePath, json);
        }

        public void LoadFromFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                _students = JsonSerializer.Deserialize<List<Student>>(json);
            }
        }
        public List<Student> GetAllStudents()
        {
            return _students;
        }
        public void AssignStudentToPort(Student s, int row, int col)
        {
            s.AssignedPortRow = row;
            s.AssignedPortCol = col;
            Matrix.OpenPort(row, col);

            int portNum = (row * 16) + col + 1;
            Logger.LogOperation("Прив'язка порту", portNum, "Студент " + s.FullName + " отримав доступ.");
            Console.WriteLine("Студента успішно прив'язано до порту!");
        }

        public List<Student> GetStudentsByPortStatus(bool isOpen)
        {
            List<Student> result = new List<Student>();
            foreach (Student s in _students)
            {
                if (s.AssignedPortRow != -1 && s.AssignedPortCol != -1)
                {
                    bool portStatus = Matrix.IsPortOpen(s.AssignedPortRow, s.AssignedPortCol);
                    if (portStatus == isOpen)
                    {
                        result.Add(s);
                    }
                }
            }
            return result;
        }

        public void SimulateLabWork(Student s, int labNumber, byte grade, byte[] dataToWrite)
        {
            if (s.AssignedPortRow == -1 || s.AssignedPortCol == -1)
            {
                Console.WriteLine("Помилка: Студент не прив'язаний до жодного порту.");
                return;
            }

            // Записуємо оцінку (одновимірний масив)
            s.AddLabGrade(labNumber, grade);

            // Записуємо дані в порт (двовимірний масив)
            Matrix.WriteToPort(s.AssignedPortRow, s.AssignedPortCol, dataToWrite);

            int portNum = (s.AssignedPortRow * 16) + s.AssignedPortCol + 1;
            Logger.LogOperation("Лабораторна", portNum, "Студент " + s.FullName + " здав лабу №" + labNumber + " на " + grade + " балів.");
            Console.WriteLine("Лабораторну роботу успішно симульовано!");
        }
    }
}