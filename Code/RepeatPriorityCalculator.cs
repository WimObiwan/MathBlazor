using System;

class RepeatPriorityCalculator
{
    public long TargetDuration { get; private set; }
    public long WrongPenalty { get; private set; }

    public RepeatPriorityCalculator(long targetDuration, long wrongPenalty)
    {
        this.TargetDuration = targetDuration;
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