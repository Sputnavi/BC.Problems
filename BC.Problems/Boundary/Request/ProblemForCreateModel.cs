namespace BC.Problems.Boundary.Request;

public class ProblemForCreateModel
{
    public ProblemForCreateBicycleModel Bicycle { get; set; }
    public Guid? UserId { get; set; }
    public string UserEmail { get; set; }
    public ProblemForCreateAddressModel Address { get; set; }
    public string Place { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateFinished { get; set; }
    public string Stage { get; set; }
    public string Description { get; set; }
    public ICollection<ProblemForCreatePartModel> Parts { get; set; }
}
