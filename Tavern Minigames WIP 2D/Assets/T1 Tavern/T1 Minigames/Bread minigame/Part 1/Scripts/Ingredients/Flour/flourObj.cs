using UnityEngine;

public class flourObj : MonoBehaviour
{

    private TargetJoint2D flourCarry;
    public GameObject Hand;
    private GameObject flour;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        flourCarry = GetComponent<TargetJoint2D>();

        // after the colone is instanticated, the target joint is disabled, so we need to enable it here
        // -- MAKE IT IN AN IF STATEMENT TO ENSURE THAT IT IS ENABLING THE TARGET JOINT OF THE COLONE AND NOT THE DEAFULT -- OR MAKE IT IN THE PREFAB (I didnt write that or know how to do it or what it means)
        flourCarry.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {


        flourFollow();




    }

    /*
     * 
     * A method that makes the flour object follow the hand object by setting the target of the target joint to the position of the hand object
     * 
     */
    public void flourFollow()
    {
        // Set the target of the target joint to the position of the hand object
        flourCarry.target = Hand.transform.position;
    }

}
