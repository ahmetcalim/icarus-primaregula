using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScenePositionManager : MonoBehaviour
{
    public Canvas canvas;
    Ray RayOrigin;
    RaycastHit HitInfo;
    public Transform canvasParent;
    public Transform targetTransform;
    AsyncOperation asyncOperation;
    void Start()
    {
        UpdatePreferences();
    }
    public static void UpdatePreferences()
    {
        if (PlayerPrefs.GetFloat("musicVolume") == 0)
        {
            PlayerPrefs.SetFloat("musicVolume", 0.5f);
        }
        if (PlayerPrefs.GetFloat("effectVolume") == 0)
        {
            PlayerPrefs.SetFloat("effectVolume", 0.5f);
        }
        if (PlayerPrefs.GetFloat("movementZMax") > 100f)
        {
            Player.VelocityZMax = PlayerPrefs.GetFloat("movementZMax");
        }
        else
        {
            PlayerPrefs.SetFloat("movementZMax", 100f);
        }

        if (PlayerPrefs.GetFloat("movementZMin") > 40f)
        {
            Player.VelocityZBase = PlayerPrefs.GetFloat("movementZMin");
        }
        else
        {
            PlayerPrefs.SetFloat("movementZMin", 40f);
        }
        if (PlayerPrefs.GetFloat("movementZTime") < 180f)
        {
            Player.VelocityZTime = PlayerPrefs.GetFloat("movementZTime");
        }
        else
        {
            PlayerPrefs.SetFloat("movementZTime", 180f);
        }
        if (PlayerPrefs.GetFloat("movementZTime") == 0)
        {
            PlayerPrefs.SetFloat("movementZTime", 180f);
        }
        if (PowerUpController.PhaseMaxTime < 3)
        {
            PowerUpController.PhaseMaxTime = 3;
        }
        if (PowerUpController.BulletTimeDuringTime < 3)
        {
            PowerUpController.BulletTimeDuringTime = 3;
        }
        if (PowerUpController.RocketCount < 20)
        {
            PowerUpController.RocketCount = 20;
        }

        if (PlayerPrefs.GetFloat("bulletTimeDuringTime") > PowerUpController.BulletTimeDuringTime)
        {
            PowerUpController.BulletTimeDuringTime = PlayerPrefs.GetFloat("bulletTimeDuringTime");
        }
        else
        {
            PlayerPrefs.SetFloat("bulletTimeDuringTime", PowerUpController.BulletTimeDuringTime);
        }
        if (PlayerPrefs.GetFloat("phasePowerUpDuringTime") > PowerUpController.PhaseMaxTime)
        {
            PowerUpController.PhaseMaxTime = PlayerPrefs.GetFloat("phasePowerUpDuringTime");
        }
        else
        {
            PlayerPrefs.SetFloat("phasePowerUpDuringTime", PowerUpController.PhaseMaxTime);
        }
        if (PlayerPrefs.GetInt("rocketCount") > PowerUpController.RocketCount)
        {
            PowerUpController.RocketCount = PlayerPrefs.GetInt("rocketCount");
        }
        else
        {
            PlayerPrefs.SetInt("rocketCount", PowerUpController.RocketCount);
        }
    }
    public void PlayGame(bool isSurvival)
    {
        Player.isSurvival = isSurvival;
        SceneManager.LoadScene(5);
    }
  
    
}
