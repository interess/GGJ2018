using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DZ.Game.Scripts
{
	public class DevControlsUnit : MonoBehaviour
	{
		float _cachedWorldTime;

		void Update()
		{
			if (Input.GetKeyDown(KeyCode.F))
			{
				FinishLevel();
			}

			if (Input.GetKeyDown(KeyCode.G))
			{
				SkipModal();
			}

			if (Input.GetKeyDown(KeyCode.H))
			{
				if (Contexts.state.worldTime != 10)
				{
					_cachedWorldTime = Contexts.state.worldTime;
					Contexts.state.worldTime = 10;
				}
				else
				{
					Contexts.state.worldTime = _cachedWorldTime;
				}

				Contexts.state.levelActiveEntity.levelSubsSpeed = Contexts.state.worldTime;
			}
		}

		void FinishLevel()
		{
			var channelGroup = Contexts.state.channelGroup;
			foreach (var entity in channelGroup)
			{
				entity.channelFinished = true;
			}
		}

		void SkipModal()
		{
			if (Contexts.state.HasModalActive())
			{
				var modalActiveEntity = Contexts.state.modalActiveEntity;
				if (modalActiveEntity.HasModalUnit())
				{
					if (modalActiveEntity.modalUnit.__internalClosable)
					{
						var closeModalEvent = Contexts.input.CreateEventEntity();
						closeModalEvent.modalCloseEvent = true;
						closeModalEvent.modalId = modalActiveEntity.modalId;
					}
					else
					{
						modalActiveEntity.modalUnit.__InternalSkip();
					}
				}
			}
		}
	}
}
