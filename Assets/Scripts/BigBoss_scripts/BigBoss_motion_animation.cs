using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BigBoss_motion_animation : MonoBehaviour {

	//public Transform[] transArra;

    public List<Transform> transList ;

    #region privates
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

    private GameObject thePlayerOBJ;


//	private bool switchon = false;


    private GameObject waipointBlockPrefab;
    private Transform waypointsGroup;
    #endregion

    void OnEnable()
	{
		Boss_3d_wordGen.OnWrongCollision += CHaaaarge;
	}
	void OnDisable()
	{
		Boss_3d_wordGen.OnWrongCollision -= CHaaaarge;
	}

	
    IEnumerator Func1()
	{
		animation.CrossFade("openingClaw");
		yield return new WaitForSeconds(animation["openingClaw"].length);

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
    IEnumerator Start()
    {

        transform.position = new Vector3(2f, 0.2f, 10f);

        thePlayerOBJ = GameObject.Find("AlbertPlayerPrefab");

        CurIndex = 0;
       // nextIndex = 1;

        transList = new List<Transform>();
        Debug.Log("init list count " + transList.Count );

        waipointBlockPrefab = GameObject.Find("waypints1");
       // Debug.Log("git " + waipointBlockPrefab.name);
        foreach (Transform c in waipointBlockPrefab.transform)
        {
           
            transList.Add(c);
        }
        Debug.Log("list count " + transList.Count);
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
	   StartCoroutine(Open_Close_animations());


      yield return StartCoroutine(Enetring());

	}

    private int CurIndex;
    private int nextIndex;

    void calcCurnext() {

        if (CurIndex < transList.Count - 1)
        {
            nextIndex = CurIndex + 1;
        }
        else 
            if (CurIndex==  transList.Count - 1)
        {
          //  CurIndex = transList.Count-1 ;
            nextIndex = 0;
        }
            else
                if (CurIndex > transList.Count - 1)
                {
                     CurIndex =0 ;
                    nextIndex = 1;
                }

    }

    IEnumerator Enetring() {

        while (transform.position.z > transList[0].position.z)
        {
            // Move the ship up
            float amtToMove = -1f * Time.deltaTime;
            transform.position = new Vector3(0f, transform.position.y, transform.position.z + amtToMove);

            yield return 0;

           
        }
        Debug.Log("arrived");
       // StartCoroutine("initMovePASS", 0);
        StartCoroutine("MovePtP_globalIndex");
    }
   

    IEnumerator initMovePASS(int initIndex)
    {

        CurIndex = initIndex;
        int indexplusone = CurIndex + 1;
        while (true)
        {
            //loop through 1-> end
            if (CurIndex >= transList.Count -1)
            {
                indexplusone = 0;
                yield return StartCoroutine(MoveObject(transform, transList[CurIndex].position, transList[indexplusone].position, 3.0f));
                indexplusone ++;
                CurIndex = 0;
            }
          //   indexplusone = CurIndex + 1;
            yield return StartCoroutine(MoveObject(transform, transList[CurIndex].position, transList[indexplusone].position, 3.0f));

            CurIndex++;
            indexplusone++;
        }


    }


    IEnumerator MovePtP_globalIndex()
    {   
        while (true)
        {
            calcCurnext();
            //loop through 1-> end
          
            //   indexplusone = CurIndex + 1;
            yield return StartCoroutine(MoveObject(transform, transList[CurIndex].position, transList[nextIndex].position, 3.0f));
            CurIndex++;         
        }


    }



    /// <summary>
    /// this is bigboss's chaaaarge , which is diffeternt than rebular boss's chaarge 
    /// , this should stop coroutine,
    /// find what index we at, 
    /// attach record albert's poistin
    /// go to that position
    /// comback to the index'es position and go on
    /// </summary>
    public void CHaaaarge()
    {
        calcCurnext();
        int curInd = CurIndex;
        //StartCoroutine(Curlup());
        Vector3 albertPos = thePlayerOBJ.transform.position;
        //Debug.Log("Albert's is at " + albertPos);

         StartCoroutine("breakCourputAttack", curInd);
       // StartCoroutine(" MovePtP_globalIndex");

    }

    IEnumerator breakCourputAttack( int oldIndex)
    {
       // Debug.Log("I should be here going to " + alberLoc);
      //  StopCoroutine("initMovePASS");
        StopCoroutine("MovePtP_globalIndex");
        yield return StartCoroutine( "MoveObjecttoalbert_local", 0.1f  );
     //   yield return StartCoroutine(MoveObject(transform, alberLoc, transList[oldIndex +1 ].position, 3.0f));
      //  yield return StartCoroutine("initMovePASS", oldIndex+1 );
    }


    IEnumerator MoveObjecttoalbert_local(float time)
    {
          CurIndex++;
       // calcCurnext();

        Vector3 endPos = thePlayerOBJ.transform.position;
        StopCoroutine("MovePtP_globalIndex");

        Debug.Log("moving to alberts" + endPos);
        var i = 0.0f;
        var rate = 1.0f / time;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            transform.position = Vector3.Lerp(transform.position, endPos, i);
            yield return null;
        }


        var j = 0.0f;
        var rate2 = 1.0f / time;
        while (j < 1.0f)
        {
            i += Time.deltaTime * rate;
            transform.position = Vector3.Lerp(transform.position, transList[CurIndex].position, i);
            yield return null;
        }

       // CurIndex++;

        calcCurnext();
      StartCoroutine("initMovePASS", CurIndex );
    //    StartCoroutine("MovePtP_globalIndex");
    }

    IEnumerator MoveObjecttoalbert(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
    {
        StopCoroutine("initMovePASS");
        Debug.Log("moving to alberts");
        var i = 0.0f;
        var rate = 1.0f / time;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            thisTransform.position = Vector3.Lerp(startPos, endPos, i);
            yield return null;
        }
    }

	IEnumerator MoveObject(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
	{
		float i = 0.0f;
		float rate = 1.0f / time;
		while (i < 1.0f)
		{
			i += Time.deltaTime * rate;
			thisTransform.position = Vector3.Lerp(startPos, endPos, i);
			yield return null;
		}
	}



	private float timeElapsed;
	private int  inttimeElapsed;


	private float time_toStayOpen = 5;
	private float time_toStayclosed = 2;
	void LateUpdate()
	{
        calcCurnext();
		animation.CrossFade("slithering");
        Debug.Log("cur is " + CurIndex);
	
	}

	void sidetoside() {
		timeElapsed += Time.deltaTime;
		float factor = Mathf.Cos(timeElapsed);
		transform.Translate(Vector3.right * (factor / 20) * Time.timeScale, Space.World);
		transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
	}

	private bool isopen=false;

	IEnumerator Open_Close_animations() {
	  //  animation.Stop("slithering");

		while (gameObject) {
			if (!isopen) {
				animation.CrossFade("openingClaw");
				yield return new WaitForSeconds(animation["openingClaw"].length);
			//	Debug.Log("This message appears after animation!");
				isopen = true;

				for (float timer = time_toStayOpen; timer >= 0; timer -= Time.deltaTime)
					yield return 0;

				//Debug.Log("This message appears after 3 seconds!");
			
			}
			else
				if (isopen) {
					animation.CrossFade("closingClaw");
					yield return new WaitForSeconds(animation["closingClaw"].length);
					//Debug.Log("This message appears after animation!");
					isopen = false;

					for (float timer = time_toStayclosed; timer >= 0; timer -= Time.deltaTime)
						yield return 0;

				}

		}

	
	}



    /*
   IEnumerator initMove()
   {
     
       //do enterring the scene between point 0 and point 1 
       yield return StartCoroutine(MoveObject(transform, transList[0].position, transList[1].position, 4.0f));
		
       while (true)
       {
           //loop through 1-> end
           if (CurIndex == transList.Count - 1) { CurIndex = 1;
           yield return StartCoroutine(MoveObject(transform, transList[transList.Count - 1].position, transList[CurIndex ].position, 3.0f));
           }
           yield return StartCoroutine(MoveObject(transform, transList[CurIndex].position, transList[CurIndex+1].position, 3.0f));

           CurIndex++;
           


          // yield return StartCoroutine(MoveObject(transform, transList[1].position, transList[2].position, 3.0f));
          // yield return StartCoroutine(MoveObject(transform, transList[2].position, transList[3].position, 3.0f));
          // yield return StartCoroutine(MoveObject(transform, transList[3].position, transList[4].position, 4.0f));
          // yield return StartCoroutine(MoveObject(transform, transList[4].position, transList[1].position, 4.0f));

       }
   }
*/




}

   

