class Exercise
{
    public Exercise(string raw, string html, int result)
    {
        Raw = raw;
        Html = html;
        Result = result;
    }

    public string Raw { get; private set; }
    public string Html { get; private set; }
    public int Result { get; private set; }
}
