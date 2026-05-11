using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

public class Flouruse : MonoBehaviour
{
    [Header("Refrences")]
    public RollingPin RollingPin;
    public CarryingDough CarryingDough;

    [Header("flour spawn")]
    public float flourAmount = 0;
    public float Maxflour = 1;
    public GameObject Flour;
    public GameObject flour;

    [Header("flour move")]
    public TargetJoint2D TargetJoint;
    public bool holdingflour = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        SpriteRenderer sr = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();


        // checks if they clicked
        Mouse mouse = Mouse.current;
        if (mouse.leftButton.wasPressedThisFrame)
        {

            // raycast to see if they clicked the flour bowl
            Debug.Log("mouse clicked");
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(mouseScreenPos), Vector2.zero);

            // if they did click the flour bowl and they are not holding the rolling pin then spawn the flour and set it to holding flour
            if (hit.collider != null && hit.collider.name == "Flour Bowl" && RollingPin.HoldingRollingPin == false)
            {

                Debug.Log("hit" + hit.collider.name);
                //mousePos = hit.collider.transform.position;
                //mousePos = Camera.main.ScreenToWorldPoint(mousePos);

                // if they are at the max amount of flour then do not spawn more
                if (flourAmount == Maxflour)
                {
                    Debug.Log("max flour");
                    return;
                }
                {
                    GameObject clone = Instantiate(Flour, hit.point, Quaternion.Euler(0, 0, 0));
                    clone.tag = "Flour";
                    Debug.Log("take dough");
                    flour = GameObject.FindGameObjectWithTag("Flour");
                    holdingflour = true;
                    flourAmount += 1;
                }
            }
        }

        if (holdingflour == true && gameObject.tag == "Flour")
        {

           // GetComponent<SpriteRenderer>(). = 5;

        }
    }
}