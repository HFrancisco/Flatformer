using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsHandler : MonoBehaviour {

	private const float Y_THRESHOLD = 0.0f;

	[SerializeField] private GameObject fallableObjects;
	[SerializeField] private GOPool playerPool;
	[SerializeField] private int spawnSize = 5;

	private Vector3 originPositions;

	private const float TIME_DELAY = 0.25f;
	private float ticks = 0.0f;

	// Use this for initialization
	void Start()
	{
		this.originPositions = new Vector3();
		this.StoreOriginPositions();

		this.playerPool.Initialize();
	}

	// Update is called once per frame
	void Update ()
	{

		if(this.fallableObjects.transform.position.y <= Y_THRESHOLD)
		{
			this.ResetToOrigin();
		}

		//spawn N objects periodically
		this.ticks += Time.deltaTime;
		if (this.ticks >= TIME_DELAY)
		{
			this.ticks = 0.0f;

			//do the spawning
			PlayerPool poolableObject = this.playerPool.RequestPoolableBatch(this.spawnSize);
			if (poolableObject == null)
			{
				return;
			}
		}
	}

	private void StoreOriginPositions()
	{
		originPositions = this.fallableObjects.transform.position;
		Debug.Log ("OP is " + originPositions);
	}

	private void ResetToOrigin()
	{
		this.fallableObjects.transform.localPosition = originPositions;
		//this.fallableObjects[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
	}
}
