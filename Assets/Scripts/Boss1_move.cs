using UnityEngine;
using System.Collections;

public class Boss1_move : MonoBehaviour {
	private int hitCount;
	private Vector3 here;
	private float tem;

    //public delegate void gunsDied();
    //public static event gunsDied 

	//private Transform _wordHook;

	//public GameObject[] alphabet;
	// Use this for initializationz

	void Awake() {    
	}

	void Start () {
		here = new Vector3( 0f, -0f, 3.1677f);
		gameObject.transform.position = here;
		tem = 0;	
	}
	
	// Update is called once per frame
	void Update()
	{
		tem += Time.deltaTime;

		//float factor=Mathf.Cos((Time.time))  ;
		float factor = Mathf.Cos((tem));
		transform.Translate(Vector3.right * (factor / 20), Space.World);
		if (transform.position.z < 2.5f)
		{

			transform.position = new Vector3(transform.position.x, transform.position.y, 2.5f);

		}

		

	}

 
	
}
