using UnityEngine;
using System.Collections;

public class DualPencilProjectile : MonoBehaviour
{

    #region Private Variables ---------------------------------------------
    private Transform Pencil1;
    private Transform Pencil2;
    #endregion Private Variables ------------------------------------------

    // Use this for initialization
	void Start () {
        //Pencil1 = this.transform.FindChild("pencil1");
        //Pencil2 = this.transform.FindChild("pencil2");
	}
	
	// Update is called once per frame
	void Update () {
        if (this.transform.childCount <= 0)
            Destroy(gameObject);
        //Pencil1.Rotate(new Vector3(0, 0, 2) * 20f * Time.deltaTime);
        //Pencil2.Rotate(new Vector3(0, 0, 2) * 20f * Time.deltaTime);
	}
}
