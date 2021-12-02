using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SanitySystem : MonoBehaviour
{

    [SerializeField] private Slider sanitySlider;
    [SerializeField] private float currentSanity;
    private float maxSanity = 100f;

    void Start()
    {
        currentSanity = maxSanity;
        sanitySlider.maxValue = maxSanity;
        sanitySlider.value = maxSanity;
    }

    void Update()
    {
        if (currentSanity <= 0) currentSanity = 0;
        if (currentSanity > 100) currentSanity = maxSanity;
        
        sanitySlider.value = currentSanity;

        Debug.Log("Sanity:" + currentSanity);
    }

    public void DecreaseSanity(float value)
    {
        currentSanity -= value;
    }

    public void TakePill()
    {
        currentSanity = maxSanity;
    }
}
