using System;
using System.Collections.Generic;

namespace ManagerLib
{
    public class Account
    {
        private static List<int> container = new List<int> { }; //Container of all unique id
        private bool IsEmpty => container.Count == 0;
        private bool IsFull => container.Count == 99900000;
        public int Id { get; private set; }
        private string Login { get; set; }
        private string Password { get; set; }

        public Account(string login, string password)
        {
            Random rand = new Random();
            try
            {
                if (IsFull) throw new MethodAccessException(nameof(container.Count));
                if (IsEmpty)
                {
                    Id = rand.Next(100000, 100000000);
                    container.Add(Id);
                }
                else
                {
                    var temp = rand.Next(100000, 100000000);
                    for (var i = 0; i < container.Count; i++)
                    {
                        if (temp == container[i])
                        {
                            temp = rand.Next(100000, 100000000);
                            i = 0;
                        }
                        else continue;
                    }
                    Id = temp;
                    container.Add(temp);
                }
                Password = password;
                Login = login;
            }
            catch (MethodAccessException)
            {
                Console.WriteLine($"Be carefully unique container overflows!");
            }
        }
        public bool IsLoginAndPassword(string login, string password)
        {
            if (Login == login && Password == password)
                return true;
            return false;
        }
    }
}
