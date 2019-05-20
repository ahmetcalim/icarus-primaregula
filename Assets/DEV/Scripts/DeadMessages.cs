using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeadMessages : MonoBehaviour
{
    public List<string> texts;
    // Start is called before the first frame update
    void OnEnable()
    {
        GetComponent<Text>().text = texts[Random.Range(0, texts.Count)];
    }
}
