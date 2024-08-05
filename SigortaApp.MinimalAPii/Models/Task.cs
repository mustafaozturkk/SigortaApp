namespace SigortaApp.MinimalAPii;

public class Task
{
    public int Id { get; set; }
    public int? OrderCompanyId { get; set; }
    public int? SendCompanyId { get; set; }
    public int? CompanyWillPayId { get; set; }
    public int? CityId { get; set; }
    public int? StatusId { get; set; }
    public DateTime BusDate { get; set; }
    public int? Carrier { get; set; }
    public bool IsActive { get; set; }
    public string? Description { get; set; }
    public int CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
}
