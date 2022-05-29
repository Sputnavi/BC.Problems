namespace BC.Problems.Boundary.Common;

public class ProblemPartModel
{
    public Guid? Id { get; set; }
    public string Name { get; set; }
    public Guid? PartModelId { get; set; }
    public string PartModelName { get; set; }
    public int? Amount { get; set; }
    public decimal? PricePerDetail { get; set; }
}
