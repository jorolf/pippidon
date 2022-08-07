using osu.Game.Beatmaps;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Pippidon.Objects;
using osu.Game.Users;
using osu.Game.Rulesets.Pippidon.Replays;
using osu.Game.Scoring;

namespace osu.Game.Rulesets.Pippidon.Mods
{
    public class PippidonModAutoplay : ModAutoplay<PippidonHitObject>
    {
        public override Score CreateReplayScore(IBeatmap beatmap) => new Score
        {
            ScoreInfo = new ScoreInfo
            {
                User = new User { Username = "pippidon" },
            },
            Replay = new PippidonAutoGenerator(beatmap).Generate(),
        };
    }
}
