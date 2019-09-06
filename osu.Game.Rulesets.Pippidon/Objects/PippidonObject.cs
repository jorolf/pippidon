using osu.Game.Rulesets.Judgements;
using osu.Game.Rulesets.Objects;

namespace osu.Game.Rulesets.Pippidon.Objects
{
    public class PippidonObject : HitObject
    {
        /// <summary>
        /// Range = [-1,1]
        /// </summary>
        public int Lane;

        public override Judgement CreateJudgement() => new Judgement();

        protected override HitWindows CreateHitWindows() => new PippidonHitWindows();
    }
}
