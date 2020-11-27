using System;

class RepeatPriorityCalculator
{
    public long TargetDuration { get; set; }
    public long WrongPenalty { get; private set; }

    public RepeatPriorityCalculator(long wrongPenalty)
    {
        this.TargetDuration = 5000;
        this.WrongPenalty = wrongPenalty;
    }

    public double Calculate(Response response)
    {
        long duration = response.Duration + (response.IsCorrect ? 0 : WrongPenalty);
        double x = (double)duration / TargetDuration;
        const double k = 1.0;
        return 1.0 - Math.Exp(-k * x);
    }
}