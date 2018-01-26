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