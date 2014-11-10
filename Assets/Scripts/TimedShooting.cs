using UnityEngine;
using System.Collections;

public class TimedShooting : MonoBehaviour {

	public GameObject theShot;
	public float shotDelay;

	IEnumerator Start() 
	{
		while(  gameObject)	
		{
			Vector3 here= new Vector3( gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);	
			{Instantiate (theShot, here, transform.rotation );	
				yield return new WaitForSeconds(1.7f);}
		}
	}	
	
}

