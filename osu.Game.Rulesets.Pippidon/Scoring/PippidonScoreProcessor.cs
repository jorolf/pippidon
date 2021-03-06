﻿using osu.Game.Rulesets.Objects;
using osu.Game.Rulesets.Pippidon.Objects;
using osu.Game.Rulesets.Scoring;
using osu.Game.Rulesets.UI;

namespace osu.Game.Rulesets.Pippidon.Scoring
{
    public class PippidonScoreProcessor : ScoreProcessor<PippidonObject>
    {
        public PippidonScoreProcessor(DrawableRuleset<PippidonObject> ruleset) : base(ruleset)
        {
        }

        protected override void Reset(bool storeResults)
        {
            base.Reset(storeResults);

            Health.Value = 1;
            Accuracy.Value = 1;
        }

        public override HitWindows CreateHitWindows() => new PippidonHitWindows();
    }
}
