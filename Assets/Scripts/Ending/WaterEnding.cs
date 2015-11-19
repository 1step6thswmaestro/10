using UnityEngine;
using System.Collections;

public class WaterEnding : MonoBehaviour {

    public GameObject water;
    public GameObject player;
    public GameObject mainBGM;
    
    private float speed = 6.0f;
    private float timeElapsed = 20.0f;
    private CharacterController charController;    

	// Use this for initialization
	void Start () {
        charController = player.GetComponent<CharacterController>();
        mainBGM.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        MoveUpward();
        timeElapsed -= Time.deltaTime;
        if( timeElapsed < 0)
        {
            EventBus.Instance.Clear();
            Application.LoadLevel(Application.loadedLevel);
        }
	}

    void MoveUpward()
    {
        Vector3 delta = Vector3.zero;
        delta = delta.normalized * speed;
        delta.y += 9.81f;

        water.transform.Translate(new Vector3(0, 0.01f, 0));
        charController.Move(delta * Time.deltaTime);
        player.transform.Translate(new Vector3(0, 0.01f, 0));          
    }


}
