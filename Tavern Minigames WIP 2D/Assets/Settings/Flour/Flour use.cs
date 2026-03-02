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
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();


        Mouse mouse = Mouse.current;
        if (mouse.leftButton.wasPressedThisFrame)
        {
            Debug.Log("mouse clicked");
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(mouseScreenPos), Vector2.zero);
            if (hit.collider != null && hit.collider.name == "Flour Bowl" && RollingPin.HoldingRollingPin == false)
            {
                Debug.Log("hit" + hit.collider.name);
                //mousePos = hit.collider.transform.position;
                //mousePos = Camera.main.ScreenToWorldPoint(mousePos);
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
    }
}
