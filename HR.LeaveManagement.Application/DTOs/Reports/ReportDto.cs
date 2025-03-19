

namespace HR.LeaveManagement.Application.DTOs.Reports;
public class ReportDto
{
    public int TotalRequests { get; set; }
    public int Approved { get; set; }
    public int Rejected { get; set; }
    public int Pending { get; set; }

}
