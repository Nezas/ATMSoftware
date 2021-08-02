using System;

namespace ATMSoftware.Writer
{
    public class ConsoleWriter : IConsoleWriter
    {
        public void Write(string message)
        {
            Console.Write(message);
        }

        public void Clear()
        {
            Console.Clear();
        }
    }
}
