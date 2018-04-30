using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{

	public GameObject target;
	public float smooth = 2.0f;
	public int option = 0;
	public Vector3 offset;

	void Update(){
		if (option == 1) { LerpTo (); } 
		else if (option == 2) { LookAt (); } 
		else if (option == 3) { move (); } 
	}
	public void LerpTo ()
	{
		transform.position = Vector3.Lerp (transform.position, target.transform.position, Time.deltaTime * smooth);
	}

	public void LookAt ()
	{
		transform.LookAt (target.transform);
	}

	public void move ()
	{
		//Vector3 offpos =  target.transform.position + offset;
		//Vector3 smoothmovement = Vector3.Lerp (transform.position, offpos,Time.deltaTime * smooth);
		transform.position =  target.transform.position + offset;;
	}
}