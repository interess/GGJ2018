// <auto-generated>
//     This code was generated with love by Gentitas.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>

using Entitas;
using System.Collections.Generic;

namespace DZ.Core {
	public partial class StateContext {
		//
		// Single Groups
		//

		// Application Group
		private IGroup<StateEntity> _applicationGroup;
		public IGroup<StateEntity> applicationGroup {
			get { 
				if (_applicationGroup == null) {
					_applicationGroup = GetGroup(StateMatcher.Application);
				}
				return _applicationGroup; } }

		public StateEntity applicationEntity {
			get {
				var cachedGroup = applicationGroup;
				if (cachedGroup.count > 1) {
					return null;
				}
				if (cachedGroup.count == 0) {
					return null;
				}
				return cachedGroup.GetSingleEntity(); } }

		public bool application { 
			get { return applicationGroup.count == 1; }	
			set {
				var cachedGroup = applicationGroup;
				if (cachedGroup.count > 1) throw new System.Exception("StateContext has more than 1 entity with component 'Application'. You can check safely with 'HasApplication()'");
				else if (cachedGroup.count == 0) {
					if (value) this.CreateEntity().application = value;
				}
				else {
					if (value) { applicationEntity.application = true; }
					else { applicationEntity.Destroy(); }
				}
			}
		}
		public bool HasApplication() {	return applicationGroup.count == 1;	}

		// LoadingManagerUnit Group
		private IGroup<StateEntity> _loadingManagerUnitGroup;
		public IGroup<StateEntity> loadingManagerUnitGroup {
			get { 
				if (_loadingManagerUnitGroup == null) {
					_loadingManagerUnitGroup = GetGroup(StateMatcher.LoadingManagerUnit);
				}
				return _loadingManagerUnitGroup; } }

		public StateEntity loadingManagerUnitEntity {
			get {
				var cachedGroup = loadingManagerUnitGroup;
				if (cachedGroup.count > 1) {
					return null;
				}
				if (cachedGroup.count == 0) {
					return null;
				}
				return cachedGroup.GetSingleEntity(); } }

		public Scripts.LoadingManagerUnit loadingManagerUnit { 
			get {
				if (loadingManagerUnitEntity == null) throw new System.Exception("StateContext has 0 or more than 1 entity with component 'LoadingManagerUnit'. You can check safely with 'HasLoadingManagerUnit()'");
				return loadingManagerUnitEntity.loadingManagerUnit;
			}	
			set { 
				if (loadingManagerUnitGroup.count > 1) throw new System.Exception("StateContext has more than 1 entity with component 'LoadingManagerUnit'. You can check safely with 'HasLoadingManagerUnit()'");
				else if (loadingManagerUnitGroup.count == 0) this.CreateEntity().loadingManagerUnit = value;
				else loadingManagerUnitEntity.loadingManagerUnit = value;
			}
		}
		public bool HasLoadingManagerUnit() {	return loadingManagerUnitGroup.count == 1;	}


		//
		// Groups
		//

	}


}
namespace DZ.Game {
	public partial class InputContext {
		//
		// Single Groups
		//


		//
		// Groups
		//

		// FlagTrashValidated Group
		private IGroup<InputEntity> _flagTrashValidatedGroup;
		public IGroup<InputEntity> flagTrashValidatedGroup {
			get { 
				if (_flagTrashValidatedGroup == null) {
					_flagTrashValidatedGroup = GetGroup(InputMatcher.FlagTrashValidated);
				}
				return _flagTrashValidatedGroup; } }

		public InputEntity[] flagTrashValidatedEntities { 
			get { return flagTrashValidatedGroup.GetEntities(); } }
		
	}

	public partial class StateContext {
		//
		// Single Groups
		//

		// StageManagerUnit Group
		private IGroup<StateEntity> _stageManagerUnitGroup;
		public IGroup<StateEntity> stageManagerUnitGroup {
			get { 
				if (_stageManagerUnitGroup == null) {
					_stageManagerUnitGroup = GetGroup(StateMatcher.StageManagerUnit);
				}
				return _stageManagerUnitGroup; } }

		public StateEntity stageManagerUnitEntity {
			get {
				var cachedGroup = stageManagerUnitGroup;
				if (cachedGroup.count > 1) {
					return null;
				}
				if (cachedGroup.count == 0) {
					return null;
				}
				return cachedGroup.GetSingleEntity(); } }

