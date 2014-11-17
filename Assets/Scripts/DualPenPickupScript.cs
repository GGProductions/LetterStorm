using UnityEngine;
using System.Collections;

public class DualPenPickupScript : MonoBehaviour
{

    #region Private Variables -------------------------------------------------
    float timeElapsed = 0;
    #endregion Private Variables ----------------------------------------------

    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.back * 1.2f * Time.deltaTime, Space.World);
        if (transform.position.z < -4f) Destroy(gameObject);

        timeElapsed += Time.deltaTime * 5;
        float factor = Mathf.Abs(Mathf.Cos(timeElapsed) * 0.5f) + 0.5f;

      //  Debug.Log(factor);
        this.transform.localScale = new Vector3(factor, 1, factor);
	}
}
