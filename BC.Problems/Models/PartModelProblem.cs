namespace BC.Problems.Models;

public class PartModelProblem
{
    public Guid Id { get; set; }
    public Guid? PartId { get; set; }
    public string PartName { get; set; }
    public Guid? PartModelId { get; set; }
    public string PartModelName { get; set; }
    public int? Amount { get; set; }
    public decimal? PricePerDetail { get; set; }

    public Guid ProblemId { get; set; }
    public Problem Problem { get; set; }
}
