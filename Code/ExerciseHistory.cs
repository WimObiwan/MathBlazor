using System.Collections.Generic;
using System.Linq;

class ExerciseHistory
{
    List<Exercise> list = new List<Exercise>();

    public IReadOnlyList<Exercise> List => list.AsReadOnly();

    public void Add(Exercise exercise)
    {
        list.Add(exercise);
    }
}
