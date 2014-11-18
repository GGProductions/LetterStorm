﻿using UnityEngine;
using System.Collections;

public class BigBoss_motion_animation : MonoBehaviour {

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
	  //  StartCoroutine(openCorout());

	}
    private float timeElapsed;
	
	// Update is called once per frame
	void LateUpdate()
	{

		animation.CrossFade("slithering");
	/*	if (Input.GetKeyDown (KeyCode.A)) {animation.CrossFade("openingClaw"); }
		else

			if (Input.GetKeyDown(KeyCode.S)){animation.CrossFade("closingClaw");	}
 */

	    timeElapsed += Time.deltaTime;

		float factor = Mathf.Cos(timeElapsed);
		transform.Translate(Vector3.right * (factor / 20) * Time.timeScale, Space.World);
		transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

	}


	IEnumerator open1() {
	  //  animation.Stop("slithering");
		animation.CrossFade("openingClaw");
		yield return new WaitForSeconds(animation["openingClaw"].length);
	
	}

	IEnumerator openCorout()
	{
		lc.transform.rotation = Quaternion.Lerp(lcclosed, lcclosed, Time.time * 300f);
			yield return null;
	}





}

   

