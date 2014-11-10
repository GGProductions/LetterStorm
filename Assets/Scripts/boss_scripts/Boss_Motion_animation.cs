using UnityEngine;
using System.Collections;

public class Boss_Motion_animation : MonoBehaviour {


	public AnimationClip ActionAnimationClip;
	public AnimationClip CurlUpAnimationClip;
	public AnimationClip UncurlAnimationClip;

	private Vector3 here;
	private float tem;

   private Vector3 point_topleft;
   private Vector3 point_mid;
   private Vector3 point_toright;
 

	void OnEnable()
	{
		Boss_3d_wordGen.OnWrongCollision += CHaaaarge;
	}
	 
	void OnDisable()
	{
		Boss_3d_wordGen.OnWrongCollision -= CHaaaarge;
	}


	void Awake()
	{
		 point_topleft= new Vector3(-3f,0f,4.5f);
		 point_mid = new Vector3(0f, 0f, 0f); 
		 point_toright=new Vector3(3f,0f,4.5f);

		here = new Vector3(0f, 0f, 4);
		gameObject.transform.position = here;
		tem = 0;
		animation.wrapMode = WrapMode.Once;
		animation.AddClip(CurlUpAnimationClip, "curl");
		animation.AddClip(UncurlAnimationClip, "uncurl");

		animation.wrapMode = WrapMode.Loop;
		animation.AddClip(ActionAnimationClip, "action");
		//animation.wrapMode = WrapMode.Default;
		//animation.Stop();
	//	animation["curl"].layer = 1;
	//	animation["uncurl"].layer = 0;
	//	animation["action"].layer = 0;
	}

	public Transform startMarker;
	public Transform endMarker;
	public float speed = 1.0F;
	private float startTime;
	private float journeyLength=10f;
	public Transform target;
	public float smooth = 5.0F;    

	void Start()
	{
		
	}
	IEnumerator MoveObject(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
	{
		var i = 0.0f;
		var rate = 1.0f / time;
		while (i < 1.0f)
		{
			i += Time.deltaTime * rate;
			thisTransform.position = Vector3.Lerp(startPos, endPos, i);
			yield return null;
		}
	}


	
	// Update is called once per frame
	void Update () {

		animation.CrossFade("action");
		tem += Time.deltaTime;

		float factor = Mathf.Cos((tem));
		transform.Translate(Vector3.right * (factor / 20), Space.World);
	transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);


	//	float distCovered = (Time.time - startTime) * speed;
	//	float fracJourney = distCovered / journeyLength;
	//	transform.position = Vector3.Lerp(new Vector3(0f, 4f, 0f), new Vector3(4f, 0f, 0f), 2.5f);


		//if (Input.GetKeyDown(KeyCode.Q)) animation.CrossFade("action");
		//if (Input.GetKeyDown(KeyCode.W)) animation.CrossFade("curl");
		//if (Input.GetKeyDown(KeyCode.E)) animation.CrossFade("uncurl");
		if(switchon)
		animation.CrossFade("curl");
	}

	private bool switchon = false;

	public void CHaaaarge( )
	{
		switchon = true;
		Debug.Log("BOOSSS" );
		//StartCoroutine(Curlup());
		//StartCoroutine(Rotatinator());
		//animation.CrossFade("curl");
		StartCoroutine(Func2());
		Debug.Log("AALLLDONE");
	//	transform.rotation = Quaternion.Euler(0, 180, 0); 
	}
	 IEnumerator Func1(){
		 animation.CrossFade("curl");
		 yield return new WaitForSeconds(animation["curl"].length);

	 }

		
	IEnumerator Func2()
	{
		yield return StartCoroutine(Func1());

		switchon = false;

		float x = transform.position.x;
		int tme = 0;
		while (tme < 72)
		{
			tme++;
			float rotationSpeed = 3000 * Time.deltaTime;
			transform.Rotate(new Vector3(10, 0, 0) * rotationSpeed);

			Debug.Log(tme);

			yield return 0;


		}

		while (transform.position.z > -4f)
		{
			transform.rotation = Quaternion.Euler(0, 180, 0);
			//animation.CrossFade("curl");
			float amtToMove = 6.52f * Time.deltaTime;
			transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - amtToMove);
			yield return 0;
		}

