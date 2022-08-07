using osu.Game.Rulesets.Judgements;
using osu.Game.Rulesets.Objects;
using osu.Game.Rulesets.Pippidon.Scoring;
using osu.Game.Rulesets.Scoring;

namespace osu.Game.Rulesets.Pippidon.Objects
{
    public class PippidonHitObject : HitObject
    {
        public int Lane;

        public override Judgement CreateJudgement() => new Judgement();

        protected override HitWindows CreateHitWindows() => new PippidonHitWindows();
    }
}
