using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tips : MonoBehaviour
{

    public TextMeshProUGUI tipsText;
    public string tip;

    private void Start() {
        StartCoroutine(TipText());
    }

    private IEnumerator TipText()
    {
        tipsText.text = tip;
        yield return new WaitForSeconds(5f);
        tipsText.text = "";
    }
}
