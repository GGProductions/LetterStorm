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


	private float time_toStayOpen ;
	private float time_toStayclosed;


	private int CurIndex;
	private int nextIndex;


	private bool isopen = false;

	private bool stop_followingWaypoints = false;

    private Transform colorCube;

    private List<Color32> mycolors;

 

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

	/// <summary>
	/// get ref to claws and initialize animations
	/// </summary>
	void Awake() {

		lc = transform.GetChild(0).GetChild(0).GetChild(2);
		rc = transform.GetChild(0).GetChild(0).GetChild(3);

		animation.AddClip(slither, "slithering");
		animation.AddClip(openClaw, "openingClaw");
		animation.AddClip(closeClaw, "closingClaw");

        colorCube = transform.Find("Cube");
        Debug.Log(colorCube.name);
      //  colorCube.GetComponent<Renderer>().material.color = Color.red;
    //    colorCube.GetComponent<Renderer>().material.color = mygreen;
        mycolors = new List<Color32>();
        Color32 c0 = new Color32(100, 48, 8, 255);
        Color32 c1 = new Color32(0, 217, 70, 255);
        Color32 c2 = new Color32(228, 240, 18, 255);
        Color32 c3 = new Color32(200, 50, 50, 255);
        Color32 c4 = new Color32(120, 40, 10, 255);
        Color32 c5 = new Color32(50, 200, 200, 255);
        Color32 c6 = new Color32(10, 0, 130, 255);
        Color32 c7 = new Color32(20, 50, 20, 255);
        Color32 c8 = new Color32(13, 75, 13, 255);
        Color32 c9 = new Color32(8, 48, 8, 255);
        
        mycolors.Add(c0);
        mycolors.Add(c1);
        mycolors.Add(c2);
        mycolors.Add(c3);
        mycolors.Add(c4);
        mycolors.Add(c5);
        mycolors.Add(c6);
        mycolors.Add(c7);
        mycolors.Add(c8);
        mycolors.Add(c9);
        
        int index = Context.LevelNum%10;
        colorCube.GetComponent<Renderer>().material.color = mycolors[index];

    }

	/// <summary>
	///  starts with "enterring" coroutine which will then start the waypoint navigation coroutine
	/// </summary>
	void Start()
	{

     //   mycolors= new Color32[10];



   
       time_toStayOpen = 0.5f;
	   time_toStayclosed = 0.2f;


		transform.position = new Vector3(2f, 0.2f, 10f);
		thePlayerOBJ = GameObject.Find("AlbertPlayerPrefab");
		CurIndex = 0;

		//find all the waypoints , they should be clumped in a single prefab .
		//basically I find that prefab and count its children (eche child is a waypoint)
		transList = new List<Transform>();
        if (Context.LevelNum % 2 == 0) { waipointBlockPrefab = GameObject.Find("waypints3"); }
        else
            if (Context.LevelNum % 2 == 1) { waipointBlockPrefab = GameObject.Find("waypints2"); }
         //   else
           //     if (Context.LevelNum % 3 == 2) { waipointBlockPrefab = GameObject.Find("waypints3"); }
        


		foreach (Transform c in waipointBlockPrefab.transform)
		{
			transList.Add(c);
		}

		animation.wrapMode = WrapMode.Loop;

		animation["openingClaw"].wrapMode = WrapMode.ClampForever;
		animation["closingClaw"].wrapMode = WrapMode.Clamp;

		//set layers for animation priorities , lowest layers have highest priority
		//this will ensure that the boss keeps on slithering while he opens and closes his claws
		animation["slithering"].layer = 0;
		animation["openingClaw"].layer = 10;
		animation["closingClaw"].layer = 10;
		//Isolate the animation for A specific bone
		animation["openingClaw"].AddMixingTransform(lc);
		animation["closingClaw"].AddMixingTransform(lc);
		animation["openingClaw"].AddMixingTransform(rc);
		animation["closingClaw"].AddMixingTransform(rc);

	   StartCoroutine(Open_Close_animations());
 
	  //entring will start the loop around the waypoint 
	  StartCoroutine("Enetring");


	}

	/// <summary>
	/// makes sure there is no overflow , the next waypoint cannot be greater than # of componants in translist
	/// </summary>
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


	/// <summary>
	/// the initial coroutine , the boss is instanciated and slowly moves to waypoint 0
	/// please make sure that waypoint 0 is lined up with the boss, because the boss only stops when he reaches the level of the waypoint on the x axis 
	/// -When boss arrives at destination, he starts his "move coroutine"
	/// </summary>
	/// <returns></returns>
	IEnumerator Enetring() {

		while (transform.position.z > transList[0].position.z)
		{
			float amtToMove = -1f * Time.deltaTime;
			transform.position = new Vector3(0f, transform.position.y, transform.position.z + amtToMove);

			yield return 0;      
		}
		StartCoroutine("Waypoint_routine");
	}


	/// <summary>
	/// will navigathe through waypoints if the boss was not hit with a wrong letter
	/// </summary>
	/// <returns></returns>
	IEnumerator Waypoint_routine()
	{
		while (true)
		{
		   if (stop_followingWaypoints)
			{
			   break;
			}  
			calcCurnext();
			yield return StartCoroutine(Move_A_B_time(transform, transList[CurIndex].position, transList[nextIndex].position, 3.0f));
			CurIndex++;
		
		}
		//starting this coroutine with move back, but mooveback will wait for move forward to be done 
		yield return StartCoroutine(move_back_from_Albert_location(transform, transList[nextIndex].position, 0.3f));

	}

	/// <summary>
	/// single coroutine that will first wait fro movetowardAlbert to finish 
	/// this will set stopfollow to false , untill boss is hit witha  wrong letter again
	/// </summary>
	/// <param name="thisTransform"> this bigboss</param>
	/// <param name="endPos"> ends at albet's location</param>
	/// <param name="time"> .2f is pretty fast </param>
	/// <returns></returns>
	IEnumerator move_back_from_Albert_location(Transform thisTransform,  Vector3 endPos, float time)
	{
		Vector3 albertisat = thePlayerOBJ.transform.position;
		Vector3 curBossPo = transform.position;
		yield return StartCoroutine(move_toward_Albert_location(transform, curBossPo, albertisat, 0.2f));

		 curBossPo = transform.position;
	  //  Debug.Log("we in special");
		float i = 0.0f;
		float rate = 1.0f / time;
		while (i < 1.0f)
		{
			i += Time.deltaTime * rate;
			thisTransform.position = Vector3.Lerp(curBossPo, endPos, i);
			yield return null;
		}
		//Debug.Log("we out special");
		stop_followingWaypoints = false;

		StartCoroutine("Waypoint_routine");
	}

	/// <summary>
	/// single coroutine moves boss from current location to Albert's location
	/// </summary>
	/// <param name="thisTransform"></param>
	/// <param name="startPos"></param>
	/// <param name="endPos"></param>
	/// <param name="time"></param>
	/// <returns></returns>
	IEnumerator move_toward_Albert_location(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
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



	/// <summary>
	/// only sets a bool instead of trying to stop waypoint coroutine, and then start a new one, then stop that one , and resume waypoinnavigation
	/// </summary>
	public void CHaaaarge()
	{
		stop_followingWaypoints = true;
	   // Debug.Log("stopped =ttrue");
	
	}

   
	/// <summary>
	/// most basic coroutine which moves any object from A to B in t time
	/// </summary>
	/// <param name="thisTransform"></param>
	/// <param name="startPos"></param>
	/// <param name="endPos"></param>
	/// <param name="time"></param>
	/// <returns></returns>
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


	/// <summary>
	/// only activates the slither animation
	/// </summary>
	void LateUpdate()
	{   
		animation.CrossFade("slithering");
	}


	
	/// <summary>
	/// opening anc closing claws accoring to opentime and close time variables
	/// </summary>
	/// <returns></returns>
	IEnumerator Open_Close_animations() {

		while (gameObject) {
			if (!isopen) {
				animation.CrossFade("openingClaw");
				yield return new WaitForSeconds(animation["openingClaw"].length);
				isopen = true;

				for (float timer = time_toStayOpen; timer >= 0; timer -= Time.deltaTime)
					yield return 0;		
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




}

   

