using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;


public class flourObj : MonoBehaviour
{


    [Header("Refrences")]
    [SerializeField] private flour flourScript;
    private TargetJoint2D flourCarry;
    private SpriteRenderer flourRenderer;


    [Header("Customization")]
    public List<Sprite> flourSprites;
    private int spriteIndex = 0;

    public GameObject Hand;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        flourCarry = GetComponent<TargetJoint2D>();
        flourRenderer = GetComponent<SpriteRenderer>();

        flourCarry.enabled = true;
        
        flourRenderer.sprite = flourSprites[flourSprite()];

    }

    // Update is called once per frame
    void Update()
    {


        flourFollow();

        if (Input.GetKeyDown(KeyCode.E)) { destroyObject(); }


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

}
