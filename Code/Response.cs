
class Response
{
    public Exercise Exercise { get; private set; }

    public int Answer { get; set; }
    public bool IsCorrect { get; set; }
    public long Duration { get; set; }
    public int Ordinal { get; set; }

    public Response(Exercise exercise) {
        Exercise = exercise;
    }
}