using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.Windows;
using Input = UnityEngine.Input;


public class flour : MonoBehaviour
{
    [Header("Flour customization")]
    public float flourGrams;
    public float gramsIncrement = 1f;
    public float maxFlourGrams = 100f;
    public int maxFlour = 2;
    public int flourAmount = 0;

    public bool holdingFlour = false;

    [Header("Flour object")]
    public GameObject flourRef;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Allow the user to scroll to change the amount of flour grams they pick up
        flourGramsScroll();

        // Clone the flour object when the user clicks on the flour bin, and assign the correct amount of grams to the new flour object
        flourPickUp();

    }


    void flourGramsScroll()
    {
        // Read the scroll input from the mouse
        Vector2 scrollDelta = Mouse.current.scroll.ReadValue();
        float scrollY = scrollDelta.y;

        // Check if the scroll input is positive (scrolling up) or negative (scrolling down)
        if (scrollY > 0f)
        {
            // Increase the flour grams by the specified increment
            flourGrams += gramsIncrement;

        }
        else if (scrollY < 0f)
        {
            // Decrease the flour grams by the specified increment
            flourGrams -= gramsIncrement;
        }

        // Clamp the flour grams to be within the range of 0 to maxFlourGrams
        if (flourGrams < 0f)
        {
            flourGrams = 0f;
        }
        else if (flourGrams > maxFlourGrams)
        {
            flourGrams = maxFlourGrams;
        }
    }

    /*
     * 
     * this function checks if the player is clicking on the flour bin, if they are then they will pick up the flour and instantiate a new flour 
     * object with the correct amount of grams, if they click somewhere else then they will not pick up the flour 
     * 
     */
    void flourPickUp()
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
            if (hit.collider != null && hit.collider.name == "FlourBin")
            {

                if (flourAmount == maxFlour)
                {
                    return;
                }
                // if the flour amount is less than the max amount then spawn the flour and set it to holding flour
                {
                    GameObject clone = Instantiate(flourRef, hit.point, Quaternion.Euler(0, 0, 0));
                    clone.tag = "Flour";
                    clone.SetActive(true);
                    holdingFlour = true;
                    flourAmount += 1;
                }

            }
            else
            {
                Debug.Log("Did not pick up flour");
            }

        }
    }

    // this will be used to decide which sprite to assign to the flower, will return int index of the sprite to use based on the amount of flour grams

}
