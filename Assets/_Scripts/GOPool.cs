using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOPool : MonoBehaviour {

	[SerializeField] private PlayerPool poolableObjectCopy; //the poolable object copy
	[SerializeField] private Transform poolableParent; //where the poolable object will spawn in the hierarchy
	[SerializeField] private int maxPoolSize = 1; //the maxinum number of allowable reusable objects
	[SerializeField] private bool fixedAllocation = true; //if TRUE, number of poolable objects instantiated is fixed. User cannot create poolable objects during run-time.

	[SerializeField] private List<PlayerPool> availableObjects = new List<PlayerPool> ();
	[SerializeField] private List<PlayerPool> usedObjects = new List<PlayerPool>();

	// Use this for initialization
	void Start () {

		this.poolableObjectCopy.gameObject.SetActive (false); //hide the poolable object copy
	}

	public void Initialize() {
		for (int i = 0; i < this.maxPoolSize; i++) {

			PlayerPool poolableObject = new PlayerPool();

			poolableObject = GameObject.Instantiate<PlayerPool> (this.poolableObjectCopy, this.poolableParent);
			poolableObject.Initialize ();
			poolableObject.gameObject.SetActive (false);
			this.availableObjects.Add (poolableObject);

			/*
			for (int j = 0; j < 3; j++) {
				poolableObject[j] = GameObject.Instantiate<APoolable> (this.poolableObjectCopy[j], this.poolableParent[j]);
				poolableObject[j].Initialize ();
				poolableObject[j].gameObject.SetActive (false);
				this.availableObjects.Add (poolableObject[j]);
			}*/

		}
	}

	public bool HasObjectAvailable(int requestSize) {
		return this.availableObjects.Count >= requestSize;
	}

	public PlayerPool RequestPoolable() {
		if (this.HasObjectAvailable (1)) {
			PlayerPool poolableObject = this.availableObjects [this.availableObjects.Count - 1];
			poolableObject.SetPoolRef (this);
			this.availableObjects.RemoveAt (this.availableObjects.Count - 1);
			this.usedObjects.Add (poolableObject);

			poolableObject.gameObject.SetActive (true);
			poolableObject.OnActivate ();
			return poolableObject;
		} else {
			Debug.LogError ("[GameObjectPool] No more poolable object available!");
			return null;
		}
	}

	public PlayerPool RequestPoolableBatch(int size) {
		if (this.HasObjectAvailable(size)) {
			PlayerPool poolableObjects = new PlayerPool();

			poolableObjects = this.RequestPoolable ();

			return poolableObjects;
		} else {
			Debug.LogError ("[GameObjectPool] Insufficient objects available in pool. Count is: " + this.availableObjects.Count + " while requested is " + size + "!");
			return null;
		}
	}

	public void ReleasePoolable(PlayerPool poolableObject) {
		this.usedObjects.Remove (poolableObject);

		poolableObject.Release ();
		poolableObject.gameObject.SetActive (false);
		this.availableObjects.Add (poolableObject);
	}
}
