using System.Collections.Generic;
using System.Linq;

class Exercise
{
    readonly List<Response> responses = new List<Response>();
    const double alfa = 0.5;
    readonly ExponentialWeightedMovingAverage repeatPriority = new ExponentialWeightedMovingAverage(alfa);

    public Exercise(string raw, string html, int result)
    {
        Raw = raw;
        Html = html;
        Result = result;
    }

    public string Raw { get; private set; }
    public string Html { get; private set; }
    public int Result { get; private set; }

    public IReadOnlyList<Response> Responses => responses.AsReadOnly();
    public Response LastResponse => responses.LastOrDefault();

    public double RepeatPriority => repeatPriority.Average;

    public void AddResponse(Response response, double repeatPriority)
    {
        responses.Add(response);
        this.repeatPriority.Add(repeatPriority);
    }

    public override int GetHashCode()
    {
        return Raw.GetHashCode();
    }

    public override bool Equals(object obj)
    {
        if (obj is Exercise other)
            return Equals(other);
        else
            return false;
    }

    public bool Equals(Exercise other)
    {
        return Raw.Equals(other.Raw);
    }
}
