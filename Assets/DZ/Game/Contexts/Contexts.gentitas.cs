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

            // Event
            var EventId = Component<string>();
        }
    }

    public class State : Context
    {
        public State()
        {
            // Flag
            var FlagTrash = Component().Universal;
            var FlagTrashValidated = Component().Universal;
            var FlagLoaded = Component();

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

            // View
            var View = Component().Group;

            // Audio
            var AudioManagerUnit = Component<Scripts.AudioManagerUnit>().GroupSingle;
            var AudioEffectManagerUnit = Component<Scripts.AudioEffectManagerUnit>().GroupSingle;
            var AudioEffectUnit = Component<Scripts.AudioEffectUnit>();
            var AudioEffectProductUnit = Component<Scripts.AudioEffectProductUnit>();

            // Subs
            var SubsManagerUnit = Component<Scripts.SubsManagerUnit>().GroupSingle;
        }
    }
}
