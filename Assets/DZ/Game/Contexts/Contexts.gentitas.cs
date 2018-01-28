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

            // Subs
            var SubsRecordStartEvent = Component();
            var SubsRecordStopEvent = Component();

            // Modal
            var ModalOpenEvent = Component();
            var ModalCloseEvent = Component();
            var ModalId = Component<string>();
            var ModalOpenBackgroundEvent = Component();

            // Level
            var LevelEvent = Component();
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
            var FlagOpened = Component();

            // Trash
            var TrashTimer = Component<float>().Universal.Group;

            // Factory
            var FactoryUnit = Component<FS.PrefabFactory.Scripts.FactoryUnit>();
            var FactoryId = Component<string>().Index;

            // Stage
            var StageManagerUnit = Component<Scripts.StageManagerUnit>().GroupSingle;

            // Level
            var LevelPart = Component().Group;
            var Level = Component().Group;
            var LevelIndex = Component<int>().Index;
            var LevelControllerUnit = Component<Scripts.LevelControllerUnit>();
            var LevelSubsSpeed = Component<float>();

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

            // Music
            var MusicManagerUnit = Component<Scripts.MusicManagerUnit>().GroupSingle;

            // Subs
            var SubsManagerUnit = Component<Scripts.SubsManagerUnit>().GroupSingle;

            // Channels
            var Channel = Component<int>().Index.Group;
            var ChannelInfoUnit = Component<Scripts.ChannelInfoUnit>().GroupSingle;
            var ChannelRecording = Component();
            var ChannelVoiceActive = Component();

            var ChannelActive = Group(Matcher.All(Channel, FlagActive)).Single;

            // Phone
            var PhoneChannelUnit = Component<Scripts.PhoneChannelUnit>();
            var PhoneManagerUnit = Component<Scripts.PhoneManagerUnit>().GroupSingle;

            // WorldTime
            var WorldTime = Component<float>().GroupSingle;
            var WorldTimeSpeed = Component<float>();

            // Effect
            var Effect = Component();
            var EffectId = Component<string>();
            var EffectFactoryUnit = Component<Scripts.EffectFactoryUnit>();

            // Config
            var ConfigId = Component<string>().Group.Index;

            // Position
            var PositionWorld = Component<UnityEngine.Vector2>();

            // MenuButton
            var MenuButtonUnit = Component<Scripts.MenuButtonUnit>();
            var MenuButtonId = Component<string>().Index.Group;

            // Light
            var LightUnit = Component<Scripts.LightUnit>();
            var LigthId = Component<string>().Index.Group;

            // Modal
            var ModalUnit = Component<Scripts.ModalUnit>();
            var ModalId = Component<string>().Index;

            var ModalOpened = Group(Matcher.All(ModalUnit, FlagOpened));
            var ModalActive = Group(Matcher.All(ModalUnit, FlagOpened, FlagActive)).Single;

            // Overlay
            var OverlayUnit = Component<Scripts.OverlayUnit>().GroupSingle;

            // Stick
            var StickUnit = Component<Scripts.StickUnit>().GroupSingle;

            // Character
            var CharacterUnit = Component<Scripts.CharacterUnit>().GroupSingle;

            // HUD
            var HudUnit = Component<Scripts.HudUnit>().GroupSingle;

            // Ticket
            var TicketManagerUnit = Component<Scripts.TicketManagerUnit>().GroupSingle;

        }
    }
}
