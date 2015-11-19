using UnityEngine;
using System.Collections;

public class FallingEnding : MonoBehaviour {

    public GameObject mainBGM;
    public GameObject floor;
    public GameObject floorLeft;
    public GameObject floorRight;
    public GameObject player;

    private float fallSpeed = 10f;

    private AudioSource screamSound;
    private AudioSource fallSound;

	// Use this for initialization
	void Start () {
        mainBGM.SetActive(false);
        floor.SetActive(false);
        SoundSourceInit();
        screamSound.Play();
	}
	
	// Update is called once per frame
	void Update () {
        MoveFloor();
	}

    void MoveFloor()
    {     
        if (floorLeft.transform.localEulerAngles.z > 270)
        {
            floorLeft.transform.Rotate(new Vector3(0, 0, -1f));    
        }        

        if( floorRight.transform.localEulerAngles.z < 90)
        {
            floorRight.transform.Rotate(new Vector3(0, 0, 1f));
        }
        
        CharacterController charControl = player.GetComponent<CharacterController>();
        charControl.Move(Vector3.down * Time.deltaTime * fallSpeed);
    }
    
   


    private void SoundSourceInit()
    {
        AudioSource[] audioSources = this.GetComponents<AudioSource>();
        foreach (AudioSource source in audioSources)
        {
            if (source.priority == 127)
            {
                fallSound = source;
            }
            else
            {
                screamSound = source;
            }
        }
    }
}
