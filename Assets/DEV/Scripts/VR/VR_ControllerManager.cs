using System.Collections.Generic;
using UnityEngine;

public class VR_ControllerManager : MonoBehaviour
{
    private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
    public GameObject triggerPrefab;
    public Transform playerTransform;
    public List<MeshRenderer> joystickRenderers;
    public Player player;
    float x;
    public enum ControlType { PHYSICS, NOTPHYSICS}
    public ControlType controlType;
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)TrackedObj.index); }
    }
    public GameObject CollidingObject { get; set; }
    public Vector3 TargetTransform { get; set; }
    public SteamVR_TrackedObject TrackedObj { get; set; }
    
    public float Angle { get; set; }

    private float turnConstant = .1f;
    private Vector3 targetTransform;

    public float GetTurnConstant() => turnConstant;
    public void SetTurnConstant(float value) => turnConstant = value;
    void Awake() => TrackedObj = GetComponent<SteamVR_TrackedObject>();
    private void Update()
    {
        if (Player.IsGameRunning)
        {
            Turn();
        }
    }
    private void Turn()
    {
        x = Controller.GetAxis(triggerButton).x;
        triggerPrefab.transform.localRotation = Quaternion.Euler(-x * 15, 0, 0);
        Angle = transform.rotation.eulerAngles.z;
        if ((Angle >= 30f && Angle < 150))
        {
            SetTurnConstant((((Angle - 30)))/9);
            playerTransform.GetComponent<Rigidbody>().velocity = Vector3.Lerp(playerTransform.GetComponent<Rigidbody>().velocity, new Vector3(0f, playerTransform.GetComponent<Rigidbody>().velocity.y, playerTransform.GetComponent<Rigidbody>().velocity.z), Time.deltaTime * 10f);

            playerTransform.GetComponent<Rigidbody>().AddForce(Vector3.left * 5000f * Time.deltaTime * turnConstant * PowerUpController.BulletTimeMultipleValue, ForceMode.Force);

        }
        else if (Angle >= 210f && Angle < 330f)
        {
            SetTurnConstant(((Mathf.Abs((330f - Angle))))/9);
            playerTransform.GetComponent<Rigidbody>().velocity = Vector3.Lerp(playerTransform.GetComponent<Rigidbody>().velocity, new Vector3(0f, playerTransform.GetComponent<Rigidbody>().velocity.y, playerTransform.GetComponent<Rigidbody>().velocity.z), Time.deltaTime * 10f);

            playerTransform.GetComponent<Rigidbody>().AddForce(Vector3.right * 5000f * Time.deltaTime * turnConstant * PowerUpController.BulletTimeMultipleValue, ForceMode.Force);
            //

        }
        else
        {
            playerTransform.GetComponent<Rigidbody>().velocity = Vector3.Lerp(playerTransform.GetComponent<Rigidbody>().velocity, new Vector3(0f, playerTransform.GetComponent<Rigidbody>().velocity.y, playerTransform.GetComponent<Rigidbody>().velocity.z), Time.deltaTime * 10f);
        }
    }
}


