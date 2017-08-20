using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Pippidon.Judgements;
using osu.Game.Rulesets.Pippidon.Objects;
using osu.Game.Rulesets.Scoring;
using osu.Game.Rulesets.UI;
using System.Linq;

namespace osu.Game.Rulesets.Pippidon.Scoring
{
    public class PippidonScoreProcessor : ScoreProcessor<PippidonObject, PippidonJudgement>
    {
        public PippidonScoreProcessor()
        {
        }

        public PippidonScoreProcessor(RulesetContainer<PippidonObject, PippidonJudgement> ruleset) : base(ruleset)
        {
        }

        protected override void OnNewJudgement(PippidonJudgement judgement)
        {
            Accuracy.Value = Judgements.Where(judgement_ => judgement_.Result == HitResult.Hit).Count() / (float)Judgements.Count;
            if (judgement.Result == HitResult.Hit)
                TotalScore.Value++;
        }

        protected override void Reset()
        {
            base.Reset();

            Health.Value = 1;
            Accuracy.Value = 1;
        }
    }
}
