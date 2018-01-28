using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace DZ.Game.Scripts
{
	public class TicketManagerUnit : MonoBehaviour
	{
		private AudioSource __audioSource;
		protected AudioSource _audioSource { get { if (__audioSource == null) { __audioSource = GetComponent<AudioSource>(); } return __audioSource; } }

		public AudioClip[] bossAudio;
		public Animator doorAnimator;
		public RectTransform ticketAnchor;
		public RectTransform raportAnchor;
		public RectTransform warningAnchor;

		public RectTransform[] raportTickets;
		public RectTransform[] warningTickets;

		private int numberOfRaports;
		private int numberOfWarnings;

		public void Init(int numberOfRaports, int numberOfWarnings)
		{
			doorAnimator.SetBool("Opened", false);

			for (int i = 0; i < 3; i++)
			{
				var raportTicket = raportTickets[i];
				var warningTicket = warningTickets[i];

				if (i < numberOfRaports)
				{
					raportTicket.GetComponent<Image>().color = Color.white;
				}
				else
				{
					var color = Color.white;
					color.a = 0f;
					raportTicket.GetComponent<Image>().color = color;
				}

				if (i < numberOfRaports)
				{
					warningTicket.GetComponent<Image>().color = Color.white;
				}
				else
				{
					var color = Color.white;
					color.a = 0f;
					warningTicket.GetComponent<Image>().color = color;
				}
			}
		}

		[FreakingEditor.FbuttonPlay]
		public void AddRaport(bool final)
		{
			if (!final)
			{
				if (numberOfRaports >= 3)
				{
					final = true;
				}
			}

			doorAnimator.SetBool("Opened", true);

			var randomBossAudio = Random.Range(0, bossAudio.Length);
			_audioSource.clip = bossAudio[randomBossAudio];
			_audioSource.Play();

			var nextRaport = raportTickets[numberOfRaports];
			nextRaport.position = ticketAnchor.position;
			nextRaport.localScale = new Vector3(1.2f, 1.2f, 1.2f);
			nextRaport.GetComponent<Image>().DOFade(1f, 0.1f);
			nextRaport.DOScale(Vector3.one, 0.3f).OnComplete(() =>
			{
				Freaking.Fwait.ForSeconds(1.5f).Done(() =>
				{
					nextRaport.DOMove(raportAnchor.position, 0.5f).SetEase(Ease.OutQuart);
				});
			});

			numberOfRaports++;

			if (numberOfRaports >= 3) { numberOfRaports = 0; }

			if (!final)
			{
				Freaking.Fwait.ForSeconds(5f).Done(() =>
				{
					doorAnimator.SetBool("Opened", false);
					Contexts.state.CreateEffectEntity("DoorCloseEffect");
				});
			}
		}

		[FreakingEditor.FbuttonPlay]
		public void AddWarning(bool final)
		{
			if (!final)
			{
				if (numberOfWarnings >= 3)
				{
					final = true;
				}
			}

			doorAnimator.SetBool("Opened", true);

			var nextWarning = warningTickets[numberOfWarnings];
			nextWarning.position = ticketAnchor.position;
			nextWarning.localScale = new Vector3(1.2f, 1.2f, 1.2f);
			nextWarning.GetComponent<Image>().DOFade(1f, 0.1f);
			nextWarning.DOScale(Vector3.one, 0.3f).OnComplete(() =>
			{
				Freaking.Fwait.ForSeconds(1.5f).Done(() =>
				{
					nextWarning.DOMove(warningAnchor.position, 0.5f).SetEase(Ease.OutQuart);
				});
			});

			var randomBossAudio = Random.Range(0, bossAudio.Length);
			_audioSource.clip = bossAudio[randomBossAudio];
			_audioSource.Play();

			numberOfWarnings++;

			if (numberOfWarnings >= 3) { numberOfWarnings = 0; }

			Freaking.Fwait.ForSeconds(2f).Done(() =>
			{
				doorAnimator.SetBool("Opened", false);
				Contexts.state.CreateEffectEntity("DoorCloseEffect");

				if (final)
				{
					foreach (var item in warningTickets)
					{
						item.GetComponent<Image>().DOFade(0, 0.3f);
					}
				}
			});
		}
	}
}
