using UnityEngine;
using System.Collections;

public class BossProjectile : MonoBehaviour {

    public float projectileSpeed;
    public GameObject explosion;
    void Start()
    {

    }

    /// <summary>
    /// Move forward and destroy if out of bounds
    /// </summary>
    void Update()
    {
        float moveY;
        moveY = projectileSpeed * Time.deltaTime;
        transform.Translate(Vector3.forward * moveY);
        if (transform.position.z > 6.4f || transform.position.z < -7f || transform.position.x < -10f || transform.position.x > 10f) { Destroy(this.gameObject); }
    }

    /// <summary>
    /// only handles collisions with Albert by tag
    /// </summary>
    /// <param name="otherObj"></param>
    void OnTriggerEnter(Collider otherObj)
    {
        if (otherObj.tag == "albertTag")
        {
            Instantiate(explosion, this.transform.position, this.transform.rotation);
            Destroy(gameObject);
        }



    }


}
