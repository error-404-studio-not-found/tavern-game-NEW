using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

public class Flour : MonoBehaviour
{
    [Header("refrences")]
    private Rigidbody2D Rb;
    private TargetJoint2D FlourTj;
    public CarryingDough CarryingDough;
    public RollingPin RollingPin;
    public MinigameDrag MinigameDrag;
    public Flouruse Flouruse;
    private ParticleSystem ParticleSystem;
    private SpriteRenderer spriteRenderer;
    private CircleCollider2D circleCollider;

    [Header("Flour drop")]
    private bool flourD = false;
    private bool flourG = false;
    private GameObject flour;
    [SerializeField] private LayerMask targetLayerMask;
    public float goalScale;
    public bool Stop = false;

    [Header("Dough Flour")]
    public bool stillIn = false;
    private float flourroll = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        FlourTj = GetComponent<TargetJoint2D>();
        ParticleSystem = GetComponent<ParticleSystem>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();
        ParticleSystem.Play();

        spriteRenderer.maskInteraction = SpriteMaskInteraction.None;
        spriteRenderer.sortingOrder = 5;
    }

    // Update is called once per frame
    void Update()
    {

        flour = GameObject.FindGameObjectWithTag("Flour");

        Mouse mouse = Mouse.current;
        if (mouse.leftButton.wasPressedThisFrame && gameObject.tag == "Flour")
        {
            Debug.Log("mouse clicked flour");
            RaycastHit2D hit = Physics2D.Raycast(flour.transform.position, Vector2.down, 10f, targetLayerMask);
            Debug.Log(hit.collider.name);

            if (hit.collider != null && hit.collider.name == "RollingBoard")
            {
                flourD = true;
                Debug.Log("drop flour " + hit.collider.name);
            }
            else if (hit.collider != null && hit.collider.tag == "Dough")
            {
                flourG = true;
                Debug.Log("rolling thing missed" + hit.collider);
            }

        }

        FlourTj.target = CarryingDough.Hand.transform.position;

        if (Flouruse.holdingflour == true && flourD == false && gameObject.tag == "Flour" && RollingPin.HoldingRollingPin == false && CarryingDough.holdingDough == false)
        {
            FlourTj.enabled = true;
            Rb.bodyType = RigidbodyType2D.Dynamic;
            Debug.Log("holding flour ");
        }


        // when the flour is dropped rescale it and freeze its position, as well as enableing the mask
        if (flourD == true && Stop == false)
        {
            Debug.Log("yay flour " + gameObject.name); 
            Flouruse.holdingflour = false;
            FlourTj.enabled = false;
            Rb.bodyType = RigidbodyType2D.Static;
            gameObject.transform.localScale = new Vector3(goalScale, goalScale, goalScale);
            Debug.Log("drop flour");
            ParticleSystem.Play();
            spriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
            Stop = true;
        }
        /*
        else if (flourG == true)
        {
            Debug.Log("yay flour " + gameObject.name); 
            Flouruse.holdingflour = false;
            FlourTj.enabled = false;
            Rb.bodyType = RigidbodyType2D.Static;
            ParticleSystem.Play();
            Invoke("Delete", 0.2f);
            Debug.Log("drop flour");
        }*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Dough" && flourroll < 3f && stillIn == false && Flouruse.holdingflour == false)
        {
            circleCollider.enabled = false;
            Invoke("EnableCollider", 0.5f);
            Debug.Log("hit dough");
            flourroll += 1f;
            stillIn = true;
        }
        else if (collision.gameObject.tag == "Dough" && flourroll >= 3f && Flouruse.holdingflour == false)
        {
            Flouruse.flourAmount -= 1;
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Dough")
        {
            stillIn = false;
        }
    }

    private void EnableCollider()
    {
        circleCollider.enabled = true;
    }
    private void Delete()
    {
        Flouruse.flourAmount -= 1;
        Debug.Log("delete flour");
        Destroy(gameObject);
    }
}
