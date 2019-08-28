using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Game.Graphics;
using osu.Framework.Allocation;
using osu.Framework.Graphics.Sprites;
using osuTK;
using osu.Framework.Input.Bindings;
using osu.Game.Graphics.Containers;
using osu.Framework.Audio.Track;
using osu.Framework.Bindables;
using osu.Game.Beatmaps.ControlPoints;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Textures;
using osu.Game.Rulesets.UI.Scrolling;

namespace osu.Game.Rulesets.Pippidon.UI
{
    [Cached]
    public class PippidonPlayfield : ScrollingPlayfield
    {
        private readonly PippidonContainer pippidon;
        public const float LANE_HEIGHT = 70;

        public BindableInt PippidonLane => pippidon.LanePosition;

        public PippidonPlayfield(PippidonRuleset ruleset)
        {
            AddRangeInternal(new Drawable[]
            {
                new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    Padding = new MarginPadding { Left = 200 },
                    Anchor = Anchor.CentreRight,
                    Origin = Anchor.CentreRight,
                    Child = HitObjectContainer
                },
                new LaneContainer
                {
                    RelativeSizeAxes = Axes.X,
                    Height = LANE_HEIGHT,
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft,
                    Depth = 1,
                },
                pippidon = new PippidonContainer
                {
                    Size = new Vector2(LANE_HEIGHT),
                    Texture = ruleset.TextureStore.Get("pippidon"),
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreRight,
                    X = 200,
                },
            });
        }

        private class LaneContainer : BeatSyncedContainer
        {
            private OsuColour osuColour;

            [BackgroundDependencyLoader]
            private void load(OsuColour colour)
            {
                Colour = colour.BlueLight;
                osuColour = colour;

                Children = new[]
                {
                    new Box
                    {
                        RelativeSizeAxes = Axes.Both,
                    },
                    new Box
                    {
                        RelativeSizeAxes = Axes.Both,
                        Anchor = Anchor.BottomLeft,
                        Origin = Anchor.TopLeft,
                        Y = 2,
                    },
                    new Box
                    {
                        RelativeSizeAxes = Axes.Both,
                        Anchor = Anchor.TopLeft,
                        Origin = Anchor.BottomLeft,
                        Y = -2,
                    },
                };
            }

            protected override void OnNewBeat(int beatIndex, TimingControlPoint timingPoint, EffectControlPoint effectPoint, TrackAmplitudes amplitudes)
            {
                this.FadeColour(effectPoint.KiaiMode ? osuColour.PinkLight : osuColour.BlueLight, 1000);
            }
        }

        private class PippidonContainer : BeatSyncedContainer, IKeyBindingHandler<PippidonAction>
        {
            public override bool HandleNonPositionalInput => true;

            public readonly BindableInt LanePosition;

            public Texture Texture
            {
                set { ((Sprite)Child).Texture = value; }
            }

            public PippidonContainer()
            {
                Child = new Sprite
                {
                    FillMode = FillMode.Fit,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Scale = new Vector2(1.2f),
                    RelativeSizeAxes = Axes.Both,
                };

                LanePosition = new BindableInt
                {
                    Value = 0,
                    MinValue = -1,
                    MaxValue = 1,
                };

                LanePosition.BindValueChanged(e =>
                {
                    this.MoveToY(e.NewValue * LANE_HEIGHT);
                });
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

                //We add 3 so we don't have weird negative modulo math
                //and we also add 1 and subtract 1 because the lane range is from [-1, 1] but modulo is [0, 2]
                LanePosition.Value = (LanePosition.Value + laneDelta + 4) % 3 - 1;

                return true;
            }

            public bool OnReleased(PippidonAction action) => false;
        }
    }
}
