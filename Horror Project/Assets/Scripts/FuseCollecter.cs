using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuseCollecter : MonoBehaviour
{

    private GameObject fuseObj;

    [SerializeField] private int maxFuses;
    public int currentFuses;
    
    [SerializeField] private Text fusesCountText;
    
    [SerializeField] private AudioSource pickSound;
    float currentAlpha = 1;
    void Update()
    {
        CollectFuses();
    }

    public void CollectFuses()
    {
        if (fuseObj != null)
        {
            fusesCountText.text = "Pick fuse (E)";

            if (Input.GetKeyDown(KeyCode.E))
            {
                currentFuses++;
                StartCoroutine(FuseCoroutine());
                Destroy(fuseObj);
                fuseObj = null;
            }
        }
    }

    private IEnumerator FuseCoroutine()
    {
        fusesCountText.text = "Fuses collected: " + currentFuses + "/" + maxFuses;
        yield return new WaitForSeconds(3f);
        float segundosFade = 3f;
        float step = 1 / segundosFade;
        currentAlpha = 1f;
        do
        {
            fusesCountText.color = new Color(fusesCountText.color.r, fusesCountText.color.g, fusesCountText.color.b, currentAlpha);
            currentAlpha -= step * Time.deltaTime;
            yield return new WaitForEndOfFrame();

        } while (currentAlpha>0);

        fusesCountText.text = "";
        fusesCountText.color = new Color(fusesCountText.color.r, fusesCountText.color.g, fusesCountText.color.b, 1);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fuse") && fuseObj == null)
        {
            fuseObj = other.gameObject;
            Debug.Log("Player has entered trigger");
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Fuse") && fuseObj != null && fuseObj == other.gameObject )
        {
            fuseObj = null;
            fusesCountText.text = "";
            Debug.Log("Player has exit the trigger");
        }
    }
}
