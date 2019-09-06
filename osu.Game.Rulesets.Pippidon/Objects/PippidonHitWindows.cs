// Copyright (c) 2007-2019 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu-framework/master/LICENCE

using System.Collections.Generic;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Objects;
using osu.Game.Rulesets.Scoring;

namespace osu.Game.Rulesets.Pippidon.Objects
{
    public class PippidonHitWindows : HitWindows
    {
        private static readonly IReadOnlyDictionary<HitResult, double> base_ranges = new Dictionary<HitResult, double>
        {
            { HitResult.Perfect, 0.15 * default_hit_window },
            { HitResult.Great, 0.30 * default_hit_window },
            { HitResult.Ok, 0.55 * default_hit_window },
            { HitResult.Meh, 1 * default_hit_window },
        };

        private const int default_hit_window = 300;
        private const double oc0_modifier = 1.5;
        private const double oc10_modifier = 0.75;

        public override bool IsHitResultAllowed(HitResult result)
        {
            switch (result)
            {
                case HitResult.Perfect:
                case HitResult.Great:
                case HitResult.Meh:
                case HitResult.Ok:
                case HitResult.Miss:
                    return true;

                default:
                    return false;
            }
        }

        public override void SetDifficulty(double difficulty)
        {
            (double, double, double) mapRange(double oc5Window) => (oc5Window * oc0_modifier, oc5Window, oc5Window * oc10_modifier);

            Perfect = BeatmapDifficulty.DifficultyRange(difficulty, mapRange(base_ranges[HitResult.Perfect]));
            Great = BeatmapDifficulty.DifficultyRange(difficulty, mapRange(base_ranges[HitResult.Great]));
            Ok = BeatmapDifficulty.DifficultyRange(difficulty, mapRange(base_ranges[HitResult.Ok]));
            Meh = BeatmapDifficulty.DifficultyRange(difficulty, mapRange(base_ranges[HitResult.Meh]));
        }
    }
}
