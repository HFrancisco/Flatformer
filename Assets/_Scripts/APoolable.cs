using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class APoolable : MonoBehaviour {

	[SerializeField] protected GOPool poolRef;

	public void SetPoolRef(GOPool poolRef) {
		this.poolRef = poolRef;
	}

	public abstract void Initialize (); //initializes the property of this object.
	public abstract void Release (); //releases this object back to the pool and clean up any data.

	//events for APoolable Object
	public abstract void OnActivate(); //throws this event when this object has been activated from the pool.
}
