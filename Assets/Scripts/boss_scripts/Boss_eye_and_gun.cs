using UnityEngine;
using System.Collections;

public class Boss_eye_and_gun : MonoBehaviour {

    private Vector3 planeLoc;
    private Vector3 rotShift;
    public Quaternion angleNeeded;
    private int hitcount;
    private GameObject theBoss;
    private Boss_3d_wordGen bossWordGenScript;
    void Awake()
    {
    
    }
    // Use this for initialization
    void Start()
    {
        bossWordGenScript = GameObject.Find("Boss_V4_prefab").GetComponent<Boss_3d_wordGen>();  
        hitcount = 0;
        rotShift = new Vector3(0, 90, 0);
    }

    // Update is called once per frame
    void Update()
    {
        planeLoc = GameObject.Find("AlbertPrefab2").transform.position;
        float damp = 1.8f;
        angleNeeded = Quaternion.LookRotation(planeLoc - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, angleNeeded, Time.deltaTime * damp);
        if (hitcount > 10)
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
