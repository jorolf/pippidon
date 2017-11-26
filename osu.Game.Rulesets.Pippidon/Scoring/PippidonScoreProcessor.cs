using osu.Game.Beatmaps;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Pippidon.Judgements;
using osu.Game.Rulesets.Pippidon.Objects;
using osu.Game.Rulesets.Scoring;
using osu.Game.Rulesets.UI;

namespace osu.Game.Rulesets.Pippidon.Scoring
{
    public class PippidonScoreProcessor : ScoreProcessor<PippidonObject>
    {
        public PippidonScoreProcessor()
        {
        }

        public PippidonScoreProcessor(RulesetContainer<PippidonObject> ruleset) : base(ruleset)
        {
        }

        protected override void SimulateAutoplay(Beatmap<PippidonObject> beatmap)
        {
            foreach (var unused in beatmap.HitObjects)
            {
                AddJudgement(new PippidonJudgement { Result = HitResult.Perfect });
            }
        }

        protected override void Reset(bool storeResults)
        {
            base.Reset(storeResults);

            Health.Value = 1;
            Accuracy.Value = 1;
        }
    }
}
