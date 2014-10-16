using UnityEngine;
using System.Collections;

public class BossV1 : MonoBehaviour {

	
	private Vector3 planeLoc;
	private Vector3 rotShift;
	public Quaternion angleNeeded;
	private int hitcount;
	// Use this for initialization
	void Start () {
		hitcount = 0;
		rotShift = new Vector3(0, 90, 0);
	}
	
	// Update is called once per frame
	void Update () {
		
		//	 planeLoc =GameObject.Find("playerFlatPrefab").transform.position;
		planeLoc =GameObject.Find("AlbertPrefab").transform.position;
		
		//Debug.Log(planeLoc);

	   // transform.LookAt(planeLoc);
		
		float damp=1.8f;

   //     angleNeeded = Quaternion.LookRotation(planeLoc - new Vector3(transform.position.x *-1 , transform.position.y, transform.position.z));
		angleNeeded = Quaternion.LookRotation(planeLoc - transform.position);
		
		transform.rotation= Quaternion.Slerp(transform.rotation, angleNeeded, Time.deltaTime*damp);


        if (hitcount > 5)
        {
            Destroy(gameObject);
        }
		
	}

	

	void OnTriggerEnter(Collider otherObj) { if (otherObj.tag == "projectileTag") {
		 hitcount++;
		 Debug.Log(hitcount);
	}
	
	}



}




