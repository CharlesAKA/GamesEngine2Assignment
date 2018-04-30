using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullets : MonoBehaviour
{
	
	public GameObject bulletSpawnPoint;
	public GameObject bulletPrefab;

	public void Startfiring(){
		StartCoroutine (TimedFiring ());
	}
		
	public IEnumerator TimedFiring ()
	{
		while (true) {
			Fire ();
			yield return new WaitForSeconds (3);
		}
	}
	public void Fire ()
	{
		GameObject bullet = GameObject.Instantiate<GameObject> (bulletPrefab);
		bullet.transform.position = bulletSpawnPoint.transform.position;
		bullet.transform.rotation = transform.rotation;

	}
}
