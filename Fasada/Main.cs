
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Fasada
{

    interface IUserService
    {
        void CreateUser(string email);
        void RemoveUser(string email);
        int UserCount();
    }

    static class EmailNotification
    {
        public static void SendEmail(string to, string subject)
        {
            Console.WriteLine(subject + " " + to);
        }
    }

    class UserRepository
    {
        private readonly List<string> users = new List<string>
        {
            "john.doe@gmail.com", "sylvester.stallone@gmail.com"
        };

        public int Licz()
        {
            return users.Count();
        }

        public bool IsEmailFree(string email)
        {
            return !users.Contains(email);
        }

        public void AddUser(string email)
        {
            users.Add(email);
        }

        public void RemoveUser(string email)
        {
            users.Remove(email);
        }
    }

    static class Validators
    {
        public static bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase);
        }
    }

    class UserService : IUserService
    {
        private readonly UserRepository userRepository = new UserRepository();
        public void CreateUser(string email)
        {
            if (!Validators.IsValidEmail(email))
            {
                throw new ArgumentException("Błędny email");
            }
            if (!userRepository.IsEmailFree(email)) throw new ArgumentException("Email zajęty");
            userRepository.AddUser(email);
            EmailNotification.SendEmail(email, "Welcome to our service");
        }

        public void RemoveUser(string email)
        {
            if (userRepository.IsEmailFree(email)) throw new ArgumentException("Email nie istnieje w bazie");
            userRepository.RemoveUser(email);
            EmailNotification.SendEmail(email, "Goodbye");
        }

        public int UserCount()
        {
            return userRepository.Licz();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            IUserService userService = new UserService();
            Console.WriteLine($"Aktualna liczba adresów: {userService.UserCount()}");
            userService.CreateUser("someemail@gmail.com");
            Console.WriteLine($"Aktualna liczba adresów: {userService.UserCount()}");
            userService.RemoveUser("john.doe@gmail.com");
            Console.WriteLine($"Aktualna liczba adresów: {userService.UserCount()}");
        }
    }
}