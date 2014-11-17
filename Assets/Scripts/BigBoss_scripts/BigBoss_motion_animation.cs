using UnityEngine;
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
	
	// Update is called once per frame
    void LateUpdate()
    {

        animation.CrossFade("slithering");
		if (Input.GetKeyDown (KeyCode.A)) {
			//Debug.Log("c.loses claw");
			//animation.Stop("slithering");
			animation.CrossFade("openingClaw");
           // StartCoroutine(open1());
          
            
		}
        else

            if (Input.GetKeyDown(KeyCode.S))
            {
                //	Debug.Log("opens claw");
                animation.CrossFade("closingClaw");
            }
   //     else
     //       animation.CrossFade("slithering");

     //   lcGO.transform.rotation = Quaternion.Lerp(lcclosed, lcclosed, Time.time * 0.2f);
       // LeftClaw.transform.rotation = Quaternion.Lerp(from.rotation, to.rotation, Time.time * speed);//look in the scene you will find 2 giant arrows " Cursor_3d_dirrectional" . each has an initial rotation stored in the form of quaternion rotation. the lerp function will rotate leftclaw.transform from the rotation of the first cursor3d to the rotation of the second cursor32


	//	lc.transform.rotation = new Quaternion(1f, 1f, Time.deltaTime, 1f);
		//Debug.Log(lc.name);

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

   