		public Scripts.StageManagerUnit stageManagerUnit { 
			get {
				if (stageManagerUnitEntity == null) throw new System.Exception("StateContext has 0 or more than 1 entity with component 'StageManagerUnit'. You can check safely with 'HasStageManagerUnit()'");
				return stageManagerUnitEntity.stageManagerUnit;
			}	
			set { 
				if (stageManagerUnitGroup.count > 1) throw new System.Exception("StateContext has more than 1 entity with component 'StageManagerUnit'. You can check safely with 'HasStageManagerUnit()'");
				else if (stageManagerUnitGroup.count == 0) this.CreateEntity().stageManagerUnit = value;
				else stageManagerUnitEntity.stageManagerUnit = value;
			}
		}
		public bool HasStageManagerUnit() {	return stageManagerUnitGroup.count == 1;	}

		// LevelActive Group
		private IGroup<StateEntity> _levelActiveGroup;
		public IGroup<StateEntity> levelActiveGroup {
			get { 
				if (_levelActiveGroup == null) {
					return GetGroup(Matcher<StateEntity>
								.AllOf(StateMatcher.Level, StateMatcher.FlagActive));
				}
				return _levelActiveGroup; } }

		public StateEntity levelActiveEntity {
			get {
				var cachedGroup = levelActiveGroup;
				if (cachedGroup.count > 1) {
					return null;
				}
				if (cachedGroup.count == 0) {
					return null;
				}
				return cachedGroup.GetSingleEntity(); } }

		public bool HasLevelActive() {	return levelActiveGroup.count == 1;	}

		// LevelActiveLoaded Group
		private IGroup<StateEntity> _levelActiveLoadedGroup;
		public IGroup<StateEntity> levelActiveLoadedGroup {
			get { 
				if (_levelActiveLoadedGroup == null) {
					return GetGroup(Matcher<StateEntity>
								.AllOf(StateMatcher.Level, StateMatcher.FlagActive, StateMatcher.FlagLoaded));
				}
				return _levelActiveLoadedGroup; } }

		public StateEntity levelActiveLoadedEntity {
			get {
				var cachedGroup = levelActiveLoadedGroup;
				if (cachedGroup.count > 1) {
					return null;
				}
				if (cachedGroup.count == 0) {
					return null;
				}
				return cachedGroup.GetSingleEntity(); } }

		public bool HasLevelActiveLoaded() {	return levelActiveLoadedGroup.count == 1;	}

		// AudioManagerUnit Group
		private IGroup<StateEntity> _audioManagerUnitGroup;
		public IGroup<StateEntity> audioManagerUnitGroup {
			get { 
				if (_audioManagerUnitGroup == null) {
					_audioManagerUnitGroup = GetGroup(StateMatcher.AudioManagerUnit);
				}
				return _audioManagerUnitGroup; } }

		public StateEntity audioManagerUnitEntity {
			get {
				var cachedGroup = audioManagerUnitGroup;
				if (cachedGroup.count > 1) {
					return null;
				}
				if (cachedGroup.count == 0) {
					return null;
				}
				return cachedGroup.GetSingleEntity(); } }

		public Scripts.AudioManagerUnit audioManagerUnit { 
			get {
				if (audioManagerUnitEntity == null) throw new System.Exception("StateContext has 0 or more than 1 entity with component 'AudioManagerUnit'. You can check safely with 'HasAudioManagerUnit()'");
				return audioManagerUnitEntity.audioManagerUnit;
			}	
			set { 
				if (audioManagerUnitGroup.count > 1) throw new System.Exception("StateContext has more than 1 entity with component 'AudioManagerUnit'. You can check safely with 'HasAudioManagerUnit()'");
				else if (audioManagerUnitGroup.count == 0) this.CreateEntity().audioManagerUnit = value;
				else audioManagerUnitEntity.audioManagerUnit = value;
			}
		}
		public bool HasAudioManagerUnit() {	return audioManagerUnitGroup.count == 1;	}

		// AudioEffectManagerUnit Group
		private IGroup<StateEntity> _audioEffectManagerUnitGroup;
		public IGroup<StateEntity> audioEffectManagerUnitGroup {
			get { 
				if (_audioEffectManagerUnitGroup == null) {
					_audioEffectManagerUnitGroup = GetGroup(StateMatcher.AudioEffectManagerUnit);
				}
				return _audioEffectManagerUnitGroup; } }

		public StateEntity audioEffectManagerUnitEntity {
			get {
				var cachedGroup = audioEffectManagerUnitGroup;
				if (cachedGroup.count > 1) {
					return null;
				}
				if (cachedGroup.count == 0) {
					return null;
				}
				return cachedGroup.GetSingleEntity(); } }

