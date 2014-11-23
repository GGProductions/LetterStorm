using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class oldBigbossMove : MonoBehaviour {


    #region publics
    public List<Transform> transList;

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


    private float time_toStayOpen = 15;
    private float time_toStayclosed = 0.5f;


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


    void Awake()
    {

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

    void calcCurnext()
    {

        if (CurIndex < transList.Count - 1)
        {
            nextIndex = CurIndex + 1;
        }
        else
            if (CurIndex == transList.Count - 1)
            {
                //  CurIndex = transList.Count-1 ;
                nextIndex = 0;
            }
            else
                if (CurIndex > transList.Count - 1)
                {
                    CurIndex = 0;
                    nextIndex = 1;
                }

    }

    IEnumerator Enetring()
    {

        while (transform.position.z > transList[0].position.z)
        {
            float amtToMove = -1f * Time.deltaTime;
            transform.position = new Vector3(0f, transform.position.y, transform.position.z + amtToMove);

            yield return 0;
        }
       // StopCoroutine("Enetring");
        Debug.Log("arrived");
        StartCoroutine("ONLYCOROUTINE");
    }


    private bool stopped = false;
    IEnumerator ONLYCOROUTINE()
    {


        while (true)
        {
            if (stopped)
            {
                yield break;
            }
            calcCurnext();
            //loop through 1-> end

            //   indexplusone = CurIndex + 1;
            yield return StartCoroutine(Move_A_B_time(transform, transList[CurIndex].position, transList[nextIndex].position, 3.0f));
            CurIndex++;

        }

        Debug.Log("GO TO ALBERT");
        Vector3 albertisat = thePlayerOBJ.transform.position;
        Vector3 curBossPo = transform.position;

        //this works  but 
        yield return StartCoroutine(Move_A_B_time(transform, curBossPo, albertisat, 0.2f));
        stopped = false;

    }



   
    public void CHaaaarge()
    {
        stopped = true;
        // Vector3 albertPos = thePlayerOBJ.transform.position;
        StopCoroutine("MovePtP_globalIndex");

        // StartCoroutine("breakCourputAttack");
  

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



 


    IEnumerator MoveObjecttoalbert_local(float time)
    {
        Vector3 albertisat = thePlayerOBJ.transform.position;

        yield return Move_A_B_time(transform, transform.position, albertisat, 0.2f);

      
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


    private bool isopen = false;

    IEnumerator Open_Close_animations()
    {
        //  animation.Stop("slithering");

        while (gameObject)
        {
            if (!isopen)
            {
                animation.CrossFade("openingClaw");
                yield return new WaitForSeconds(animation["openingClaw"].length);
                //	Debug.Log("This message appears after animation!");
                isopen = true;

                for (float timer = time_toStayOpen; timer >= 0; timer -= Time.deltaTime)
                    yield return 0;

                //Debug.Log("This message appears after 3 seconds!");

            }
            else
                if (isopen)
                {
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


