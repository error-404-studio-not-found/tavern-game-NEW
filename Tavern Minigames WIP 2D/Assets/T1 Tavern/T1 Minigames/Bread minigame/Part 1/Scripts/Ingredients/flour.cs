using UnityEngine;
using UnityEngine.UI;


public class flour : MonoBehaviour
{
    public float flourGrams;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float scrollInput = Input.mouseScrollDelta.y;

        if (scrollInput != 0)
        {
            flourGrams = scrollInput;
            Debug.Log("Scrolling: " + scrollInput);
        }
    }
}
