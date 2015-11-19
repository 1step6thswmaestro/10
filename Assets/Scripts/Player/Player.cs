using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private const float speed =  6.0f;

    private float mouseX = 0;

    public  GameObject mainCamera;

    public StereoController mainStereoController;
    public StereoController hudStereoController;

    private CharacterController charController;
    private PlayerTools tools;

    private Interpolate.Function zoomInOut;
    private float zoomElapsedTime;
    private float zoomBegin;
    private float zoomDistance;
    private const float zoomTime = 0.07f;

    private AudioSource footstepSound;

	public ChooseTool chooseTool;

    // Use this for initialization
    void Start () {

        zoomInOut = Interpolate.Ease(Interpolate.EaseType.EaseInCirc);
        zoomElapsedTime = 0;
        zoomBegin = 0;
        zoomDistance = 0;

        charController = this.GetComponent<CharacterController>();

        tools = this.GetComponentInChildren<PlayerTools>();
        footstepSound = this.GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        this.Translate();
        this.Rotate();

        this.Zoom();

        this.Interact();

        if (Input.GetButtonDown("Fire3"))
        {
            tools.ThrowTool();
        }
        if (Input.GetButtonDown("Button 4"))
        {
            tools.PrevTool();
        }
        if (Input.GetButtonDown("Button 5") || Input.GetKeyDown(KeyCode.Tab))
        {
            tools.NextTool();
        }
    }

    private void Translate ()
    {
        // Translate
        Vector3 delta = Vector3.zero;

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 projectedForward = Vector3.ProjectOnPlane(mainCamera.transform.forward, this.transform.up);
        Vector3 projectedRight = Vector3.ProjectOnPlane(mainCamera.transform.right, this.transform.up);

        delta += projectedRight * horizontal;
        delta += projectedForward * vertical;

        delta = delta.normalized * speed;
        // gravity
        delta.y -= 9.81f;

        charController.Move(delta * Time.deltaTime);

        // sound
        if (horizontal == 0 && vertical == 0)
        {
            footstepSound.Pause();
        }
        else
        {
            if (!footstepSound.isPlaying)
                footstepSound.Play();
        }
    }

    private void Rotate ()
    {
        // Rotate
        mouseX += Input.GetAxis("Mouse X") * 5;
        if (mouseX <= -180)
        {
            mouseX += 360;
        }
        else if (mouseX > 180)
        {
            mouseX -= 360;
        }

        this.transform.rotation = Quaternion.Euler(0, mouseX, 0);
    }

    private void Zoom ()
    {
        // Zoom
        zoomElapsedTime += Time.deltaTime;

        // Zoom in
        if (Input.GetButtonDown("Fire2"))
        {
            zoomElapsedTime = 0;
            zoomBegin = mainStereoController.matchByZoom;
            zoomDistance = 1 - mainStereoController.matchByZoom;
        }

        // Zoom out
        if (Input.GetButtonUp("Fire2"))
        {
            zoomElapsedTime = 0;
            zoomBegin = mainStereoController.matchByZoom;
            zoomDistance = -mainStereoController.matchByZoom;
        }

        float zoom = zoomInOut(zoomBegin, zoomDistance, zoomElapsedTime, zoomTime);
        mainStereoController.matchByZoom = zoom;
        hudStereoController.matchByZoom = zoom;
    }

    private RaycastHit RayCast ()
    {
        // RayCast
        Transform mainCameraTransform = mainStereoController.gameObject.transform;
        Ray ray = new Ray(mainCameraTransform.position, mainCameraTransform.forward);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {
            //Debug.Log("Ray cast : " + hitInfo.collider.gameObject.name);
        }

        if (hitInfo.collider.gameObject != null)
        {
            this.PublishRayCastEvent(hitInfo.collider.gameObject);
        }

        return hitInfo;
    }

    private void Interact ()
    {
        RaycastHit hitInfo = this.RayCast();

        // Interact
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject target = hitInfo.collider.gameObject;

            InteractiveObject interactiveObject = target.GetComponent<InteractiveObject>();

            if (interactiveObject != null)
            {
                interactiveObject.OnInteraction(tools.CurrentTool);
                return;
            }

            Tool tool = target.GetComponent<Tool>();

            if (tool != null)
            {
                tools.ObtainTool(tool);              
                if ( chooseTool != null)
                {
    				chooseTool.SendMessage("CheckTool", tool.gameObject.name);
                }
                DestroyObject(tool.gameObject);
            }
        }
    }

    // Event

    public class PlayerRayCastObjectEvent
    {
        public GameObject gameObject;
        public PlayerRayCastObjectEvent(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }
    }

    public void PublishRayCastEvent(GameObject gameObject)
    {
        EventBus.Instance.Post(new PlayerRayCastObjectEvent(gameObject));
    }

}
