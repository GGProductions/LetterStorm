using UnityEngine;
using System.Collections;

public class Pencil1Projectile : MonoBehaviour
{
    #region Public Variables ---------------------------------------------
    public GameObject PencilExplosion;
    #endregion Public Variables ------------------------------------------

    #region Private Variables ---------------------------------------------
    private float currentRotationSpeed;
    #endregion Private Variables ------------------------------------------

    /// <summary>
    /// Initialize the pencils's rotation speed
    /// </summary>
    void Start()
    {
        currentRotationSpeed = 300f;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(new Vector3(0, 0, 2) * currentRotationSpeed * Time.deltaTime);
        if (transform.position.z > 20)
            Destroy(gameObject);
    }

    /// <summary>
    /// Pencil reacts to only two types of adverse tags: enemy and bossTag
    /// </summary>
    /// <param name="otherObj"></param>
    void OnTriggerEnter(Collider otherObj)
    {
        if (otherObj.tag == "enemy" || otherObj.tag == "bossTag")
        {
            Instantiate(PencilExplosion, transform.position, Quaternion.Euler(270, 0, 0));
            Destroy(gameObject);
        }
    }
}
