using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;


public class flourObj : MonoBehaviour
{


    [Header("Refrences")]
    [SerializeField] private flour flourScript;
    private TargetJoint2D flourCarry;
    private SpriteRenderer flourRenderer;
    private Rigidbody2D Rb;


    [Header("Customization")]
    public List<Sprite> flourSprites;
    private int spriteIndex = 0;

    public float weight;

    public GameObject Hand;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        flourCarry = GetComponent<TargetJoint2D>();
        flourRenderer = GetComponent<SpriteRenderer>();
        Rb = GetComponent<Rigidbody2D>();

        flourCarry.enabled = true;

        flourRenderer.sprite = flourSprites[flourSprite()];

        weight = flourScript.flourGrams;

    }

    // Update is called once per frame
    void Update()
    {


        flourFollow();

        if (Input.GetKeyDown(KeyCode.E)) { destroyObject(); }

        flourDropCheck();
    }

    /*
     * 
     * A method that makes the flour object follow the hand object by setting the target of the target joint to the position of the hand object
     * 
     */
    public void flourFollow()
    {
        // Set the target of the target joint to the position of the hand object
        flourCarry.target = Hand.transform.position;
    }

    int flourSprite()
    {
        float grams = flourScript.flourGrams;

        // Ensure the list has stuff in it to avoid errors
        if (flourSprites == null || flourSprites.Count == 0)
        {
            Debug.LogWarning("flourSprites list is empty or null.");
            spriteIndex = 0;
            return spriteIndex;
        }

        // Clamps the grams to 0 - 100
        float clamped = Mathf.Clamp(grams, 0f, 100f);

        // Map 0..100 to 0..(count-1) and use rounding so indexes are divided more evenly
        int lastIndex = flourSprites.Count - 1;
        spriteIndex = Mathf.Clamp(Mathf.RoundToInt(clamped * lastIndex / 100f), 0, lastIndex);


        Debug.Log(spriteIndex);

        return spriteIndex;

    }

    void destroyObject()
    {

        flourScript.holdingFlour = false;
        flourScript.flourAmount -= 1;
        GameObject.Destroy(gameObject);

    }

    void flourDropCheck()
    {
        // Get the mouse position in screen coordinates
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();

        // Check if the left mouse button was pressed this frame
        Mouse mouse = Mouse.current;
        if (mouse.leftButton.wasPressedThisFrame)
        {
            // Perform a raycast from the mouse position to check if it hits the flour bin
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(mouseScreenPos), Vector2.zero);

            // If the raycast hits a collider and the collider's name is "FlourBin", instantiate a new flour object at the hit point
            if (hit.collider != null && hit.collider.name == "DoughTrough")
            {

                flourDrop();


            }
            else
            {
                Debug.Log("Did not drop flour, you hit the ");
            }

        }
    }

    void flourDrop()
    {
        flourRenderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;

        flourCarry.enabled = false; // Disable the TargetJoint2D to drop the flour object

        Rb.bodyType = RigidbodyType2D.Static;
    }

}
