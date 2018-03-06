using osu.Game.Rulesets.Replays;
using System.Collections.Generic;
using System.Linq;
using osu.Framework.Input;

namespace osu.Game.Rulesets.Pippidon.Replays
{
    public class PippidonReplayInputHandler : FramedReplayInputHandler<PippidonReplayFrame>
    {
        public PippidonReplayInputHandler(Replay replay) : base(replay)
        {
        }

        protected override bool IsImportant(PippidonReplayFrame frame) => frame.Actions.Any();

        public override List<InputState> GetPendingStates()
        {
            return new List<InputState>
            {
                new ReplayState<PippidonAction>
                {
                    PressedActions = CurrentFrame.Actions
                }
            };
        }
    }
}
