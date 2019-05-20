using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuController : MonoBehaviour
{

    public AudioClip hover;
    public AudioClip click;
    public GameObject VROrigin;
    public GameObject VRPlayer;
    public AudioSource gas;
    public Rigidbody playerRb;
    public List<Light> lights;
    public ReflectionProbe reflectionProbe;
    private int cost;
    public void OpenSceneByIndex(int index)
    {
        SceneManager.LoadScene(index);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Hover()
    {
        GetComponent<AudioSource>().clip = hover;
        GetComponent<AudioSource>().Play();
    }
    public void Click()
    {
        GetComponent<AudioSource>().clip = click;
        GetComponent<AudioSource>().Play();
    }
    public void Continue()
    {
        if (Player.lifeCount <3)
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
            PlayerPrefs.SetFloat("gResource", PlayerPrefs.GetFloat("gResource") - cost);
            ReduceLightIntensity();
        }
        else
        {

        }
      
    }
    private void ReduceLightIntensity()
    {
        reflectionProbe.intensity = .8f;
       
            if (lights[0].enabled == false)
            {
                foreach (var item in lights)
                {
                    item.enabled = true;
                }
            }
            Player.lifeCount++;
            VROrigin.SetActive(false);
            VRPlayer.SetActive(true);
            gas.enabled = true;
            Player.isGameRunning = true;
        
    }
}
