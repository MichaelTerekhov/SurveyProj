using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace ManagerLib
{
    public class ManagerControlEmulator
    {
        private static List<Account> container = new List<Account> { };
        private Account activeAcc;
        private static List<Survey> surveys = new List<Survey> { };

        public void StartUpMenu()
        {
            Deserialization();
            MainMenu();
            while (true)
            {
                MenuList();
                Console.Write("Select -> ");
                bool input = int.TryParse(Console.ReadLine(), out int MainMenuChoose);
                if (!input)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Unsupported command! Try again");
                    Console.ResetColor();
                    continue;
                }
                switch (MainMenuChoose)
                {
                    case 1:
                        RegistrationForm();
                        break;
                    case 2:
                        LoginForm();
                        break;
                    case 3:
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine("See you next time!");
                        Console.ResetColor();
                        return;
                    default:
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Unsupported command! Try again");
                        Console.ResetColor();
                        continue;
                }
            }
        }

        private void LoginForm()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nWelcome to the login form.\n" +
                "Please enter all required login details(your login / username, password)");
            Console.ResetColor();
            string login = "";
            string password = "";
            bool logined = true;
            while (logined)
            {
                Console.ResetColor();
                Console.Write("Login(your username) -> ");
                login = Console.ReadLine().Replace(" ", "");
                if (String.IsNullOrEmpty(login))
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Unsupported command! Try again");
                    Console.ResetColor();
                    continue;
                }
                while (true)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nPlease enter your password without empty entries!\n" +
                        "If you accidentally entered the wrong login,\n" +
                        "just write 'newlogin' in the password field\tExample: Password -> newlogin\n");
                    Console.ResetColor();
                    Console.Write("Password -> ");
                    password = Console.ReadLine().Replace(" ", "");
                    if (String.IsNullOrEmpty(password))
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Unsupported command! Try again");
                        Console.ResetColor();
                        continue;
                    }
                    if (password == "newlogin")
                    {
                        break;
                    }
                    else
                    {
                        bool accountIsValid = false;
                        foreach (var m in container)
                        {
                            if (m.IsLoginAndPassword(login, password))
                            {
                                accountIsValid = true;
                            }
                        }
                        if (!accountIsValid)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Account not found!");
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("\n1.\tIf you are not registered, please register\tEnter: 1\n" +
                                "2.\tIf you want to go through the login procedure again\tEnter: 2\n" +
                                "3.\tIf you want to leave this form \t Enter: 3\n");
                            Console.ResetColor();
                            while (true)
                            {
                                Console.Write("Select -> ");
                                bool input = int.TryParse(Console.ReadLine(), out int MenuChoose);
                                if (!input)
                                {
                                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                    Console.WriteLine("Unsupported command! Try again");
                                    Console.ResetColor();
                                    continue;
                                }
                                switch (MenuChoose)
                                {
                                    case 1:
                                        RegistrationForm();
                                        return;
                                    case 2:
                                        LoginForm();
                                        //Try to add surveyoperator
                                        return;
                                    case 3:
                                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                                        Console.WriteLine("See you next time!");
                                        Console.ResetColor();
                                        return;
                                    default:
                                        Console.ForegroundColor = ConsoleColor.DarkRed;
                                        Console.WriteLine("Unsupported command! Try again");
                                        Console.ResetColor();
                                        continue;
                                }
                            }
                        }
                        else
                        {
                            SurveyOperator();
                            return;
                        }
                    }
                }
            }
        }

        private void RegistrationForm()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nWelcome new user!" +
                "To get started with the platform, please, come up with a username and password.");
            string login = "";
            string password = "";
            while (true)
            {

                bool passwordConfirmed = true;
                Console.ResetColor();
                Console.Write("Login(your username) -> ");
                login = Console.ReadLine().Replace(" ", "");
                if (String.IsNullOrEmpty(login))
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Unsupported command! Try again");
                    Console.ResetColor();
                    continue;
                }
                while (passwordConfirmed)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nPlease enter your password without empty entries!");
                    Console.ResetColor();
                    Console.Write("Password -> ");
                    password = Console.ReadLine().Replace(" ", "");
                    if (String.IsNullOrEmpty(password))
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Unsupported command! Try again");
                        Console.ResetColor();
                        continue;
                    }
                    while (true)
                    {
                        Console.Write("Confirm your password -> ");
                        string passwordDupliceted = Console.ReadLine().Replace(" ", "");
                        if (String.IsNullOrEmpty(passwordDupliceted))
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("Unsupported command! Try again");
                            Console.ResetColor();
                            continue;
                        }
                        if (passwordDupliceted == "nomatch")
                            break;
                        if (password != passwordDupliceted)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("Passwords do not match");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("\n If you want to come up with a different password,\n" +
                                "just enter 'nomatch' in lower case\tExample:\tConfirm your password -> nomatch");
                            Console.ResetColor();
                            continue;
                        }
                        else
                        {
                            passwordConfirmed = false;
                            break;
                        }
                    }
                }
                break;
            }
            //testing part(Not finished)
            Console.WriteLine($"Your username/login: {login} password: {password} ");
            activeAcc = new Account(login, password);
            container.Add(activeAcc);
        }

        public void SurveyOperator()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n Welcome to survey management.\n" +
                "You are given the ability to create, edit or delete a specific survey.\n\n" +
                "You can also see statistics for a specific survey (How users answered)");
            Console.ResetColor();
            while (true)
            {
                SurveyMenuManagerList();
                Console.Write("Select -> ");
                bool input = int.TryParse(Console.ReadLine(), out int SurveyMenuChoose);
                if (!input)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Unsupported command! Try again");
                    Console.ResetColor();
                    continue;
                }
                switch (SurveyMenuChoose)
                {
                    case 1:
                        CreateSurvey();
                        break;
                    case 2:
                        EditSurvey();
                        break;
                    case 3:
                        DeleteSurvey();
                        break;
                    case 4:
                        // Stats();
                        continue;
                    case 5:
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine("See you next time!");
                        Console.ResetColor();
                        return;
                    default:
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Unsupported command! Try again");
                        Console.ResetColor();
                        continue;
                }
            }
        }


        private void DeleteSurvey()
        {
            Console.WriteLine("\nPlease enter id of note, which you would like to DELETE.");
            int parsed;
            while (true)
            {
                Console.Write("ID-> ");
                bool input = int.TryParse(Console.ReadLine(), out parsed);
                if (!input)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Wrong input param");
                    Console.ResetColor();
                    continue;
                }
                break;
            }
            bool surveyFound = false;
            foreach (var m in surveys)
            {
                if (m.Id == parsed)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    surveyFound = true;
                    Console.WriteLine("\n" + m.ToString() + "\n");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You seriously want to permanently DELETE this survey. ");
                    Console.ResetColor();
                    string solution;
                    while (true)
                    {
                        Console.WriteLine("Please write yes / no or y / n");
                        solution = Console.ReadLine().Replace(" ", "").ToLower();
                        if (String.IsNullOrEmpty(solution))
                        {
                            Console.WriteLine("Unknown operation!");
                            continue;
                        }
                        if (solution == "yes" || solution == "no" || solution == "y" || solution == "n")
                            break;
                        else
                        {
                            Console.WriteLine("Unknown operation!");
                            continue;
                        }
                    }
                    if (solution == "yes" || solution == "y")
                    {
                        surveys.Remove(m);
                        Serialization(surveys);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("The survey has been deleted");
                        Console.ResetColor();
                    }
                    return;
                }
            }
            Console.ForegroundColor = ConsoleColor.Red;
            if (!surveyFound)
                Console.WriteLine($"\nSorry, but there is no such note with [{parsed} id] in the database :(\n");
            Console.ResetColor();
        }

        private void EditSurvey()
        {
            Deserialization();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Welcome to the survey builder.\n" +
                "You can create a poll from 0 and fill it with questions.\n\n");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("First, come up with a survey topic");
            Console.ResetColor();
            int id = 0;
            while (true)
            {
                Console.Write("ID-> ");
                bool input = int.TryParse(Console.ReadLine(), out id);
                if (!input)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Wrong input param");
                    Console.ResetColor();
                    continue;
                }
                break;
            }

            bool surveyFound = false;
            foreach (var m in surveys)
            {
                if (m.Id == id)
                {
                    surveyFound = true;
                    while (true)
                    {
                        Serialization(surveys);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("1.\tChange subject\n" +
                            "2.\tWork with questions in survey\n" +
                            "3\tExit\n)");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("Just enter a number. Example: Select-> 1");
                        Console.ResetColor();
                        Console.Write("Select -> ");
                        bool input = int.TryParse(Console.ReadLine(), out int SurveyMenuChoose);
                        if (!input)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("Unsupported command! Try again");
                            Console.ResetColor();
                            continue;
                        }

                        switch (SurveyMenuChoose)
                        {
                            case 1:
                                m.Subject = ChangeSubject();
                                m.Date = DateTime.UtcNow;
                                Serialization(surveys);
                                break;
                            case 2:
                                m.QuestionContainer = WorkWithQuestions(m);
                                m.Date = DateTime.UtcNow;
                                string findKind = "";
                                int kindCounter = 0;
                                foreach (var n in m.QuestionContainer)
                                    if (n.QuestionType == "OPEN")
                                        kindCounter++;
                                if (kindCounter == m.QuestionContainer.Count)
                                    findKind = "OPN";
                                else if (kindCounter == 0)
                                    findKind = "CLSD";
                                else
                                    findKind = "Multivariant";
                                m.Kind = findKind;
                                Serialization(surveys);
                                break;
                            case 3:
                                Serialization(surveys);
                                return;
                            default:
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("Unsupported command! Try again");
                                Console.ResetColor();
                                continue;
                        }
                    }
                }
            }
            Console.ForegroundColor = ConsoleColor.Red;
            if (!surveyFound)
                Console.WriteLine($"\nSorry, but there is no such survey with [{id} id] in the database :(\n");
            Console.ResetColor();
            Serialization(surveys);
        }

        private List<Question> WorkWithQuestions(Survey survey)
        {
            List<Question> res = survey.QuestionContainer;
            Console.ForegroundColor = ConsoleColor.Cyan;
            for (var i = 0; i < res.Count; i++)
            {
                Console.WriteLine($"{i + 1 }. {res[i]}");
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Select number of question Which one you want to work with\n" +
                $"To stop editing, just write 'stop'\tExample: Select -> stop");
            Console.ResetColor();
            while (true)
            {
                Console.Write("Select -> ");
                var line = Console.ReadLine();
                if (line == "stop")
                    break;
                bool input = int.TryParse(line, out int questionNumber);
                if (!input)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Unsupported command! Try again");
                    Console.ResetColor();
                    continue;
                }
                if (questionNumber <= 0 || questionNumber > res.Count)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Unsupported command! Try again");
                    Console.ResetColor();
                    continue;
                }
                else
                {
                    while (true)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Please edit type of question! (Open or Closed)\tExample: Type-> open\n\n" +
                            "If you wouldnt like to edit, just leave empty string\tExample: Type-> ");
                        Console.ResetColor();
                        Console.Write("Type -> ");
                        var type = Console.ReadLine().Replace(" ", "").ToUpper();
                        if (String.IsNullOrEmpty(type))
                        {
                            break;
                        }
                        else if (type == "OPEN" || type == "CLOSED")
                        {
                            if (type == "OPEN" && res[questionNumber - 1].QuestionType == "CLOSED")
                            {
                                res[questionNumber - 1].QuestionType = type;
                                res[questionNumber - 1].answerOptions = null;
                                break;
                            }
                            else if (type == "CLOSED" && res[questionNumber - 1].QuestionType == "OPEN")
                            {
                                res[questionNumber - 1].QuestionType = type;
                                res[questionNumber - 1].answerOptions = InputAnswerOptions();
                                break;
                            }
                            else if (res[questionNumber - 1].QuestionType == "CLOSED")
                            {
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                if (res[questionNumber - 1].answerOptions.Count == 0)
                                {
                                    Console.WriteLine("List of answer options is empty! Try to add some.\n" +
                                        "If its enough options, just write 'enough'\tExample: Option-> enough");
                                    while (true)
                                    {
                                        Console.Write("Option-> ");
                                        var newOption = Console.ReadLine().TrimStart().TrimEnd();
                                        if (String.IsNullOrEmpty(newOption))
                                            continue;
                                        if (newOption == "enough")
                                            break;
                                        else
                                        {
                                            res[questionNumber - 1].answerOptions.Add(newOption);
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            Console.WriteLine("Complete");
                                            Console.ResetColor();
                                        }
                                    }
                                    return res;
                                }
                                Console.WriteLine("Try to input some new anwer options!\tExample: Answer option-> 1.\n" +
                                        "If you wouldnt like to add, just leave empty string or write 'enough'\tExample: Answer option-> ");
                                while (true)
                                {
                                    Console.Write("Option-> ");
                                    var newOption = Console.ReadLine().TrimStart().TrimEnd();
                                    if (String.IsNullOrEmpty(newOption))
                                        break;
                                    if (newOption == "enough")
                                        break;
                                    else
                                    {
                                        res[questionNumber - 1].answerOptions.Add(newOption);
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.WriteLine("Complete");
                                        Console.ResetColor();
                                    }
                                }

                                while (true)
                                {
                                    for (var i = 0; i < res[questionNumber - 1].answerOptions.Count; i++)
                                        Console.WriteLine($"{i + 1}. " + res[questionNumber - 1].answerOptions[i]);
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine("Please edit answer options!\tExample: Answer option-> 1.\n\n" +
                                        "If you wouldnt like to edit, just leave empty string\tExample: Answer option-> ");
                                    Console.ResetColor();
                                    Console.Write("Answer option -> ");

                                    bool inputOption = int.TryParse(Console.ReadLine(), out int optionNumber);
                                    if (!inputOption)
                                    {
                                        return res;
                                    }
                                    if (optionNumber <= 0 || optionNumber > res[questionNumber - 1].answerOptions.Count)
                                    {
                                        Console.ForegroundColor = ConsoleColor.DarkRed;
                                        Console.WriteLine("Unsupported command! Try again");
                                        Console.ResetColor();
                                        continue;
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.Blue;
                                        Console.WriteLine(res[questionNumber - 1].answerOptions[optionNumber - 1]);
                                        Console.WriteLine("Which action would you like to perform?");
                                        while (true)
                                        {
                                            Console.WriteLine("1\tMake new option\n" +
                                                "2\tEdit substring\n" +
                                                "3\tRemove option\n" +
                                                "4\tExit");
                                            Console.Write("Select -> ");
                                            bool operation = int.TryParse(Console.ReadLine(), out int MainMenuChoose);
                                            if (!operation)
                                            {
                                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                                Console.WriteLine("Unsupported command! Try again");
                                                Console.ResetColor();
                                                continue;
                                            }
                                            switch (MainMenuChoose)
                                            {
                                                case 1:
                                                    while (true)
                                                    {
                                                        var newOption = Console.ReadLine().TrimStart().TrimEnd();
                                                        if (String.IsNullOrEmpty(newOption))
                                                            continue;
                                                        else
                                                        {
                                                            res[questionNumber - 1].answerOptions[optionNumber - 1] = newOption;
                                                            break;
                                                        }
                                                    }
                                                    break;
                                                case 2:
                                                    while (true)
                                                    {
                                                        var newOption = Console.ReadLine().TrimStart().TrimEnd();
                                                        if (String.IsNullOrEmpty(newOption))
                                                            continue;
                                                        else
                                                        {
                                                            res[questionNumber - 1].answerOptions[optionNumber - 1] += newOption;
                                                            break;
                                                        }
                                                    }
                                                    break;
                                                case 3:
                                                    res[questionNumber - 1].answerOptions.Remove(res[questionNumber - 1].answerOptions[optionNumber - 1]);
                                                    break;
                                                case 4:
                                                    break;
                                                default:
                                                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                                    Console.WriteLine("Unsupported command! Try again");
                                                    Console.ResetColor();
                                                    continue;
                                            }
                                            break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                res[questionNumber - 1].QuestionType = type;
                                break;
                            }
                        }
                    }
                }
                break;
            }

            return res;
        }

        private string ChangeSubject()
        {
            string res = "";
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("1.\tSports\n" +
                    "2.\tSocial survey\n" +
                    "3.\tEducational survey\n" +
                    "4.\tCustom(you need to write\n\n)");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Just enter a number. Example: Select-> 1");
                Console.ResetColor();
                Console.Write("Select -> ");
                bool input = int.TryParse(Console.ReadLine(), out int SurveyMenuChoose);
                if (!input)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Unsupported command! Try again");
                    Console.ResetColor();
                    continue;
                }
                switch (SurveyMenuChoose)
                {
                    case 1:
                        res = "Sports";
                        break;
                    case 2:
                        res = "Social survey";
                        break;
                    case 3:
                        res = "Educational survey";
                        break;
                    case 4:
                        Console.Write("Custom subject -> ");
                        var subject = Console.ReadLine().TrimStart().TrimEnd();
                        if (String.IsNullOrEmpty(subject))
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("Unsupported command! Try again");
                            Console.ResetColor();
                            continue;
                        }
                        res = subject;
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Unsupported command! Try again");
                        Console.ResetColor();
                        continue;
                }
                break;
            }
            return res;
        }

        private void CreateSurvey()
        {
            var createdSurvey = new Survey();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Welcome to the survey builder.\n" +
                "You can create a poll from 0 and fill it with questions.\n\n");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("First, come up with a survey topic");
            Console.ResetColor();

            createdSurvey.Subject = ChangeSubject();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nSecond, try to come up with questions!\n");
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Please enter type of question! (Open or Closed)\tExample: Type-> open\n\n" +
                    "If you wouldnt like to add more questions write 'exit'\tExample: Type-> exit");
                Console.ResetColor();
                Console.Write("Type -> ");
                var type = Console.ReadLine().Replace(" ", "").ToUpper();
                if (String.IsNullOrEmpty(type))
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Unsupported type! Try again");
                    Console.ResetColor();
                    continue;
                }
                if (type == "EXIT")
                {
                    if (createdSurvey.QuestionContainer.Count == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Sorry, but you cant create survey with 0 questions");
                        Console.ResetColor();
                        createdSurvey = null;
                        return;
                    }
                    break;
                }
                else if (type == "OPEN")
                {
                    createdSurvey.QuestionContainer.Add(new Question { QuestionType = type, Text = InputQuestionText(), answerOptions = null });
                    continue;
                }
                else if (type == "CLOSED")
                {
                    createdSurvey.QuestionContainer.Add(new Question { QuestionType = type, Text = InputQuestionText(), answerOptions = InputAnswerOptions() });
                    continue;
                }
            }
            string findKind = "";
            int kindCounter = 0;
            foreach (var m in createdSurvey.QuestionContainer)
                if (m.QuestionType == "OPEN")
                    kindCounter++;
            if (kindCounter == createdSurvey.QuestionContainer.Count)
                findKind = "OPN";
            else if (kindCounter == 0)
                findKind = "CLSD";
            else
                findKind = "Multivariant";

            var maxId = 0;
            foreach (var m in surveys)
                if (m.Id >= maxId)
                    maxId = m.Id;

            createdSurvey.Kind = findKind;
            createdSurvey.Id = maxId + 1;
            createdSurvey.AuthorId = activeAcc.Id;
            createdSurvey.Date = DateTime.UtcNow;

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"Survey created!");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"{createdSurvey}");
            foreach (var m in createdSurvey.QuestionContainer)
                Console.WriteLine(m);
            surveys.Add(createdSurvey);
            Serialization(surveys);
        }
        private static void Serialization(List<Survey> sr)
        {
            string pathToSurveys = $@"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName}\Surveys\surveys.json";
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            string jsonOutput;
            jsonOutput = JsonSerializer.Serialize(sr, options);
            File.WriteAllText(pathToSurveys, jsonOutput);
        }

        private static void Deserialization()
        {
            string pathToSurveys = $@"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName}\Surveys\surveys.json";
            string jsonSurveys = File.ReadAllText(pathToSurveys);
            surveys = JsonSerializer.Deserialize<List<Survey>>(jsonSurveys);
        }

        private string InputQuestionText()
        {
            string s = "";

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nPlease write body of your question");
                Console.ResetColor();
                Console.Write("Composition of the question -> ");
                var body = Console.ReadLine().TrimStart().TrimEnd();
                if (String.IsNullOrEmpty(body))
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Try again");
                    Console.ResetColor();
                    continue;
                }
                else
                {
                    s = body;
                    break;
                }
            }
            return s;
        }

        private List<string> InputAnswerOptions()
        {
            List<string> res = new List<string>();
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nPlease write answer options()\n\n");

                Console.ResetColor();
                Console.Write("Answer option -> ");
                var body = Console.ReadLine();
                if (String.IsNullOrEmpty(body))
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Unsupported! Try again");
                    Console.ResetColor();
                    continue;
                }
                if (body == "enough")
                {
                    if (res.Count == 0)
                        res.Add("1. Choose me!");
                    break;
                }
                else
                {
                    res.Add(body);
                    Console.WriteLine("If you think that there are enough answers, then write 'enough'\tExample: Answer option -> enough");
                    continue;
                }
            }
            return res;
        }
        private void SurveyMenuManagerList()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n1.\tCreate Survey\n" +
                "2.\tEdit Survey\n" +
                "3.\tDelete Survey\n" +
                "4.\tSee stats\n" +
                "5.\tExit survey manager\n");
            Console.ResetColor();
        }

        private void MainMenu()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Welcome to Survey Manager.\n" +
                "You are given the opportunity to manage(create an edit system of the public survey)\n\n");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("To get started, you need to LOG IN to the system, or REGISTER");
            Console.ResetColor();
        }
        private void MenuList()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n1.\tRegister\n" +
                "2.\tLogin\n" +
                "3.\tExit\n");
            Console.ResetColor();
        }
    }
}
