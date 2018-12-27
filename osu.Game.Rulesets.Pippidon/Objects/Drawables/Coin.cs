using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Graphics;
using osu.Game.Rulesets.Objects.Drawables;
using osuTK;
using System;
using osuTK.Graphics;
using osu.Game.Rulesets.Scoring;

namespace osu.Game.Rulesets.Pippidon.Objects.Drawables
{
    public class Coin : DrawableHitObject<PippidonObject>
    {
        private const int hit_window = 100;

        private readonly Func<int, bool> touchingPippi;

        public Coin(PippidonObject hitObject, TextureStore textures, Func<int, bool> touchingPippi) : base(hitObject)
        {
            Size = new Vector2(40);
            Y = hitObject.Lane * 79;

            this.touchingPippi = touchingPippi;

            AddInternal(new Sprite
            {
                RelativeSizeAxes = Axes.Both,
                Texture = textures.Get("coin"),
            });
        }

        protected override void Update()
        {
            base.Update();

            if (Time.Current - HitObject.StartTime < -hit_window)
                UpdateResult(true);
        }

        protected override void CheckForResult(bool userTriggered, double timeOffset)
        {
            if (Math.Abs(timeOffset) < hit_window)
            {
                if (touchingPippi(HitObject.Lane))
                {
                    ApplyResult(r => r.Type = HitResult.Perfect);
                }
            }
            else if (timeOffset > hit_window)
            {
                ApplyResult(r => r.Type = HitResult.Miss);
            }
        }

        protected override void UpdateState(ArmedState state)
        {
            ClearTransformsAfter(Time.Current);
            switch (state)
            {
                case ArmedState.Hit:
                    this.ScaleTo(5, 1500, Easing.OutQuint).FadeOut(1500, Easing.OutQuint).Expire();
                    break;
                case ArmedState.Miss:
                    this.FadeColour(Color4.Red, 1000, Easing.InQuint).Then().FadeOut(1000, Easing.InQuint).Expire();
                    break;
            }
        }
    }
}
