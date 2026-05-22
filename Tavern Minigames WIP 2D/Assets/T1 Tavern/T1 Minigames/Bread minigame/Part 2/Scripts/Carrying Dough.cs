using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.Windows;
using Input = UnityEngine.Input;

public class CarryingDough : MonoBehaviour
{
    [Header("References")]
    private Rigidbody2D Rb;
    public TargetJoint2D Tj2d;
    public TargetJoint2D Tj2dD;
    public MinigameDrag MinigameDrag;
    private SpriteRenderer spriteRenderer;
    private ParticleSystem ParticleSystem;
    public RollingPin RollingPin;
    private BoxCollider2D BoxCollider;
    /*
    [Header("mouse Lock")]
    private Vector3 mousePos;
    private float armlength;
    public Transform shoulder;
    */
    [Header("move dough")]
    [SerializeField] private LayerMask targetLayerMask;
    public bool doughD = false;
    public GameObject Dough;
    public GameObject Hand;
    public bool holdingDough = false;

    [Header("drop dough")]
    public float targetScale = 1.5f;
    public List<Sprite> DDSprites = new List<Sprite>();
    private bool Stop = false;
    public bool doughdown = false;
    public Sprite defaultSprite;
    private bool pickingDough = false;

    [Header("Roll Dough")]
    public float flaten = 1.5f;
    public List<Sprite> DoughRoll = new List<Sprite>();
    public bool rolling = false;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        Tj2d = GetComponent<TargetJoint2D>();
        Tj2dD = GetComponent<TargetJoint2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        ParticleSystem = GetComponent<ParticleSystem>();
        BoxCollider = GetComponent<BoxCollider2D>();

        doughD = false;
        Stop = false;
        targetScale = 1.5f;
        rolling = false;
        RollingPin.StillIn = false;

        ParticleSystem.Play();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        // Destroy Dough
        if (Input.GetKeyDown(KeyCode.E) && gameObject.tag == "Dough")
        {
            flaten = 1.5f;
            MinigameDrag.holdingdough = false;
            MinigameDrag.doughActive = false;
            MinigameDrag.doughAmount -= 1;
            Destroy(gameObject);
        }

        if ( gameObject.tag == "Arm")
        {
            Tj2d.enabled = true;
        }

        Dough = GameObject.FindGameObjectWithTag("Dough");
        Mouse mouse = Mouse.current;
        if (mouse.leftButton.wasPressedThisFrame && gameObject.tag == "Dough")
        {
            RaycastHit2D hit = Physics2D.Raycast(Dough.transform.position, Vector2.down, 10f, targetLayerMask);

            if (hit.collider != null && hit.collider.name == "RollingBoard" || hit.collider != null && hit.collider.tag == "Flour")
            {
                doughD = true;
            }
            else
            {
                Debug.Log("rolling thing missed" + hit.collider);
            }

        }


        if (mouse.leftButton.wasPressedThisFrame)
        {
            //raycast to see if they clicked the dough, and if they did pick it up again
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(mouseScreenPos), Vector2.zero);

            if (hit.collider != null && hit.collider.tag == "Dough" && Stop == true)
            {
                pickUpDough();
            }

        }


        if (rolling == true)
        {
            Dough.transform.localScale = new Vector3(flaten, flaten, flaten);
            rolling = false;
        }




        if (MinigameDrag.holdingdough == false && gameObject.tag == "Dough" || gameObject.layer == 7)
        {
            Tj2d.enabled = false;
        }    
        if (MinigameDrag.holdingdough == true && doughD == false && gameObject.tag == "Dough" )
        {
            Tj2d.enabled = true;
            holdingDough = true;
        }

        // rescales and changes sprite when dropped
        else if (MinigameDrag.holdingdough == true && doughD == true && gameObject.tag == "Dough" && Stop == false && pickingDough == false)
        {
            dropDough();
        }


            /* 
            // Set the target position of the TargetJoint2D to the mouse position in world space
            Vector3 mousepos = Camera.main.ScreenToWorldPoint(new Vector3 (Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
            mousePos.z = shoulder.position.z;

            Vector3 direction = mousePos - shoulder.position;


            if (direction.magnitude > armlength)
            {
                direction = direction.normalized * armlength;
            }

            Vector3 finalWorldPos = shoulder.position + direction;
            softwareCursor.position = Camera.main.WorldToScreenPoint(finalWorldPos);
           */
            Input.mousePosition.Set(Input.mousePosition.x, Input.mousePosition.y, 0);
        Tj2d.target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Tj2dD.target = Hand.transform.position;

        if (MinigameDrag.holdingdough == true && gameObject.tag == "Dough" && doughD == false)
        {
            Rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }
    void spriteselect()
    {
        int randomIndex = Random.Range(0, DDSprites.Count);
        spriteRenderer.sprite = DDSprites[randomIndex];
    }

    //A function to pick up dough, after it has already been placed
    void pickUpDough()
    {
        holdingDough = true;
        Tj2d.enabled = true;
        Rb.bodyType = RigidbodyType2D.Dynamic;
        Debug.Log("Rb body type is " + Rb.bodyType);
        targetScale = 1.5f;
        gameObject.transform.localScale = new Vector3(targetScale, targetScale, targetScale);
        Stop = false;
        Invoke("enablePickUp", 0.5f);
        pickingDough = true;
    }

    // - - THE DOUGH IS PICKED UP IMMEADIATLY AFTER BEING PLACED, FIX THIS - -

    // a void to drop the dough made to keep things organized
    void dropDough()
    {
        Debug.Log("dough dropped");
        holdingDough = false;
        Tj2d.enabled = false;
        Rb.bodyType = RigidbodyType2D.Static;
        gameObject.transform.localScale = new Vector3(targetScale, targetScale, targetScale);
        Stop = true;
    }

    void enablePickUp()
    {

        pickingDough = false;

    }
}
