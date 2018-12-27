using osu.Game.Beatmaps;
using osu.Game.Rulesets.Pippidon.Objects;
using osu.Game.Rulesets.Scoring;
using osu.Game.Rulesets.UI;
using osu.Game.Rulesets.Pippidon.Scoring;
using osu.Framework.Input;
using osu.Framework.Graphics;
using osu.Game.Input.Handlers;
using osu.Game.Replays;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Pippidon.Objects.Drawables;
using osu.Game.Rulesets.Pippidon.Replays;
using osu.Game.Rulesets.UI.Scrolling;

namespace osu.Game.Rulesets.Pippidon.UI
{
    public class PippidonRulesetContainer : ScrollingRulesetContainer<PippidonPlayfield, PippidonObject>
    {
        private readonly PippidonRuleset pippidonRuleset;

        public PippidonRulesetContainer(PippidonRuleset ruleset, WorkingBeatmap beatmap) : base(ruleset, beatmap)
        {
            pippidonRuleset = ruleset;
            Direction.Value = ScrollingDirection.Left;
            TimeRange.Value = 6000;
        }

        public override ScoreProcessor CreateScoreProcessor() => new PippidonScoreProcessor(this);

        protected override Playfield CreatePlayfield() => new PippidonPlayfield(pippidonRuleset);

        public override DrawableHitObject<PippidonObject> GetVisualRepresentation(PippidonObject h)
        {
            return new Coin(h, pippidonRuleset.TextureStore, lane => Playfield.PippidonLane == lane)
            {
                Anchor = Anchor.CentreLeft,
                Origin = Anchor.Centre,
            };
        }

        protected override ReplayInputHandler CreateReplayInputHandler(Replay replay) => new PippidonReplayInputHandler(replay);

        public override PassThroughInputManager CreateInputManager() => new PippidonInputManager(Ruleset?.RulesetInfo);
    }
}
