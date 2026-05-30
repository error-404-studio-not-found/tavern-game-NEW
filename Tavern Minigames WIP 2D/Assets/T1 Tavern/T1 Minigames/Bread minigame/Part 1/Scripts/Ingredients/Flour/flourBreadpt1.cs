using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.Windows;
using Input = UnityEngine.Input;


public class flour : MonoBehaviour
{
    [Header("Flour customization")]
    private float flourGrams;
    public float gramsIncrement = 1f;
    public GameObject flour;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Allow the user to scroll to change the amount of flour grams they pick up
        flourGramsScroll();

        //
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
    }

    public void flourPickUp()
    {
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();

        Mouse mouse = Mouse.current;
        if (mouse.leftButton.wasPressedThisFrame && holdingWater == false)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(mouseScreenPos), Vector2.zero);
            if (hit.collider != null && hit.collider.name == "FlourBin")
            {
                GameObject clone = Instantiate(flour, hit.point, Quaternion.Euler(0, 0, 0));
                clone.tag = "Flour";
            }
            else
            {
                Debug.Log("Did not pick up flour");
            }

        }
    }

}
