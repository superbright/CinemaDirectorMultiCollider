using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CollisionHandler : MonoBehaviour {

	// Event fired when Cutscene starts to play or is resumed.
	public event CollisionTriggeredHandler onCollisionTriggered;
	public int index;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {
		Debug.Log ("other " + other.name);
		if (onCollisionTriggered != null)
		{
			onCollisionTriggered(this, new CollisionEventArgs(index));
		}
	}


}

// Delegate for handling Cutscene Events
public delegate void CollisionTriggeredHandler(object sender, CollisionEventArgs e);

/// <summary>
/// Cutscene event arguments. Blank for now.
/// </summary>
public class CollisionEventArgs : EventArgs
{

	public int index;

	public CollisionEventArgs()
	{

	}

	public CollisionEventArgs(int _index)
	{
		index = _index;
	}
}
