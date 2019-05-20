using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSelector : MonoBehaviour
{
    public List<GameObject> lasers;
    public List<GameObject> cubes;
    public List<GameObject> robotArms;
    public bool laser;
    public bool robotArm;
    public bool cube;
    public bool has;
    void Start()
    {
        if (has)
        {
            if (laser)
            {
                lasers[Random.Range(0, lasers.Count)].SetActive(true);
            }
            if (cube)
            {
                cubes[Random.Range(0, cubes.Count)].SetActive(true);
            }
            if (robotArm)
            {
                robotArms[Random.Range(0, robotArms.Count)].SetActive(true);
            }
        }
    }
}
