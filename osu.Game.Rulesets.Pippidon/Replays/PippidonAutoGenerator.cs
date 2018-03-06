using osu.Game.Beatmaps;
using osu.Game.Rulesets.Pippidon.Objects;
using osu.Game.Rulesets.Replays;
using osu.Game.Users;
using System.Collections.Generic;
using System;

namespace osu.Game.Rulesets.Pippidon.Replays
{
    public class PippidonAutoGenerator : AutoGenerator<PippidonObject>
    {
        protected Replay Replay;
        protected List<ReplayFrame> Frames => Replay.Frames;

        public PippidonAutoGenerator(Beatmap<PippidonObject> beatmap) : base(beatmap)
        {
            Replay = new Replay
            {
                User = new User
                {
                    Username = @"Pippppipiddddooon",
                }
            };
        }

        public override Replay Generate()
        {
            int lastLane = 0;
            foreach(PippidonObject hitObject in Beatmap.HitObjects)
            {
                if (lastLane == hitObject.Lane)
                    continue;

                PippidonAction button; //Left = Up, Right = Down
                switch (lastLane)
                {
                    case -1:
                        button = hitObject.Lane == 0 ? PippidonAction.MoveDown : PippidonAction.MoveUp;
                        break;
                    case 0:
                        button = hitObject.Lane == 1 ? PippidonAction.MoveDown : PippidonAction.MoveUp;
                        break;
                    case 1:
                        button = hitObject.Lane == -1 ? PippidonAction.MoveDown : PippidonAction.MoveUp;
                        break;
                    default:
                        throw new Exception("Unknown lane");
                }

                Frames.Add(new PippidonReplayFrame(button));
                Frames.Add(new PippidonReplayFrame()); //Release the keys as well
                lastLane = hitObject.Lane;
            }

            return Replay;
        }
    }
}
