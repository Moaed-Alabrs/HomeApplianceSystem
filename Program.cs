using System;
using System.Collections.Generic;
using System.Linq;

namespace HomeApplianceSystem
{
    public class Program
    {
        private static readonly List<Appliance> appliances = new();

        public static void Main(string[] args)
        {
            Console.WriteLine("========================================");
            Console.WriteLine(" Home Appliance Maintenance Center");
            Console.WriteLine("========================================");

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. Add appliance");
                Console.WriteLine("2. Show all appliances");
                Console.WriteLine("3. Show summary");
                Console.WriteLine("4. Update appliance status");
                Console.WriteLine("5. Exit");
                Console.Write("Your choice: ");

                string? choice = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(choice))
                {
                    Console.WriteLine("Please enter a valid option.");
                    continue;
                }

                switch (choice.Trim())
                {
                    case "1":
                        AddAppliance();
                        break;
                    case "2":
                        ShowAllAppliances();
                        break;
                    case "3":
                        ShowSummary();
                        break;
                    case "4":
                        UpdateStatus();
                        break;
                    case "5":
                        Console.WriteLine("Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

