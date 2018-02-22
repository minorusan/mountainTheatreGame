using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAudioBehaviour : MonoBehaviour
{
	private float _originalPitch;
	private AudioSource _source;
	public AudioSettings settings;

	private void Start()
	{
		_source = GetComponent <AudioSource> ();
		_originalPitch = _source.pitch;
		var position = Camera.main.transform.position;
		var inputHandler = FindObjectOfType<InputBehaviour> ();

		inputHandler.beganHold += () => {
			PlaySoundWithSettingsForEvent (EAudioEventType.hold);
		};

		inputHandler.jumpedOffSurface += () => {
			PlaySoundWithSettingsForEvent (EAudioEventType.jumpedOffCliff);
		};

		inputHandler.reachedSurface += () => {
			PlaySoundWithSettingsForEvent (EAudioEventType.reachedSurface);
		};

		inputHandler.tappedWhileInAir += () => {

			PlaySoundWithSettingsForEvent (EAudioEventType.jumpedinAir);
		};

		inputHandler.releasedWhileInAir += () => {

			PlaySoundWithSettingsForEvent (EAudioEventType.releaseInTheAir);
		};
	}

	public void PlaySound(EAudioEventType type)
	{
		PlaySoundWithSettingsForEvent (type);
	}

	void OnPlayerKilled()
	{
		PlaySoundWithSettingsForEvent (EAudioEventType.death);
	}

	private void PlaySoundWithSettingsForEvent(EAudioEventType type)
	{
		var settingsForEvent = settings.settingsForEventType (type);
		_source.pitch = _originalPitch + Random.Range (-settingsForEvent.pitchVariety, settingsForEvent.pitchVariety);
		_source.volume = settingsForEvent.volume;
		_source.PlayOneShot (settingsForEvent.clip);
	}
}
