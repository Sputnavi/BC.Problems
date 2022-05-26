namespace BC.Problems.Boundary.Features
{
    public class BicycleParameters : RequestParameters
    {
        public BicycleParameters()
        {
            OrderBy = "serialNumber";
        }

        public string SearchTerm { get; set; }
    }
}
