// -- UNUSED SCRIPT --

using UnityEngine;

public class Followmouse : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        // Retreives the position of the user's mouse and sets the z value to 0 so that the object will be on the same plane as the camera
        Input.mousePosition.Set(Input.mousePosition.x, Input.mousePosition.y, 0);
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
