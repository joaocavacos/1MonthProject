using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PillSystem : MonoBehaviour
{
    [SerializeField] private TMP_Text pillText;
    //[SerializeField] private TMP_Text promptText;
    [SerializeField] private int currentPills = 5;

    private SanitySystem _sanitySystem;


    void Start()
    {
        _sanitySystem = GetComponent<SanitySystem>();
    }

    void Update()
    {
        pillText.text = "Pills: " + currentPills;
        if (currentPills > 0)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                _sanitySystem.TakePill();
                currentPills -= 1;
            }
        }
        else
        {
            Debug.Log("Not enough pills");
        }
    }
}