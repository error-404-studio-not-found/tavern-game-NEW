using UnityEngine;
using UnityEngine.UI;

public class FlourTransparency : MonoBehaviour
{
    public Flouruse Flouruse;
    private SpriteMask _mask;
    private GameObject flour;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _mask = GetComponent<SpriteMask>();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.parent = Flouruse.flour.transform;
        _mask.sprite = Flouruse.flour.GetComponent<SpriteRenderer>().sprite;
    }
}
