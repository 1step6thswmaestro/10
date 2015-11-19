using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CombinationListener : MonoBehaviour {

    private Text text;

    void Start ()
    {
        text = this.GetComponent<Text>();
        EventBus.Instance.Register(this);
    }

	public void OnEvent (NumPad.CombinationChangedEvent e)
    {
        text.text = e.combination;
    }

    void OnDestroy()
    {
        EventBus.Instance.Unregister(this);
    }
}
