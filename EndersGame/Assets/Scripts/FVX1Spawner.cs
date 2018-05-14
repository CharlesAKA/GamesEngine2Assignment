using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FVX1Spawner : MonoBehaviour
{
	public float leaders = 3;
	public float gapL = 50;
	public float gap = 50;
	public float followers = 1;
	public GameObject sprefab;
	public GameObject prefab;
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
			
		GameObject leader = GameObject.Instantiate<GameObject> (sprefab);
		leader.transform.position = this.transform.TransformPoint (new Vector3(0,-50,0));
		leader.transform.rotation = this.transform.rotation;

		FollowPath fpath = leader.AddComponent<FollowPath> ();
		fpath.path = GameObject.Find ("Spath").GetComponent<Path> ();
		fpath.enabled = fpath.enabled;
		ObstacleAvoidance obavd = leader.AddComponent<ObstacleAvoidance> ();
//		leader.GetComponent<Boid> ().maxSpeed = 30;
			
	}
	void CreateLeaders (Vector3 newpos)
	{
		//Create Spaceship 
		GameObject leader = GameObject.Instantiate<GameObject> (prefab);
		leader.transform.parent = this.transform;
		leader.transform.position = this.transform.TransformPoint (newpos);
		leader.transform.rotation = this.transform.rotation;

		Arrive arive = leader.AddComponent<Arrive> ();
		arive.targetPosition = leader.transform.position + leader.transform.forward * 100;
		Seek seek = leader.AddComponent<Seek> ();
		seek.enabled = !seek.enabled;
		Path path = leader.AddComponent<Path> ();
		path.warship = warship;
		FollowPath fpath = leader.AddComponent<FollowPath> ();
		fpath.path = path;
		fpath.enabled = fpath.enabled;
		ObstacleAvoidance obavd = leader.AddComponent<ObstacleAvoidance> ();
		obavd.enabled = !obavd.enabled;

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
		follower.transform.position = this.transform.TransformPoint (offset);
		follower.transform.parent = this.transform;
		follower.transform.rotation = this.transform.rotation;

		OffsetPursue op = follower.AddComponent<OffsetPursue>();
		op.leader = leader;
		Seek seek = follower.AddComponent<Seek> ();
		seek.enabled = !seek.enabled;
		Path path = follower.AddComponent<Path> ();
		path.warship = warship;
		FollowPath fpath = follower.AddComponent<FollowPath> ();
		fpath.path = path;
		fpath.enabled = fpath.enabled;
		ObstacleAvoidance obavd = follower.AddComponent<ObstacleAvoidance> ();
		obavd.enabled = !obavd.enabled;
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
