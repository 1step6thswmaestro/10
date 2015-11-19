using UnityEngine;
using System.Collections;

public class C4Led : MonoBehaviour {

    private C4Bomb parent;

    private Color color;
    public  Color Color
    {
        get { return color; }
        set { color = value; SetColorAndEmission(color); }
    }
    
	private void SetColorAndEmission (Color color)
    {
        parent = GetComponentInParent<C4Bomb>();

        Renderer renderer = GetComponent<Renderer>();

        if (renderer)
        {
            renderer.material.color = color;
            renderer.material.SetColor("_EmissionColor", color);
        }
    }
}
