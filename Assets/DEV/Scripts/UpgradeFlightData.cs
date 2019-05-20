using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpgradeFlightData : MonoBehaviour
{
    Text fDataTxt;
        // Start is called before the first frame update
    void Start()
    {
        fDataTxt = GetComponent<Text>();
        StartCoroutine(Recursive());
    }

    // Update is called once per frame
    IEnumerator Recursive()
    {
        yield return new WaitForSeconds(1f);
        fDataTxt.text = PlayerPrefs.GetFloat("gResource").ToString();
        StartCoroutine(Recursive());
    }
    void LateUpdate()
    {
        
    }
}
