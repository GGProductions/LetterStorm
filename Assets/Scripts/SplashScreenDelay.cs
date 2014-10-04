using UnityEngine;
using System.Collections;

public class SplashScreenLoader : MonoBehaviour {
    public float delayTime = 5;
    public bool done = false;
    
    private float timer;
    
    void Start() {
        timer = delayTime;
        
        StartCoroutine( "SomeFunction");
    }
    
    void Update() {
        
        if(timer > 0)
        timer -= Time.deltaTime;
        return;
        
        if(done)
        Application.LoadLevel(1);
    }
    
    IEnumerator SomeFunction() {
        
        
        yield return null;
    } 
}