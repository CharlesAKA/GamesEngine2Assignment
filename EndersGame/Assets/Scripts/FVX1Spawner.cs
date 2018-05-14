using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FVX1Spawner : MonoBehaviour
{
	public float leaders = 1;
	public float gapL = 100;
	public float gap = 100;
	public float followers = 1;
	public GameObject ssprefab;
	public GameObject warship;
	public GameObject expPrefab; 
	private Vector3 mshipsize;

	private void Awake ()
	{
		warship = GameObject.Find ("Warship");
		mshipsize = warship.GetComponent<Collider> ().bounds.size;
		for (int i = 1; i <= leaders; i++) {
			Vector3 rndPos = new Vector3 (Random.Range ((-leaders * gapL), (leaders * gapL)), Random.Range ((-leaders * gapL), (leaders * gapL)), 0);
			CreateLeaders (rndPos);
		}
			


	}
	void CreateLeaders (Vector3 newpos)
	{
		//Create Spaceship 
		GameObject leader = GameObject.Instantiate<GameObject> (ssprefab);
		leader.transform.position = this.transform.TransformPoint (new Vector3(0,-50,0));
		leader.transform.rotation = this.transform.rotation;

		Arrive arive = leader.AddComponent<Arrive> ();
		arive.targetPosition = leader.transform.position + leader.transform.forward * 100;
		Seek seek = leader.AddComponent<Seek> ();
		seek.enabled = !seek.enabled;


		FollowPath fpath = leader.AddComponent<FollowPath> ();
		fpath.path = GameObject.Find("Spath").GetComponent<Path>();
		fpath.enabled = fpath.enabled;
		leader.GetComponent<Boid> ().maxSpeed = 30;

		for (int i = 1; i <= followers; i++) {
			Vector3 offset = new Vector3 (gap * i, 0, -gap * i);
			CreateFollower (offset, leader.GetComponent<Boid> ());
			offset = new Vector3 (-gap * i, 0, -gap * i);
			CreateFollower (offset, leader.GetComponent<Boid> ());            
		}
	}

	void CreateFollower (Vector3 offset, Boid leader)
	{
		GameObject follower = GameObject.Instantiate<GameObject> (ssprefab);
		follower.transform.position = this.transform.TransformPoint (offset);
		follower.transform.parent = this.transform;
		follower.transform.rotation = this.transform.rotation;
		OffsetPursue op = follower.AddComponent<OffsetPursue>();
		op.leader = leader;
		//Seek seek = follower.AddComponent<Seek> ();
		//seek.enabled = !seek.enabled;

		//ObstacleAvoidance obavd = follower.AddComponent<ObstacleAvoidance> ();

	}

	Vector3 RandomTarget () 
	{
		return new Vector3 (warship.transform.position.x,
			warship.transform.position.y,
			Random.Range (-mshipsize.z / 2, mshipsize.z / 2) + warship.transform.position.z
		);
	}
	// Update is called once per frame
	void Update ()
	{

	}
}