		public Scripts.AudioEffectManagerUnit audioEffectManagerUnit { 
			get {
				if (audioEffectManagerUnitEntity == null) throw new System.Exception("StateContext has 0 or more than 1 entity with component 'AudioEffectManagerUnit'. You can check safely with 'HasAudioEffectManagerUnit()'");
				return audioEffectManagerUnitEntity.audioEffectManagerUnit;
			}	
			set { 
				if (audioEffectManagerUnitGroup.count > 1) throw new System.Exception("StateContext has more than 1 entity with component 'AudioEffectManagerUnit'. You can check safely with 'HasAudioEffectManagerUnit()'");
				else if (audioEffectManagerUnitGroup.count == 0) this.CreateEntity().audioEffectManagerUnit = value;
				else audioEffectManagerUnitEntity.audioEffectManagerUnit = value;
			}
		}
		public bool HasAudioEffectManagerUnit() {	return audioEffectManagerUnitGroup.count == 1;	}

		// MusicManagerUnit Group
		private IGroup<StateEntity> _musicManagerUnitGroup;
		public IGroup<StateEntity> musicManagerUnitGroup {
			get { 
				if (_musicManagerUnitGroup == null) {
					_musicManagerUnitGroup = GetGroup(StateMatcher.MusicManagerUnit);
				}
				return _musicManagerUnitGroup; } }

		public StateEntity musicManagerUnitEntity {
			get {
				var cachedGroup = musicManagerUnitGroup;
				if (cachedGroup.count > 1) {
					return null;
				}
				if (cachedGroup.count == 0) {
					return null;
				}
				return cachedGroup.GetSingleEntity(); } }

		public Scripts.MusicManagerUnit musicManagerUnit { 
			get {
				if (musicManagerUnitEntity == null) throw new System.Exception("StateContext has 0 or more than 1 entity with component 'MusicManagerUnit'. You can check safely with 'HasMusicManagerUnit()'");
				return musicManagerUnitEntity.musicManagerUnit;
			}	
			set { 
				if (musicManagerUnitGroup.count > 1) throw new System.Exception("StateContext has more than 1 entity with component 'MusicManagerUnit'. You can check safely with 'HasMusicManagerUnit()'");
				else if (musicManagerUnitGroup.count == 0) this.CreateEntity().musicManagerUnit = value;
				else musicManagerUnitEntity.musicManagerUnit = value;
			}
		}
		public bool HasMusicManagerUnit() {	return musicManagerUnitGroup.count == 1;	}

		// SubsManagerUnit Group
		private IGroup<StateEntity> _subsManagerUnitGroup;
		public IGroup<StateEntity> subsManagerUnitGroup {
			get { 
				if (_subsManagerUnitGroup == null) {
					_subsManagerUnitGroup = GetGroup(StateMatcher.SubsManagerUnit);
				}
				return _subsManagerUnitGroup; } }

		public StateEntity subsManagerUnitEntity {
			get {
				var cachedGroup = subsManagerUnitGroup;
				if (cachedGroup.count > 1) {
					return null;
				}
				if (cachedGroup.count == 0) {
					return null;
				}
				return cachedGroup.GetSingleEntity(); } }

		public Scripts.SubsManagerUnit subsManagerUnit { 
			get {
				if (subsManagerUnitEntity == null) throw new System.Exception("StateContext has 0 or more than 1 entity with component 'SubsManagerUnit'. You can check safely with 'HasSubsManagerUnit()'");
				return subsManagerUnitEntity.subsManagerUnit;
			}	
			set { 
				if (subsManagerUnitGroup.count > 1) throw new System.Exception("StateContext has more than 1 entity with component 'SubsManagerUnit'. You can check safely with 'HasSubsManagerUnit()'");
				else if (subsManagerUnitGroup.count == 0) this.CreateEntity().subsManagerUnit = value;
				else subsManagerUnitEntity.subsManagerUnit = value;
			}
		}
		public bool HasSubsManagerUnit() {	return subsManagerUnitGroup.count == 1;	}

		// ChannelActive Group
		private IGroup<StateEntity> _channelActiveGroup;
		public IGroup<StateEntity> channelActiveGroup {
			get { 
				if (_channelActiveGroup == null) {
					return GetGroup(Matcher<StateEntity>
								.AllOf(StateMatcher.Channel, StateMatcher.FlagActive));
				}
				return _channelActiveGroup; } }

		public StateEntity channelActiveEntity {
			get {
				var cachedGroup = channelActiveGroup;
				if (cachedGroup.count > 1) {
					return null;
				}
				if (cachedGroup.count == 0) {
					return null;
				}
				return cachedGroup.GetSingleEntity(); } }

		public bool HasChannelActive() {	return channelActiveGroup.count == 1;	}

		// PhoneManagerUnit Group
		private IGroup<StateEntity> _phoneManagerUnitGroup;
		public IGroup<StateEntity> phoneManagerUnitGroup {
			get { 
				if (_phoneManagerUnitGroup == null) {
					_phoneManagerUnitGroup = GetGroup(StateMatcher.PhoneManagerUnit);
				}
				return _phoneManagerUnitGroup; } }

