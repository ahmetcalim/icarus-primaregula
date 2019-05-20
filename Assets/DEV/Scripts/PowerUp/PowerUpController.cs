using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PowerUpController : MonoBehaviour
{
    public Image slotLeft;
    public Image slotRight;
    public GameObject phaseCameraEffect;
    public AudioSource mainAudioSource;
    public AudioSource countDownAudioSource;
    public SoundController soundController;
    public Material m_Controller_Fade;
    public Material m_Controller_Default;
    public RocketDestroyManager rocketDestroyManager;
    public InputManager inputManager;
    public bool isPhaseActive;
    public bool isRocketActive;
    public bool isBulletTimeActive;
    public float phaseMaxTimeMultipleValue = 1f;
    public static float PhaseMaxTime { get; set; }
    public static int RocketCount { get; set; }
    public static float BulletTimeMultipleValue { get; set; } = 1f;
    public static float BulletTimeDuringTime { get; set; }
    public int LastSlot { get; set; } = 1;
    public RocketLauncher rocketLauncherL;
    public RocketLauncher rocketLauncherR;
    private List<MeshRenderer> joysticRenderersL;
    private List<MeshRenderer> joysticRenderersR;
    public Animator rocketValidTarget;
    void Start()
    {
        joysticRenderersL = inputManager.leftController.GetComponent<VR_ControllerManager>().joystickRenderers;
        joysticRenderersR = inputManager.rightController.GetComponent<VR_ControllerManager>().joystickRenderers;
        Physics.gravity = new Vector3(0f,-20f,0f);

        ChangeControllerMaterialAlpha(m_Controller_Default);
    }
    public void SetPowerUp(Sprite powerUpSprite, string pUpTag)
    {
        HapticEffect();
        if (slotLeft.sprite == null)
        {
            slotLeft.sprite = powerUpSprite;
            slotLeft.gameObject.tag = pUpTag;
        }
        else if(slotRight.sprite == null)
        {
            slotRight.sprite = powerUpSprite;
            slotRight.gameObject.tag = pUpTag;
        }
        else
        {
            switch (LastSlot)
            {
                case 0:
                    slotRight.sprite = powerUpSprite;
                    slotRight.gameObject.tag = pUpTag;
                    LastSlot = 1;
                    break;
                case 1:
                    slotLeft.sprite = powerUpSprite;
                    slotLeft.gameObject.tag = pUpTag;
                    LastSlot = 0;
                    break;
                default:
                    break;
            }
        }
    }
    public void UseRocket(int index)
    {
        switch (index)
        {
            case 0:
                PlaySound(soundController.rocketEnter);
                if (rocketLauncherL.canLaunch == false)
                {
                   
                    rocketLauncherL.Launch();
                    if (!rocketLauncherL.isOk)
                    {
                        rocketValidTarget.SetTrigger("NoValidTargets");
                    }
                }
                break;
            case 1:
                PlaySound(soundController.rocketEnter);
                if (rocketLauncherR.canLaunch == false)
                {
                  
                    rocketLauncherR.Launch();
                    if (!rocketLauncherR.isOk)
                    {
                        rocketValidTarget.SetTrigger("NoValidTargets");
                    }
                }
                break;
            default:
                break;
        }
        
      
    }
    public void UsePhase()
    {
        phaseMaxTimeMultipleValue = 3.5f;
        PlaySound(soundController.phaseEnter);
        phaseCameraEffect.SetActive(true);
        ChangeControllerMaterialAlpha(m_Controller_Fade);
        isPhaseActive = true;
        StartCoroutine(ActivatePowerupTimer(0, PhaseMaxTime));
    }
    public void UseBulletTime()
    {
        PlaySound(soundController.bulletTimeEnter);
        BulletTimeMultipleValue = 2f;
        Time.timeScale = 0.5f;
        Physics.gravity = new Vector3(0f, -100f, 0f);
        isBulletTimeActive = true;
        StartCoroutine(ActivatePowerupTimer(1, BulletTimeDuringTime));
    }
    public bool IsPhaseActive => isPhaseActive;
    private void ChangeControllerMaterialAlpha(Material mat)
    {
        for (int i = 0; i < joysticRenderersL.Count; i++)
        {
            joysticRenderersL[i].sharedMaterial = mat;
            joysticRenderersR[i].sharedMaterial = mat;
        }
    }
    private void PlaySound(AudioClip clip)
    {
        mainAudioSource.clip = clip;
        mainAudioSource.Play();
    }
    private IEnumerator ActivatePowerupTimer(int powerupIndex, float timeCount)
    {
        yield return new WaitForSeconds(1f / BulletTimeMultipleValue);
        timeCount--;
        if (timeCount >= 1f && timeCount <= 4f)
        {
            countDownAudioSource.Play();
            HapticEffect();
        }
        if (timeCount < 2)
        {
            switch (powerupIndex)
            {
                case 0:
                    StartCoroutine(DeactivatePhase());
                    break;
                case 1:
                    DeactivateBulletTime();
                    break;
                default:
                    break;
            }
        }
        else
        {
            StartCoroutine(ActivatePowerupTimer(powerupIndex, timeCount));
        }
    }
    private void HapticEffect()
    {
        inputManager.ControllerL.TriggerHapticPulse(50000);
    }
    private void DeactivateBulletTime()
    {
        isPhaseActive = false;
        Physics.gravity = new Vector3(0f, -20f, 0f);
        BulletTimeMultipleValue = 1f;
        Time.timeScale = 1f;
        PlaySound(soundController.bulletTimeOut);
    }
    private IEnumerator DeactivatePhase()
    {
        PlaySound(soundController.phaseOut);
        phaseMaxTimeMultipleValue = 1f;
        yield return new WaitForSeconds(2f);
        isPhaseActive = false;
        ChangeControllerMaterialAlpha(m_Controller_Default);
        phaseCameraEffect.SetActive(false);
      
    }
}
