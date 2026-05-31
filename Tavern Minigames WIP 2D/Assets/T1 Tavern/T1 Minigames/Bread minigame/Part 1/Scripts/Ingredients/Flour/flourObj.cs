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
        flourCarry.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {

        flour = GameObject.FindGameObjectWithTag("Flour");

        flourFollow();




    }

    public void flourFollow()
    {

        flourCarry.target = Hand.transform.position;
    }

}
