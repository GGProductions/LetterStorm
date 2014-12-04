using UnityEngine;
using System.Collections;

public class AlbertOnlyRun : MonoBehaviour {

	public AnimationClip walkAnimationClip;
    private float OriginalScale = 4f;
	/// </summary>
	void Awake()
	{

		animation.wrapMode = WrapMode.Loop;
		animation.AddClip(walkAnimationClip, "walking");

   
	}

	// Use this for initialization
	void Start () {
        this.transform.localScale = new Vector3(OriginalScale, OriginalScale, OriginalScale);
	}
	
	// Update is called once per frame
	void Update () {
		animation.Play("walking");
	}
}
