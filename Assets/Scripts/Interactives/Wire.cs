using UnityEngine;
using System.Collections;
using System;

public class Wire : InteractiveObject {

    public WireMachine wireMachine;
    
    public Color wireColor;

    private GameObject wire;
    private GameObject cutWire;

    private Renderer wireRenderer;

    private Renderer cutWireRenderer1;
    private Renderer cutWireRenderer2;

    public override void Start ()
    {
        base.Start();

        wire    = this.transform.FindChild("wire").gameObject;
        cutWire = this.transform.FindChild("cutwire").gameObject;

        try
        {
            wireRenderer = wire.GetComponent<Renderer>();
            cutWireRenderer1 = cutWire.transform.FindChild("Sweep").GetComponent<Renderer>();
            cutWireRenderer2 = cutWire.transform.FindChild("Sweep_1").GetComponent<Renderer>();
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }

        SetCut(false);
        SetWireColor(this.wireColor);
    }

    public override void OnInteraction(Tool tool)
    {
        if (tool is Plier)
        {
            this.Defuse();
            tool.Use();
            this.SetCut(true);
            this.CallWireMachineCallback();
        }
    }

    private void SetCut(bool cut)
    {
        wire.SetActive(!cut);
        cutWire.SetActive(cut);
    }

    private void CallWireMachineCallback()
    {
        if (wireMachine != null)
        {
            wireMachine.OnWireCut(wireColor);
        }
    }

    private void SetWireColor (Color wireColor)
    {
        wireRenderer.material.color = wireColor;
        cutWireRenderer1.material.color = wireColor;
        cutWireRenderer2.material.color = wireColor;
    }

    public override void SetHighlight(bool highlight)
    {
        Shader shader = base.FindHighlightOrStdShader(highlight);

        if (wireRenderer != null)
        {
            wireRenderer.material.shader = shader;
        }

        if (cutWireRenderer1 != null)
        {
            cutWireRenderer1.material.shader = shader;
        }
        if (cutWireRenderer2 != null)
        {
            cutWireRenderer2.material.shader = shader;
        }
    }


}
