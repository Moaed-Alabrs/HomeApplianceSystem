namespace HomeApplianceSystem
{
    // Subclass: WashingMachine (Inheritance & Polymorphism)
    public class WashingMachine : Appliance
    {
        private readonly decimal partsCost;
        private readonly bool motorNeedsRepair;

        public WashingMachine(string serialNumber, string brand, string model, string status,
                              decimal partsCost, bool motorNeedsRepair)
            : base(serialNumber, brand, model, status)
        {
            this.partsCost = partsCost;
            this.motorNeedsRepair = motorNeedsRepair;
        }

        // Override: washing-machine-specific repair cost
        public override decimal CalculateCost()
        {
            decimal motorFee = motorNeedsRepair ? 80m : 0m;
            return 120m + partsCost + motorFee;
        }
    }
}