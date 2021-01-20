using System;
using ManagerLib;

namespace PmSurveyManager
{
    class Program
    {
        static void Main(string[] args)
        {
            var emulator = new ManagerControlEmulator();
            emulator.StartUpMenu();
        }
    }
}
