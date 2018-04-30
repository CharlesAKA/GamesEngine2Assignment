using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public float leaders = 3;
	public float gapL = 10;
	public float gap = 10;
	public float followers = 1;
	public GameObject prefab;
	//public GameObject warship;
	//private Vector3 shipsize;
	//Could create the list here and use at the AI script aswell
	private void Awake ()
	{
		//warship = GameObject.Find ("warship");
	//	shipsize = GetComponent<Collider> ().bounds.size;
		for (int i = 1; i <= leaders; i++) {
			Vector3 rndPos = new Vector3 (Random.Range ((-leaders * gapL), (leaders * gapL)),0, Random.Range ((-leaders * gapL), (leaders * gapL)));
			CreateLeaders (rndPos);
		}

	}

	void CreateLeaders (Vector3 newpos)
	{
		GameObject leader = GameObject.Instantiate<GameObject> (prefab);
		leader.transform.parent = this.transform;
		leader.transform.position = this.transform.TransformPoint (newpos);
		leader.transform.rotation = this.transform.rotation;

		//add components path,pathfollow,seek,arrive,obsavoidance,
		//Wander w = leader.AddComponent<Wander>();
		//Arrive arive = leader.AddComponent<Arrive> ();
		//arive.targetPosition = leader.transform.position + leader.transform.forward * 100;
		Seek seek = leader.AddComponent<Seek> ();
		seek.enabled = !seek.enabled;
		//Path path = leader.AddComponent<Path> ();
		//path.warship = warship;
		//FollowPath fpath = leader.AddComponent<FollowPath> ();
		//fpath.path = path;
		//fpath.enabled = !fpath.enabled;
		ObstacleAvoidance obavd = leader.AddComponent<ObstacleAvoidance> ();


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

		//Wander w = follower.AddComponent<Wander>();
		//OffsetPursue op = follower.AddComponent<OffsetPursue> ();
		//op.leader = leader;
		Seek seek = follower.AddComponent<Seek> ();
		seek.enabled = !seek.enabled;

		ObstacleAvoidance obavd = follower.AddComponent<ObstacleAvoidance> ();

	}
		
	// Update is called once per frame
	void Update ()
	{

	}
}
