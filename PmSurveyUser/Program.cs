using System;
using ManagerLib;

namespace PmSurveyUser
{
    class Program
    {
        static void Main(string[] args)
        {
            var emulator = new UserControlEmulator();
            emulator.StartUpMenu();
        }
    }
}
