using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPool : APoolable {

	private const float Y_THRESHOLD = -0.7f;
	private Transform trans;

	// Use this for initialization
	void Start () {
		trans = this.transform;
	}

	// Update is called once per frame
	void Update () {
		if(this.transform.localPosition.y <= Y_THRESHOLD && this.poolRef != null)
		{
			this.poolRef.ReleasePoolable(this);
		}
	}

	public override void Initialize()
	{
		Debug.Log("Shape Init");
	}

	public override void OnActivate()
	{

	}

	public override void Release()
	{
		this.transform.position = new Vector3(-0.889f, 0.229f, 0.0f);
		this.transform.rotation = trans.rotation;
		this.GetComponent<Rigidbody>().velocity = Vector3.zero;
	}
}
