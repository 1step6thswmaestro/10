using UnityEngine;
using System.Collections;
using System;

public class LightSwitch : InteractiveObject {   

    public Light spotLight;
    public Light[] pointLights;

    private bool lightState = false;
    private Animator animSwitch;
    private bool switchOn = true;
    private float switchInterval = 0.5f;

    private AudioSource switchSound;

	public GameObject hint;
    // Use this for initialization
    public override void Start()
    {
        base.Start();

        this.Activate();
        this.TurnOn(lightState);

        animSwitch = this.GetComponent<Animator>();
        switchSound = this.GetComponent<AudioSource>();
    }

    private void TurnOn(bool lightState)
    {        
        SetRenderIntensities(lightState);

        spotLight.gameObject.SetActive(!lightState);

        foreach (Light pointLight in pointLights)
        {
            pointLight.gameObject.SetActive(lightState);
        }

        EventBus.Instance.Post(new LightStateChangedEvent(lightState));
    }

    private void SetRenderIntensities (bool lightState)
    {
        if (lightState)
        {
            RenderSettings.ambientIntensity = 1;
            RenderSettings.reflectionIntensity = 1;
        }
        else
        {
            RenderSettings.ambientIntensity = 0;
            RenderSettings.reflectionIntensity = 0;
        }
        RenderSettings.defaultReflectionMode = UnityEngine.Rendering.DefaultReflectionMode.Skybox;
    }

    public override void OnInteraction (Tool tool)
    {
        if (tool is Hand)
        {
            lightState = !lightState;                                   
            StartCoroutine(SwitchOnOff(lightState, switchInterval)); 
			hint.SendMessage("Solve");
        }
    }

    private void SwithOnOffAnim()
    {
        animSwitch.SetBool("SwitchOn", switchOn);
        switchOn = !switchOn;
        switchSound.Play();
    }

    IEnumerator SwitchOnOff(bool lightState, float switchInterval){
        SwithOnOffAnim();
        yield return new WaitForSeconds(switchInterval);

        if (lightState == false)
        {
            this.ReloadLevel();
        }
        else
        {
            TurnOn(lightState);
        }
    }

    private void ReloadLevel ()
    {
        EventBus.Instance.Clear();
        Application.LoadLevel(Application.loadedLevel);
    }

    // Event

    public class LightStateChangedEvent
    {
        public bool lightState;
        public LightStateChangedEvent(bool lightState)
        {
            this.lightState = lightState;
        }
    }
}
