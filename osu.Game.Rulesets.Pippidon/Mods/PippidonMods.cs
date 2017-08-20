using osu.Game.Beatmaps;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Pippidon.Objects;
using osu.Game.Rulesets.Scoring;
using osu.Game.Users;
using osu.Game.Rulesets.Pippidon.Replays;

namespace osu.Game.Rulesets.Pippidon.Mods
{
    public class PippidonModAutoplay : ModAutoplay<PippidonObject>
    {
        protected override Score CreateReplayScore(Beatmap<PippidonObject> beatmap) => new Score
        {
            User = new User { Username = "pippidon" },
            Replay = new PippidonAutoGenerator(beatmap).Generate(),
        };
    }
}
