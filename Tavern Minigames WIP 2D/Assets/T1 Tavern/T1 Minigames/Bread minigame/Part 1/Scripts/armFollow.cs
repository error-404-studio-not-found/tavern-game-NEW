using UnityEngine;
using UnityEngine.InputSystem;
using Input = UnityEngine.Input;

public class armFollow : MonoBehaviour
{

    public TargetJoint2D Tj2d;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        Tj2d = GetComponent<TargetJoint2D>();

    }

    // Update is called once per frame
    void Update()
    {
        // constantly follow the mouse position
        followMouse();
    }

    void followMouse()
    {
        // Intitialize and find the mouse
        Mouse mouse = Mouse.current;

        // find the mouse position and set the z to 0 so it doesn't mess with the target joint, also make the target join follow the mouse
        Input.mousePosition.Set(Input.mousePosition.x, Input.mousePosition.y, 0);
        Tj2d.target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

}
