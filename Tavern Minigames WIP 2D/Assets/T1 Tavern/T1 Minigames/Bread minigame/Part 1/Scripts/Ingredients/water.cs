using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.Windows;
using Input = UnityEngine.Input;

public class water : MonoBehaviour
{

    [Header("Components")]
    private TargetJoint2D waterJugCarry;
    private ParticleSystem particleSystem;
    private Rigidbody2D Rb;

    [Header("Carry Water")]
    public GameObject hand;
    public bool holdingWater = false;
    public GameObject waterPlacement;

    [Header("References")]
    [SerializeField] private LayerMask targetLayerMask;
    [SerializeField] private flour flourScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        waterJugCarry = GetComponent<TargetJoint2D>();
        particleSystem = GetComponent<ParticleSystem>();

        Rb = GetComponent<Rigidbody2D>();

        particleSystem.Stop();
    }

    // Update is called once per frame
    void Update()
    {

        // check if they should be holding the water
        waterPickUp();


        //Enable the collider and carry if the player is holding the water, otherwise disable it
        if (holdingWater)
        {
            holdWater();
        }
        else
        {
            waterJugCarry.enabled = false;
        }

        if (holdingWater == true && Mouse.current.leftButton.isPressed)
        {
            pourWater();
        }
        else
        {
            particleSystem.Emit(0);
        }

    }

    /* 
     * this function checks if the player is clicking on the water jug, if they are then they will hold the water until they click again, if they click somewhere else then they will not hold the water 
     */
    public void waterPickUp()
    {
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();

        Mouse mouse = Mouse.current;
        if (mouse.leftButton.wasPressedThisFrame && !holdingWater && !flourScript.holdingFlour)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(mouseScreenPos), Vector2.zero);
            if (hit.collider != null && hit.collider.name == "WaterJug")
            {
                holdingWater = true;
                waterJugCarry.enabled = true;
                Rb.bodyType = RigidbodyType2D.Dynamic;
                Rb.freezeRotation = true;
            }
            else
            {
                Debug.Log("Did not click water");
            }

        }

        // Place water down if it is currently being held and the player clicks somewhere else
        else if (mouse.leftButton.wasPressedThisFrame && holdingWater)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(mouseScreenPos), Vector2.zero);
            if (hit.collider != null && hit.collider.name == "JugPlacement")
            {
                placeWater();
            }
            else
            {
                Debug.Log("Did put water down");
            }

        }

    }

    public void holdWater()
    {
        waterJugCarry.target = hand.transform.position;
    }

    public void placeWater()
    {
        holdingWater = false;
        waterJugCarry.enabled = false;
        Rb.bodyType = RigidbodyType2D.Static;

        gameObject.transform.position = waterPlacement.transform.position;
    }

    void pourWater()
    {

        particleSystem.Emit(1);

    }


}
