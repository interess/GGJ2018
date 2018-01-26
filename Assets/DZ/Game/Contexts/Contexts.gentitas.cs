using Entitas.Gentitas.Declaration;

namespace DZ.Game.Declaration
{
    public class Input : Context
    {
        public Input()
        {
            // Flag
            var FlagEvent = Component();
            var FlagTrash = Component();
            var FlagTrashValidated = Component();
        }
    }

    public class State : Context
    {
        public State()
        {
            // Flag
            var FlagTrash = Component();
            var FlagTrashValidated = Component();

            // Stage
            var StageManagerUnit = Component<Scripts.StageManagerUnit>().GroupSingle;
        }
    }
}
