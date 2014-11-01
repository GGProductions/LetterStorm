using UnityEngine;
using System.Collections;

public class Boss1_moveV2 : MonoBehaviour {
	private int hitCount;
	private Vector3 here;
	private float tem;


	public AnimationClip ActionAnimationClip;
	public AnimationClip CurlUpAnimationClip;
	public AnimationClip UncurlAnimationClip;

	
	//private Transform _wordHook;

	//public GameObject[] alphabet;
	// Use this for initializationz

	void Awake() {
		animation.wrapMode = WrapMode.Loop;
		animation.AddClip(ActionAnimationClip, "action");
		animation.wrapMode = WrapMode.Default;
		animation.AddClip(CurlUpAnimationClip, "curl");
		animation.AddClip(UncurlAnimationClip, "uncurl");
		animation.Stop();

	}

	void Start () {
		here = new Vector3( 0f, -0f, 3.1677f);
		gameObject.transform.position = here;
		tem = 0;	
	}
	public void getPissed()
	{
		Debug.Log("BOOSSS" );
		// transform.position = new Vector3(0f, transform.position.y, transform.position.z - amtToMove);


		StartCoroutine(RotateMe());
	//	StartCoroutine(rush());
	}

	
	// Update is called once per frame
	void Update()
	{
		//animation.CrossFade("action");
		tem += Time.deltaTime;

		float factor = Mathf.Cos((tem));
		transform.Translate(Vector3.right * (factor / 20), Space.World);
		transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
		

	}
	

	IEnumerator rush()
	{
		float x = transform.position.x;

//		while (true )
//		{
		//	float rotationSpeed = 5f * Time.deltaTime;
		//	transform.Rotate(new Vector3(0, 0, 2) * rotationSpeed);

		//	yield return new WaitForSeconds(1.5f);
		//}

	  //  while (true) { 
		
		
	  //  }

		while (transform.position.z > -4f)
		{
			Debug.Log("in corout");
			float amtToMove = 6.52f * Time.deltaTime;
			transform.position = new Vector3(x, transform.position.y, transform.position.z - amtToMove);
			yield return 0;
		}

		while (transform.position.z < 4.0f)
		{
			// Move the ship up
			float amtToMove = 5.52f * Time.deltaTime;
			transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + amtToMove);
			yield return 0;
		}
		yield return null;
	}


	public Vector3 rot;
//	IEnumerator RotateMe(Vector3 byAngles, float inTime)
	IEnumerator RotateMe()
	{
		Debug.Log("incoroutine");
		Vector3 rowX = new Vector3(360f, 0, 0);
	   // float rotationSpeed = 300 * Time.deltaTime;
	  //  transform.Rotate(new Vector3(1, 0, 0) * rotationSpeed);

		Quaternion fromAngle = transform.rotation;
		Quaternion toAngle = Quaternion.Euler(transform.eulerAngles + rowX);
		//for (float t = 0f; t < 1f; t += Time.deltaTime / inTime)
		for (float t = 0f; t < 1f; t += Time.deltaTime )
		{
			transform.rotation = Quaternion.Lerp(fromAngle, toAngle, t);
			yield return null;
		}
	}



}
