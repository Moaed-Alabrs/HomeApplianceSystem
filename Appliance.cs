using System;
using System.Collections.Generic;
using System.Linq;

namespace HomeApplianceSystem
{
    public delegate void ApplianceStatusChangedHandler(Appliance appliance, string oldStatus, string newStatus);

    public abstract class Appliance : IAppliance
    {
        private static readonly List<Appliance> registry = new();

        public static int TotalAppliances => registry.Count;

        public static int RepairedCount => registry.Count(a => a.Status == "Repaired");

        public static int UnderMaintenanceCount => registry.Count(a => a.Status == "Under Maintenance");

        public static decimal TotalRepairCost => registry.Sum(a => a.CalculateCost());

        public static int CountByStatus(string status)
        {
            if (!ApplianceValidator.IsValidStatus(status))
            {
                return 0;
            }

            return registry.Count(a => a.Status == status);
        }

        public event ApplianceStatusChangedHandler? StatusChanged;

        private readonly string serialNumber;
        private readonly string brand;
        private readonly string model;
        private string status;

        public string SerialNumber => serialNumber;
        public string Brand => brand;
        public string Model => model;

        public string Status
        {
            get => status;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Status cannot be empty.");
                }

                string normalizedValue = ApplianceValidator.GetStatusLabel(value);
                if (!ApplianceValidator.IsValidStatus(normalizedValue))
                {
                    throw new ArgumentException("Invalid appliance status.");
                }

                if (status != normalizedValue)
                {
                    string oldStatus = status;
                    status = normalizedValue;
                    StatusChanged?.Invoke(this, oldStatus, status);
                }
            }
        }

        protected Appliance(string serialNumber, string brand, string model, string status)
        {
            if (!ApplianceValidator.IsValidSerialNumber(serialNumber))
            {
                throw new ArgumentException("Invalid serial number format.");
            }

            if (string.IsNullOrWhiteSpace(brand))
            {
                throw new ArgumentException("Brand cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(model))
            {
                throw new ArgumentException("Model cannot be empty.");
            }

            this.serialNumber = serialNumber;
            this.brand = brand;
            this.model = model;
            this.status = "Under Maintenance";
            Status = status;
            registry.Add(this);
        }

        public abstract decimal CalculateCost();

        public virtual void DisplayInfo()
        {
            Console.WriteLine($"Serial Number: {SerialNumber}");
            Console.WriteLine($"Brand: {Brand}");
            Console.WriteLine($"Model: {Model}");
            Console.WriteLine($"Status: {Status}");
        }
    }
}