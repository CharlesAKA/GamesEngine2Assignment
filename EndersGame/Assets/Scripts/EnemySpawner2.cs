using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner2 : MonoBehaviour {

	public float leaders = 3;
	public float gapL = 10;
	public float gap = 10;
	public float followers = 1;
	public GameObject prefab;

	private void Awake ()
	{
		for (int i = 1; i <= leaders; i++) {
			Vector3 rndPos = new Vector3 (Random.Range ((-leaders * gapL), (leaders * gapL)),Random.Range ((-leaders * gapL), (leaders * gapL)), Random.Range ((-leaders * gapL), (leaders * gapL)));
			CreateLeaders (rndPos);
		}
	}

	void CreateLeaders (Vector3 newpos)
	{
		GameObject leader = GameObject.Instantiate<GameObject> (prefab);
		leader.transform.parent = this.transform;
		leader.transform.position = this.transform.TransformPoint (newpos);
		leader.transform.rotation = this.transform.rotation;


		Seek seek = leader.AddComponent<Seek> ();
		seek.enabled = !seek.enabled;
		Boid b = leader.GetComponent<Boid> ();
		b.maxSpeed = 5;


		for (int i = 1; i <= followers; i++) {
			Vector3 offset = new Vector3 (gap * i, 0, -gap * i);
			CreateFollower (offset, leader.GetComponent<Boid> ());
			offset = new Vector3 (-gap * i, 0, -gap * i);
			CreateFollower (offset, leader.GetComponent<Boid> ());            
		}
	}

	void CreateFollower (Vector3 offset, Boid leader)
	{
		GameObject follower = GameObject.Instantiate<GameObject> (prefab);
		follower.transform.position = leader.transform.TransformPoint (offset);
		follower.transform.parent = this.transform;
		follower.transform.rotation = this.transform.rotation;

		OffsetPursue op = follower.AddComponent<OffsetPursue> ();
		op.leader = leader;
		Seek seek = follower.AddComponent<Seek> ();
		seek.enabled = !seek.enabled;
		Boid b = follower.GetComponent<Boid> ();
		b.maxSpeed = 5;

	}
		
	// Update is called once per frame
	void Update ()
	{

	}
}
