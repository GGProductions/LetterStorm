using UnityEngine;
using System.Collections;

public class BossV1  : MonoBehaviour {

	

	private Vector3 planeLoc;
	private Vector3 rotShift;
	public Quaternion angleNeeded;
	private int hitcount;
	private GameObject theBoss;
  private  Boss3dWordGen b3dwg;
	void Awake()
	{
		theBoss = GameObject.Find("Boss1Prefab_V2(Clone)");
		b3dwg = theBoss.GetComponent<Boss3dWordGen>();
	}
	// Use this for initialization
	void Start () {

		hitcount = 0;
		rotShift = new Vector3(0, 90, 0);
	}
	
	// Update is called once per frame
	void Update () {
		
		//	 planeLoc =GameObject.Find("playerFlatPrefab").transform.position;
		planeLoc =GameObject.Find("AlbertPrefab2").transform.position;
		
		//Debug.Log(planeLoc);

	   // transform.LookAt(planeLoc);
		
		float damp=1.8f;

   //     angleNeeded = Quaternion.LookRotation(planeLoc - new Vector3(transform.position.x *-1 , transform.position.y, transform.position.z));
		angleNeeded = Quaternion.LookRotation(planeLoc - transform.position);
		
		transform.rotation= Quaternion.Slerp(transform.rotation, angleNeeded, Time.deltaTime*damp);


		if (hitcount > 6)
		{
			b3dwg.AllerAlbert();
			Destroy(gameObject);
		}
		
	}

	

	void OnTriggerEnter(Collider otherObj) { 
		 //otherObj.tag == "projectileTag" || 
		if (otherObj.tag == "projectileTag" || otherObj.tag == "letterProjectile")
		{



		 hitcount++;
		// Debug.Log(hitcount);


			
	}
	
	}



}




