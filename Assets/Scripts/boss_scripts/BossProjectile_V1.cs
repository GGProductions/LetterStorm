using UnityEngine;
using System.Collections;

public class BossProjectile_V1 : MonoBehaviour {

    private float projectileSpeed;
    public GameObject explosion;
    void Start()
    {
        projectileSpeed = 10f;
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
        {
            Instantiate(explosion, this.transform.position, this.transform.rotation);
            Destroy(gameObject);
        }



    }


}
