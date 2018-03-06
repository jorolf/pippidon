using System.Collections.Generic;
using osu.Game.Rulesets.Replays;

namespace osu.Game.Rulesets.Pippidon.Replays
{
    public class PippidonReplayFrame : ReplayFrame
    {
        public List<PippidonAction> Actions = new List<PippidonAction>();

        public PippidonReplayFrame(PippidonAction? button = null)
        {
            if (button.HasValue)
                Actions.Add(button.Value);
        }
    }
}
