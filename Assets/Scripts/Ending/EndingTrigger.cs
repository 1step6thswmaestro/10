using UnityEngine;
using System.Collections;

public class EndingTrigger : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        EventBus.Instance.Clear();
        Application.LoadLevel("Ending");
    }
}
