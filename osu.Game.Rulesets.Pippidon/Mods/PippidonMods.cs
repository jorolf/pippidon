using osu.Game.Beatmaps;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Pippidon.Objects;
using osu.Game.Users;
using osu.Game.Rulesets.Pippidon.Replays;
using osu.Game.Scoring;

namespace osu.Game.Rulesets.Pippidon.Mods
{
    public class PippidonModAutoplay : ModAutoplay<PippidonObject>
    {
        protected override Score CreateReplayScore(Beatmap<PippidonObject> beatmap) => new Score
        {
            ScoreInfo = new ScoreInfo
            {
                User = new User { Username = "pippidon" },
            },
            Replay = new PippidonAutoGenerator(beatmap).Generate(),
        };
    }
}
