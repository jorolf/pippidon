using osu.Game.Beatmaps;
using osu.Game.Rulesets.Beatmaps;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Pippidon.Judgements;
using osu.Game.Rulesets.Pippidon.Objects;
using osu.Game.Rulesets.Scoring;
using osu.Game.Rulesets.UI;
using osu.Game.Rulesets.Pippidon.Scoring;
using osu.Game.Rulesets.Pippidon.Beatmaps;
using osu.Framework.Input;
using osu.Framework.Graphics;
using osu.Game.Rulesets.Pippidon.Objects.Drawables;
using osu.Game.Rulesets.Replays;
using osu.Game.Rulesets.Pippidon.Replays;

namespace osu.Game.Rulesets.Pippidon.UI
{
    public class PippidonRulesetContainer : ScrollingRulesetContainer<PippidonPlayfield, PippidonObject, PippidonJudgement>
    {
        private PippidonRuleset pippidonRuleset;

        public PippidonRulesetContainer(PippidonRuleset ruleset, WorkingBeatmap beatmap, bool isForCurrentRuleset) : base(ruleset, beatmap, isForCurrentRuleset)
        {
            pippidonRuleset = ruleset;
            AspectAdjust = false;
        }

        public override ScoreProcessor CreateScoreProcessor() => new PippidonScoreProcessor(this);

        protected override BeatmapConverter<PippidonObject> CreateBeatmapConverter() => new PippidonBeatmapConverter();

        protected override Playfield<PippidonObject, PippidonJudgement> CreatePlayfield() => new PippidonPlayfield(pippidonRuleset);

        protected override DrawableHitObject<PippidonObject, PippidonJudgement> GetVisualRepresentation(PippidonObject h)
        {
            return new Coin(h, pippidonRuleset.TextureStore, lane => Playfield.PippidonLane == lane)
            {
                Anchor = Anchor.CentreLeft,
                Origin = Anchor.Centre,
            };
        }

        protected override FramedReplayInputHandler CreateReplayInputHandler(Replay replay) => new PippidonFramedReplayInputHandler(replay);

        public override PassThroughInputManager CreateInputManager() => new PippidonInputManager(Ruleset?.RulesetInfo);
    }
}
