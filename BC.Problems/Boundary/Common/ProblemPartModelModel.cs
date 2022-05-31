namespace BC.Problems.Boundary.Common;

public class ProblemPartModelModel
{
    public Guid PartId { get; set; }
    public string PartName { get; set; }
    public Guid? PartModelId { get; set; }
    public string PartModelName { get; set; }
    public int? Amount { get; set; }
    public decimal? PricePerDetail { get; set; }
}
