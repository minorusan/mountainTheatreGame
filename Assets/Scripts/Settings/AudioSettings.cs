using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.DemiLib;


public enum EAudioEventType
{
	jumpedOffCliff,
	reachedSurface,
	jumpedinAir,
	releaseInTheAir,
	death,
	hold,
	rocksFalling,
	coinPicked
}

[SerializableAttribute]
public struct AudioSetting
{
	public EAudioEventType type;
	public AudioClip[] clips;

	public AudioClip getClip()
	{
		return clips [UnityEngine.Random.Range (0, clips.Length - 1)];
	}

	[Range (0f, 1f)]
	public float volume;
	public float pitchVariety;
}

[CreateAssetMenu (fileName = "Player audio settings", menuName = "Gameplay/Game settings/Player audio settings")]
public class AudioSettings : ScriptableObject
{
	private Dictionary<EAudioEventType, AudioSetting> _settings;

	public AudioSetting[] settings;

	private void OnEnable()
	{
		_settings = new Dictionary<EAudioEventType, AudioSetting> ();
		for (int i = 0; i < settings.Length; i++)
		{
			_settings.Add (settings [i].type, settings [i]);
		}
	}

	private void OnValidate()
	{
		OnEnable ();
	}

	public AudioSetting settingsForEventType(EAudioEventType type)
	{
		return _settings [type];
	}
}
