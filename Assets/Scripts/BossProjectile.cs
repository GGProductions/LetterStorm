using UnityEngine;
using System.Collections;

public class BossProjectile : MonoBehaviour {

    // declar a folat variable  (declaring a public in the header before START makes the values accessible mid game
    public float projectileSpeed;
   // public GameObject boom2;

    public GameObject explosion;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        float moveY;
        moveY = projectileSpeed * Time.deltaTime;
        transform.Translate(Vector3.forward * moveY);
        if (transform.position.z > 6.4f || transform.position.z < -7f || transform.position.x < -10f || transform.position.x > 10f) { Destroy(this.gameObject); }

    }

    void OnTriggerEnter(Collider otherObj)
    {
        if (otherObj.tag == "albertTag")
            // Context.PlayerInventory.AddCollectedLetter(otherObj.name);
            //GameObject go = otherObj.gameObject;
        {
            Instantiate(explosion, this.transform.position, this.transform.rotation);


            Destroy(gameObject);
        }



    }


}
