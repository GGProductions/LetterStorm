using UnityEngine;
using System.Collections;

public class Boss_eye_and_gun : MonoBehaviour {

    private Vector3 planeLoc;
    private Vector3 rotShift;
    public Quaternion angleNeeded;
    private int hitcount;
    private GameObject theBoss;
    private Boss_3d_wordGen bossWordGenScript;
    //  float damp = 1.8f; //easy
  // public  float damp = 10f; //hard
   public float damp = 3f; //med
   private int MaxHits = 5;

   private Transform cannonText_trans;
    void Awake()
    {
        hitcount = 0;
    }
    // Use this for initialization
    void Start()
    {

        cannonText_trans = transform.FindChild("CannonHEalthLabel");
       
        bossWordGenScript = GameObject.Find("Boss_V4_prefab").GetComponent<Boss_3d_wordGen>();  
       
        rotShift = new Vector3(0, 90, 0);
    }

    // Update is called once per frame
    void Update()
    {


        if (cannonText_trans!= null)
            cannonText_trans.GetComponent<TextMesh>().text = (MaxHits - hitcount + 1).ToString();

       // transform.FindChild("CannonHEalthLabel")
        planeLoc = GameObject.Find("AlbertPrefab2").transform.position;

        angleNeeded = Quaternion.LookRotation(planeLoc - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, angleNeeded, Time.deltaTime * damp);
        if (hitcount > MaxHits)
        {
            bossWordGenScript.AllerAlbert();
            Destroy(gameObject);
        }

    }
    
      


    void OnTriggerEnter(Collider otherObj)
    {
        
        if (otherObj.tag == "projectileTag" || otherObj.tag == "letterProjectile")
        {
            hitcount++;
        }

    }
}
