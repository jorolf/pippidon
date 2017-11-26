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
            Frames.Add(new ReplayFrame(-100000, null, null, ReplayButtonState.None));
            Frames.Add(new ReplayFrame(Beatmap.HitObjects[0].StartTime - 1000, null, null, ReplayButtonState.None));

            double lastTime = Beatmap.HitObjects[0].StartTime - 1000;
            int lastLane = 0;
            foreach(PippidonObject hitObject in Beatmap.HitObjects)
            {
                double time = (lastTime + hitObject.StartTime) / 2;
                lastTime = hitObject.StartTime;

                if (lastLane == hitObject.Lane)
                    continue;

                ReplayButtonState button; //Left = Up, Right = Down
                switch (lastLane)
                {
                    case -1:
                        button = hitObject.Lane == 0 ? ReplayButtonState.Right1 : ReplayButtonState.Left1;
                        break;
                    case 0:
                        button = hitObject.Lane == 1 ? ReplayButtonState.Right1 : ReplayButtonState.Left1;
                        break;
                    case 1:
                        button = hitObject.Lane == -1 ? ReplayButtonState.Right1 : ReplayButtonState.Left1;
                        break;
                    default:
                        throw new Exception("Unknown lane");
                }

                Frames.Add(new ReplayFrame(time, null, null, button));
                Frames.Add(new ReplayFrame(time + KEY_UP_DELAY, null, null, ReplayButtonState.None)); //Release the keys as well
                lastLane = hitObject.Lane;
            }

            return Replay;
        }
    }
}
