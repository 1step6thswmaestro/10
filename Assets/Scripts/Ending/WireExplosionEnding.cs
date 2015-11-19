using UnityEngine;
using System.Collections;

public class WireExplosionEnding : MonoBehaviour {


	private ParticleSystem[] particles;
	private AudioSource boomSound;
	private AudioSource explosionBGM;

	public GameObject[] officeStuff;

    public GameObject mainBGM;

	// Use this for initialization
	void Start () {
		//StartCoroutine (ReloadLevel (30.0f));

        mainBGM.SetActive(false);

		InitSettings ();
		StartCoroutine(PlayParticles ());
		StartCoroutine(ExplodeObjects(officeStuff));
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Q)) 
		{
			StartCoroutine(ExplodeObjects(officeStuff));
		}
	}

	IEnumerator ExplodeObjects(GameObject[] officeStuff)
	{
		while (true) 
		{
			float adjustForce = 3000.0f;			
			
			foreach (GameObject stuff in officeStuff) 
			{
				Debug.Log (stuff.name);
				float force = 300f;
				Rigidbody rb = stuff.GetComponent<Rigidbody> ();
				
				if(rb.mass > 1)
				{
					force *= adjustForce;
				}
				rb.AddExplosionForce (force, stuff.transform.position, 1000f);
			}
			yield return new WaitForSeconds (2.0f);
		}
	}

	private void InitSettings()
	{
		particles = this.GetComponentsInChildren<ParticleSystem> ();

		AudioSource[] AudioSources = this.GetComponents<AudioSource> ();
		boomSound = AudioSources [0];
        explosionBGM = AudioSources[1];
	}

	IEnumerator PlayParticles()
	{
		while (true) 
		{	
			foreach(ParticleSystem particle in particles )
			{
				if( !particle.isPlaying )
				{
					particle.Play ();
				}

				if (!boomSound.isPlaying )
				{
					boomSound.Play ();
				}

				yield return new WaitForSeconds(1.0f);
			}
		}
	}

	private IEnumerator ReloadLevel(float reloadTime)
	{
		yield return new WaitForSeconds(reloadTime);
		EventBus.Instance.Clear();
		Application.LoadLevel(Application.loadedLevel);        
	}
}
