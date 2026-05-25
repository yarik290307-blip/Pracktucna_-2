using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleApp1.Pracktuchna__1_Костюк_Ярослав
{
    public class StudentGroup
    {
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
    }
}
