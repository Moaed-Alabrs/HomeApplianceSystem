using System;
using System.Collections.Generic;

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

                switch (choice?.Trim())
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
                        Console.WriteLine("Status update will be added later.");
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

        private static void AddAppliance()
        {
            Console.WriteLine("Appliance types will be added in the next stage.");
        }

        private static void ShowAllAppliances()
        {
            if (appliances.Count == 0)
            {
                Console.WriteLine("No appliances in the system.");
                return;
            }

            foreach (Appliance appliance in appliances)
            {
                Console.WriteLine("---------------------------");
                appliance.DisplayInfo();
                Console.WriteLine($"Estimated repair cost: {appliance.CalculateCost():C}");
            }
        }

        private static void ShowSummary()
        {
            Console.WriteLine("\nSummary:");
            Console.WriteLine($"Total appliances: {Appliance.TotalAppliances}");
            Console.WriteLine($"Repaired: {Appliance.RepairedCount}");
            Console.WriteLine($"Under maintenance: {Appliance.UnderMaintenanceCount}");
            Console.WriteLine($"Total repair cost: {Appliance.TotalRepairCost:C}");
        }
    }
}