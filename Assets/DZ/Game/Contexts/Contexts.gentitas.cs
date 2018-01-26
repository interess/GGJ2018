using Entitas.Gentitas.Declaration;

namespace DZ.Game.Declaration
{
    public class Input : Context
    {
        public Input()
        {
            // Flag
            var FlagEvent = Component();
            var FlagTrash = Component().Universal;
            var FlagTrashValidated = Component().Universal.Group;

            // Trash
            var TrashTimer = Component<float>().Universal;
        }
    }

    public class State : Context
    {
        public State()
        {
            // Flag
            var FlagTrash = Component().Universal;
            var FlagTrashValidated = Component().Universal;

            // Trash
            var TrashTimer = Component<float>().Universal.Group;

            // Stage
            var StageManagerUnit = Component<Scripts.StageManagerUnit>().GroupSingle;

            // Level
            var LevelPart = Component().Group;

            // Uid
            var Uid = Component<int>().Index;
            var UidUnit = Component<FS.Uid.Scripts.UidUnit>();

            // Product
            var ProductUnit = Component<FS.PrefabFactory.Scripts.ProductUnit>();
        }
    }
}
