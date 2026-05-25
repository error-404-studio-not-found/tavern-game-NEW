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
        followMouse();
    }

    void followMouse()
    {
        Mouse mouse = Mouse.current;
        Input.mousePosition.Set(Input.mousePosition.x, Input.mousePosition.y, 0);
        Tj2d.target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

}
