using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.ObjectPooling;


public class PlayerAudioBehaviour : MonoBehaviour
{
	private float _originalPitch;

	public PlayerAudioPlayerBehaviour prefab;
	public AudioSettings settings;

	private void Start()
	{
		PoolManager.Instance.CreatePool (prefab.gameObject, 20);

		var inputHandler = FindObjectOfType<InputBehaviour> ();

		inputHandler.beganHold += () => {
			var settingsForEvent = settings.settingsForEventType (EAudioEventType.hold);
			PoolManager.Instance.ReuseObject (prefab.gameObject, transform.position, Quaternion.identity, settingsForEvent);
		};

		inputHandler.jumpedOffSurface += () => {
			var settingsForEvent = settings.settingsForEventType (EAudioEventType.jumpedOffCliff);
			PoolManager.Instance.ReuseObject (prefab.gameObject, transform.position, Quaternion.identity, settingsForEvent);
		};

		inputHandler.reachedSurface += () => {
			var settingsForEvent = settings.settingsForEventType (EAudioEventType.reachedSurface);
			PoolManager.Instance.ReuseObject (prefab.gameObject, transform.position, Quaternion.identity, settingsForEvent);
		};

		inputHandler.tappedWhileInAir += () => {

			var settingsForEvent = settings.settingsForEventType (EAudioEventType.jumpedinAir);
			PoolManager.Instance.ReuseObject (prefab.gameObject, transform.position, Quaternion.identity, settingsForEvent);
		};

		inputHandler.releasedWhileInAir += () => {

			var settingsForEvent = settings.settingsForEventType (EAudioEventType.releaseInTheAir);
			PoolManager.Instance.ReuseObject (prefab.gameObject, transform.position, Quaternion.identity, settingsForEvent);
		};
	}

	public void PlaySound(EAudioEventType type)
	{
		var settingsForEvent = settings.settingsForEventType (type);
		PoolManager.Instance.ReuseObject (prefab.gameObject, transform.position, Quaternion.identity, settingsForEvent);
	}

	void OnPlayerKilled()
	{
		var settingsForEvent = settings.settingsForEventType (EAudioEventType.death);
		PoolManager.Instance.ReuseObject (prefab.gameObject, transform.position, Quaternion.identity, settingsForEvent);
	}
}
