using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Localization;

class ExerciseRepeater
{
    ExerciseHistory exerciseHistory;
    long targetDuration;

    public ExerciseRepeater(ExerciseHistory exerciseHistory, RepeatPriorityCalculator repeatPriorityCalculator)
    {
        this.exerciseHistory = exerciseHistory;
        this.targetDuration = repeatPriorityCalculator.TargetDuration; // TODO: Take from configuration
    }
    
    public Exercise ProcessExercise(int currentOrdinal, Exercise exercise)
    {
        var list = exerciseHistory.List;

        if (!list.Any(e => e.Equals(exercise)))
        {
            exerciseHistory.Add(exercise);
            return exercise;
        }

        int MaxProximity = Math.Max(5, list.Count(e => !e.LastResponseFirstTrial.IsCorrect || e.LastResponseFirstTrial.Duration > targetDuration));

        return list.OrderByDescending(
            e => e.RepeatPriority / Math.Max(1, MaxProximity - (currentOrdinal - e.LastResponse.Ordinal - 1))
            ).First();
    }

    public int CountInvalidExercises()
    {
        return exerciseHistory.List.Count(e => !e.LastResponseFirstTrial.IsCorrect || e.LastResponseFirstTrial.Duration > targetDuration);
    }
}