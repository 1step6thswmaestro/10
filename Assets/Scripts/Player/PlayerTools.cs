using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerTools : MonoBehaviour {

    private IList<Tool> tools;
    private int currentToolIndex = 0;

    // Player's tool
    public Hand hand;
    public Plier plier;
    public ScrewDriver screwDriver;
    public Hammer hammer;

    // prefabs
    public GameObject plierPrefab;
    public GameObject screwDriverPrefab;
    public GameObject hammerPrefab;

    private AudioSource getToolSound;
    private AudioSource dropToolSound;

    public Tool CurrentTool
    {
        get { return tools[CurrentToolIndex]; }
    }

    private int CurrentToolIndex
    {
        get { currentToolIndex %= tools.Count; return currentToolIndex; }
        set { currentToolIndex = value;  }
    }

    // Use this for initialization
    void Start ()
    {
        SetupTools();
        SoundSourceInit();
    }

    private void SetupTools ()
    {
        tools = new List<Tool>();
        tools.Add(hand);
    }

    public void NextTool ()
    {
        CurrentTool.gameObject.SetActive(false);
        CurrentToolIndex += 1;
        CurrentTool.gameObject.SetActive(true);
    }

    public void PrevTool ()
    {
        CurrentTool.gameObject.SetActive(false);
        CurrentToolIndex -= 1;
        CurrentTool.gameObject.SetActive(true);
    }

    public void ObtainTool (Tool toolOnWorld)
    {
        Tool playerTool = GetPlayerTool(toolOnWorld);

        tools.Add(playerTool);

        CurrentTool.gameObject.SetActive(false);
        playerTool.gameObject.SetActive(true);

        CurrentToolIndex = tools.Count - 1;
    }

    public void ThrowTool ()
    {
        if ((CurrentTool is Hand) == false)
        {
            Tool removed = CurrentTool;

            tools.RemoveAt(CurrentToolIndex);
            removed.gameObject.SetActive(false);
            CurrentTool.gameObject.SetActive(true);

            GameObject prefab = null;

            if (removed is Plier)
                prefab = plierPrefab;
            else if (removed is ScrewDriver)
                prefab = screwDriverPrefab;
            else if (removed is Hammer)
                prefab = hammerPrefab;

            GameObject throwingTool = Instantiate(prefab);
            throwingTool.transform.position = this.transform.position;
            throwingTool.transform.localPosition += new Vector3(0, 0, 1);
            throwingTool.GetComponent<Rigidbody>().AddForce(this.transform.forward * 1000);

            dropToolSound.Play();
        }
    }

    private Tool GetPlayerTool (Tool toolOnWorld)
    {
        if (toolOnWorld is Plier)
            return plier;
        else if (toolOnWorld is ScrewDriver)
            return screwDriver;
        else if (toolOnWorld is Hammer)
            return hammer;
        else
            return hand;

        getToolSound.Play();
    }

    private void SoundSourceInit()
    {
        AudioSource[] audioSources = this.GetComponents<AudioSource>();
        foreach (AudioSource source in audioSources)
        {
            if (source.priority == 127)
            {
                getToolSound = source;
            }
            else
            {
                dropToolSound = source;
            }
        }
    }
}
