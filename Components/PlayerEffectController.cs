﻿using System.Collections;
using UnityEngine;

namespace Origins.Components
{
    internal class PlayerEffectController : MonoBehaviour
    {
		private PlayerCameraEffectController _cameraEffectController;

		private static PlayerEffectController _instance;

		private void Start()
		{
			_instance = this;
			_cameraEffectController = FindObjectOfType<PlayerCameraEffectController>();
		}

		public static void Blink(float length = 2) => _instance.StartCoroutine(_instance.BlinkCoroutine(length));
		public static void CloseEyes(float length) => _instance._cameraEffectController.CloseEyes(length);
		public static void OpenEyes(float length) => _instance._cameraEffectController.OpenEyes(length, false);
		public static void AddLock(float length) => _instance.StartCoroutine(_instance.LockCoroutine(length));

		private IEnumerator BlinkCoroutine(float length)
		{
			CloseEyes(length / 1f);
			yield return new WaitForSeconds(length / 1f);
			OpenEyes(length / 12f);
		}
		private IEnumerator LockCoroutine(float length)
		{
			OWInput.ChangeInputMode(InputMode.None);
			Locator.GetPauseCommandListener().AddPauseCommandLock();

			yield return new WaitForSeconds(length);

			OWInput.ChangeInputMode(InputMode.Character);
			Locator.GetPauseCommandListener().RemovePauseCommandLock();

		}

		private void DisableLock()
		{

		}

		public static void PlayAudioOneShot(AudioType audio, float volume) => Locator.GetPlayerAudioController()._oneShotExternalSource.PlayOneShot(audio, volume);
		public static void PlayAudioExternalOneShot(AudioClip audio, float volume = 2f) => Locator.GetPlayerAudioController()._oneShotExternalSource.PlayOneShot(audio, volume);
				

	}
}

