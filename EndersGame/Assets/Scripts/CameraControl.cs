using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

	public List<GameObject> cameras = new List<GameObject> ();
	public int next = 1;
	public int prev = 0;
	// Use this for initialization
	void Start ()
	{
		cameras.Clear ();
		int count = transform.childCount;
		for (int i = 0; i < count; i++) {
			cameras.Add (transform.GetChild (i).gameObject);
		}
		//deactive all cameras except 1st camera
		for (int i = 1; i < count; i++) {
			cameras [i].SetActive (false);
		}
	}

	public void nextCam ()
	{

		if (next > cameras.Count-1) {
			cameras [cameras.Count-1].SetActive (true); 
		} else {
			cameras [prev].SetActive (false);
			cameras [next].SetActive (true);
			prev = next;
			next++; 
		}
	}

	public void prevCam ()
	{			
		if (prev == 0) {
			cameras [prev].SetActive (true); 
		} else {
			prev--;
			next--;
			cameras [prev].SetActive (true); 
			cameras [next].SetActive (false);
		}
	}

	void Update ()
	{
		if (Input.GetKey ("o")) {
			prevCam ();
		}
		if (Input.GetKey ("p")) {
			nextCam ();
		}
	}
}
