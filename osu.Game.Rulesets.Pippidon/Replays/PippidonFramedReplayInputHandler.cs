using osu.Game.Rulesets.Replays;
using System.Collections.Generic;
using osu.Framework.Input;
using OpenTK.Input;

namespace osu.Game.Rulesets.Pippidon.Replays
{
    public class PippidonFramedReplayInputHandler : FramedReplayInputHandler
    {
        public PippidonFramedReplayInputHandler(Replay replay) : base(replay)
        {
        }

        public override List<InputState> GetPendingStates()
        {
            var keys = new List<Key>();

            if (CurrentFrame?.MouseRight1 == true)
                keys.Add(Key.S);
            if (CurrentFrame?.MouseLeft1 == true)
                keys.Add(Key.W);

            return new List<InputState>
            {
                new InputState { Keyboard = new ReplayKeyboardState(keys) }
            };
        }
    }
}
