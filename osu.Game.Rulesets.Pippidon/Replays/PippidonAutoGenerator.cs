using osu.Game.Beatmaps;
using osu.Game.Rulesets.Pippidon.Objects;
using osu.Game.Rulesets.Replays;
using System.Collections.Generic;
using System;
using osu.Game.Replays;
using osu.Game.Rulesets.Pippidon.UI;

namespace osu.Game.Rulesets.Pippidon.Replays
{
    public class PippidonAutoGenerator : AutoGenerator
    {
        protected Replay Replay;
        protected List<ReplayFrame> Frames => Replay.Frames;

        public new Beatmap<PippidonHitObject> Beatmap => (Beatmap<PippidonHitObject>)base.Beatmap;

        public PippidonAutoGenerator(IBeatmap beatmap) : base(beatmap)
        {
            Replay = new Replay();
        }

        public override Replay Generate()
        {
            const int half_total_lanes = PippidonPlayfield.LANE_COUNT / 2;
            int currentLane = 0;
            Frames.Add(new PippidonReplayFrame());

            foreach(PippidonHitObject hitObject in Beatmap.HitObjects)
            {
                if (currentLane == hitObject.Lane)
                    continue;

                int totalDistance = Math.Abs(hitObject.Lane - currentLane);
                var direction = (hitObject.Lane > currentLane) ^ (totalDistance > half_total_lanes) ? PippidonAction.MoveDown : PippidonAction.MoveUp;
                totalDistance %= half_total_lanes;

                double time = hitObject.StartTime - 5;
                time -= totalDistance * KEY_UP_DELAY;

                for (int i = 0; i < totalDistance; i++)
                {
                    addFrame(time, direction);
                    time += KEY_UP_DELAY;
                }

                currentLane = hitObject.Lane;
            }

            return Replay;
        }

        private void addFrame(double time, PippidonAction button)
        {
            Frames.Add(new PippidonReplayFrame(button) { Time = time });
            Frames.Add(new PippidonReplayFrame { Time = time + KEY_UP_DELAY }); //Release the keys as well
        }
    }
}
