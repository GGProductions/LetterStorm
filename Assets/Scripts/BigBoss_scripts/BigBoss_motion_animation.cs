using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BigBoss_motion_animation : MonoBehaviour {

	public Transform[] transArra;


	public AnimationClip slither;
	public AnimationClip openClaw;
	public AnimationClip closeClaw;
	private Transform lc;

	private Transform rc;

	Quaternion rotation1;
	Quaternion rotation2;

	Quaternion rcclosed;
	Quaternion rcopen;
	Quaternion lcclosed;
	Quaternion lcopen;

	private GameObject lcGO;
	private GameObject rcGO;



	private bool switchon = false;


	void OnEnable()
	{
		Boss_3d_wordGen.OnWrongCollision += CHaaaarge;
	}
	void OnDisable()
	{
		Boss_3d_wordGen.OnWrongCollision -= CHaaaarge;
	}

	public void CHaaaarge()
	{
		switchon = true;
		Debug.Log("BOOSSS");
		//StartCoroutine(Curlup());
		//StartCoroutine(Rotatinator());
		//animation.CrossFade("curl");
		StartCoroutine(Func2());
		Debug.Log("AALLLDONE");
		//	transform.rotation = Quaternion.Euler(0, 180, 0); 
	}
	IEnumerator Func1()
	{
		animation.CrossFade("openingClaw");
		yield return new WaitForSeconds(animation["openingClaw"].length);

	}



	IEnumerator Func2()
	{
		yield return StartCoroutine(Func1());

		switchon = false;

		float x = transform.position.x;
	   

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
			animation.CrossFade("closingClaw");
			float amtToMove = 5.52f * Time.deltaTime;
			transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + amtToMove);
			yield return 0;
		}

		transform.rotation = Quaternion.Euler(0, 180, 0);
		yield return new WaitForSeconds(0.2f);

		yield return null;
	}

	void Awake() {
		//listOfPoints = new Transform[4];

		 rcclosed=new Quaternion(1.0f,0.2f,0.0f,0.0f);
		 rcopen=new Quaternion(0.9f,-0.4f,0.0f,0.0f);
		 lcclosed=new Quaternion(1.0f,-0.2f,0.0f,0.0f);
		 lcopen = new Quaternion(0.9f, 0.4f, 0.0f, 0.0f);

		lc = transform.GetChild(0).GetChild(0).GetChild(2);//.FindChild("claw_l");
		rc = transform.GetChild(0).GetChild(0).GetChild(3);//.FindChild("claw_l");

		lcGO = GameObject.Find("claw_l");
		rcGO = GameObject.Find("claw_r");

		if (lcGO != null) Debug.Log("got it" + lcGO.name);
		else
			Debug.Log("not get it"+ rc.name );


		//originalQuat = lc.transform.rotation;
	  //  originalQuat = new Quaternion(-0.6f, -0.4f, -0.4f, 0.6f);
	   // finalQuat = new Quaternion(-0.6f, -0.4f, -0.4f, 0.6f);
	   // Debug.Log("og quat" + originalQuat);


		animation.AddClip(slither, "slithering");
		animation.AddClip(openClaw, "openingClaw");
		animation.AddClip(closeClaw, "closingClaw");
	}

	// Use this for initialization
	void Start () {
		animation.wrapMode = WrapMode.Loop;

		animation["openingClaw"].wrapMode = WrapMode.ClampForever;
		animation["closingClaw"].wrapMode = WrapMode.Clamp;
  



		animation["slithering"].layer = 0;
		animation["openingClaw"].layer = 10;
		animation["closingClaw"].layer = 10;
		animation["openingClaw"].AddMixingTransform(lc);
		animation["closingClaw"].AddMixingTransform(lc);
		animation["openingClaw"].AddMixingTransform(rc);
		animation["closingClaw"].AddMixingTransform(rc);
	   StartCoroutine(open1());
	   StartCoroutine(initMove());

	}
	IEnumerator initMove()
	{
		yield return StartCoroutine(MoveObject(transform, transArra[0].position, transArra[1].position, 4.0f));
		//Vector3 pointA_init = new Vector3(transform.position.x, transform.position.y, transform.position.z);
		while (true)
		{
			
			yield return StartCoroutine(MoveObject(transform, transArra[1].position, transArra[2].position, 3.0f));
			yield return StartCoroutine(MoveObject(transform, transArra[2].position, transArra[3].position, 3.0f));
			yield return StartCoroutine(MoveObject(transform, transArra[3].position, transArra[4].position, 4.0f));
			yield return StartCoroutine(MoveObject(transform, transArra[4].position, transArra[1].position, 4.0f));

		}
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



	private float timeElapsed;
	private int  inttimeElapsed;

	//private float firstTimer = 5;

	//private float secondTimer = 10;

	private float time_toStayOpen = 3;
	private float time_toStayclosed = 8;
	// Update is called once per frame
	void LateUpdate()
	{

		animation.CrossFade("slithering");

		/*
		if (Input.GetKeyDown (KeyCode.A)) {animation.CrossFade("openingClaw"); }
		else
			if (Input.GetKeyDown(KeyCode.S)){animation.CrossFade("closingClaw");	}
 
		*/

   //     inttimeElapsed = (int)timeElapsed;


		//Debug.Log(inttimeElapsed + " " + isopen);

		sidetoside();
	}

	void sidetoside() {
		timeElapsed += Time.deltaTime;
		float factor = Mathf.Cos(timeElapsed);
		transform.Translate(Vector3.right * (factor / 20) * Time.timeScale, Space.World);
		transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
	}

	private bool isopen=false;

	IEnumerator open1() {
	  //  animation.Stop("slithering");

		while (gameObject) {
			if (!isopen) {
				animation.CrossFade("openingClaw");
				yield return new WaitForSeconds(animation["openingClaw"].length);
				Debug.Log("This message appears after animation!");
				isopen = true;

				for (float timer = time_toStayOpen; timer >= 0; timer -= Time.deltaTime)
					yield return 0;

				Debug.Log("This message appears after 3 seconds!");
			
			}
			else
				if (isopen) {
					animation.CrossFade("closingClaw");
					yield return new WaitForSeconds(animation["closingClaw"].length);
					Debug.Log("This message appears after animation!");
					isopen = false;

					for (float timer = time_toStayclosed; timer >= 0; timer -= Time.deltaTime)
						yield return 0;

				}

		}

	
	}





}

   

