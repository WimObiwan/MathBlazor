using System.Collections.Generic;

class Exercise
{
    public Exercise(string raw, string html, int result)
    {
        Raw = raw;
        Html = html;
        Result = result;
        Responses = new List<Response>();
    }

    public string Raw { get; private set; }
    public string Html { get; private set; }
    public int Result { get; private set; }
    public Exercise HasBeenRepeatedBy { get; private set; }
    public Exercise IsRepeatFrom { get; private set; }

    public List<Response> Responses { get; private set;} 
}
