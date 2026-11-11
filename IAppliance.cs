namespace HomeApplianceSystem
{
    // Interface: the common contract every appliance must follow (Abstraction)
    public interface IAppliance
    {
        string SerialNumber { get; }
        string Brand { get; }
        string Model { get; }
        string Status { get; set; }
        decimal CalculateCost();
        void DisplayInfo();
    }
}