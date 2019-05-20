using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Upgrade : MonoBehaviour
{
    public enum UpgradeTitle {POWERUP, MOVEMENT};
    public UpgradeTitle upgradeTitle;
    public enum UpgradeType {PHASE, ROCKET, BULLET_TIME, movementZTime, MOVEMENT_Z_Min, MOVEMENT_Z_Max}
    public UpgradeType upgradeType;
    public enum Operator {ADDITION, ELIMINATION, MULTIPLICATION, DIVISION}
    public Operator _operator;
    public float cost;
    public float upgradeAmount;
    public int index;
    private UpgradeManager upgradeManager;
    public GameObject lockObject;
    public GameObject keyObject;
    public GameObject costBox;
    public Text costText;
    public string informationValue;
    public Text information;
    public Button yesButton;
    public Button noButton;
    public GameObject popupPanel;
    public GameObject informationPanel;
    public UpgradeController upgradeController;
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(EnablePopUp);
        upgradeManager = FindObjectOfType<UpgradeManager>();

        CheckAvailabity();

    }
    
    public void OnHover()
    {
        information.text = informationValue;
    }
    public void OnExit()
    {
        information.text = "";
    }
    public void ApplyUpgrade()
    {
        popupPanel.SetActive(false);
        informationPanel.SetActive(true);
        if (PlayerPrefs.GetFloat("gResource") >= cost)
        {
           switch (upgradeType)
                {
                    case UpgradeType.PHASE:
                        if (upgradeManager.phaseUpgradeLevelIndex == index)
                        {
                            PlayerPrefs.SetFloat("phasePowerUpDuringTime", PlayerPrefs.GetFloat("phasePowerUpDuringTime") + upgradeAmount);
                            DoTheseBeforeUpgrade();
                            upgradeManager.phaseUpgradeLevelIndex++;
                            PlayerPrefs.SetInt("phaseUpgradeLevelIndex", upgradeManager.phaseUpgradeLevelIndex);
                        }
                        break;
                    case UpgradeType.ROCKET:
                        if (upgradeManager.rocketUpgradeLevelIndex == index)
                        {
                            PlayerPrefs.SetInt("rocketCount", PlayerPrefs.GetInt("rocketCount") + (int)upgradeAmount);
                            DoTheseBeforeUpgrade();
                            upgradeManager.rocketUpgradeLevelIndex++;
                            PlayerPrefs.SetInt("rocketUpgradeLevelIndex", upgradeManager.rocketUpgradeLevelIndex);

                        }
                        break;
                    case UpgradeType.BULLET_TIME:
                        if (upgradeManager.bulletTimeUpgradeLevelIndex == index)
                        {
                            PlayerPrefs.SetFloat("bulletTimeDuringTime", PlayerPrefs.GetFloat("bulletTimeDuringTime") + upgradeAmount);
                            DoTheseBeforeUpgrade();
                            upgradeManager.bulletTimeUpgradeLevelIndex++;
                            PlayerPrefs.SetInt("bulletTimeUpgradeLevelIndex", upgradeManager.bulletTimeUpgradeLevelIndex);
                           
                        }
                        break;
                    case UpgradeType.MOVEMENT_Z_Min:
                        if (upgradeManager.movementZMinUpgradeLevelIndex == index)
                        {
                            PlayerPrefs.SetFloat("movementZMin", PlayerPrefs.GetFloat("movementZMin") + upgradeAmount);
                            DoTheseBeforeUpgrade();
                            upgradeManager.movementZMinUpgradeLevelIndex++;
                            PlayerPrefs.SetInt("movementZMinUpgradeLevelIndex", upgradeManager.movementZMinUpgradeLevelIndex);
                            
                        }
                        break;
                    case UpgradeType.MOVEMENT_Z_Max:
                        if (upgradeManager.movementZMaxUpgradeLevelIndex == index)
                        {
                            PlayerPrefs.SetFloat("movementZMax", PlayerPrefs.GetFloat("movementZMax") + upgradeAmount);
                            DoTheseBeforeUpgrade();
                            upgradeManager.movementZMaxUpgradeLevelIndex++;
                            PlayerPrefs.SetInt("movementZMaxUpgradeLevelIndex", upgradeManager.movementZMaxUpgradeLevelIndex);
                          
                        }
                        break;
                    case UpgradeType.movementZTime:
                        if (upgradeManager.movementZTimeUpgradeLevelIndex == index)
                        {
                            PlayerPrefs.SetFloat("movementZTime", PlayerPrefs.GetFloat("movementZTime") - upgradeAmount);
                            DoTheseBeforeUpgrade();
                            upgradeManager.movementZTimeUpgradeLevelIndex++;
                            PlayerPrefs.SetInt("movementZTimeUpgradeLevelIndex", upgradeManager.movementZTimeUpgradeLevelIndex);
                           
                        }
                        break;
                    default:
                        break;
            }
            for (int i = 0; i < upgradeController.upgrades.Count; i++)
            {
                upgradeController.upgrades[i].CheckAvailabity();
            }
        }
        else
        {
            upgradeController.NotEnoughError();
        }
    }
    public void EnablePopUp()
    {
        popupPanel.SetActive(true);
        informationPanel.SetActive(false);
        GetComponent<Button>().enabled = true;
        yesButton.onClick.AddListener(ApplyUpgrade);
        noButton.onClick.AddListener(DisablePopup);
    }
    public void DisablePopup()
    {
        GetComponent<Button>().enabled = true;
        GetComponent<Button>().onClick.AddListener(EnablePopUp);
       

    }
    public void CheckAvailabity()
    {
       
        switch (upgradeType)
        {
            case UpgradeType.PHASE:
                if (PlayerPrefs.GetInt("phaseUpgradeLevelIndex") > this.index)
                {
                    costBox.SetActive(false);
                    GetComponent<Button>().enabled = false;
                }
                else if (PlayerPrefs.GetInt("phaseUpgradeLevelIndex") == this.index)
                {
                    SetAvailabity(true, false);
                }
                else
                {
                    SetAvailabity(false, true);
                    
                }
               
                break;
            case UpgradeType.ROCKET:
                if (PlayerPrefs.GetInt("rocketUpgradeLevelIndex") > index)
                {
                    costBox.SetActive(false);
                    GetComponent<Button>().enabled = false;
                }
                else if (PlayerPrefs.GetInt("rocketUpgradeLevelIndex") == this.index)
                {
                    SetAvailabity(true, false);
                }
                else
                {
                    SetAvailabity(false, true);

                }
                break;
            case UpgradeType.BULLET_TIME:
                if (PlayerPrefs.GetInt("bulletTimeUpgradeLevelIndex") > index)
                {
                    costBox.SetActive(false);
                    GetComponent<Button>().enabled = false;
                }
                else if (PlayerPrefs.GetInt("bulletTimeUpgradeLevelIndex") == this.index)
                {
                    SetAvailabity(true, false);
                }
                else
                {
                    SetAvailabity(false, true);

                }
                break;
            case UpgradeType.MOVEMENT_Z_Min:
                if (PlayerPrefs.GetInt("movementZMinUpgradeLevelIndex") > index)
                {
                    costBox.SetActive(false);
                    GetComponent<Button>().enabled = false;

                }
                else if (PlayerPrefs.GetInt("movementZMinUpgradeLevelIndex") == this.index)
                {
                    SetAvailabity(true, false);
                }
                else
                {
                    SetAvailabity(false, true);

                }
                break;
            case UpgradeType.MOVEMENT_Z_Max:
                if (PlayerPrefs.GetInt("movementZMaxUpgradeLevelIndex") > index)
                {
                    costBox.SetActive(false);
                    GetComponent<Button>().enabled = false;
                }
                else if (PlayerPrefs.GetInt("movementZMaxUpgradeLevelIndex") == this.index)
                {
                    SetAvailabity(true, false);
                }
                else
                {
                    SetAvailabity(false, true);

                }
                break;
            case UpgradeType.movementZTime:
                if (PlayerPrefs.GetInt("movementZTimeUpgradeLevelIndex") > index)
                {
                    costBox.SetActive(false);
                    GetComponent<Button>().enabled = false;
                }
                else if (PlayerPrefs.GetInt("movementZTimeUpgradeLevelIndex") == this.index)
                {
                    SetAvailabity(true, false);
                }
                else
                {
                    SetAvailabity(false, true);

                }
                break;
            default:
                break;
        }  
    }
    private void SetAvailabity(bool _key, bool _lock)
    {
        costText.text = cost.ToString();
        keyObject.SetActive(_key);
        lockObject.SetActive(_lock);
        GetComponent<Button>().enabled = _key;
    }
    private void DoTheseBeforeUpgrade()
    {
        PlayerPrefs.SetFloat("gResource", PlayerPrefs.GetFloat("gResource") - cost);
        upgradeManager.UpdateResourceText(PlayerPrefs.GetFloat("gResource"));
    }
}
