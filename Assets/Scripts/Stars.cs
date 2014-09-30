using UnityEngine;
using System.Collections;

public class Stars : MonoBehaviour
{
    #region Fields

    public float Speed;

    #endregion

    #region Properties

    #endregion

    #region Functions

    void Update()
    {
        float amtToMove = Speed * Time.deltaTime;
        transform.Translate(Vector3.back * amtToMove, Space.World);

        if (transform.position.z < -12.3)
        {
            transform.position = new Vector3(transform.position.x,  transform.position.y,  12);
        }
    }
    #endregion
}
