using System.Collections.Generic;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.UI;
using osu.Framework.Input.Bindings;
using osu.Framework.IO.Stores;
using osu.Framework.Graphics.Textures;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Game.Rulesets.Difficulty;
using osu.Game.Rulesets.Pippidon.Beatmaps;
using osu.Game.Rulesets.Pippidon.Mods;
using osu.Game.Rulesets.Pippidon.UI;

namespace osu.Game.Rulesets.Pippidon
{
    public class PippidonRuleset : Ruleset
    {
        public readonly IResourceStore<byte[]> ResourceStore;
        public readonly TextureStore TextureStore;

        public PippidonRuleset(RulesetInfo rulesetInfo) : base(rulesetInfo)
        {
            ResourceStore = CreateResourceStore();
            TextureStore = new TextureStore(new TextureLoaderStore(new NamespacedResourceStore<byte[]>(ResourceStore, @"Textures")));
        }

        public override string Description => "pippipidoooooon";

        public override DrawableRuleset CreateDrawableRulesetWith(IWorkingBeatmap beatmap, IReadOnlyList<Mod> mods) => new PippidonDrawableRuleset(this, beatmap, mods);

        public override IBeatmapConverter CreateBeatmapConverter(IBeatmap beatmap) => new PippidonBeatmapConverter(beatmap);

        public override DifficultyCalculator CreateDifficultyCalculator(WorkingBeatmap beatmap) => new PippidonDifficultyCalculator(this, beatmap);

        public override IEnumerable<Mod> GetModsFor(ModType type)
        {
            switch (type)
            {
                case ModType.Automation:
                    return new[] { new PippidonModAutoplay() };
                default:
                    return new Mod[] { null };
            }
        }

        public override string ShortName => "pippidon";

        public override IEnumerable<KeyBinding> GetDefaultKeyBindings(int variant = 0) => new[]
        {
            new KeyBinding(InputKey.W, PippidonAction.MoveUp),
            new KeyBinding(InputKey.S, PippidonAction.MoveDown),
            new KeyBinding(InputKey.D, PippidonAction.Boost),
        };

        public override Drawable CreateIcon() => new Sprite
        {
            Margin = new MarginPadding { Top = 3 },
            Texture = TextureStore.Get("coin"),
        };
    }
}
