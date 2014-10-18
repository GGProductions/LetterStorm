using UnityEngine;
using System.Collections;

public class Boss1_move : MonoBehaviour {
	private int hitCount;

	private Vector3 here;

	private float tem;

	//public GameObject wordHook;

    private Transform _wordHook;

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
	//	Debug.Log(factor);

		//transform.Translate(Vector3.back * 0.02f);
		if (transform.position.z < 2.5f)
		{

			transform.position = new Vector3(transform.position.x, transform.position.y, 2.5f);

		}

		

	}

    void OnTriggerEnter() {

        string lenetrnam = "N";

        //  if (wordHook.transform.childCount > 0 ) Destroy(wordHook.transform.GetChild(0).gameObject);


      //  GameObject go = Instantiate(Resources.Load("Boss_letters/" + lenetrnam + "boss"), _wordHook.transform.position, Quaternion.identity) as GameObject;

   //    go.transform.parent = _wordHook.transform;//
    
    }
	
}
