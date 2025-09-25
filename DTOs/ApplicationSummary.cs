namespace Loan_Management_System.DTOs
{
    public class ApplicationSummary
    {
        public int Total { get; set; }
        public int Approved { get; set; }
        public int Rejected { get; set; }
        public int Pending { get; set; }
        public int Disbursed { get; set; }
    }
}
