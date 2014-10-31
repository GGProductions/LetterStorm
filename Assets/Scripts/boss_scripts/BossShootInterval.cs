using UnityEngine;
using System.Collections;

public class BossShootInterval_V1 : MonoBehaviour {
    public GameObject theShot;
    public float fireRate = 1.8f;




    IEnumerator Start()
    {
        while (gameObject)
        {

            Vector3 here = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
            Instantiate(theShot, here, transform.rotation);

            yield return new WaitForSeconds(fireRate);
        }


    }


}
