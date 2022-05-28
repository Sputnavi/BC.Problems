namespace BC.Problems.Boundary.Request;

public class PartForCreate
{
    public Guid? Id { get; set; }
    public string Name { get; set; }
    public Guid? ModelId { get; set; }
    public string ModelName { get; set; }
    public int? Amount { get; set; }
    public decimal? PricePerDetail { get; set; }
}
