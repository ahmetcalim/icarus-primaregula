using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TutorialController : MonoBehaviour
{
    private bool isDontShowAgainChecked;
    public GameObject tick;
   
    private void Start()
    {
        if (PlayerPrefs.GetInt("isTutorialShown") == 0)
        {
            PlayerPrefs.SetInt("isTutorialShown", 1);
        }
        if (PlayerPrefs.GetInt("isTutorialShown") == 2)
        {
            SceneManager.LoadScene(1);
        }
    }
    public void GetDontShowAgain()
    {
        switch (PlayerPrefs.GetInt("isTutorialShown"))
        {
            case 1:
                PlayerPrefs.SetInt("isTutorialShown", 2);
                isDontShowAgainChecked = true;
                break;
            case 2:
                PlayerPrefs.SetInt("isTutorialShown", 1);
                isDontShowAgainChecked = false;
                break;
            default:
                break;
        }
        tick.SetActive(isDontShowAgainChecked);
    }
    public void SetFalse()
    {
        PlayerPrefs.SetInt("isTutorialShown", 1);
        isDontShowAgainChecked = false;
        tick.SetActive(isDontShowAgainChecked);
    }
}
