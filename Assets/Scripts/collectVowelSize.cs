using UnityEngine;
using System.Collections;

public class collectVowelSize : MonoBehaviour {

  
    // Use this for initialization
    void Start()
    {
        transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.back * 1.2f * Time.deltaTime, Space.World);
        if (transform.position.z < -11f) Destroy(gameObject);
    }
}
