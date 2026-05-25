using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Port
    {
        public int PortNumber { get; }
        public byte[] DataBuffer { get; } 
        public bool IsOpen { get; private set; }
        public string DeviceName { get; }

        public Port(int portNumber, string deviceName)
        {
            PortNumber = portNumber;
            DeviceName = deviceName;
            DataBuffer = new byte[64];
            IsOpen = false;
        }

        public void Open()
        {
            IsOpen = true;
        }

        public void Close()
        {
            IsOpen = false;
            for (int i = 0; i < DataBuffer.Length; i++)
            {
                DataBuffer[i] = 0;
            }
        }

        public void WriteData(byte[] data)
        {
            if (!IsOpen)
            {
                Console.WriteLine("Помилка: Порт закритий. Запис неможливий.");
                return;
            }

            if (data.Length > DataBuffer.Length)
            {
                Console.WriteLine("Помилка: Об'єм даних перевищує розмір буфера порту.");
                return;
            }

            for (int i = 0; i < data.Length; i++)
            {
                DataBuffer[i] = data[i];
            }
        }
    }
}