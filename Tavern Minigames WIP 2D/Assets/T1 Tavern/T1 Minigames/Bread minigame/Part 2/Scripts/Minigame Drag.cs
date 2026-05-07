using UnityEngine;
using UnityEngine.InputSystem;

public class MinigameDrag : MonoBehaviour
{
    public float maxDis;

    //[Header("Mouse")]

    private float mouseX;
    private float mouseY;
    private Vector3 mousePos;

    [Header("Dough retrive")]
    public GameObject Dough;
    public int doughAmount = 0;
    public int Maxdough = 1;
    public bool holdingdough = false;
    public bool doughActive = false;

    [Header("Refrences")]
    public RollingPin RollingPin;
    private CarryingDough carryingDough;
    public Flouruse Flouruse;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mouseX = Input.mousePosition.x;
        mouseY = Input.mousePosition.y;


        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();


        mousePos = Input.mousePosition;

        Mouse mouse = Mouse.current;
        if (mouse.leftButton.wasPressedThisFrame)
        {
            Debug.Log("mouse clicked");
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(mouseScreenPos), Vector2.zero);
            if (hit.collider != null && hit.collider.name == "DoughBowl")
            {
                Debug.Log("hit" + hit.collider.name);
                //mousePos = hit.collider.transform.position;
                //mousePos = Camera.main.ScreenToWorldPoint(mousePos);
                if (doughAmount == Maxdough)
                {
                    Debug.Log("max dough");
                    return;
                }
                else
                { 
                    GameObject clone = Instantiate(Dough, hit.point, Quaternion.Euler(0, 0, 0));
                    clone.tag = "Dough";
                    clone.SetActive(true);
                    carryingDough = clone.GetComponent<CarryingDough>();
                    Debug.Log("take dough");
                    carryingDough.flaten = 1.5f;
                    doughActive = true;
                    holdingdough = true;
                    doughAmount += 1;
                }
              

                takeDough();
            }
        }
    }
    
    void takeDough()
    {
     
    }
}
