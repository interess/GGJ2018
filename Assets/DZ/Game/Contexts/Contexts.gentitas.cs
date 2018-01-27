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

            // Channel
            var ChannelSwitchEvent = Component();
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
            var FlagActive = Component();

            // Trash
            var TrashTimer = Component<float>().Universal.Group;

            // Stage
            var StageManagerUnit = Component<Scripts.StageManagerUnit>().GroupSingle;

            // Level
            var LevelPart = Component().Group;
            var Level = Component().Group;
            var LevelIndex = Component<int>().Index;

            var LevelActive = Group(Matcher.All(Level, FlagActive)).Single;
            var LevelActiveLoaded = Group(Matcher.All(Level, FlagActive, FlagLoaded)).Single;

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

            // Channels
            var Channel = Component<int>().Index.Group;
            var ChannelInfoUnit = Component<Scripts.ChannelInfoUnit>();

            var ChannelActive = Group(Matcher.All(Channel, FlagActive)).Single;

            // WorldTime
            var WorldTime = Component<float>();
            var WorldTimeSpeed = Component<float>();
        }
    }
}
