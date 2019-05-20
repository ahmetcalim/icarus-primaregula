using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpgradeManager : MonoBehaviour
{
    public Text playerResource;
    public int 
        phaseUpgradeLevelIndex = 0,
        rocketUpgradeLevelIndex = 0,
        bulletTimeUpgradeLevelIndex = 0,
        movementZMinUpgradeLevelIndex = 0,
        movementZTimeUpgradeLevelIndex = 0,
        movementZMaxUpgradeLevelIndex = 0;
    void Start()
    {
        UpdateUpgradeLevels();
        UpdateResourceText(PlayerPrefs.GetFloat("gResource"));
    }

    private void UpdateUpgradeLevels()
    {
        phaseUpgradeLevelIndex = PlayerPrefs.GetInt("phaseUpgradeLevelIndex");
        rocketUpgradeLevelIndex = PlayerPrefs.GetInt("rocketUpgradeLevelIndex");
        bulletTimeUpgradeLevelIndex = PlayerPrefs.GetInt("bulletTimeUpgradeLevelIndex");
        movementZMinUpgradeLevelIndex = PlayerPrefs.GetInt("movementZMinUpgradeLevelIndex");
        movementZMaxUpgradeLevelIndex = PlayerPrefs.GetInt("movementZMaxUpgradeLevelIndex");
        movementZTimeUpgradeLevelIndex = PlayerPrefs.GetInt("movementZTimeUpgradeLevelIndex");
        SetPlayerPrefs(PlayerPrefs.GetInt("phaseUpgradeLevelIndex"), "phaseUpgradeLevelIndex");
        SetPlayerPrefs(PlayerPrefs.GetInt("rocketUpgradeLevelIndex"), "rocketUpgradeLevelIndex");
        SetPlayerPrefs(PlayerPrefs.GetInt("bulletTimeUpgradeLevelIndex"), "bulletTimeUpgradeLevelIndex");
        SetPlayerPrefs(PlayerPrefs.GetInt("movementZMinUpgradeLevelIndex"), "movementZMinUpgradeLevelIndex");
        SetPlayerPrefs(PlayerPrefs.GetInt("movementZMaxUpgradeLevelIndex"), "movementZMaxUpgradeLevelIndex");
        SetPlayerPrefs(PlayerPrefs.GetInt("movementZTimeUpgradeLevelIndex"), "movementZTimeUpgradeLevelIndex");
    }

    private void SetPlayerPrefs(float a, string name)
    {
        if (a<1)
        {
            PlayerPrefs.SetInt(name, 0);
        }
    }
    public void UpdateResourceText(float resourceAmount)
    {
        playerResource.text ="Resource: " + ((int)resourceAmount).ToString();
    }
    
}
