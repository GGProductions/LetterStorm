using UnityEngine;
using System.Collections;

public class HurricaneProjectile : MonoBehaviour
{

    #region Private Variables ---------------------------------------------
    private Transform CenterTarget;
    private float speedOfRotation;
    #endregion Private Variables ------------------------------------------

    // Use this for initialization
    void Start()
    {
        CenterTarget = GameObject.Find("AlbertPlayerPrefab").transform;     // Prepare hurricane projectile's center to be Albert
        speedOfRotation = 150f;
    }

    // Update is called once per frame
    void Update()
    {
        // Destroy Hurrican projectile if all outer projectiles are destroyed
        if (this.transform.childCount <= 0)
            Destroy(gameObject);

        this.transform.position = CenterTarget.position;                    // Center hurricane projectile's center to be Albert
        this.transform.Rotate(new Vector3(0, 2, 0) * speedOfRotation * Time.deltaTime); // Rotate center of hurricane projectile along with all attached (outer) projectiles
    }
}
