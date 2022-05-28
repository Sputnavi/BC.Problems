namespace BC.Problems.Boundary.Request;

public class ProblemForCreateOrUpdateModel
{
    public BicycleForCreate Bicycle { get; set; }
    public Guid? UserId { get; set; }
    public string UserEmail { get; set; }
    public AddressForCreate Address { get; set; }
    public string Place { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateFinished { get; set; }
    public string Stage { get; set; }
    public string Description { get; set; }
    public ICollection<PartForCreate> Parts { get; set; }
}
