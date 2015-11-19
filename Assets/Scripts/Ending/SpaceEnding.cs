using UnityEngine;
using System.Collections;

public class SpaceEnding : MonoBehaviour {

    public GameObject[] walls;
    public GameObject[] officeStuff;
    public GameObject floor;
    public GameObject mainBGM;
    public GameObject player;

    private float reloadTime = 30.0f;
    private AudioSource spaceSound;
    private CharacterController charController;

    void Start()
    {
        mainBGM.SetActive(false);   
        spaceSound = this.GetComponent<AudioSource>();
        charController = player.GetComponent<CharacterController>();
        
        HideObjects();
        StartCoroutine(ScaleUpFloor());
        StartCoroutine(ReloadLevel(reloadTime));
    }

    private void HideObjects()
    {
        foreach(GameObject wall in walls)
        {            
            StartCoroutine(ScaleDownWalls(wall));
        }

        foreach(GameObject stuff in officeStuff)
        {
            stuff.SetActive(false);
        }
    }    

    private IEnumerator ScaleDownWalls(GameObject wall)
    {
        int i = 0;
        int count = 1000;
        while( i < count )
        {
            i++;
            if( wall.transform.localScale.x < 0 )
            {
                wall.SetActive(false);
                break;
            }
            wall.transform.localScale -= new Vector3(0.05f, 0, 0);
            yield return null;
        }
    }

    private IEnumerator ScaleUpFloor()
    {
        int i = 0;
        int count = 1000;

        while( i < count)
        {
            i++;
            floor.transform.localScale += new Vector3(0.05f, 0, 0.05f);            
            yield return null;
        }
    }

    private IEnumerator ReloadLevel(float reloadTime)
    {
        yield return new WaitForSeconds(reloadTime);
        EventBus.Instance.Clear();
        Application.LoadLevel(Application.loadedLevel);        
    }
}
