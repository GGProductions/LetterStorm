using UnityEngine;
using System.Collections;

public class GeneratedLand : MonoBehaviour {


   // public delegate void blockReachedEnd();
   // public static event blockReachedEnd OnblockReachedEnd;

    #region Fields

    private float Speed = 1.2f;

    private GameObject theLandGenerator;
    private LandGenerator landGenScript;

  
    #endregion

    #region Properties

    #endregion

    #region Functions



    void Awake() {
        theLandGenerator = GameObject.Find("LandGenerator");
        landGenScript = theLandGenerator.GetComponent<LandGenerator>();
    }

    void Update()
    {
        float amtToMove = Speed * Time.deltaTime;
        transform.Translate(Vector3.back * amtToMove, Space.World);

        if (transform.position.z < -16)
        {
            landGenScript.Changestate();
            Destroy(gameObject);
 
        }
    

    }



    #endregion
}
