using AplikacjaWyplat.Pages.Enums;

namespace AplikacjaWyplat.Models
{
    public class Employer
    {
        public JobPosition jobPosition { get; set; }
        public int Overtime { get; set; }
        
        public int NumberOfDelegation { get; set; }

        public double Salary { get; set; }

        public double TotalSalary { get; set; }
    }
}
