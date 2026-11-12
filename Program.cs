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

        private static void AddAppliance()
        {
            Console.WriteLine("\nSelect appliance type:");
            Console.WriteLine("1. Refrigerator");
            Console.WriteLine("2. Washing Machine");
            Console.WriteLine("3. Air Conditioner");
            Console.Write("Type: ");

            string? typeInput = Console.ReadLine();

            if (!int.TryParse(typeInput, out int typeNumber))
            {
                Console.WriteLine("Invalid appliance type.");
                return;
            }

            try
            {
                Console.Write("Serial number (REF###, WAS###, AIR###): ");
                string serialNumber = Console.ReadLine() ?? string.Empty;

                Console.Write("Brand: ");
                string brand = Console.ReadLine() ?? string.Empty;

                Console.Write("Model: ");
                string model = Console.ReadLine() ?? string.Empty;

                Console.Write("Status (Under Maintenance / Repaired / Available): ");
                string status = Console.ReadLine() ?? "Under Maintenance";

                Console.Write("Parts cost: ");
                decimal partsCost = decimal.Parse(Console.ReadLine() ?? "0");

                Appliance appliance;

                switch (typeNumber)
                {
                    case 1:
                        Console.Write("Needs gas refill? (Y/N): ");
                        bool needsGasRefill = ReadYesNo();

                        appliance = new Refrigerator(
                            serialNumber,
                            brand,
                            model,
                            status,
                            partsCost,
                            needsGasRefill);

                        break;

                    case 2:
                        Console.Write("Motor needs repair? (Y/N): ");
                        bool motorNeedsRepair = ReadYesNo();

                        appliance = new WashingMachine(
                            serialNumber,
                            brand,
                            model,
                            status,
                            partsCost,
                            motorNeedsRepair);

                        break;

                    case 3:
                        Console.Write("Needs gas refill? (Y/N): ");
                        bool acNeedsGasRefill = ReadYesNo();

                        appliance = new AirConditioner(
                            serialNumber,
                            brand,
                            model,
                            status,
                            partsCost,
                            acNeedsGasRefill);

                        break;

                    default:
                        Console.WriteLine("Invalid appliance type.");
                        return;
                }

                appliances.Add(appliance);
                appliance.StatusChanged += OnStatusChanged;

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
                Console.WriteLine(
                    $"Estimated repair cost: {appliance.CalculateCost():C}");
            }
        }

        private static void ShowSummary()
        {
            Console.WriteLine("\nSummary:");
            Console.WriteLine($"Total appliances: {Appliance.TotalAppliances}");
            Console.WriteLine($"Repaired: {Appliance.RepairedCount}");
            Console.WriteLine(
                $"Under maintenance: {Appliance.UnderMaintenanceCount}");
            Console.WriteLine($"Total repair cost: {Appliance.TotalRepairCost:C}");
            Console.WriteLine(
                $"Available appliances: {Appliance.CountByStatus("Available")}");
        }

        private static void UpdateStatus()
        {
            if (appliances.Count == 0)
            {
                Console.WriteLine("No appliances available to update.");
                return;
            }

            Console.WriteLine("Choose an appliance by serial number:");

            foreach (Appliance appliance in appliances)
            {
                Console.WriteLine($"- {appliance.SerialNumber}");
            }

            Console.Write("Serial number: ");
            string serial = Console.ReadLine() ?? string.Empty;

            Appliance? target = appliances.FirstOrDefault(
                appliance => appliance.SerialNumber == serial);

            if (target == null)
            {
                Console.WriteLine("Appliance not found.");
                return;
            }

            Console.Write(
                "New status (Under Maintenance / Repaired / Available): ");

            string newStatus = Console.ReadLine() ?? string.Empty;

            try
            {
                target.Status = newStatus;

                Console.WriteLine(
                    $"Status updated for {target.SerialNumber}.");

                if (target.Status == "Repaired")
                {
                    Console.WriteLine(
                        $"[EVENT] {target.SerialNumber} has been repaired " +
                        "and is ready for delivery.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private static void OnStatusChanged(
            Appliance appliance,
            string oldStatus,
            string newStatus)
        {
            Console.WriteLine(
                $"[EVENT] {appliance.SerialNumber}: " +
                $"{oldStatus} -> {newStatus}");

            if (newStatus == "Available")
            {
                Console.WriteLine(
                    $"[NOTICE] {appliance.SerialNumber} is ready " +
                    "for customer collection.");
            }
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
                    return answer.Trim().Equals(
                        "Y",
                        StringComparison.OrdinalIgnoreCase);
                }

                Console.Write("Please enter Y or N: ");
            }
        }
    }
}