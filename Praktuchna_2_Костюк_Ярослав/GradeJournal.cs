using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class GradeJournal
    {
        private Dictionary<string, double> _grades;

        public GradeJournal()
        {
            _grades = new Dictionary<string, double>();
        }

        public void AddGrade(string subject, double grade)
        {
            _grades[subject] = grade;
        }

        public double CalculateAverage()
        {
            if (_grades.Count == 0)
            {
                return 0;
            }
            double sum = 0;
            foreach (double grade in _grades.Values)
            {
                sum += grade;
            }
            return Math.Round(sum / _grades.Count, 2);
        }
    }
}
