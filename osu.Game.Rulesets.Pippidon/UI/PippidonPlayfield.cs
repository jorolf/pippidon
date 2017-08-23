using osu.Game.Rulesets.UI;
using osu.Game.Rulesets.Pippidon.Judgements;
using osu.Game.Rulesets.Pippidon.Objects;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Game.Graphics;
using osu.Framework.Allocation;
using osu.Framework.Audio;
using osu.Framework.Graphics.Sprites;
using OpenTK;
using osu.Framework.Input.Bindings;
using osu.Game.Graphics.Containers;
using osu.Framework.Audio.Track;
using osu.Game.Beatmaps.ControlPoints;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Textures;

namespace osu.Game.Rulesets.Pippidon.UI
{
    public class PippidonPlayfield : ScrollingPlayfield<PippidonObject, PippidonJudgement>
    {
        private readonly PippidonRuleset ruleset;

        private readonly Container content;

        protected override Container<Drawable> Content => content;

        private PippidonContainer pippidon;
        public int PippidonLane => pippidon.LanePosition;

        public PippidonPlayfield(PippidonRuleset ruleset) : base(Axes.X)
        {
            this.ruleset = ruleset;

            VisibleTimeRange.Value = 6000;

            AddRangeInternal(new Drawable[]
            {
                content = new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    Padding = new MarginPadding { Left = 200 },
                    Anchor = Anchor.CentreRight,
                    Origin = Anchor.CentreRight,
                },
                new LaneContainer
                {
                    RelativeSizeAxes = Axes.X,
                    Height = 70,
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft,
                    Depth = 1,
                },
                pippidon = new PippidonContainer
                {
                    Size = new Vector2(70),
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

                if (effectPoint.KiaiMode)
                    this.FadeColour(osuColour.PinkLight, 1000);
                else
                    this.FadeColour(osuColour.BlueLight, 1000);
            }
        }

        private class PippidonContainer : BeatSyncedContainer, IKeyBindingHandler<PippidonAction>
        {
            public override bool HandleInput => true;

            public int LanePosition
            {
                get
                {
                    return (int)(Y / Height);
                }
                set
                {
                    if (value < -1)
                        Y = 1 * Height;
                    else if (value > 1)
                        Y = -1 * Height;
                    else
                        Y = value * Height;
                }
            }

            public Texture Texture
            {
                set
                {
                    (Child as Sprite).Texture = value;
                }
            }

            public PippidonContainer()
            {
                Child = new Sprite
                {
                    RelativeSizeAxes = Axes.Both,
                };
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
                switch (action)
                {
                    case PippidonAction.MoveUp:
                        LanePosition--;
                        return true;
                    case PippidonAction.MoveDown:
                        LanePosition++;
                        return true;
                    default:
                        return false;
                }
            }

            public bool OnReleased(PippidonAction action) => false;
        }
    }
}