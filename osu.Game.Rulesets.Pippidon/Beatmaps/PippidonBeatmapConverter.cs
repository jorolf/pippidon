using System;
using System.Collections.Generic;
using System.Linq;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Objects;
using osu.Game.Rulesets.Pippidon.Objects;
using osu.Game.Rulesets.Objects.Types;

namespace osu.Game.Rulesets.Pippidon.Beatmaps
{
    public class PippidonBeatmapConverter : BeatmapConverter<PippidonObject>
    {
        protected override IEnumerable<Type> ValidConversionTypes { get; } = new[] { typeof(IHasXPosition) };

        private readonly Dictionary<IBeatmap, FloatRange> floatRanges = new Dictionary<IBeatmap, FloatRange>();

        protected override IEnumerable<PippidonObject> ConvertHitObject(HitObject original, IBeatmap beatmap)
        {
            float pos = (original as IHasYPosition)?.Y ?? ((IHasXPosition)original).X;

            if (!floatRanges.ContainsKey(beatmap))
                calcRange(beatmap);

            yield return new PippidonObject
            {
                Samples = original.Samples,
                StartTime = original.StartTime,
                Lane = (int)((pos - floatRanges[beatmap].Min) / floatRanges[beatmap].Range * 3) - 1
            };
        }

        private void calcRange(IBeatmap beatmap)
        {
            List<float> positions = beatmap.HitObjects.OfType<IHasYPosition>().Select(hitObject => hitObject.Y).ToList();
            if(!positions.Any())
                positions = beatmap.HitObjects.OfType<IHasXPosition>().Select(hitObject => hitObject.X).ToList();

            floatRanges[beatmap] = new FloatRange
            {
                Min = positions.Min(),
                Max = positions.Max() + 1, //So we exclude '1' later
            };
        }

        private class FloatRange
        {
            public float Max, Min;
            public float Range => Max - Min;
        }

        public PippidonBeatmapConverter(IBeatmap beatmap)
            : base(beatmap)
        {
        }
    }
}
