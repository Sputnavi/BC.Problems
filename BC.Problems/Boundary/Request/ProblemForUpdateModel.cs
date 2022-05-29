namespace BC.Problems.Boundary.Request;

public class ProblemForUpdateModel
{
    public ProblemBicycleModel Bicycle { get; set; }
    public Guid? UserId { get; set; }
    public string UserEmail { get; set; }
    public ProblemAddressModel Address { get; set; }
    public string Place { get; set; }
    public DateTime? DateFinished { get; set; }
    public string Stage { get; set; }
    public string Description { get; set; }
    public ICollection<ProblemPartModel> Parts { get; set; }
}
