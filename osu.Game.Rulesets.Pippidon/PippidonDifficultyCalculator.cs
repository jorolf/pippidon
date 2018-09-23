using osu.Game.Beatmaps;
using osu.Game.Rulesets.Difficulty;
using osu.Game.Rulesets.Mods;

namespace osu.Game.Rulesets.Pippidon
{
    public class PippidonDifficultyCalculator : DifficultyCalculator
    {
        public PippidonDifficultyCalculator(Ruleset ruleset, WorkingBeatmap beatmap)
            : base(ruleset, beatmap)
        {
        }

        protected override DifficultyAttributes Calculate(IBeatmap beatmap, Mod[] mods, double timeRate)
        {
            return new DifficultyAttributes(mods, 0);
        }
    }
}
