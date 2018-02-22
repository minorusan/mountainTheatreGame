using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu (fileName = "Player input settings", menuName = "Gameplay/Game settings/Player input settings")]
public class PlayerInputSettings : ScriptableObject
{
	public float speed;
	public float maxX;
	public float reachingSurface;

	[Header ("Initial jump settings")]
	public float tapTimeTrashhold;
	public float initialJumpLenght;
	public float jumpPower;
	public float jumpTime;
}
