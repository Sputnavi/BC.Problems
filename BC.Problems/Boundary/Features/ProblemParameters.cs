namespace BC.Problems.Boundary.Features
{
    public class ProblemParameters : RequestParameters
    {
        public ProblemParameters()
        {
            OrderBy = "dataCreated";
        }

        public string SearchTerm { get; set; }
    }
}
