using UnityEngine;
using UnityEngine.InputSystem;

public class RollingPin : MonoBehaviour
{
    private CarryingDough CarryingDough;
    public TargetJoint2D Tj2dR;
    public Rigidbody2D Rb;
    private BoxCollider2D BoxCollider2D;
    public Flouruse Flouruse;
    public MinigameDrag MinigameDrag;
    private SpriteRenderer spriteRnd;
    [SerializeField] private LayerMask targetLayerMask;
    public bool StillIn = false;
    public bool HoldingRollingPin = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Tj2dR = GetComponent<TargetJoint2D>();
        Rb = GetComponent<Rigidbody2D>();
        Tj2dR.enabled = false;
        Rb.bodyType = RigidbodyType2D.Static;
        BoxCollider2D = GetComponent<BoxCollider2D>();
        spriteRnd = GetComponent<SpriteRenderer>();
        CarryingDough.flaten = 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        var doughObject = GameObject.FindGameObjectWithTag("Dough");
        if (doughObject)
        {
            CarryingDough = doughObject.GetComponent<CarryingDough>();
        }

        Debug.Log(CarryingDough.flaten);
        Tj2dR.target = CarryingDough.Hand.transform.position;

        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();

        Mouse mouse = Mouse.current;
        if (mouse.leftButton.wasPressedThisFrame)
        {
            Debug.Log("mouse clicked");
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(mouseScreenPos), Vector2.zero);
            Debug.Log("here " + hit.collider.name + HoldingRollingPin);
            if (hit.collider != null && hit.collider.name == "Rolling pin" && Flouruse.holdingflour == false && CarryingDough.holdingDough == false)
            {
                Tj2dR.enabled = true;
                HoldingRollingPin = true;
                Rb.bodyType = RigidbodyType2D.Dynamic;
                spriteRnd.sortingOrder = 3;
            }
            else if (hit.collider != null && hit.collider.name == "Rolling pin rack" && HoldingRollingPin == true)
            {
                Tj2dR.enabled = false;
                HoldingRollingPin = false;
                Rb.bodyType = RigidbodyType2D.Static;
                transform.position = new Vector3(-4.71f, 3.45f, 0);
                transform.rotation = Quaternion.Euler(0, 0, 0);
                spriteRnd.sortingOrder = 0;
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("triggered " + collision.gameObject.tag);

        // Check if the collided object has the tag "Dough" and if the rolling pin is not already in contact with the dough
        Debug.Log(CarryingDough.flaten);
        if (collision.gameObject.tag == "Dough" && StillIn == false && MinigameDrag.doughActive)
        {
            if (CarryingDough.flaten < 2.5f)
            {
                BoxCollider2D.enabled = false;
                Invoke("EnableCollider", 0.5f);
                Debug.Log("hit dough");
                CarryingDough.rolling = true;
                CarryingDough.flaten += 0.5f;
                Debug.Log("flaten " + CarryingDough.flaten);
                Debug.Log("Rolling " + CarryingDough.rolling);
                StillIn = true;
            }
            else if (CarryingDough.flaten >= 2.5f)
            {
                CarryingDough.flaten = 2.5f;
                Debug.Log("dough fully flattened");
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Dough")
        {
            StillIn = false;
        }
    }

    private void EnableCollider()
    {
        BoxCollider2D.enabled = true;
        Debug.Log("collider enabled");
    }
}
