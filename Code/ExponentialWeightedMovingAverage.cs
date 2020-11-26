class ExponentialWeightedMovingAverage
{
    public double Alfa { get; private set; }
    public double Average { get; private set; }
    bool first = true;

    public ExponentialWeightedMovingAverage(double alfa)
    {
        Alfa = alfa;
    }

    public void Add(double value)
    {
        if (first)
        {
            Average = value;
            first = false;
        }
        else
        {
            Average = Alfa * value + (1.0 - Alfa) * Average;
        }
    }
}