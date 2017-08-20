using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Graphics;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Pippidon.Judgements;
using OpenTK;
using System;

namespace osu.Game.Rulesets.Pippidon.Objects.Drawables
{
    public class Coin : DrawableScrollingHitObject<PippidonObject, PippidonJudgement>
    {
        private readonly Func<int, bool> touchingPippi;

        public Coin(PippidonObject hitObject, TextureStore textures, Func<int, bool> touchingPippi) : base(hitObject)
        {
            Size = new Vector2(40);
            Y = hitObject.Lane * 79;

            this.touchingPippi = touchingPippi;

            Add(new Sprite
            {
                RelativeSizeAxes = Axes.Both,
                Texture = textures.Get("coin"),
            });
        }

        protected override void Update()
        {
            base.Update();

            if (HitObject.StartTime < Time.Current)
                UpdateJudgement(true);
        }

        protected override void CheckJudgement(bool userTriggered)
        {
            if (HitObject.StartTime < Time.Current)
            {
                Judgement.Result = touchingPippi(HitObject.Lane) ? HitResult.Hit : HitResult.Miss;
                Judgement.TimeOffset = Time.Current - HitObject.StartTime;
            }
        }

        protected override PippidonJudgement CreateJudgement() => new PippidonJudgement();

        protected override void UpdateState(ArmedState state)
        {
            if (state == ArmedState.Hit)
                this.ScaleTo(5, 1500, Easing.OutQuint).FadeOut(1500, Easing.OutQuint).Expire();
        }
    }
}
