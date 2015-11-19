using UnityEngine;
using System.Collections;
using System;

public abstract class Tool : MonoBehaviour {

    public const string OutlineShaderName = "Custom/Custom-OutlineDiffuse";
    public const string ColorOverLayShaderName = "Custom/Custom-ColorOverlay";
    public const string StandardShaderName = "Standard";

    public virtual void Start()
    {
        EventBus.Instance.Register(this);
    }

    public virtual void SetHighlight(bool highlight)
    {
        Renderer renderer = this.GetComponent<Renderer>();

        if (renderer != null)
        {
            renderer.material.shader = this.FindHighlightOrStdShader(highlight);
        }

		if (renderer == null) 
		{
			Renderer[] renderers = this.GetComponentsInChildren<Renderer>();
			if( renderers != null )
			{
				foreach(Renderer render in renderers )
				{
					render.material.shader = this.FindHighlightOrStdShader(highlight);
				}
			}
		}
    }

    protected Shader FindHighlightOrStdShader(bool isHighlighted)
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

    public abstract void Use ();

    // Event Callback

    public void OnEvent(Player.PlayerRayCastObjectEvent e)
    {
        if (this != null)
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

}
