using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu (fileName = "Game settings", menuName = "Gameplay/Game settings/Game settings")]
public class GameSettings : ScriptableObject
{
	private static GameSettings _instance;

	public static GameSettings instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = Resources.Load <GameSettings> ("Settings/Game Settings");	
			}
			Debug.Assert (_instance != null, "GameSettings::No settings found.");
			return _instance;
		} 
	}

	[Header ("Scrolling mountain settings")]
	public int mountainPeriod;
	public float mountainSpeed;
	public float mountainSpeedDump;
}
