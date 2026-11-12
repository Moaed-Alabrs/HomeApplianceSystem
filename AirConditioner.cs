namespace HomeApplianceSystem
{
    // Subclass: AirConditioner (Inheritance & Polymorphism)
    public class AirConditioner : Appliance
    {
        private readonly decimal partsCost;
        private readonly bool needsGasRefill;

        public AirConditioner(string serialNumber, string brand, string model, string status,
                              decimal partsCost, bool needsGasRefill)
            : base(serialNumber, brand, model, status)
        {
            this.partsCost = partsCost;
            this.needsGasRefill = needsGasRefill;
        }

        // Override: air-conditioner-specific repair cost
        public override decimal CalculateCost()
        {
            decimal gasFee = needsGasRefill ? 120m : 0m;
            return 180m + partsCost + gasFee;
        }
    }
}