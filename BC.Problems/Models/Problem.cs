namespace BC.Problems.Models;

public class Problem
{
    public Guid Id { get; set; }
    public Guid? BicycleId { get; set; }
    public string BicycleModel { get; set; }
    public string BicycleSerialNumber { get; set; }
    public Guid? UserId { get; set; }
    public string UserEmail { get; set; }
    public Guid? MasterId { get; set; }
    public string MasterEmail { get; set; }
    public Address Address { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime? DateFinished { get; set; }
    public ProblemStage Stage { get; set; }
    public string Description { get; set; }
    public ICollection<PartModelProblem> PartModelProblems { get; set; }
}
