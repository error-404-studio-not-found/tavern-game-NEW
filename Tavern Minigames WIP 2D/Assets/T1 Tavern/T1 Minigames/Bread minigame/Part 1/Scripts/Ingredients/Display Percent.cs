using UnityEngine;
using TMPro;

public class DisplayPercent : MonoBehaviour
{


    public TextMeshPro textMeshPro;
    public TMP_Text myTextComponent;
    private int PERCENT = 100;


    [Header("Refrecnes")]

    [SerializeField] private flour flourScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        


        textMeshPro = GetComponent<TextMeshPro>();
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        myTextComponent.text = "Your percent is: %" + flourScript.flourGrams;

    }
}
