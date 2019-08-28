using System.Collections.Generic;
using osu.Framework.Allocation;
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
    [Cached]
    public class PippidonDrawableRuleset : DrawableScrollingRuleset<PippidonObject>
    {
        private readonly PippidonRuleset pippidonRuleset;

        public PippidonDrawableRuleset(PippidonRuleset ruleset, WorkingBeatmap beatmap, IReadOnlyList<Mod> mods)
            : base(ruleset, beatmap, mods)
        {
            pippidonRuleset = ruleset;
            Direction.Value = ScrollingDirection.Left;
            TimeRange.Value = 6000;
        }

        public override ScoreProcessor CreateScoreProcessor() => new PippidonScoreProcessor(this);

        protected override Playfield CreatePlayfield() => new PippidonPlayfield(pippidonRuleset);

        protected override ReplayInputHandler CreateReplayInputHandler(Replay replay) => new PippidonFramedReplayInputHandler(replay);

        public override DrawableHitObject<PippidonObject> CreateDrawableRepresentation(PippidonObject h)
        {
            return new Coin(h, pippidonRuleset.TextureStore);
        }

        protected override PassThroughInputManager CreateInputManager() => new PippidonInputManager(Ruleset?.RulesetInfo);
    }
}
