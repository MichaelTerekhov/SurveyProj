using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace ManagerLib
{
    public class UserControlEmulator
    {
        private static List<Survey> surveys = new List<Survey> { };

        public void StartUpMenu()
        {
            Deserialization();
            if (surveys.Count == 0 || surveys == null)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Please contact the survey manager as the repository is empty as it is impossible to get all surveys");
                Console.ResetColor();
                Console.ReadKey();
                Environment.Exit(-1);
            }
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
                        SeeAllSurveys();
                        break;
                    case 2:
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

        private void SeeAllSurveys()
        {
            foreach (var m in surveys)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(m);
                Console.ResetColor();
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nPlease enter id of survey, which you would like to take\n" +
                "If you still change your mind about taking the survey, enter a negative id, for Example:\tID-> -1");
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
                if (parsed < 0)
                    return;
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
                    Console.ResetColor();
                    for (var i = 0; i < m.QuestionContainer.Count; i++)
                    {
                        if (m.QuestionContainer[i].QuestionType == "OPEN")
                        {
                            Console.WriteLine($"{i + 1}.\t{m.QuestionContainer[i].Text}");
                            while (true)
                            {
                                Console.Write("Ans-> ");
                                var input = Console.ReadLine().TrimStart().TrimEnd();
                                if (String.IsNullOrEmpty(input))
                                {
                                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                    Console.WriteLine("Wrong input param");
                                    Console.ResetColor();
                                    continue;
                                }
                                m.QuestionContainer[i].Answers.Add(input);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Saved");
                                Console.ResetColor();
                                break;
                            }
                        }
                        else if (m.QuestionContainer[i].QuestionType == "CLOSED")
                        {
                            Console.WriteLine($"{i + 1}.\t{m.QuestionContainer[i].Text}");
                            if (m.QuestionContainer[i].AnswerOptions.Count == 0)
                            {
                                while (true)
                                {
                                    Console.Write("Ans-> ");
                                    var input = Console.ReadLine().TrimStart().TrimEnd();
                                    if (String.IsNullOrEmpty(input))
                                    {
                                        Console.ForegroundColor = ConsoleColor.DarkRed;
                                        Console.WriteLine("Wrong input param");
                                        Console.ResetColor();
                                        continue;
                                    }
                                    m.QuestionContainer[i].Answers.Add(input);
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("Saved");
                                    Console.ResetColor();
                                    break;
                                }
                                break;
                            }
                            for (var j = 0; j < m.QuestionContainer[i].AnswerOptions.Count; j++)
                            {
                                Console.WriteLine($"{j + 1}\t{m.QuestionContainer[i].AnswerOptions[j]}");
                            }
                            while (true)
                            {
                                Console.Write("Select -> ");
                                bool input = int.TryParse(Console.ReadLine(), out int ans);
                                if (!input)
                                {
                                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                    Console.WriteLine("Unsupported command! Try again");
                                    Console.ResetColor();
                                    continue;
                                }
                                if (ans <= 0 || ans >= m.QuestionContainer[i].AnswerOptions.Count)
                                {
                                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                    Console.WriteLine("Not supported answer!");
                                    Console.ResetColor();
                                    continue;
                                }
                                else 
                                {
                                    m.QuestionContainer[i].Answers.Add(m.QuestionContainer[i].AnswerOptions[ans - 1]);
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("Saved");
                                    break;
                                }
                            }
                        }
                    }
                    break;
                }
            }
            Serialization(surveys);
            Console.ForegroundColor = ConsoleColor.Red;
            if (!surveyFound)
                Console.WriteLine($"\nSorry, but there is no such survey with [{parsed} id] in the database :(\n");
            Console.ResetColor();
        }

        private void MenuList()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n1.\tSee all surveys\n" +
                "2.\tExit\n");
            Console.ResetColor();
        }

        private void MainMenu()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Welcome to collection of surveys.\n" +
                "You are given the opportunity to take a survey\n\n");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("To get started, you need to choose particular survey and than enjoy :)");
            Console.ResetColor();
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
            try
            {
                string pathToSurveys = $@"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName}\Surveys\surveys.json";
                string jsonSurveys = File.ReadAllText(pathToSurveys);
                surveys = JsonSerializer.Deserialize<List<Survey>>(jsonSurveys);
            }
            catch (Exception)
            {
                Console.WriteLine("[Couldnt get data!]");
            }
        }

    }
}
