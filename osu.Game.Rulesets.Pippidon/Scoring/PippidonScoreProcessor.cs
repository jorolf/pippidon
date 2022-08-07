using osu.Game.Rulesets.Pippidon.Objects;
using osu.Game.Rulesets.Scoring;
using osu.Game.Rulesets.UI;

namespace osu.Game.Rulesets.Pippidon.Scoring
{
    public class PippidonScoreProcessor : ScoreProcessor<PippidonHitObject>
    {
        public PippidonScoreProcessor(DrawableRuleset<PippidonHitObject> ruleset) : base(ruleset)
        {
        }

        public override HitWindows CreateHitWindows() => new PippidonHitWindows();
    }
}
