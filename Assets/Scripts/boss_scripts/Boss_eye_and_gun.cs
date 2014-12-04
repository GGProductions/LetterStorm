using UnityEngine;
using System.Collections;

public class Boss_eye_and_gun : MonoBehaviour {
    /// <summary>
    /// damp factor that controls how fast aiming at albert is to be done.
    /// damp = 1.8f ----> easy  mode , long lag 
    /// damp = 3.0f ----> medium  mode , medium lag 
    /// damp = 10f ----> Hard  mode , very short lag,  
    /// </summary>
   public float damp = 3f; //med

    /// <summary>
    ///  Maximum amount of hits needed to kill the gun, this script is also attached to bossEey, but no worries: BossEye's collider is turned off
    /// </summary>
   private int MaxHits = 5;


   private Vector3 player_location_transform;
   public Quaternion angleNeeded;
   private int hitcount;
   private GameObject theBoss;
   private Boss_3d_wordGen bossWordGenScript;
   private Transform cannonText_trans;

    /// <summary>
    /// intitialize the hitcount to 0 
    /// </summary>
    void Awake()
    {
        hitcount = 0;
    }

    /// <summary>
    /// on start get a ref to :
    /// 1) cannonText label
    /// 2) The Boss of this gun 
    /// </summary>
    void Start()
    {
        cannonText_trans = transform.FindChild("CannonHEalthLabel");
       
        bossWordGenScript = GameObject.Find("Boss_V4_prefab").GetComponent<Boss_3d_wordGen>();  

    }

    /// <summary>
    /// update the cannon's text lable
    /// update the rotation of the gun or eye aiming at albert 
    /// </summary>
    void Update()
    {
        //Since this script is also attached to the bosseye and the eye has no CanonText attached to it ...
        if (cannonText_trans!= null)
            cannonText_trans.GetComponent<TextMesh>().text = (MaxHits - hitcount + 1).ToString();

        //locating Albert must be done continuously 
        player_location_transform = GameObject.Find("AlbertPlayerPrefab").transform.position;
        angleNeeded = Quaternion.LookRotation(player_location_transform - this.transform.position);

        //tilt the gun and aim acording to the damp factor for speed of rotation
        transform.rotation = Quaternion.Slerp(this.transform.rotation, angleNeeded, Time.deltaTime * damp);

        if (hitcount > MaxHits)
        {
            //Let the boss know that this gun has died. Boss will deal with allerting allbert if the required amount of guns have died.
            bossWordGenScript.AllerAlbert();
            Destroy(gameObject);
        }

    }
     
    /// <summary>
    /// Register hits from  Pencils tagged "projectileTag" or "letterprojectile" <--- the latter may not be needed ... keep for now
    /// </summary>
    /// <param name="otherObj"></param>
    void OnTriggerEnter(Collider otherObj)
    {     
        if (otherObj.tag == "projectileTag" || otherObj.tag == "letterProjectile")
        {
            hitcount++;
        }

    }


}
