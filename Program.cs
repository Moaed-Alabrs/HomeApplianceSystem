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
                Console.WriteLine("1. Add refrigerator");
                Console.WriteLine("2. Show all appliances");
                Console.WriteLine("3. Show summary");
                Console.WriteLine("4. Update appliance status");
                Console.WriteLine("5. Exit");
                Console.Write("Your choice: ");

                string? choice = Console.ReadLine();

                switch (choice?.Trim())
                {
                    case "1":
                        AddRefrigerator();
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

        private static void AddRefrigerator()
        {
            try
            {
                Console.Write("Serial number: ");
                string serialNumber = Console.ReadLine() ?? string.Empty;

                Console.Write("Brand: ");
                string brand = Console.ReadLine() ?? string.Empty;

                Console.Write("Model: ");
                string model = Console.ReadLine() ?? string.Empty;

                Console.Write("Status: ");
                string status = Console.ReadLine() ?? "Under Maintenance";

                Console.Write("Parts cost: ");
                decimal partsCost = decimal.Parse(Console.ReadLine() ?? "0");

                Console.Write("Needs gas refill? (Y/N): ");
                bool needsGasRefill = ReadYesNo();

                Appliance appliance = new Refrigerator(
                    serialNumber,
                    brand,
                    model,
                    status,
                    partsCost,
                    needsGasRefill);

                appliances.Add(appliance);

                Console.WriteLine(
                    $"Appliance {appliance.SerialNumber} was added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating appliance: {ex.Message}");
            }
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

        private static bool ReadYesNo()
        {
            while (true)
            {
                string? answer = Console.ReadLine();

                if (answer != null &&
                    (answer.Trim().Equals("Y", StringComparison.OrdinalIgnoreCase) ||
                     answer.Trim().Equals("N", StringComparison.OrdinalIgnoreCase)))
                {
                    return answer.Trim().Equals("Y", StringComparison.OrdinalIgnoreCase);
                }

                Console.Write("Please enter Y or N: ");
            }
        }
    }
}