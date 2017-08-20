using osu.Framework.Input.Bindings;
using osu.Game.Input.Bindings;
using System.ComponentModel;

namespace osu.Game.Rulesets.Pippidon
{
    public class PippidonInputManager : DatabasedKeyBindingInputManager<PippidonAction>
    {
        public PippidonInputManager(RulesetInfo ruleset) : base(ruleset, 0, SimultaneousBindingMode.Unique)
        {
        }
    }


    public enum PippidonAction
    {
        [Description("Move up")]
        MoveUp,
        [Description("Move down")]
        MoveDown,
        [Description("Boost")]
        Boost
    }
}
