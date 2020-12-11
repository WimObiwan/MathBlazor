using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Localization;

class ExerciseRepeater
{
    readonly Random random = new Random();
    ExerciseHistory exerciseHistory;
    RepeatPriorityCalculator repeatPriorityCalculator;

    public ExerciseRepeater(ExerciseHistory exerciseHistory, RepeatPriorityCalculator repeatPriorityCalculator)
    {
        this.exerciseHistory = exerciseHistory;
        this.repeatPriorityCalculator = repeatPriorityCalculator;
    }
    
    public Exercise ProcessExercise(Exercise originalExercise)
    {
        var list = exerciseHistory.List;

        // Check if exercise has been asked already --> if not, accept the exercise
        if (!list.Any(e => e.Equals(originalExercise)))
        {
            exerciseHistory.Add(originalExercise);
            return originalExercise;
        }

        // Exclude last 3 exercise (to prevent repeats)
        var listLastSkipped = list.OrderByDescending(e => e.LastResponseFirstTrial.Ordinal).Skip(3);

        // Take oldest wrong answer
        Exercise exercise = listLastSkipped.Where(e => !e.LastResponseFirstTrial.IsCorrect).OrderBy(e => e.LastResponseFirstTrial.Ordinal).FirstOrDefault();
        if (exercise != null)
            return exercise;

        // Take oldest slow answer
        var targetDuration = repeatPriorityCalculator.TargetDuration;
        if (targetDuration > 0)
        {
            exercise = listLastSkipped.Where(e => e.LastResponseFirstTrial.Duration > targetDuration).OrderBy(e => e.LastResponseFirstTrial.Ordinal).FirstOrDefault();
            if (exercise != null)
                return exercise;
        }

        // Take random item from 10% slowest (with minimum of 10 items, and with maximum of total count)
        int count = listLastSkipped.Count();
        int slowCount = Math.Min(count, Math.Max(10, count / 10)); // choose from at least 10 items
        int randomItem = random.Next(slowCount);
        exercise = listLastSkipped.OrderByDescending(e => e.LastResponseFirstTrial.Duration).Skip(randomItem).FirstOrDefault();
        if (exercise != null)
            return exercise;

        return originalExercise;
    }

    public int CountValidExercises()
    {
        var targetDuration = repeatPriorityCalculator.TargetDuration;
        return exerciseHistory.List.Count(e => e.IsValid(targetDuration));
    }

    public int CountInvalidExercises()
    {
        var targetDuration = repeatPriorityCalculator.TargetDuration;
        return exerciseHistory.List.Count(e => !e.IsValid(targetDuration));
    }

    public int CountExercises()
    {
        return exerciseHistory.List.Count();
    }
}