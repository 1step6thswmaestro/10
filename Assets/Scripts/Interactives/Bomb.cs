using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {

    // Events
    public class BombDefusedEvent { }
    public class BombExplodedEvent { }

    public class BombTimeUpdatedEvent {
        public float bombTime;
        public BombTimeUpdatedEvent(float bombTime) { this.bombTime = bombTime; }
    }

    // enum
    private enum State
    {
        Planted,
        Exploded,
        Defused,
    }

    private State state;

    public float bombTime;
    private AudioSource clickSound;

    
    // Use this for initialization
    void Start () {

        this.state = State.Planted;
        EventBus.Instance.Register(this);
    }
	
	// Update is called once per frame
	void Update () {

        switch (state)
        {
            case State.Planted:

                bombTime -= Time.deltaTime;

                if (bombTime < 0)
                {
                    this.state = State.Exploded;
                    EventBus.Instance.Post(new BombExplodedEvent());
                    bombTime = 0;
                }

                EventBus.Instance.Post(new BombTimeUpdatedEvent(bombTime));

                break;

            default:
                break;
        }
    }

    void OnDestroy()
    {
        EventBus.Instance.Unregister(this);
    }

    public void OnEvent(BombDefusedEvent evnt)
    {
        this.state = State.Defused;
        Debug.Log("The bomb has been defused");

        // TODO : do game over (successful)
        StartCoroutine(WaitAndReload(5));
    }

    public void OnEvent(BombExplodedEvent evnt)
    {
        this.state = State.Exploded;
        Debug.Log("The bomb has been exploded");

        // TODO : do game over (fail)
        StartCoroutine(WaitAndReload(5));
    }

    private IEnumerator WaitAndReload (float seconds)
    {
        yield return new WaitForSeconds(seconds);

        Application.LoadLevel(Application.loadedLevel);

        yield break;
    }
}
