using UnityEngine;
using System.Collections;

public class LeverEnding : MonoBehaviour {

	private ParticleSystem[] particleSystem;
	private int number;

	private AudioSource[] audios;

	// Use this for initialization
	void Start () {
		particleSystem = this.GetComponentsInChildren<ParticleSystem> ();
		audios = this.GetComponents<AudioSource> ();

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.M))
		{
			ShowParticles();
		}
	}

	public void ShowParticles()
	{
		if (particleSystem != null) 
		{
			if( particleSystem[number].isPlaying )
			{
				particleSystem[number].Stop();
				audios[number].Stop ();
			}
			
			Random.seed = (int)System.DateTime.Now.Ticks;
			int fRand = Random.Range(0, 3); 
			number = fRand;
			
			particleSystem[number].Play();
			particleSystem[number].Play();
			audios[number].Play ();
		}
	}

	public void HideParticles()
	{
		foreach (ParticleSystem particle in particleSystem )
		{
			Destroy(particle);
		}
	}
}
