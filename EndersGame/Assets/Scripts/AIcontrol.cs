using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AIcontrol : MonoBehaviour
{
	//set up on the inspector
	public GameObject Fspwn;	//FVspawner
	public GameObject emspwn; 	//enemyspawner
	public GameObject fsSpwn;	//fightscenespawner
	public GameObject warship; //warship model
	public GameObject spaceship; //Spaceship game object
	public GameObject emspwn2; //enemyspawner 2
	public CameraControl camCon; //Camera control script.. use to set which camera to activate and cameras target setup
	//Place all ships in a list
	private List<GameObject> FVspawn = new List<GameObject> ();
	private List<GameObject> enemies = new List<GameObject> ();
	private List<GameObject> enemies2 = new List<GameObject> ();
	//List of camerabehavior to set up cameras target object
	private List<CameraBehaviour> camsBehavior = new List<CameraBehaviour>();

	void Start ()
	{
		spaceship = GameObject.Find ("HeavyBattleship(Clone)");
		//clear all list
		FVspawn.Clear ();
		enemies.Clear ();
		enemies2.Clear ();
		camsBehavior.Clear ();
		//add all FUX1 plane to list
		for (int i = 0; i < Fspwn.transform.childCount; i++) {
			FVspawn.Add (Fspwn.transform.GetChild (i).gameObject);
		}
		//add all enemy ships to list
		for (int i = 0; i < emspwn.transform.childCount; i++) {
			enemies.Add (emspwn.transform.GetChild (i).gameObject);
		}
		//add all enemy ships 2 to list 
		for (int i = 0; i < emspwn2.transform.childCount; i++) {
			enemies2.Add (emspwn2.transform.GetChild (i).gameObject);
		}
		//set all enemies2 to seek spaceship plane
		for(int i = 0; i < enemies2.Count; i++)
		{
			Seek sk = enemies2[i].GetComponent<Seek> ();
			sk.targetGameObject = spaceship;
			sk.enabled = enabled;
			//enemies2[i].GetComponent<FireBullets>().Startfiring();
		}
		//set camera behaviour list
		//for (int i = 0; i < camCon.cameras.Count; i++) {
		//	camsBehavior.Add (camCon.cameras[i].GetComponent<CameraBehaviour>());
		//}
		//set cameras target object
		//camsBehavior[0].target = FVspawn[0];
		//camsBehavior[1].target = enemies[0];
		//camsBehavior [4].target = spaceship;
		//camsBehavior[4].offset = camsBehavior[4].transform.position - spaceship.transform.position;
		//camsBehavior [6].target = spaceship; 
		//camsBehavior [0].option = 2; //make camera 1 lookAt FUX1 plane
		// Deactive other objects that are not needed yet
		fsSpwn.SetActive(false);
		emspwn2.SetActive(false);
		spaceship.SetActive (false);
		StartCoroutine (FireMissiles ());
		Debug.Log ("ab8count: " + FVspawn.Count);
	}

	public IEnumerator FireMissiles ()
	{
		yield return new WaitForSeconds (10);
		StartCoroutine (Pathfollow ());

		for (int i = 0; i < FVspawn.Count; i++) {
			for (int j = 0; j < FVspawn [i].transform.Find ("Bombs").transform.childCount; j++) { //fire first 2 missilbe of each FUX1
				FVspawn [i].transform.Find ("Bombs").GetChild (j).gameObject.GetComponent<Seek> ().enabled = enabled;
				yield return new WaitForSeconds (0.3f);
				FVspawn [i].transform.Find ("Bombs").GetChild (j).gameObject.GetComponent<Rigidbody> ().isKinematic = false;

			}
		}
		Debug.Log ("Missiles Fired!");
		yield break;
	}

	//Change FVspawn plane behaviour to path follow 
	//Change enemies behavior to seek each FVspawn
	public IEnumerator Pathfollow ()
	{
		yield return new WaitForSeconds (5);
		camCon.nextCam (); //goes to the next camera view
		yield return new WaitForSeconds (10);
		camsBehavior [1].option = 1;

		//for loop to set FVspawn and enemies
		//must have both equal numbers of ship count
		for(int i = 0; i < FVspawn.Count; i++)
		{
			if (FVspawn[i].name != "follower") {
				//disable previous behaviour of leader
				FVspawn[i].GetComponent<Arrive> ().enabled = !enabled;
			} else {
				//disable previous behaviour of follower
				FVspawn[i].GetComponent<OffsetPursue> ().enabled = !enabled;
			}
			//enable follow path script and obstacle avoidance for FUX1 planes
			FVspawn[i].GetComponent<FollowPath> ().enabled = enabled;
			FVspawn[i].GetComponent<ObstacleAvoidance> ().enabled = enabled;
			FVspawn[i].GetComponent<Boid> ().maxSpeed = 25;
			//disable previous behaviour of enemies
			//		es.GetComponent<Wander>().enabled = !enabled;
			//make seek script of enemies to follow each one of FUX1 planes
			Seek sk = enemies[i].GetComponent<Seek>();
			sk.targetGameObject = FVspawn[i];
			sk.enabled = enabled;
			enemies[i].GetComponent<FireBullets>().Startfiring();
		}
		yield return new WaitForSeconds (3);
		camCon.prevCam ();
		yield return new WaitForSeconds (5);
		camCon.nextCam ();
		StartCoroutine (StartFightScene ());
		yield break;
	}

	//Show the short fight scene and the whole chase scene
	public IEnumerator StartFightScene (){
		camCon.nextCam (); //goes to the next camera view
		fsSpwn.SetActive (true); //starts the fight scene
		yield return new WaitForSeconds (10);
		fsSpwn.SetActive (false); //disable fight scene afterwards
		camCon.nextCam ();
		yield return new WaitForSeconds (5);
		warship.transform.GetChild(0).gameObject.SetActive(true);
		camsBehavior [3].option = 1; //make the camera lerp to the warship final attack
		yield return new WaitForSeconds (5);
		StartCoroutine (KRscene ());
		yield break;
	}

	public IEnumerator KRscene (){
		camCon.nextCam (); //goes to the next camera view
		camsBehavior[4].option = 3;
		spaceship.SetActive (true); //show k Spaceship plane
		emspwn2.SetActive (true); //show enemies spawner 2 
		yield return new WaitForSeconds (5);
		camCon.nextCam ();
		yield return new WaitForSeconds (10);
		camCon.nextCam ();
		camsBehavior [6].option = 2; //make the camera look at k Spaceship
		yield return new WaitForSeconds (5);
		//load end scene
		SceneManager.LoadScene (1);
		yield break;
	}
	// Update is called once per frame
	void Update ()
	{

	}
}
