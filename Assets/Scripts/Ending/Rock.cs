using UnityEngine;
using System.Collections;

public class Rock : MonoBehaviour {

	private AudioSource rockSound;

	// Use this for initialization
	void Start () {
		rockSound = this.GetComponent<AudioSource> ();
	}
	
	void OnCollisionEnter(Collision collision)
	{
		if (!rockSound.isPlaying) 
		{
			rockSound.Play ();
		}
	}
}
