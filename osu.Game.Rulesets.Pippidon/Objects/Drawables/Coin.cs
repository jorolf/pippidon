using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Graphics;
using osu.Game.Rulesets.Objects.Drawables;
using OpenTK;
using System;
using osu.Game.Rulesets.Judgements;
using osu.Game.Rulesets.Pippidon.Judgements;
using OpenTK.Graphics;
using osu.Game.Rulesets.Scoring;

namespace osu.Game.Rulesets.Pippidon.Objects.Drawables
{
    public class Coin : DrawableHitObject<PippidonObject>
    {
        private const int hit_window = 100;

        private readonly Func<int, bool> touchingPippi;

        private Judgement judgement;

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

            if (Time.Current - HitObject.StartTime < -hit_window)
                UpdateJudgement(true);
        }

        protected override void CheckForJudgements(bool userTriggered, double timeOffset)
        {
            if (Math.Abs(timeOffset) < hit_window)
            {
                if (touchingPippi(HitObject.Lane))
                {
                    if (judgement == null)
                    {
                        judgement = new PippidonJudgement
                        {
                            Result = HitResult.Perfect,
                            TimeOffset = timeOffset,
                        };
                    }
                    else if (timeOffset <= 0)
                    {
                        judgement.TimeOffset = 0;
                    }
                }

                if (judgement != null && timeOffset >= 0)
                {
                    AddJudgement(judgement);
                }
            }
            else if (timeOffset > hit_window)
            {
                AddJudgement(new PippidonJudgement
                {
                    Result = HitResult.Miss,
                });
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
