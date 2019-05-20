using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    public string name;
    public int index;
    private float cost;
    public GameObject contObj;
    // Start is called before the first frame update
    void OnEnable()
    {
        switch (Player.lifeCount)
        {
            case 0:
                cost = 2000;
                break;
            case 1:
                cost = 5000;
                break;
            case 2:
                cost = 10000;
                break;
            default:
                break;
        }
        StartCoroutine(ContinueObj());
        switch (index)
        {
            case 0:
                GetComponent<Text>().text = name + Player.TravelledDistance.ToString("F0");
                break;
            case 1:
                GetComponent<Text>().text = name + cost.ToString();
                break;
            default:
                break;
        }
       
    }
    IEnumerator ContinueObj()
    {
        yield return new WaitUntil(() => contObj != null);

            if (cost > Player.TravelledDistance)
            {

                if (contObj != null)
                {
                    contObj.SetActive(false);
                }
            }
        if (!Player.isSurvival && cost < Player.TravelledDistance)
        {
            contObj.SetActive(true);
        }
       
       
    }
}
