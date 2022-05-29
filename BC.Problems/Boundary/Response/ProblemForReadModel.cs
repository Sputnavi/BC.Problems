using BC.Problems.Boundary.Common;
using BC.Problems.Models;

namespace BC.Problems.Boundary.Response;

public class ProblemForReadModel
{
    public Guid Id { get; set; }
    public ProblemBicycleModel Bicycle { get; set; }
    public Guid? UserId { get; set; }
    public string UserEmail { get; set; }
    public ProblemAddressModel Address { get; set; }
    public string Place { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateFinished { get; set; }
    public ProblemStage Stage { get; set; }
    public string Description { get; set; }
    public ICollection<ProblemPartModel> Parts { get; set; }
}
