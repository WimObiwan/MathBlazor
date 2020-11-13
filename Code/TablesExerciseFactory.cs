using System;
using System.Collections.Generic;
using Microsoft.Extensions.Localization;

class TablesExerciseFactory
{
    IStringLocalizer<TablesExerciseFactory> Loc { get; set; }
    private Random random = new Random();

    IList<int> Tables { get; set; }
    IList<int> Types { get; set; }

    public TablesExerciseFactory(IStringLocalizer<TablesExerciseFactory> loc)
    {
        Loc = loc;
    }
    
    public void Initialize(IList<int> tables, IList<int> types)
    {
        Tables = tables;
        Types = types;
    }

    public Exercise GetExercise()
    {
        int num1 = random.Next(0, 11);
        int num2 = Tables[random.Next(Tables.Count)];
        int result = num1 * num2;
        string raw, exercise;

        int type = Types[random.Next(Types.Count)];
        switch (type)
        {
            default:
            case 0:
                raw = $"{num1} x {num2} = ?";
                exercise = @Loc["{0} &times; {1} &equals; &#x2753;", num1, num2];
                break;
            case 1:
                raw = $"{num2} x {num1} = ?";
                exercise = @Loc["{0} &times; {1} &equals; &#x2753;", num2, num1];
                break;
            case 2:
                raw = $"{result} : {num2} = ?";
                exercise = @Loc["{0} &div; {1} &equals; &#x2753;", result, num2];
                result = num1;
                break;
        }

        return new Exercise(raw, exercise, result);
    }
}