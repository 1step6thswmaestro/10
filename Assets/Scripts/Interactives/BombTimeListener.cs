using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BombTimeListener : MonoBehaviour {

    private Text text;

	// Use this for initialization
	void Start ()
    {
        text = this.GetComponent<Text>();
        EventBus.Instance.Register(this);
    }

    public void OnEvent(Bomb.BombTimeUpdatedEvent e)
    {
        // TODO : format string in 00:00:00

        string timeStr =  string.Format("{0:D2}:{1:D2}:{2:D2}", 
            (int)(e.bombTime / 60), 
            (int)(e.bombTime % 60), 
            (int)(e.bombTime * 100 % 100));

        text.text = timeStr;
    }

    void OnDestroy()
    {
        EventBus.Instance.Unregister(this);
    }
}
