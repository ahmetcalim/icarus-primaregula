using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletStorage : MonoBehaviour
{
    public static int bulletAmount;
    public static Text bulletAmount_T;
    public Text _bulletAmountTxt;

    
    public static int BulletAmount
    {
        get { return bulletAmount; }
        set { bulletAmount = value; bulletAmount_T.text = bulletAmount.ToString(); }
    }
    private void Start()
    {
        bulletAmount_T = _bulletAmountTxt;
        bulletAmount = 10000;
    }
}
