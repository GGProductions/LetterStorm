﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class musics : MonoBehaviour {

	public AudioClip[] AudioClips;

	public AudioSource au_footsteps;
	// Use this for initialization

	private List<string> songnames =new List<string>();

	private 

	void Start () {
		songnames.Add("SFX/A_Night_Of_Dizzy_Spells");
		songnames.Add("SFX/Letterstorm_by_Jake_Han");
		songnames.Add("SFX/Chibi_Ninja");
		songnames.Add("SFX/Come_and_Find_Me");
		songnames.Add("SFX/HHavok_intro");
		songnames.Add("SFX/Jumpshot");
	   
	   // audio.clip = AudioClips[0];

   // audio.Play();

		int index = Context.LevelNum % 6;

		au_footsteps = (AudioSource)gameObject.AddComponent("AudioSource");
		AudioClip myAudioclip;
		myAudioclip = (AudioClip)Resources.Load(songnames[index]);



		au_footsteps.clip = myAudioclip;
		au_footsteps.loop=true;
		au_footsteps.Play();
	}
	
	// Update is called once per frame
	void Update () {

		

	}


}
