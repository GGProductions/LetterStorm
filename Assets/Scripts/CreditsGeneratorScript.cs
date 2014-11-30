using UnityEngine;
using System.Collections;

public class CreditsGeneratorScript : MonoBehaviour {
    private string ls = "letter storm";
    private string agame = "a game by";
    private string nabil = "nabil lamriben";
    private string david = "david lustig";
    private string paul = "paul pollack";
    private string jeannie = "jeannie trinh";
    private string james = "james raygor";
    private string music = "music by";
    private string han = "paul han";
    

    void makeString(string s, float zpos)
    {
        
        for (int index = 0; index < s.Length; index++) {

            char letter = s[index];
            float kern = (float)index;
            kern = kern * 0.8f;

            if (letter == ' ') { Debug.Log("blank"); }
            else
            if(letter == 'a' ||letter == 'e' ||letter == 'i' ||letter == 'o' ||letter == 'u' ||letter == 'y')
            Instantiate(Resources.Load("forCredits/" + letter),
               new Vector3(transform.position.x + kern, transform.position.y, transform.position.z + zpos), Quaternion.Euler(0, 0, 0));
            else
                Instantiate(Resources.Load("forCredits/" + letter),
              new Vector3(transform.position.x + kern, transform.position.y, transform.position.z + zpos), Quaternion.Euler(-90, 0, 0));
         
        }
    
    
    }

    float line = 0;
    float space = 7f;

	// Use this for initialization
	void Start () {
        makeString(ls, line);
        line = line + space;
        makeString(agame, line);
        line = line + space;
        makeString(david, line);
        line = line + space;
        makeString(james, line);
        line = line + space;
        makeString(jeannie, line);
        line = line + space;
        makeString(nabil, line);
        line = line + space;
        makeString(paul, line);
        line = line + space;
        makeString(music, line);
        line = line + space;
        makeString(han, line);
       

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
