using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class PortLogger
    {
        private StringBuilder _logBuilder;

        public PortLogger()
        {
            _logBuilder = new StringBuilder();
            _logBuilder.AppendLine("--- Ініціалізація журналу логів ---");
        }

        public void LogOperation(string operation, int portNumber, string details)
        {
            StringBuilder entry = new StringBuilder();

            entry.Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            entry.Append(" | ОПЕРАЦІЯ: ");
            entry.Append(operation);
            entry.Append(" | ПОРТ: ");
            entry.Append(portNumber);
            entry.Append(" | ДЕТАЛІ: ");
            entry.Append(details);

            _logBuilder.AppendLine(entry.ToString());
        }

        public string GetFullLog()
        {
            return _logBuilder.ToString();
        }

        public void SaveLogToFile()
        {
            string filePath = "port_logs.txt";
            File.WriteAllText(filePath, _logBuilder.ToString());
            Console.WriteLine("Логи успішно збережено у файл " + filePath);
        }
    }
}