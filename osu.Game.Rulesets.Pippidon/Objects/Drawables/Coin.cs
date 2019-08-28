using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Graphics;
using osu.Game.Rulesets.Objects.Drawables;
using osuTK;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Game.Rulesets.Pippidon.UI;
using osuTK.Graphics;
using osu.Game.Rulesets.Scoring;

namespace osu.Game.Rulesets.Pippidon.Objects.Drawables
{
    public class Coin : DrawableHitObject<PippidonObject>
    {
        private const int hit_window = 100;

        private bool laneLost;
        private BindableInt pippidonLane;

        public Coin(PippidonObject hitObject, TextureStore textures) : base(hitObject)
        {
            Size = new Vector2(40);
            Anchor = Anchor.CentreLeft;
            Origin = Anchor.Centre;
            Y = hitObject.Lane * PippidonPlayfield.LANE_HEIGHT;

            AddInternal(new Sprite
            {
                RelativeSizeAxes = Axes.Both,
                Texture = textures.Get("coin"),
            });
        }

        [BackgroundDependencyLoader]
        private void load(PippidonPlayfield playfield)
        {
            pippidonLane = (BindableInt)playfield.PippidonLane.GetBoundCopy();
            pippidonLane.ValueChanged += e =>
            {
                if (e.OldValue == HitObject.Lane && e.NewValue != HitObject.Lane)
                {
                    laneLost = true;
                    UpdateResult(true);
                } else if (e.OldValue != HitObject.Lane && e.NewValue == HitObject.Lane)
                    laneLost = false;
            };
        }

        protected override void Update()
        {
            base.Update();

            if (Time.Current - HitObject.StartTime < -hit_window)
                UpdateResult(true);
        }

        protected override void CheckForResult(bool userTriggered, double timeOffset)
        {
            if (laneLost && timeOffset > -hit_window)
            {
                ApplyResult(r => r.Type = HitResult.Perfect);
            }
            else if (timeOffset >= 0 && timeOffset <= hit_window)
            {
                if (pippidonLane.Value == HitObject.Lane)
                {
                    ApplyResult(r => r.Type = HitResult.Perfect);
                }
            }
            else if (timeOffset > hit_window)
            {
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
