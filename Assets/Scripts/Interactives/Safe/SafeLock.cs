using UnityEngine;
using System.Collections;

public class SafeLock : InteractiveObject {

    public GameObject door;
	public GameObject c4Bomb;
    public GameObject mainBGM;
    private int lockCount = 0;        

	// Use this for initialization
	public override void Start () {
        base.Start();
        
	}
	
	// Update is called once per frame
    public override void Update()
    {
        base.Update();
	}

    public override void OnInteraction(Tool tool)
    {
        if ( tool is Hand)
        {
            Debug.Log("Lock working");
            StartCoroutine(RotateLock());

            if ( lockCount == 3)
            {
                StartCoroutine(OpenDoor());
            }
        }
    }

    IEnumerator RotateLock()
    {
        int i = 0;
        int count = 20;
        lockCount += 1;

        while (i < count)
        {
            i++;
            transform.Rotate(new Vector3(0f, 1f, 0));
            yield return null;
        }       
    }

    IEnumerator OpenDoor()
    {
        int i = 0;
        int count = 100;        

        while (i < count)
        {
            i++;
            door.transform.Rotate(new Vector3(0f, -1f, 0));
            yield return null;
        }
        Debug.Log("Open door");
		StartCoroutine (MoveC4Bomb ());
    }    

	IEnumerator MoveC4Bomb()
	{
		yield return new WaitForSeconds (0.1f);
		//c4Bomb.GetComponent<Rigidbody> ().AddForce(new Vector3(0, 200f, 0));

		int i = 0;
		int count = 180;

		while (i < count) 
		{
			i++;
			c4Bomb.transform.Translate(0, 0, 0.05f);
			yield return null;
		}
        mainBGM.SetActive(false);
        c4Bomb.GetComponent<AudioSource>().Play();
	}
}
