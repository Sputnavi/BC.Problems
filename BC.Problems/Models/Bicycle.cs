namespace BC.Problems.Models;

public class Bicycle
{
    public Guid Id { get; set; }
    public string Model { get; set; }
    public string SerialNumber { get; set; }
    public Guid? UserId { get; set; }
    public string Email { get; set; }
}
