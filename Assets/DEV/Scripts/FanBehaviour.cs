using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanBehaviour : MonoBehaviour
{
    public float powerOfAnarchy;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Rigidbody>().AddForce(Vector3.up * (powerOfAnarchy + Player.Difficulty), ForceMode.Impulse);
        }
    }
}
