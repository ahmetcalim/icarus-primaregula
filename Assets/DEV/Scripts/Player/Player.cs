using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using System.Linq;

public class Player : MonoBehaviour
{
    public List<AudioClip> musicList;
    public List<AudioSource> effects;
    public AudioSource music;
    public GameObject UIVROrigin;
    public GameObject VRGameOrigin;
    public List<Image> speedBar;
    public GameObject glassTunnel;
    public Text resource;
    public Text speed;
    public Text travelledDistance;
    public Text resourceSum;
    public PowerUpController powerUpController;
    public RectTransform dashBoard;
    public AudioSource bonusRecieved;
    public List<Light> lights;
    public static int lifeCount;
    public static bool isSurvival; 
    private PowerUp powerUp;
    public GameObject secondChance;
    private bool oneTime;
    private int currentMusic = 0;
    public int SpeedBarState { get; set; } = 0;
    public float GainedResource { get; set; }
    public float GainedResourceSum { get; set; }
    public float SpeedBarInterval { get; set; } = 50;
    public float LoadAmount { get; set; } = 0f;
    public static float TravelledDistance { get; set; }
    public float VelocityIncreaseAmount { get; set; }
    public static bool isGameRunning = true;
    public static bool IsGlassTunnelActive { get; set; } = false;
    public static float VelocityZMax { get; set; }
    public static float VelocityZTime { get; set; }
    public static float _velocityZBase = 40;
    public static float VelocityZBase { get => _velocityZBase; set => _velocityZBase = value; }
    public static float ResourceMultipleValue { get; set; } = .01f;
    public static float TotalDistance { get; set; }
    public static float Difficulty { get; set; } = 0;
    public AudioSource gas;
    private GameObject[] closest;
    private GameObject[] closestLaser;
    public static float secondCCost;
    public ReflectionProbe reflectionProbe;
    
