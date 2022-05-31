namespace BC.Problems.Boundary.Request;

public class ProblemProgressForUpdateModel
{
    public Guid ProblemId { get; set; }
    public Guid MasterId { get; set; }
    public string MasterEmail { get; set; }
    public string Stage { get; set; }
}
