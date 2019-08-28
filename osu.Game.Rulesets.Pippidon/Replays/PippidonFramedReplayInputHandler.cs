using System.Collections.Generic;
using osu.Game.Rulesets.Replays;
using System.Linq;
using osu.Framework.Input.StateChanges;
using osu.Game.Replays;

namespace osu.Game.Rulesets.Pippidon.Replays
{
    public class PippidonFramedReplayInputHandler : FramedReplayInputHandler<PippidonReplayFrame>
    {
        public PippidonFramedReplayInputHandler(Replay replay) : base(replay)
        {
        }

        protected override bool IsImportant(PippidonReplayFrame frame) => frame.Actions.Any();

        public override List<IInput> GetPendingInputs()
        {
            return new List<IInput>
            {
                new ReplayState<PippidonAction>
                {
                    PressedActions = CurrentFrame?.Actions ?? new List<PippidonAction>(),
                }
            };
        }
    }
}
