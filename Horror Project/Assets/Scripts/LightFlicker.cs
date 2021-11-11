using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    private Light _light;
    [SerializeField] private float minWaitTime;
    [SerializeField] private float maxWaitTime;
    void Start()
    {
        _light = GetComponent<Light>();
        StartCoroutine(Flicker());
    }

    private IEnumerator Flicker()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
            _light.enabled = !_light.enabled;
        }
    }
}
