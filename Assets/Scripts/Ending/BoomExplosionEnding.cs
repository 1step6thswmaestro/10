using UnityEngine;
using System.Collections;

public class BoomExplosionEnding : MonoBehaviour {

	private AudioSource boomSound;
	private AudioSource glassSound;
	private ParticleSystem particle;

	public GameObject[] walls;

	// Use this for initialization
	void Start () {
		InitSound ();
		particle = this.GetComponentInChildren<ParticleSystem> ();

	}

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.N)) {
			Explosion();
		}
	}

	public void Explosion()
	{
		boomSound.Play ();
		glassSound.Play ();
		particle.Play ();

		if (walls != null) 
		{
			foreach (GameObject wall in walls) {
				Rigidbody rb = wall.AddComponent<Rigidbody>();
				rb.useGravity = false;
				rb.AddExplosionForce(1f, wall.transform.position, 10000f);
			}
		}

		StartCoroutine(ReloadLevel(3.0f));
	}

	private IEnumerator ReloadLevel(float reloadTime)
	{
		yield return new WaitForSeconds(reloadTime);
		EventBus.Instance.Clear();
		Application.LoadLevel(Application.loadedLevel);        
	}

	private void InitSound()
	{
		AudioSource[] audios = this.GetComponents<AudioSource> ();

		foreach (AudioSource audio in audios) 
		{
			if( audio.priority == 128 )
			{
				boomSound = audio;
			}
			else if ( audio.priority == 127 )
			{
				glassSound = audio;
			}
		}
	}
}