		while (transform.position.z < 4.0f)
		{
			animation.CrossFade("uncurl");
			float amtToMove = 5.52f * Time.deltaTime;
			transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + amtToMove);
			yield return 0;
		}

		transform.rotation = Quaternion.Euler(0, 180, 0);
		yield return new WaitForSeconds(0.2f);

		yield return null;
	}


	IEnumerator oldRush()
	{


		while (transform.position.z > -4f)
		{
			animation.CrossFade("curl");
			float amtToMove = 6.52f * Time.deltaTime;
			transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - amtToMove);
			yield return 0;
		}

		while (transform.position.z < 4.0f)
		{
			animation.CrossFade("uncurl");
			float amtToMove = 5.52f * Time.deltaTime;
			transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + amtToMove);
			yield return 0;
		}
		yield return null;
	}


	IEnumerator rush()
	{
		//yield return new WaitForSeconds(animation["curl"].length);
		//animation.CrossFade("curl");
		animation.CrossFade("curl");
		yield return new WaitForSeconds(animation["curl"].length);

		switchon = false;

		float x = transform.position.x;
		int tme=0;
		while (tme < 72)
		{
			tme++;
			float rotationSpeed = 3000 * Time.deltaTime;
			transform.Rotate(new Vector3(10, 0, 0) * rotationSpeed);

			Debug.Log(tme);

			yield return 0;

		   
		}

		while (transform.position.z > -4f)
		{
			transform.rotation = Quaternion.Euler(0, 180, 0); 
			//animation.CrossFade("curl");
			float amtToMove = 6.52f * Time.deltaTime;
			transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - amtToMove);
			yield return 0;
		}

		while (transform.position.z < 4.0f)
		{
			animation.CrossFade("uncurl");
			float amtToMove = 5.52f * Time.deltaTime;
			transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + amtToMove);
			yield return 0;
		}

		transform.rotation = Quaternion.Euler(0, 180, 0);
		yield return new WaitForSeconds(0.2f);
		
		yield return null;
	}
	IEnumerator RotateMe()
	{
		Debug.Log("rotinating");
		Vector3 rowX = new Vector3(180f, 180f, 180);
		// float rotationSpeed = 300 * Time.deltaTime;
		//  transform.Rotate(new Vector3(1, 0, 0) * rotationSpeed);



		Quaternion fromAngle = transform.rotation;
		Quaternion toAngle = Quaternion.Euler(transform.eulerAngles + rowX);
		//for (float t = 0f; t < 1f; t += Time.deltaTime / inTime)
		for (float t = 0f; t < 1000f; t += Time.deltaTime)
		{
			transform.rotation = Quaternion.Lerp(fromAngle, toAngle, t);
			
		}


		yield return null;
	}

   IEnumerator Rotatinator(){
	   int tme = 0;
	   while (   tme < 70 )
	   {
		   tme++;
		   //animation.CrossFade("uncurl");
		   //float amtToMove = 5.52f * Time.deltaTime;
		   //transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + amtToMove);
			float rotationSpeed = 3000 * Time.deltaTime;
		   transform.Rotate(new Vector3(10, 0, 0) * rotationSpeed);

		   int rots = (int)transform.rotation.y;
		   int rotsz = (int)transform.rotation.z;
		 //  Debug.Log(rots.ToString() +  rotsz.ToString());
		   Debug.Log(tme);

		   yield return 0;
	   }
	   yield return null;
	}


   private IEnumerator BossEnters()
   {
	   while (transform.position.z < -4f)
	   {
		   transform.rotation = Quaternion.Euler(0, 180, 0);
		   //animation.CrossFade("curl");
		   float amtToMove = 6.52f * Time.deltaTime;
		   transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - amtToMove);
		   yield return 0;
	   }
   }
}
