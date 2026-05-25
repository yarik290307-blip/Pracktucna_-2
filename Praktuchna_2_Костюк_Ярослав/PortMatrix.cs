using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class PortMatrix
    {
        // Двовимірний масив портів
        private Port[,] _matrix;

        private const int Rows = 16;
        private const int Cols = 16;

        public PortMatrix()
        {
            // Ініціалізація матриці 16x16
            _matrix = new Port[Rows, Cols];

            // Заповнення матриці об'єктами Port
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    // Генеруємо унікальний номер порту
                    int portNum = (i * Cols) + j + 1;
                    _matrix[i, j] = new Port(portNum, "Device_" + i + "_" + j);
                }
            }
        }

        private bool IsValidIndex(int row, int col)
        {
            if (row >= 0 && row < Rows && col >= 0 && col < Cols)
            {
                return true;
            }
            Console.WriteLine("Помилка: Невірні координати порту.");
            return false;
        }

        public void OpenPort(int row, int col)
        {
            if (IsValidIndex(row, col))
            {
                _matrix[row, col].Open();
            }
        }

        public void WriteToPort(int row, int col, byte[] data)
        {
            if (IsValidIndex(row, col))
            {
                _matrix[row, col].WriteData(data);
            }
        }

        public byte[] ReadFromPort(int row, int col)
        {
            if (IsValidIndex(row, col))
            {
                return _matrix[row, col].DataBuffer;
            }
            return new byte[0]; // Повертаємо порожній масив у разі помилки
        }

        public void ScanMatrix()
        {
            int openCount = 0;

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    if (_matrix[i, j].IsOpen)
                    {
                        openCount++;
                        Console.WriteLine("Порт [" + i + "," + j + "] (" + _matrix[i, j].DeviceName + ") ВІДКРИТИЙ.");
                    }
                }
            }

            Console.WriteLine("Загалом відкритих портів: " + openCount);
        }
    }
}
