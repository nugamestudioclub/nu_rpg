using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(
	fileName = nameof(DoorSpriteSheet),
	menuName = "Scriptable Objects/" + nameof(DoorSpriteSheet))
]
public class DoorSpriteSheet : ScriptableObject {
	[field: SerializeField]
	public Sprite Opened { get; private set; }

	[field: SerializeField]
	public Sprite Closed { get; private set; }
}