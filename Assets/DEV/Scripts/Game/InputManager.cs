using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class InputManager : MonoBehaviour
{
    public SteamVR_TrackedObject leftController;
    public SteamVR_TrackedObject rightController;
    public PowerUpController powerUpController;
    public Player player;
    public List<Image> throttleLoadingBar;
    public Text throttlePowerTxt;
    public List<Image> dashBoardImageElements;
    public List<Text> dashBoardTextElements;
    public Rigidbody playerRigidbody;
    public AudioSource jetpackThrottle;
    int rocketIndex;
    public Gun gunR;
    public Gun gunL;
    public enum HandType {Left, Right};
    public static HandType hand;
    public bool isGun;
    private float gunTimer;
    private float gunInterval;
    public UnityEvent onLeftTriggerDown;
    public UnityEvent onRightTriggerDown;

    public SteamVR_Controller.Device ControllerL
    {
        get { return SteamVR_Controller.Input((int)leftController.index); }
    }
    public SteamVR_Controller.Device ControllerR
    {
        get { return SteamVR_Controller.Input((int)rightController.index); }
    }
    Color a;
  
    public bool IsDashboardVisible { get; set; } = true;
    public float AccelerationY { get; set; }
    public static float YMovementConstant_1 { get; set; } = 44;
    public static float YMovementConstant_2 { get; set; } = 100;
    public static float YMovementConstant_3 { get; set; } = 3;
    public float Timer { get; set; }
    public float AngleXController { get; set; }
    private void Start()
    {
        gunInterval = 1f/9f;
        switch (hand)
        {
            case HandType.Left:
                leftController.GetComponent<VR_ControllerManager>().enabled = true;
                gunR.gunObj.SetActive(true);
                gunR.joyBooster.SetActive(false);
                gunL.joyBooster.SetActive(true);
                break;
            case HandType.Right:
                rightController.GetComponent<VR_ControllerManager>().enabled = true;
                gunL.gunObj.SetActive(true);
                gunL.joyBooster.SetActive(false);
                gunR.joyBooster.SetActive(true);
                break;
            default:
                break;
        }
    }
    void FixedUpdate()
    {
        if (Player.IsGameRunning)
        {
            CheckTouchpadInput();
            CheckTriggerInput();
            CheckGripInput();
        }
    }
    private void CheckTouchpadInput()
    {
        if (ControllerL.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
        {
            UsePowerup(leftController);
            rocketIndex = 0;
        }
        if (ControllerR.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
        {
            UsePowerup(rightController);
            rocketIndex = 1;
        }
    }
    private void UsePowerup(SteamVR_TrackedObject controller)
    {
        switch (controller.powerUpSlot.tag)
        {
            case "Phase":
                if (!powerUpController.IsPhaseActive)
                {
                    powerUpController.UsePhase();
                    controller.powerUpSlot.tag = "Untagged";
                    controller.powerUpSlot.GetComponent<Image>().sprite = null;
                }
                break;
            case "Rocket":
                
                powerUpController.UseRocket(rocketIndex);
                controller.powerUpSlot.tag = "Untagged";
                controller.powerUpSlot.GetComponent<Image>().sprite = null;
                break;
            case "BulletTime":
                powerUpController.UseBulletTime();
                controller.powerUpSlot.tag = "Untagged";
                controller.powerUpSlot.GetComponent<Image>().sprite = null;
                break;
            default:
                break;
        }
    }
    private void CheckTriggerInput()
    {
       
        switch (hand)
        {
            case HandType.Left:
                AngleXController = leftController.transform.rotation.x * -1f;
                Fly(ControllerL, ControllerR, gunR);
                break;
            case HandType.Right:
                AngleXController = rightController.transform.rotation.x * -1f;
                Fly(ControllerR, ControllerL, gunL);
                break;
            default:
                break;
        }
        
    }

    private void Fly(SteamVR_Controller.Device controllerFly, SteamVR_Controller.Device controllerGun, Gun gun)
    {
        if (controllerFly.GetHairTrigger())
        {
            if (Timer <= 1f)
            {
                Timer += Time.deltaTime;
                jetpackThrottle.volume += Timer / 500f;
                if (throttleLoadingBar[(int)(Timer * 10f)].enabled == false)
                {
                    throttleLoadingBar[(int)(Timer * 10f)].enabled = true;
                }
                throttlePowerTxt.text = "%" + ((int)(Timer * 100f)).ToString();
            }
            if (AngleXController > 0f)
            {
                Up(1);
            }
            else
            {
                Up(-1);
            }
        }
        else
        {
            if (Timer >= 0f)
            {
                Timer -= Time.deltaTime;
                jetpackThrottle.volume -= Timer / 500f;
                if (throttleLoadingBar[(int)(Timer * 10f) + 1].enabled == true)
                {
                    throttleLoadingBar[(int)(Timer * 10f) + 1].enabled = false;
                }
                if (Timer <= 0.01f)
                {
                    throttleLoadingBar[0].enabled = false;
                }
                throttlePowerTxt.text = "%" + ((int)(Timer * 100f) + 1).ToString();
            }
        }
        if (controllerGun.GetHairTriggerDown())
        {
            gunTimer = 0;
        }
        if (controllerGun.GetHairTrigger())
        {
            if (isGun)
            {
                gunTimer += Time.deltaTime;
                if (gunTimer > gunInterval)
                {
                    gun.Fire();
                    gunTimer = 0f;
                }
                
            }
          
        }
    }

    private void CheckGripInput()
    {
        if (ControllerL.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
        {
            ChangeDashboardVisibleAfterFrame();
        }
    }
    private void ChangeDashboardVisibility(List<Text> texts, List<Image> images, float visibilityAmount)
    {
        for (int i = 0; i < images.Count; i++)
        {
            images[i].color = new Color(1f, 1f, 1f, visibilityAmount);
        }
        for (int i = 0; i < texts.Count; i++)
        {
            texts[i].color = new Color(1f, 1f, 1f, visibilityAmount);
        }
    }
    public void ChangeDashboardVisibleAfterFrame()
    {
        switch (IsDashboardVisible)
        {
            case true:
                StartCoroutine(DashboardVisible(false));
                ChangeDashboardVisibility(dashBoardTextElements, dashBoardImageElements, .05f);
                break;
            default:
                StartCoroutine(DashboardVisible(true));
                ChangeDashboardVisibility(dashBoardTextElements, dashBoardImageElements, 1f);
                break;
        }
    }

    private IEnumerator DashboardVisible(bool state)
    {
        yield return new WaitForSeconds(.1f);
        IsDashboardVisible = state;
    }
    public void Up(float side)
    {
        if (Player.IsGameRunning)
        {
            AccelerationY = Mathf.Pow(YMovementConstant_3 * (Time.deltaTime + 0.03f), YMovementConstant_1 / YMovementConstant_2);
            if (side < 0)
            {
                AccelerationY *= (AngleXController * -8f);
            }
            switch (side)
            {
                case 1:
                    playerRigidbody.velocity += Vector3.up * 1.3f * AccelerationY * PowerUpController.BulletTimeMultipleValue;
                    break;
                case -1:
                    playerRigidbody.velocity += Vector3.down * AccelerationY * PowerUpController.BulletTimeMultipleValue;
                    break;
                default:
                    break;
            }
            
        }
    }
}
