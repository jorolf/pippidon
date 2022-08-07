using System.Collections.Generic;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Pippidon.Objects;
using osu.Game.Rulesets.Scoring;
using osu.Game.Rulesets.UI;
using osu.Game.Rulesets.Pippidon.Scoring;
using osu.Framework.Input;
using osu.Game.Input.Handlers;
using osu.Game.Replays;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Pippidon.Objects.Drawables;
using osu.Game.Rulesets.Pippidon.Replays;
using osu.Game.Rulesets.UI.Scrolling;

namespace osu.Game.Rulesets.Pippidon.UI
{
    public class PippidonDrawableRuleset : DrawableScrollingRuleset<PippidonHitObject>
    {
        public PippidonDrawableRuleset(PippidonRuleset ruleset, IWorkingBeatmap beatmap, IReadOnlyList<Mod> mods)
            : base(ruleset, beatmap, mods)
        {
            Direction.Value = ScrollingDirection.Left;
            TimeRange.Value = 6000;
        }

        public override ScoreProcessor CreateScoreProcessor() => new PippidonScoreProcessor(this);

        protected override Playfield CreatePlayfield() => new PippidonPlayfield();

        protected override ReplayInputHandler CreateReplayInputHandler(Replay replay) => new PippidonFramedReplayInputHandler(replay);

        public override DrawableHitObject<PippidonHitObject> CreateDrawableRepresentation(PippidonHitObject h)
        {
            return new DrawablePippidonCoin(h);
        }

        protected override PassThroughInputManager CreateInputManager() => new PippidonInputManager(Ruleset?.RulesetInfo);
    }
}
