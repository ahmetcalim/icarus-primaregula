
using System.Collections.Generic;
using UnityEngine;

public class UpgradeController : MonoBehaviour
{
    public List<Upgrade> upgrades;
    public Animator notenough;
    public void NotEnoughError()
    {
        notenough.SetTrigger("notEnough");
    }
}