		public StateEntity phoneManagerUnitEntity {
			get {
				var cachedGroup = phoneManagerUnitGroup;
				if (cachedGroup.count > 1) {
					return null;
				}
				if (cachedGroup.count == 0) {
					return null;
				}
				return cachedGroup.GetSingleEntity(); } }

		public Scripts.PhoneManagerUnit phoneManagerUnit { 
			get {
				if (phoneManagerUnitEntity == null) throw new System.Exception("StateContext has 0 or more than 1 entity with component 'PhoneManagerUnit'. You can check safely with 'HasPhoneManagerUnit()'");
				return phoneManagerUnitEntity.phoneManagerUnit;
			}	
			set { 
				if (phoneManagerUnitGroup.count > 1) throw new System.Exception("StateContext has more than 1 entity with component 'PhoneManagerUnit'. You can check safely with 'HasPhoneManagerUnit()'");
				else if (phoneManagerUnitGroup.count == 0) this.CreateEntity().phoneManagerUnit = value;
				else phoneManagerUnitEntity.phoneManagerUnit = value;
			}
		}
		public bool HasPhoneManagerUnit() {	return phoneManagerUnitGroup.count == 1;	}

		// WorldTime Group
		private IGroup<StateEntity> _worldTimeGroup;
		public IGroup<StateEntity> worldTimeGroup {
			get { 
				if (_worldTimeGroup == null) {
					_worldTimeGroup = GetGroup(StateMatcher.WorldTime);
				}
				return _worldTimeGroup; } }

		public StateEntity worldTimeEntity {
			get {
				var cachedGroup = worldTimeGroup;
				if (cachedGroup.count > 1) {
					return null;
				}
				if (cachedGroup.count == 0) {
					return null;
				}
				return cachedGroup.GetSingleEntity(); } }

		public float worldTime { 
			get {
				if (worldTimeEntity == null) throw new System.Exception("StateContext has 0 or more than 1 entity with component 'WorldTime'. You can check safely with 'HasWorldTime()'");
				return worldTimeEntity.worldTime;
			}	
			set { 
				if (worldTimeGroup.count > 1) throw new System.Exception("StateContext has more than 1 entity with component 'WorldTime'. You can check safely with 'HasWorldTime()'");
				else if (worldTimeGroup.count == 0) this.CreateEntity().worldTime = value;
				else worldTimeEntity.worldTime = value;
			}
		}
		public bool HasWorldTime() {	return worldTimeGroup.count == 1;	}


		//
		// Groups
		//

		// TrashTimer Group
		private IGroup<StateEntity> _trashTimerGroup;
		public IGroup<StateEntity> trashTimerGroup {
			get { 
				if (_trashTimerGroup == null) {
					_trashTimerGroup = GetGroup(StateMatcher.TrashTimer);
				}
				return _trashTimerGroup; } }

		public StateEntity[] trashTimerEntities { 
			get { return trashTimerGroup.GetEntities(); } }
		
		// LevelPart Group
		private IGroup<StateEntity> _levelPartGroup;
		public IGroup<StateEntity> levelPartGroup {
			get { 
				if (_levelPartGroup == null) {
					_levelPartGroup = GetGroup(StateMatcher.LevelPart);
				}
				return _levelPartGroup; } }

		public StateEntity[] levelPartEntities { 
			get { return levelPartGroup.GetEntities(); } }
		
		// Level Group
		private IGroup<StateEntity> _levelGroup;
		public IGroup<StateEntity> levelGroup {
			get { 
				if (_levelGroup == null) {
					_levelGroup = GetGroup(StateMatcher.Level);
				}
				return _levelGroup; } }

		public StateEntity[] levelEntities { 
			get { return levelGroup.GetEntities(); } }
		
		// View Group
		private IGroup<StateEntity> _viewGroup;
		public IGroup<StateEntity> viewGroup {
			get { 
				if (_viewGroup == null) {
					_viewGroup = GetGroup(StateMatcher.View);
				}
				return _viewGroup; } }

		public StateEntity[] viewEntities { 
			get { return viewGroup.GetEntities(); } }
		
		// Channel Group
		private IGroup<StateEntity> _channelGroup;
		public IGroup<StateEntity> channelGroup {
			get { 
				if (_channelGroup == null) {
					_channelGroup = GetGroup(StateMatcher.Channel);
				}
				return _channelGroup; } }

		public StateEntity[] channelEntities { 
			get { return channelGroup.GetEntities(); } }
		
		// ConfigId Group
		private IGroup<StateEntity> _configIdGroup;
		public IGroup<StateEntity> configIdGroup {
			get { 
				if (_configIdGroup == null) {
					_configIdGroup = GetGroup(StateMatcher.ConfigId);
				}
				return _configIdGroup; } }

		public StateEntity[] configIdEntities { 
			get { return configIdGroup.GetEntities(); } }
		
	}


}
