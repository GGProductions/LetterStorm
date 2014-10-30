using UnityEngine;
using System.Collections;

public class terrainScroll : MonoBehaviour {

    #region Fields

    private float Speed=1.2f;

    #endregion

    #region Properties

    #endregion

    #region Functions

    void Update()
    {
        float amtToMove = Speed * Time.deltaTime;
        transform.Translate(Vector3.back * amtToMove, Space.World);

        if (transform.position.z < -16)
        {
           // Destroy(gameObject);
            //old option to send th eblock back to the start w
            transform.position = new Vector3(transform.position.x, transform.position.y, 176f);
        }
    }
    #endregion
}
