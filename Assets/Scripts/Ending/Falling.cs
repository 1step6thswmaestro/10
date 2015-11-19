using UnityEngine;
using System.Collections;

public class Falling : MonoBehaviour {

    private AudioSource fallSound;

	// Use this for initialization
	void Start () {
        fallSound = this.GetComponent<AudioSource>();
	}


    void OnCollisionEnter(Collision collision)
    {
        
        
            if (!fallSound.isPlaying)
            {
                fallSound.Play();                
                StartCoroutine(ReloadLevel(2f));
            }
            
        
                
    }    

    IEnumerator ReloadLevel(float time)
    {
        yield return new WaitForSeconds(time);
        EventBus.Instance.Clear();
        Application.LoadLevel(Application.loadedLevel);
    }
    
}
