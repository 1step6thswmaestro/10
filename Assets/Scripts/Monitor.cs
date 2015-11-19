using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Monitor : MonoBehaviour {

    private static string PlayCount = "PlayCount";

    public Text display;

    private int playCount;

	// Use this for initialization
	void Start () {
        playCount = PlayerPrefs.GetInt(PlayCount, 0);
        playCount++;
        PlayerPrefs.SetInt(PlayCount, playCount);
        PlayerPrefs.Save();

        if (display)
        {
            display.text = "<color=white>" + playCount + "</color>";
        }

        TurnOnOff(false);

        EventBus.Instance.Register(this);
	}

    private void TurnOnOff(bool turn)
    {
        display.gameObject.transform.parent.gameObject.SetActive(turn);
    }

    public void OnEvent(LightSwitch.LightStateChangedEvent e)
    {
        TurnOnOff(e.lightState);
    }
}
