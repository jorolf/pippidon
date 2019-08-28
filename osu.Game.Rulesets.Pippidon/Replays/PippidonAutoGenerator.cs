using osu.Game.Beatmaps;
using osu.Game.Rulesets.Pippidon.Objects;
using osu.Game.Rulesets.Replays;
using System.Collections.Generic;
using System;
using osu.Game.Replays;

namespace osu.Game.Rulesets.Pippidon.Replays
{
    public class PippidonAutoGenerator : AutoGenerator
    {
        protected Replay Replay;
        protected List<ReplayFrame> Frames => Replay.Frames;

        public new Beatmap<PippidonObject> Beatmap => (Beatmap<PippidonObject>)base.Beatmap;

        public PippidonAutoGenerator(IBeatmap beatmap) : base(beatmap)
        {
            Replay = new Replay();
        }

        public override Replay Generate()
        {
            int lastLane = 0;
            Frames.Add(new PippidonReplayFrame());

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

                Frames.Add(new PippidonReplayFrame(button)
                {
                    Time = hitObject.StartTime - KEY_UP_DELAY
                });
                Frames.Add(new PippidonReplayFrame
                {
                    Time = hitObject.StartTime
                }); //Release the keys as well
                lastLane = hitObject.Lane;
            }

            return Replay;
        }
    }
}
