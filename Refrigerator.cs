namespace HomeApplianceSystem
{
    // Subclass: Refrigerator (Inheritance & Polymorphism)
    public class Refrigerator : Appliance
    {
        private readonly decimal partsCost;
        private readonly bool needsGasRefill;

        public Refrigerator(string serialNumber, string brand, string model, string status,
                            decimal partsCost, bool needsGasRefill)
            : base(serialNumber, brand, model, status)
        {
            this.partsCost = partsCost;
            this.needsGasRefill = needsGasRefill;
        }

        // Override: refrigerator-specific repair cost
        public override decimal CalculateCost()
        {
            decimal gasFee = needsGasRefill ? 100m : 0m;
            return 150m + partsCost + gasFee;
        }
    }
}