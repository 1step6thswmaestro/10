using UnityEngine;
using System.Collections;
using System;

public abstract class InteractiveObject : MonoBehaviour {

    public const string OutlineShaderName = "Custom/Custom-OutlineDiffuse";
    public const string ColorOverLayShaderName = "Custom/Custom-ColorOverlay";
    public const string StandardShaderName = "Standard";

    public InteractiveObject[] followingObjects;

    public bool isBegin;
    public bool isEnd;

    private State state;
    public State CurrentState
    {
        get { return state; }
    }

    public enum State
    {
        Inactivated,
        Activated,
        Defused,
    }

    public virtual void Awake()
    {

    }

    public virtual void Start()
    {
        if (isBegin)
            state = State.Activated;
        else
            state = State.Inactivated;

        EventBus.Instance.Register(this);
    }

    public virtual void Update()
    {

    }

    public void Defuse()
    {
        switch (state)
        {
            case State.Activated:

                Debug.Log("" + this + " has been defused.");
                state = State.Defused;

                foreach (InteractiveObject obj in followingObjects)
                {
                    obj.Activate();
                    Debug.Log("Following " + obj + " is activated.");
                }

                if (isEnd)
                {
                    EventBus.Instance.Post(new Bomb.BombDefusedEvent());
                }

                break;

            case State.Defused:

                break;

            case State.Inactivated:
                EventBus.Instance.Post(new Bomb.BombExplodedEvent());
                break;
        }
    }

    public virtual void Activate()
    {
        if (state == State.Inactivated)
        {
            state = State.Activated;
        }
    }

    public virtual void SetHighlight(bool highlight)
    {
        Renderer renderer = this.GetComponent<Renderer>();

        if (renderer != null)
        {
            renderer.material.shader = this.FindHighlightOrStdShader(highlight);
        }
    }

    protected Shader FindHighlightOrStdShader (bool isHighlighted)
    {
        if (isHighlighted)
        {
            // return highlighted shader
            return Shader.Find(ColorOverLayShaderName);
        }
        else
        {
            // return standard shader
            return Shader.Find(StandardShaderName);
        }
    }

    // abstract methods

    public abstract void OnInteraction(Tool tool);

    // Event Callback
    public void OnEvent (Player.PlayerRayCastObjectEvent e)
    {
        if (e.gameObject == this.gameObject)
        {
            this.SetHighlight(true);
        }
        else
        {
            this.SetHighlight(false);
        }
    }

}
