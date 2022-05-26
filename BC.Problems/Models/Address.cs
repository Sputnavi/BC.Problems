namespace BC.Problems.Models;

public class Address
{
    public Guid Id { get; set; }
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }

    public ICollection<Problem> Problems { get; set; }
}
