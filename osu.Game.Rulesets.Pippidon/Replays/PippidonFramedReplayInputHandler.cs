using osu.Game.Rulesets.Replays;
using System.Collections.Generic;
using osu.Framework.Input;

namespace osu.Game.Rulesets.Pippidon.Replays
{
    public class PippidonFramedReplayInputHandler : FramedReplayInputHandler
    {
        public PippidonFramedReplayInputHandler(Replay replay) : base(replay)
        {
        }

        public override List<InputState> GetPendingStates()
        {
            var keys = new List<PippidonAction>();

            if (CurrentFrame?.MouseRight1 == true)
                keys.Add(PippidonAction.MoveDown);
            if (CurrentFrame?.MouseLeft1 == true)
                keys.Add(PippidonAction.MoveUp);

            return new List<InputState>
            {
                new ReplayState<PippidonAction>
                {
                    PressedActions = keys,
                }
            };
        }
    }
}
