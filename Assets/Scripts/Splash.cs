using UnityEngine;
using System.Collections;

public class Splash : MonoBehaviour {
  
    public float delayTime = 5;
    
    public Texture backgroundTexture;
    
   
    
    void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), backgroundTexture);
    }
    
    IEnumerator Start()
    {
        yield return new WaitForSeconds( delayTime );
        
        Application.LoadLevel(1);
        
    }
}
