using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LandGenerator : MonoBehaviour {

	/*
	void OnEnable(){GeneratedLand.OnblockReachedEnd += changeState;}

	void OnDisable() { GeneratedLand.OnblockReachedEnd -= changeState;}
*/
	void changeState() {Debug.Log("im in land generator and heard that"); }
/*
	public GameObject block1;
	public GameObject block2;
	public GameObject block3;
	public GameObject block4;
	public GameObject block5;
	public GameObject block6;
	public GameObject block7;
	public GameObject block8;
	public GameObject block9;
*/


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

	public void Changestate() {
	
		int choice=0;
	//	Debug.Log("changeState" + state.ToString() + choice);
		///STATE1

		if (state == LandGenerator.State.brick) {

	  
			if (choice == 0) { state = LandGenerator.State.briksand; briksand();  }
		   else
				if (choice == 1) { state = LandGenerator.State.brick; brick(); }
		}
		else
		///STATE2
		if (state == LandGenerator.State.briksand) { state = LandGenerator.State.sand; sand(); }
		else
		///STATE13
		if (state == LandGenerator.State.sand)
		{
			choice = Random.Range(0, 3);
			if (choice == 0) { state = LandGenerator.State.sanddirt; sanddirt();}
			else
				if (choice == 1) { state = LandGenerator.State.sandbrick; sandbrick(); }
				else
					if (choice == 2) { state = LandGenerator.State.sand; sand(); }
		
		}
		else
		///STATE4
		if (state == LandGenerator.State.sandbrick) { state = LandGenerator.State.brick; brick(); }
		else
		///STATE5
		if (state == LandGenerator.State.sanddirt) {
			choice = Random.Range(0, 4);
			if (choice == 0) { state = LandGenerator.State.dirt; dirt(); }
			else
				if (choice == 1) { state = LandGenerator.State.grass; grass(); }
				else
					if (choice == 2) { state = LandGenerator.State.dirtsand; dirtsand(); }
					else
						if (choice == 3) { state = LandGenerator.State.bridge; bridge(); }
		}
		else
		///STATE6
		if (state == LandGenerator.State.dirt) {
			choice = Random.Range(0, 4);
			if (choice == 0) { state = LandGenerator.State.grass; grass(); }
			else
				if (choice == 1) { state = LandGenerator.State.dirt; dirt(); }
				else
					if (choice == 2) { state = LandGenerator.State.dirtsand; dirtsand(); }
					else
						if (choice == 3) { state = LandGenerator.State.bridge; bridge(); }
		
		}

		else
		///STATE7
		///


		if (state == LandGenerator.State.grass)
		{
			choice = Random.Range(0, 4);
			if (choice == 0) { state = LandGenerator.State.bridge; bridge(); }
			else
				if (choice == 1) { state = LandGenerator.State.grass; grass(); }
				else
					if (choice == 2) { state = LandGenerator.State.dirtsand; dirtsand(); }
					else
						if (choice == 3) { state = LandGenerator.State.dirt; dirt(); }

		}
		else

		///STATE8
		if (state == LandGenerator.State.bridge)
		{
			choice = Random.Range(0, 3);

			if (choice == 0) { state = LandGenerator.State.dirtsand; dirtsand(); }
			else
				if (choice == 1) { state = LandGenerator.State.grass; grass(); }
				else
					if (choice == 2) { state = LandGenerator.State.dirt; dirt(); }
			

		}
		else
		///STATE9
		if (state == LandGenerator.State.dirtsand)
		{
			state = LandGenerator.State.sand; sand(); 
		
		}
		///
		
	}


	/*
		void brick() { Instantiate(Resources.Load("LandBlocks/1_block"), transform.position, Quaternion.Euler(-180, 0, 0)); }
		void briksand(){Instantiate(Resources.Load("LandBlocks/2_block"), transform.position, Quaternion.Euler(-180, 0, 0)); }
		void sand() { Instantiate(Resources.Load("LandBlocks/3_block"), transform.position, Quaternion.Euler(-180, 0, 0)); }
	   
	void sandbrick() { Instantiate(Resources.Load("LandBlocks/4_block"), transform.position, Quaternion.Euler(-180, 0, 0)); }
	   
	void sanddirt() { Instantiate(Resources.Load("LandBlocks/5_block"), transform.position, Quaternion.Euler(-180, 0, 0)); }
	  
	void dirt() { Instantiate(Resources.Load("LandBlocks/6_block"), transform.position, Quaternion.Euler(-180, 0, 0)); }
 
	void grass() { Instantiate(Resources.Load("LandBlocks/7_block"), transform.position, Quaternion.Euler(-180, 0, 0)); }
	
	void bridge() { Instantiate(Resources.Load("LandBlocks/8_block"), transform.position, Quaternion.Euler(-180, 0, 0)); }
		void dirtsand() { Instantiate(Resources.Load("LandBlocks/9_block"), transform.position, Quaternion.Euler(-180, 0, 0)); }

	*/
			void brick() {
				if(Context.LevelNum %2 ==0)
				Instantiate(Resources.Load("LandBlocksIce/1_block"), transform.position, Quaternion.Euler(-180, 0, 0));
				else
					Instantiate(Resources.Load("LandBlocks/1_block"), transform.position, Quaternion.Euler(-180, 0, 0));

			}

		void briksand(){
			if (Context.LevelNum % 2 == 0)
				Instantiate(Resources.Load("LandBlocksIce/2_block"), transform.position, Quaternion.Euler(-180, 0, 0));
			 else
				Instantiate(Resources.Load("LandBlocks/2_block"), transform.position, Quaternion.Euler(-180, 0, 0));
		}
		void sand() {
			if (Context.LevelNum % 2 == 0) 
				Instantiate(Resources.Load("LandBlocksIce/3_block"), transform.position, Quaternion.Euler(-180, 0, 0));
			 else
				Instantiate(Resources.Load("LandBlocks/3_block"), transform.position, Quaternion.Euler(-180, 0, 0)); 
		}
	   
	void sandbrick() {
		if (Context.LevelNum % 2 == 0)
		Instantiate(Resources.Load("LandBlocksIce/4_block"), transform.position, Quaternion.Euler(-180, 0, 0)); 
	 else
		   Instantiate(Resources.Load("LandBlocks/4_block"), transform.position, Quaternion.Euler(-180, 0, 0)); 
		}
	   
	void sanddirt() {
		if (Context.LevelNum % 2 == 0)
		Instantiate(Resources.Load("LandBlocksIce/5_block"), transform.position, Quaternion.Euler(-180, 0, 0));
		 else
			Instantiate(Resources.Load("LandBlocks/5_block"), transform.position, Quaternion.Euler(-180, 0, 0)); 
	}
	  
	void dirt() {
		if (Context.LevelNum % 2 == 0) 
			Instantiate(Resources.Load("LandBlocksIce/6_block"), transform.position, Quaternion.Euler(-180, 0, 0));
		 else
			Instantiate(Resources.Load("LandBlocks/6_block"), transform.position, Quaternion.Euler(-180, 0, 0));
	}
 
	void grass() {
		if (Context.LevelNum % 2 == 0) 
			Instantiate(Resources.Load("LandBlocksIce/7_block"), transform.position, Quaternion.Euler(-180, 0, 0));
		 else
			Instantiate(Resources.Load("LandBlocks/7_block"), transform.position, Quaternion.Euler(-180, 0, 0)); 
	}
	
	void bridge() {
		if (Context.LevelNum % 2 == 0)
		Instantiate(Resources.Load("LandBlocksIce/8_block"), transform.position, Quaternion.Euler(-180, 0, 0));
		 else
			Instantiate(Resources.Load("LandBlocks/8_block"), transform.position, Quaternion.Euler(-180, 0, 0)); 
	}
		
	
	void dirtsand() {
		if (Context.LevelNum % 2 == 0)
		Instantiate(Resources.Load("LandBlocksIce/9_block"), transform.position, Quaternion.Euler(-180, 0, 0));
		 else
			Instantiate(Resources.Load("LandBlocks/9_block"), transform.position, Quaternion.Euler(-180, 0, 0)); 
	}

  


	// Update is called once per frame
	void Update () {
	
	}
}
