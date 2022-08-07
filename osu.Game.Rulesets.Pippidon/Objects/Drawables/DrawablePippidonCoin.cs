using System;
using System.Collections.Generic;
using System.Diagnostics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Graphics;
using osu.Game.Rulesets.Objects.Drawables;
using osuTK;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Game.Audio;
using osu.Game.Beatmaps.ControlPoints;
using osu.Game.Rulesets.Pippidon.UI;
using osuTK.Graphics;
using osu.Game.Rulesets.Scoring;

namespace osu.Game.Rulesets.Pippidon.Objects.Drawables
{
    public class DrawablePippidonCoin : DrawableHitObject<PippidonHitObject>
    {
        private bool laneLost;
        private BindableInt currentLane;

        public DrawablePippidonCoin(PippidonHitObject hitObject) : base(hitObject)
        {
            Size = new Vector2(40);
            Origin = Anchor.Centre;
            Y = hitObject.Lane * PippidonPlayfield.LANE_HEIGHT;
        }

        [BackgroundDependencyLoader]
        private void load(PippidonPlayfield playfield, TextureStore textures)
        {
            AddInternal(new Sprite
            {
                RelativeSizeAxes = Axes.Both,
                Texture = textures.Get("coin"),
            });

            currentLane = (BindableInt)playfield.PippidonLane.GetBoundCopy();
            currentLane.ValueChanged += e =>
            {
                if (e.OldValue == HitObject.Lane && e.NewValue != HitObject.Lane)
                {
                    laneLost = true;
                    UpdateResult(true);
                    laneLost = false;
                }
            };
        }

        protected override IEnumerable<HitSampleInfo> GetSamples() => new[]
        {
            new HitSampleInfo
            {
                Bank = SampleControlPoint.DEFAULT_BANK,
                Name = HitSampleInfo.HIT_NORMAL,
            }
        };

        /*protected override void Update()
        {
            base.Update();
            Debug.Assert(HitObject.HitWindows != null);

            //timeOffset is inverted because we need to check whether the hitobject could've been hit in the past
            if (HitObject.HitWindows.CanBeHit(HitObject.StartTime - Time.Current))
                UpdateResult(true);
        }*/

        protected override void CheckForResult(bool userTriggered, double timeOffset)
        {
            Debug.Assert(HitObject.HitWindows != null);
            var timeOffsetAbs = Math.Abs(timeOffset);

            if (laneLost && HitObject.HitWindows.CanBeHit(timeOffsetAbs))
            {
                ApplyResult(r => r.Type = HitObject.HitWindows.ResultFor(timeOffset));
            }
            else if (timeOffset >= 0)
            {
                if (HitObject.HitWindows.CanBeHit(timeOffsetAbs))
                {
                    if (currentLane.Value == HitObject.Lane)
                        ApplyResult(r => r.Type = HitObject.HitWindows.ResultFor(timeOffset));
                }
                else
                    ApplyResult(r => r.Type = HitResult.Miss);
            }
        }

        protected override void UpdateStateTransforms(ArmedState state)
        {
            switch (state)
            {
                case ArmedState.Hit:
                    this.ScaleTo(5, 1500, Easing.OutQuint).FadeOut(1500, Easing.OutQuint).Expire();
                    break;
                case ArmedState.Miss:
                    this.FadeColour(Color4.Red, 100, Easing.InQuint).Then().FadeOut(1000, Easing.InQuint).Expire();
                    break;
            }
        }
    }
}
