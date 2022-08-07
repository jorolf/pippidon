using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Game.Graphics;
using osu.Framework.Allocation;
using osu.Game.Graphics.Containers;
using osu.Framework.Audio.Track;
using osu.Framework.Bindables;
using osu.Game.Beatmaps.ControlPoints;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Textures;
using osu.Game.Rulesets.UI.Scrolling;
using osuTK.Graphics;

namespace osu.Game.Rulesets.Pippidon.UI
{
    [Cached]
    public class PippidonPlayfield : ScrollingPlayfield
    {
        private PippidonCharacter pippidon;

        public const int LANE_COUNT = 3;
        public const float LANE_HEIGHT = 70;

        public BindableInt PippidonLane => pippidon.LanePosition;

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {
            AddInternal(new LaneContainer
            {
                RelativeSizeAxes = Axes.X,
                AutoSizeAxes = Axes.Y,
                Anchor = Anchor.CentreLeft,
                Origin = Anchor.CentreLeft,
                Child = new Container
                {
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                    Padding = new MarginPadding
                    {
                        Left = 200,
                        Top = LANE_HEIGHT / 2,
                        Bottom = LANE_HEIGHT / 2
                    },
                    Children = new Drawable[]
                    {
                        HitObjectContainer,
                        pippidon = new PippidonCharacter
                        {
                            Origin = Anchor.Centre,
                        },
                    }
                },
            });
        }

        private class LaneContainer : BeatSyncedContainer
        {
            private OsuColour colours;
            private FillFlowContainer fill;

            private readonly Container content = new Container
            {
                RelativeSizeAxes = Axes.Both,
            };

            protected override Container<Drawable> Content => content;

            [BackgroundDependencyLoader]
            private void load(OsuColour colours)
            {
                this.colours = colours;

                InternalChildren = new Drawable[]
                {
                    fill = new FillFlowContainer
                    {
                        RelativeSizeAxes = Axes.X,
                        AutoSizeAxes = Axes.Y,
                        Colour = colours.BlueLight,
                        Direction = FillDirection.Vertical,
                    },
                    content,
                };

                for (int i = 0; i < LANE_COUNT; i++)
                {
                    fill.Add(new Lane
                    {
                        RelativeSizeAxes = Axes.X,
                        Height = LANE_HEIGHT,
                    });
                }
            }

            protected override void OnNewBeat(int beatIndex, TimingControlPoint timingPoint, EffectControlPoint effectPoint, TrackAmplitudes amplitudes)
            {
                this.FadeColour(effectPoint.KiaiMode ? colours.PinkLight : colours.BlueLight, 1000);
            }

            private class Lane : CompositeDrawable
            {
                public Lane()
                {
                    InternalChild = new Box
                    {
                        Colour = Color4.White,
                        RelativeSizeAxes = Axes.Both,
                        Anchor = Anchor.CentreLeft,
                        Origin = Anchor.CentreLeft,
                        Height = 0.95f,
                    };
                }
            }
        }
    }
}
