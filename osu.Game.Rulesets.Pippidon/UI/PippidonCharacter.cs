// Copyright (c) 2007-2019 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu-framework/master/LICENCE

using osu.Framework.Allocation;
using osu.Framework.Audio.Track;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Input.Bindings;
using osu.Game.Beatmaps.ControlPoints;
using osu.Game.Graphics.Containers;
using osuTK;

namespace osu.Game.Rulesets.Pippidon.UI
{
    public class PippidonCharacter : BeatSyncedContainer, IKeyBindingHandler<PippidonAction>
    {
        public readonly BindableInt LanePosition = new BindableInt
        {
            Value = PippidonPlayfield.LANE_COUNT / 2,
            MinValue = 0,
            MaxValue = PippidonPlayfield.LANE_COUNT - 1,
        };

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {
            Size = new Vector2(PippidonPlayfield.LANE_HEIGHT);

            Child = new Sprite
            {
                FillMode = FillMode.Fit,
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Scale = new Vector2(1.2f),
                RelativeSizeAxes = Axes.Both,
                Texture = textures.Get("pippidon")
            };

            LanePosition.BindValueChanged(e => { this.MoveToY(e.NewValue * PippidonPlayfield.LANE_HEIGHT); }, true);
        }

        protected override void OnNewBeat(int beatIndex, TimingControlPoint timingPoint, EffectControlPoint effectPoint, TrackAmplitudes amplitudes)
        {
            if (effectPoint.KiaiMode)
            {
                bool rightward = beatIndex % 2 == 1;
                double duration = timingPoint.BeatLength / 2;

                Child.RotateTo(rightward ? 10 : -10, duration * 2, Easing.InOutSine);

                Child.Animate(i => i.MoveToY(-10, duration, Easing.Out))
                     .Then(i => i.MoveToY(0, duration, Easing.In));
            }
            else
            {
                Child.ClearTransforms();
                Child.RotateTo(0, 500, Easing.Out);
                Child.MoveTo(Vector2.Zero, 500, Easing.Out);
            }
        }

        public bool OnPressed(PippidonAction action)
        {
            int laneDelta = 0;
            switch (action)
            {
                case PippidonAction.MoveUp:
                    laneDelta--;
                    break;
                case PippidonAction.MoveDown:
                    laneDelta++;
                    break;
                default:
                    return false;
            }

            //We add the lane count so we don't have weird negative modulo math
            LanePosition.Value = (LanePosition.Value + laneDelta + PippidonPlayfield.LANE_COUNT) % PippidonPlayfield.LANE_COUNT;

            return true;
        }

        public bool OnReleased(PippidonAction action) => false;
    }
}
