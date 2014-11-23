using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BigBoss_motion_animation : MonoBehaviour
{


    #region publics
    public List<Transform> transList ;

    public AnimationClip slither;
    public AnimationClip openClaw;
    public AnimationClip closeClaw;

    #endregion

    #region privates


    //local ref to leftclaw and rightclaw
	private Transform lc;
	private Transform rc;

    private GameObject thePlayerOBJ;

    private GameObject waipointBlockPrefab;
    private Transform waypointsGroup;

    private float timeElapsed;
    private int inttimeElapsed;


    private float time_toStayOpen = 3;
    private float time_toStayclosed = 5f;


    private int CurIndex;
    private int nextIndex;

    #endregion

    #region listenners

    void OnEnable()
	{
		Boss_3d_wordGen.OnWrongCollision += CHaaaarge;
	}
	void OnDisable()
	{
		Boss_3d_wordGen.OnWrongCollision -= CHaaaarge;
	}
    #endregion


	void Awake() {

		lc = transform.GetChild(0).GetChild(0).GetChild(2);
		rc = transform.GetChild(0).GetChild(0).GetChild(3);

		animation.AddClip(slither, "slithering");
		animation.AddClip(openClaw, "openingClaw");
		animation.AddClip(closeClaw, "closingClaw");
	}

	// Use this for initialization
    void Start()
    {
        transform.position = new Vector3(2f, 0.2f, 10f);
        thePlayerOBJ = GameObject.Find("AlbertPlayerPrefab");
        CurIndex = 0;

        //find all the waypoints , they should be clumped in a single prefab .
        //basically I find that prefab and count its children (eche child is a waypoint)
        transList = new List<Transform>();
        waipointBlockPrefab = GameObject.Find("waypints1");
        foreach (Transform c in waipointBlockPrefab.transform)
        {
            transList.Add(c);
        }

 
		animation.wrapMode = WrapMode.Loop;

		animation["openingClaw"].wrapMode = WrapMode.ClampForever;
		animation["closingClaw"].wrapMode = WrapMode.Clamp;
  
		animation["slithering"].layer = 0;
		animation["openingClaw"].layer = 10;
		animation["closingClaw"].layer = 10;
        //Isolate the animation for A specific bone
		animation["openingClaw"].AddMixingTransform(lc);
		animation["closingClaw"].AddMixingTransform(lc);
		animation["openingClaw"].AddMixingTransform(rc);
		animation["closingClaw"].AddMixingTransform(rc);

	   StartCoroutine(Open_Close_animations());
    //   StartCoroutine("MovePtP_globalIndex");
        //entring will start the loop around the waypoint 
      StartCoroutine("Enetring");

	}

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
            float amtToMove = -1f * Time.deltaTime;
            transform.position = new Vector3(0f, transform.position.y, transform.position.z + amtToMove);

            yield return 0;      
        }
        StopCoroutine("Enetring");
        Debug.Log("arrived");
        StartCoroutine("onlyCoroutine");
    }


    private bool stopped=false;
    IEnumerator onlyCoroutine()
    {
       

        while (true)
        {
           if (stopped)
            {
               break;
            }  
            calcCurnext();
            //loop through 1-> end
          
            //   indexplusone = CurIndex + 1;
            yield return StartCoroutine(Move_A_B_time(transform, transList[CurIndex].position, transList[nextIndex].position, 3.0f));
            CurIndex++;
        
        }
        //Vector3 curBossPo = transform.position;

        //this works  but 
        //yield return StartCoroutine(Special_Move_A_B_time(transform, curBossPo, albertisat, 0.2f));
        yield return StartCoroutine(Special_back_Move_A_B_time(transform, transList[nextIndex].position, 0.3f));
      


    }

    IEnumerator Special_back_Move_A_B_time(Transform thisTransform,  Vector3 endPos, float time)
    {
        Vector3 albertisat = thePlayerOBJ.transform.position;
        Vector3 curBossPo = transform.position;
        yield return StartCoroutine(Special_Move_A_B_time(transform, curBossPo, albertisat, 0.2f));

         curBossPo = transform.position;
        Debug.Log("we in special");
        float i = 0.0f;
        float rate = 1.0f / time;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            thisTransform.position = Vector3.Lerp(curBossPo, endPos, i);
            yield return null;
        }
        Debug.Log("we out special");
        stopped = false;

        StartCoroutine("onlyCoroutine");
    }


    IEnumerator Special_Move_A_B_time(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
    {
        Debug.Log("we in special");
        float i = 0.0f;
        float rate = 1.0f / time;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            thisTransform.position = Vector3.Lerp(startPos, endPos, i);
            yield return null;
        }
        Debug.Log("we out special");
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
        stopped = true;
        Debug.Log("stopped =ttrue");
       // Vector3 albertPos = thePlayerOBJ.transform.position;
     //  StopCoroutine("MovePtP_globalIndex");
      
       // StartCoroutine("breakCourputAttack");
       // Debug.Log("GO TO ALBERT");
      

        //this doesnot work
       // StartCoroutine("breakCourputAttack");
    }

    IEnumerator Move_A_B_time2(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
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



    IEnumerator breakCourputAttack( )
    {
        //stopped = true;
       // StopCoroutine("Enetring");
      //
       // StopCoroutine("Enetring");
       // StopCoroutine("MovePtP_globalIndex");

        Debug.Log("GO TO ALBERT");
        Vector3 albertisat = thePlayerOBJ.transform.position;
        Vector3 curBossPo = transform.position;
        StartCoroutine(Move_A_B_time(transform, curBossPo, albertisat, 0.2f));
        yield return 0;
     //   yield return StartCoroutine(Move_A_B_time(transform, curBossPo, transList[CurIndex].position, 0.2f));
     //    yield return 0;
     //   yield return StartCoroutine(MoveObjecttoalbert_local(0.2f));

       // Debug.Log("I should be here going to " + alberLoc);
      //  StopCoroutine("initMovePASS");
       
      //  yield return StartCoroutine( "MoveObjecttoalbert_local", 0.1f  );
     //   yield return StartCoroutine(MoveObject(transform, alberLoc, transList[oldIndex +1 ].position, 3.0f));
      //  yield return StartCoroutine("initMovePASS", oldIndex+1 );
    }


    IEnumerator MoveObjecttoalbert_local(float time)
    {
          Vector3 albertisat = thePlayerOBJ.transform.position;

        yield return Move_A_B_time(transform, transform.position, albertisat, 0.2f);

/*

       //   CurIndex++;
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

   
  //    StartCoroutine("initMovePASS", CurIndex );
    //    StartCoroutine("MovePtP_globalIndex");


        */
    }


	IEnumerator Move_A_B_time(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
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




	void LateUpdate()
	{
       
		animation.CrossFade("slithering");
     //   Debug.Log("cur is " + CurIndex);
	
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
  ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
 /// this guy and the other one with no parameters is how I used to move from wp to wp
 ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////   
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
            else
          //   indexplusone = CurIndex + 1;
            yield return StartCoroutine(MoveObject(transform, transList[CurIndex].position, transList[indexplusone].position, 3.0f));

            CurIndex++;
            indexplusone++;
        }


    }
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
     	
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     * 
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
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     * 
    IEnumerator Func1()
	{
		animation.CrossFade("openingClaw");
		yield return new WaitForSeconds(animation["openingClaw"].length);

	}


 
      
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
     * 
     *
	void sidetoside() {
		timeElapsed += Time.deltaTime;
		float factor = Mathf.Cos(timeElapsed);
		transform.Translate(Vector3.right * (factor / 20) * Time.timeScale, Space.World);
		transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
	} 
     * 
     * 
     * 
     *
*/




}

   

