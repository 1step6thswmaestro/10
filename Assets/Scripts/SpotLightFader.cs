using UnityEngine;
using System.Collections;

public class SpotLightFader : MonoBehaviour {

    public const float MaximumIntensity = 2.57f;
    private const float IntensityIncreaseRate = 0.03f;

    private Light spotLight;

	// Use this for initialization
	void Start () {
        spotLight = GetComponent<Light>();
        spotLight.intensity = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (spotLight.intensity < MaximumIntensity)
        {
            spotLight.intensity += IntensityIncreaseRate;
        }
	}
}
