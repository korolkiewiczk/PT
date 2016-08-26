namespace PT.Algorithm.Model
{
    public struct MonteCarloResult
    {
        public double Better { get; }

        public double Exact { get; }

        public double Smaller => (1 - Better - Exact);

        public MonteCarloResult(double better, double exact)
        {
            Better = better;
            Exact = exact;
        }
    }
}