    private void Start()
    {
        secondCCost = 0f;

        if (PlayerPrefs.GetFloat("best") < TravelledDistance)
        {
            PlayerPrefs.SetFloat("best", TravelledDistance);
        }
        StartScenePositionManager.UpdatePreferences();
        TravelledDistance = 0f;
        lifeCount = 0;
        oneTime = false;
        ResetStaticValues();
        SetVelocityValues();
        isGameRunning = true;
        SpeedBarInterval = VelocityZBase + ((VelocityZMax - VelocityZBase) * 0.16666666666f);
        ChangeShadowState();
        SetMusicSettings();
        SetRenderScale();
        RepositionDashboard();
        Time.timeScale = 1f;
       
    }
    private void OnEnable()
    {
        StartCoroutine(IncreaseVelocityZ);
        music.pitch = 1f;
        music.volume = PlayerPrefs.GetFloat("musicVolume");
    }
    private void RepositionDashboard() 
    {
        float a = dashBoard.position.z - transform.position.z;
        dashBoard.position = new Vector3(transform.position.x, dashBoard.position.y-.05f, dashBoard.position.z + Mathf.Abs(a));
    }
    private static void SetVelocityValues()
    {
        VelocityZMax = PlayerPrefs.GetFloat("movementZMax");
        VelocityZBase = PlayerPrefs.GetFloat("movementZMin");
        VelocityZTime = PlayerPrefs.GetFloat("movementZTime");
    }
    private static void SetRenderScale()
    {
        if (PlayerPrefs.GetFloat("renderScale") == 0)
        {
            PlayerPrefs.SetFloat("renderScale", 1f);
        }
        XRSettings.eyeTextureResolutionScale = PlayerPrefs.GetFloat("renderScale");
    }
    private void SetMusicSettings()
    {
        for (int i = 0; i < effects.Count; i++)
        {
            effects[i].volume = PlayerPrefs.GetFloat("effectVolume");
        }
        music.volume = PlayerPrefs.GetFloat("musicVolume");
    }
    private void ChangeShadowState()
    {
        switch (PlayerPrefs.GetInt("isShadowsEnabled"))
        {
            case 0:
                PlayerPrefs.SetInt("isShadowsEnabled", 0);
                for (int i = 0; i < lights.Count; i++)
                {
                    lights[i].shadows = LightShadows.None;
                }
                break;
            case 1:
                PlayerPrefs.SetInt("isShadowsEnabled", 1);
                for (int i = 0; i < lights.Count; i++)
                {
                    lights[i].shadows = LightShadows.Hard;
                }
                break;
            default:
                break;
        }
    }
    private void Update() => RuntimeEvents();
    public static bool IsGameRunning => isGameRunning;
    private void RuntimeEvents()
    {
        if (IsGameRunning)
        {
            CalculateBaseValues();
            PrintValuesToDashboard();
        }
    }
    private void CalculateBaseValues()
    {
        Difficulty = Mathf.Pow(3, (Time.timeSinceLevelLoad * 2 / (90 + Time.timeSinceLevelLoad)) + 1) - 3;
        TravelledDistance = (Time.timeSinceLevelLoad * VelocityZBase) - secondCCost;
        GainedResource = TravelledDistance * Difficulty * ResourceMultipleValue;
    }
    private void PrintValuesToDashboard()
    {
        PrintValueToText(resource, ((int)TravelledDistance).ToString(), "");
        PrintValueToText(travelledDistance, ((int)GainedResource).ToString(), "");
        PrintValueToText(resourceSum, ((int)PlayerPrefs.GetFloat("best")).ToString(), "");
    }
    public static void ResetStaticValues()
    {
        Difficulty = 0f;
        ResourceMultipleValue = .5f;
        IsGlassTunnelActive = false;
    }
    private void GetClosest()
    {
        closest = GameObject.FindGameObjectsWithTag("Obstacle");
        closestLaser = GameObject.FindGameObjectsWithTag("Laser");
        closest = closest.OrderBy(t =>

        (t.transform.position - transform.position).sqrMagnitude).Take(15).ToArray();
        closestLaser = closestLaser.OrderBy(t =>

        (t.transform.position - transform.position).sqrMagnitude).Take(10).ToArray();
        if (closest.Length == closestLaser.Length)
        {
            for (int i = 0; i < closest.Length; i++)
            {
                Destroy(closest[i].gameObject);
                Destroy(closestLaser[i].gameObject);
            }
        }
        else
        {
            for (int i = 0; i < closest.Length; i++)
            {
                Destroy(closest[i].gameObject);
            }
            for (int i = 0; i < closestLaser.Length; i++)
            {
             
                Destroy(closestLaser[i].gameObject);
            }

        }
      
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle") || other.CompareTag("ObstacleSelected")|| other.CompareTag("Laser"))
        {
            Destroy(other.gameObject);
            gas.enabled = false;
            if (lifeCount>2)
            {
                secondChance.SetActive(false);
            }
          
            if (!oneTime)
            {
                oneTime = true;
            }
            GameOver(other);

        }
        if (other.CompareTag("Powerup"))
        {
            GetPowerup(other);
        }
    }
    private void GetPowerup(Collider other)
    {
        bonusRecieved.Play();
        Destroy(other.gameObject);
        powerUp = other.GetComponent<PowerUp>();
        powerUpController.SetPowerUp(powerUp.sprite, powerUp.tagName);
    }
    private void GameOver(Collider other)
    {
        if (!powerUpController.IsPhaseActive)
        {
            GetClosest();
            StartCoroutine(ReduceLightIntensity());
            StartCoroutine(ChangePitch());
            isGameRunning = false;
            PlayerPrefs.SetFloat("gResourceCurrent", GainedResource);
            GainedResourceSum = PlayerPrefs.GetFloat("gResource") + GainedResource;
            PlayerPrefs.SetFloat("gResource", GainedResourceSum);
           
        }
    }
    private IEnumerator ChangePitch()
    {
        yield return new WaitForSeconds(.1f);

        if (music.pitch >= 0f)
        {
            music.pitch -= .05f;
            if (music.pitch <= 0.3f)
            {
                music.volume = 0f;
            }
            StartCoroutine(ChangePitch());
        }
      
    }
    private IEnumerator ReduceLightIntensity()
    {
        yield return new WaitForSeconds(.1f);
      
        if (reflectionProbe.intensity >=0f)
        {
            reflectionProbe.intensity -= .05f;
            StartCoroutine(ReduceLightIntensity());
        }
        else
        {
            if (lights[0].enabled == true)
            {
                for (int i = 0; i < lights.Count; i++)
                {
                    lights[i].enabled = false;
                }
            }
            
            VRGameOrigin.SetActive(false);
            UIVROrigin.SetActive(true);
        }
    }
    public void PrintValueToText(Text textObject, string value, string name) => textObject.text = name + "" + value;
    private IEnumerator IncreaseVelocityZ
    {
        get
        {
            yield return new WaitForSeconds(.1f * PowerUpController.BulletTimeMultipleValue);
            if (VelocityZBase < VelocityZMax && IsGameRunning)
            {
                LoadAmount += (VelocityZMax - VelocityZBase) / (VelocityZTime * 100f);
                speedBar[SpeedBarState].color = new Color(1f, 1f, 1f, LoadAmount);
                if (VelocityZBase > SpeedBarInterval)
                {
                    LoadAmount = 0f;
                    SpeedBarInterval += (VelocityZMax - VelocityZBase) / 6f;
                    if (SpeedBarState<6)
                    {
                        SpeedBarState++;
                    }
                }
                VelocityZBase += (VelocityZMax - VelocityZBase) / (VelocityZTime * 10f);
                StartCoroutine(IncreaseVelocityZ);
            }
        }
    }
    public void ActivateGlassTunnel(bool state)
    {
        glassTunnel.SetActive(state);
    }
}
