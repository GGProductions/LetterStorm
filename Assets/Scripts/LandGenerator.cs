using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LandGenerator : MonoBehaviour {


	public GameObject block1;
	public GameObject block2;
	public GameObject block3;
	public GameObject block4;
	public GameObject block5;
	public GameObject block6;
	public GameObject block7;
	public GameObject block8;
	public GameObject block9;

    private bool block1_wasSpawned = false;
    private bool block2_wasSpawned = false;

	public enum State
	{
		brick,
		briksand,
		sand,
		sandbrick,
		sanddirt,
		dirt,
		grass,
		bridge,
		dirtsand
	}

	public State state;  

    void Awake()
    {
        state = LandGenerator.State.brick;
    }

	IEnumerator Start()
	{
		while (true)
		{
			switch (state)
			{
				case State.brick:
					brick();
					break;
				case State.briksand:
					briksand();
					break;
				case State.sand:
					sand();
					break;
				case State.sandbrick:
					sandbrick();
					break;
				case State.sanddirt:
					sanddirt();
					break;
				case State.dirt:
					dirt();
					break;
				case State.grass:
					grass();
					break;
				case State.bridge:
					bridge();
					break;
				case State.dirtsand:
					dirtsand();
			   
					break;
			}

			yield return 0;
		}
	}

   
		void brick(){
            if ( !block1_wasSpawned)
            Instantiate(block1, transform.position, transform.rotation);
            block1_wasSpawned = true;
            

        }
		void briksand(){}
		void sand(){}
		void sandbrick(){}
		void sanddirt(){}
		void dirt(){}
		void grass(){}
		void bridge(){}
		void dirtsand() { }




	// Update is called once per frame
	void Update () {
	
	}
}
