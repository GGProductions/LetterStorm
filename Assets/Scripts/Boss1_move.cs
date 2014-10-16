using UnityEngine;
using System.Collections;

public class Boss1_move : MonoBehaviour {
	private int hitCount;

	private Vector3 here;

	private float tem;
	// Use this for initializationz
	void Start () {
        here = new Vector3(0f, -0f, 3.1677f);

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
	//	Debug.Log(factor);

		//transform.Translate(Vector3.back * 0.02f);
		if (transform.position.z < 2.5f)
		{

			transform.position = new Vector3(transform.position.x, transform.position.y, 2.5f);

		}

		

	}
	
}
