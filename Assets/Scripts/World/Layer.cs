using UnityEngine;
using System.Collections;

public class Layer : CacheMB 
{
	private Vector2 MaxDistance;
	private Vector2 StartPosition;

	private Vector2 MoveDirection;

	void Start()
	{
//		transform.position = Vector3.zero;

		StartPosition = (Vector2)transform.position;

		MaxDistance = Vector2.zero;

		for(int i = 0; i < transform.childCount; ++i)
		{
			MaxDistance += new Vector2(transform.GetChild(i).GetComponent<Renderer>().bounds.size.x, transform.GetChild(i).GetComponent<Renderer>().bounds.size.y);
		}

		MaxDistance = MaxDistance / 2;
	}
	
	public void Update()
	{
				if (Global.Instance.IsPlaying) {
						if (FindObjectOfType<PlayerController> ().CurPickup == PickupType.ColaBottle) {
								MoveDirection = new Vector2 (0F, -Global.Instance.Speed * 2);
						} else {
								MoveDirection = new Vector2 (0F, -Global.Instance.Speed);
						}

						transform.Translate (MoveDirection * Time.deltaTime);
						if (transform.name == "Layer") {
								if (transform.position.y <= -MaxDistance.y) {
										transform.position = StartPosition;
								}
						}
				}
	}
}
