// <auto-generated>
//     This code was generated with love by Gentitas.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>

using UnityEngine;
using System.Collections.Generic;

namespace DZ.Core.Components {
    namespace State {
        // Flag Components
        public partial class Loaded: Entitas.IComponent { }
        public partial class Application: Entitas.IComponent { }
        // Value Components
        public partial class LoadingProgress: Entitas.IComponent { public float value; }
        public partial class SceneName: Entitas.IComponent { public string value; }
        public partial class LoadingManagerUnit: Entitas.IComponent { public Scripts.LoadingManagerUnit value; }
        public partial class LoadingSeconds: Entitas.IComponent { public float value; }
    }
}
namespace DZ.Game.Components {
    namespace Input {
        // Flag Components
        public partial class FlagEvent: Entitas.IComponent { }
        public partial class FlagTrash: Entitas.IComponent { }
        public partial class FlagTrashValidated: Entitas.IComponent { }
        public partial class ChannelSwitchEvent: Entitas.IComponent { }
        public partial class SubsRecordStartEvent: Entitas.IComponent { }
        public partial class SubsRecordStopEvent: Entitas.IComponent { }
        public partial class ModalOpenEvent: Entitas.IComponent { }
        public partial class ModalCloseEvent: Entitas.IComponent { }
        // Value Components
        public partial class TrashTimer: Entitas.IComponent { public float value; }
        public partial class EventId: Entitas.IComponent { public string value; }
        public partial class ModalId: Entitas.IComponent { public string value; }
    }
    namespace State {
        // Flag Components
        public partial class FlagTrash: Entitas.IComponent { }
        public partial class FlagTrashValidated: Entitas.IComponent { }
        public partial class FlagLoaded: Entitas.IComponent { }
        public partial class FlagActive: Entitas.IComponent { }
        public partial class FlagOpened: Entitas.IComponent { }
        public partial class LevelPart: Entitas.IComponent { }
        public partial class Level: Entitas.IComponent { }
        public partial class View: Entitas.IComponent { }
        public partial class ChannelRecording: Entitas.IComponent { }
        public partial class ChannelVoiceActive: Entitas.IComponent { }
        public partial class Effect: Entitas.IComponent { }
        // Value Components
        public partial class TrashTimer: Entitas.IComponent { public float value; }
        public partial class FactoryUnit: Entitas.IComponent { public FS.PrefabFactory.Scripts.FactoryUnit value; }
        public partial class FactoryId: Entitas.IComponent { public string value; }
        public partial class StageManagerUnit: Entitas.IComponent { public Scripts.StageManagerUnit value; }
        public partial class LevelIndex: Entitas.IComponent { public int value; }
        public partial class LevelControllerUnit: Entitas.IComponent { public Scripts.LevelControllerUnit value; }
        public partial class Uid: Entitas.IComponent { public int value; }
        public partial class UidUnit: Entitas.IComponent { public FS.Uid.Scripts.UidUnit value; }
        public partial class ProductUnit: Entitas.IComponent { public FS.PrefabFactory.Scripts.ProductUnit value; }
        public partial class AudioManagerUnit: Entitas.IComponent { public Scripts.AudioManagerUnit value; }
        public partial class AudioEffectManagerUnit: Entitas.IComponent { public Scripts.AudioEffectManagerUnit value; }
        public partial class AudioEffectUnit: Entitas.IComponent { public Scripts.AudioEffectUnit value; }
        public partial class AudioEffectProductUnit: Entitas.IComponent { public Scripts.AudioEffectProductUnit value; }
        public partial class MusicManagerUnit: Entitas.IComponent { public Scripts.MusicManagerUnit value; }
        public partial class SubsManagerUnit: Entitas.IComponent { public Scripts.SubsManagerUnit value; }
        public partial class Channel: Entitas.IComponent { public int value; }
        public partial class ChannelInfoUnit: Entitas.IComponent { public Scripts.ChannelInfoUnit value; }
        public partial class PhoneChannelUnit: Entitas.IComponent { public Scripts.PhoneChannelUnit value; }
        public partial class PhoneManagerUnit: Entitas.IComponent { public Scripts.PhoneManagerUnit value; }
        public partial class WorldTime: Entitas.IComponent { public float value; }
        public partial class WorldTimeSpeed: Entitas.IComponent { public float value; }
        public partial class EffectId: Entitas.IComponent { public string value; }
        public partial class EffectFactoryUnit: Entitas.IComponent { public Scripts.EffectFactoryUnit value; }
        public partial class ConfigId: Entitas.IComponent { public string value; }
        public partial class PositionWorld: Entitas.IComponent { public UnityEngine.Vector2 value; }
        public partial class MenuButtonUnit: Entitas.IComponent { public Scripts.MenuButtonUnit value; }
        public partial class MenuButtonId: Entitas.IComponent { public string value; }
        public partial class LightUnit: Entitas.IComponent { public Scripts.LightUnit value; }
        public partial class LigthId: Entitas.IComponent { public string value; }
        public partial class ModalUnit: Entitas.IComponent { public Scripts.ModalUnit value; }
        public partial class ModalId: Entitas.IComponent { public string value; }
        public partial class OverlayUnit: Entitas.IComponent { public Scripts.OverlayUnit value; }
    }
}
