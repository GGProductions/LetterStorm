using UnityEngine;
using System.Collections;

public class Boss_Eye_script : MonoBehaviour {

    private float damp = 15f; //fast

    private Vector3 player_location_transform;
    public Quaternion angleNeeded;

    private Material _mat;
    public void Awake()
    {

        _mat = GetComponent<MeshRenderer>().material;

    }

/*
    void OnEnable()
    {
        Boss_3d_wordGen.OnWrongCollision += DoFlashEye;
    }
    void OnDisable()
    {
        Boss_3d_wordGen.OnWrongCollision -= DoFlashEye;
    }

    */
    /// <summary>
    /// update the rotation of the eye only
    /// //may be able to make it blink when boss gets upset or something like that
    /// </summary>
    void Update()
    {
        if(switchon)Debug.Log( switchon.ToString() + "EEEEEE" );
        Debug.Log(switchon.ToString());

        //locating Albert must be done continuously 
        player_location_transform = GameObject.Find("AlbertPlayerPrefab").transform.position;
        angleNeeded = Quaternion.LookRotation(player_location_transform - this.transform.position);
        //tilt the eye and aim acording to the damp factor for speed of rotation
        transform.rotation = Quaternion.Slerp(this.transform.rotation, angleNeeded, Time.deltaTime * damp);


    }

    public void DoFlashEye() {
        float howlong = 3.0f;
        StartCoroutine(waiting(howlong));
        StartCoroutine(FlashEye());  
        //_mat.color = Color.white;
       
    }


    private bool switchon = false;

    IEnumerator waiting(float timetowait)
    {
        switchon = true;
        yield return new WaitForSeconds(timetowait);
       switchon = false;
    }

    IEnumerator FlashEye()
    {

        float intervalTime = 0.2f;
        float time = 2f;
        float elapsedTime = 0f;
        int index = 0;
       // Debug.Log(" out time" + elapsedTime); 
        while (switchon)
        {
      //      Debug.Log(" index " + index);      

            elapsedTime += Time.deltaTime;
            index++;
            if (index % 2 == 0) _mat.color = Color.white;
            else
                _mat.color = Color.red;

            yield return new WaitForSeconds(intervalTime);
        }
        _mat.color = Color.white;
       
    
    }

}
