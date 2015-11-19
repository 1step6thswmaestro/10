using UnityEngine;
using System.Collections;

public class Ceiling : MonoBehaviour {

	public GameObject wallLine;

    private AudioSource ceilingSound;

    void Awake()
    {
        ceilingSound = this.GetComponent<AudioSource>();
    }

	// Use this for initialization
	void Start () {

	}	
	

    void CeilingDown()
    {
        StartCoroutine(MoveDownWards());
    }
    

	IEnumerator MoveDownWards()
	{
        if (!ceilingSound.isPlaying)
        {
           ceilingSound.Play();
        }        

		while (transform.position.y > wallLine.transform.position.y) 
		{			
			transform.Translate(new Vector3(0, -0.02f,0));
			yield return null;
		}        
	}    
}
