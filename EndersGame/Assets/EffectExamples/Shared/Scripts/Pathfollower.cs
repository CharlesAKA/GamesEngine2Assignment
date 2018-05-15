using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfollower : MonoBehaviour {
	public Transform[] Fpath;
	public float speed = 1.0f;
	public float reachDist = 1.0f;
	public int currentPoint = 0;

	void Update(){
		Vector3 dir = Fpath [currentPoint].position - transform.position;
		transform.position += dir * Time.deltaTime * speed;

		if (dir.magnitude <= reachDist) {
			currentPoint++;
		}
		if (currentPoint >= Fpath.Length) {
			currentPoint = 5;
		}
	}
	void OnDrawGizmos (){
		if(Fpath.Length > 0)
		for (int i = 0; i < Fpath.Length; i++) {
			if (Fpath [i] != null) {
				Gizmos.DrawSphere (Fpath [i].position, reachDist);
			}
		}
	}
}
