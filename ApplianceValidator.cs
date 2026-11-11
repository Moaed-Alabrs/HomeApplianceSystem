namespace HomeApplianceSystem
{
    // Static class: data validation helpers (Static Classes & Members)
    public static class ApplianceValidator
    {
        public static bool IsValidSerialNumber(string serialNumber)
        {
            if (string.IsNullOrWhiteSpace(serialNumber))
            {
                return false;
            }

            if (serialNumber.Length != 6)
            {
                return false;
            }

            string prefix = serialNumber.Substring(0, 3).ToUpper();

            if (prefix != "REF" && prefix != "WAS" && prefix != "AIR")
            {
                return false;
            }

            string digits = serialNumber.Substring(3);
            return digits.All(char.IsDigit);
        }

        public static bool IsValidStatus(string status)
        {
            if (string.IsNullOrWhiteSpace(status))
            {
                return false;
            }

            string normalizedStatus = status.Trim();
            return normalizedStatus == "Under Maintenance" ||
                   normalizedStatus == "Repaired" ||
                   normalizedStatus == "Available";
        }

        public static bool IsValidDate(DateTime date)
        {
            return date >= DateTime.Today.AddYears(-50) && date <= DateTime.Today.AddYears(10);
        }

        public static string GetStatusLabel(string status)
        {
            return string.IsNullOrWhiteSpace(status) ? "Unknown" : status.Trim();
        }
    }
}