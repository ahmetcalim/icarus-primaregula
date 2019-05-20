using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KNode : MonoBehaviour
{
    public List<KNode> links;
    public GameObject asset;
    public float scaleFactor;
    public KNode nextNode,prevNode;
    public TunnelType tunnelType;
    public ThemeType themeType;
    public bool hasCable;
    public GameObject cable;
    private Animator cableAnimator;
    private CapsuleCollider cableCollider;
    private Camera cam;
    private void Start()
    {
        cam = Camera.main;
        if (!hasCable)
        {
            if (cable != null)
            {
                cable.SetActive(false);
            }

        }
        else
        {
            cableAnimator = cable.GetComponent<Animator>();
            cableCollider = cable.GetComponent<CapsuleCollider>();

        }
        StartCoroutine(Recursive());
       
    }
    IEnumerator Recursive()
    {
        yield return new WaitForSeconds(0.01f);
        
            if (hasCable)
            {
                if (cam != null)
                {
                    if ((transform.position.z - cam.transform.position.z) < 200f)
                    {
                        hasCable = false;
                        if (cableAnimator != null && cableCollider != null)
                        {
                            cableAnimator.SetTrigger("ReleaseCables");
                        cableCollider.enabled = true;
                        }
                          
                        
                    }
                    else
                    {
                        StartCoroutine(Recursive());
                    }
                }
            }
    }
    public enum TunnelType
    {
        Lab_Column, Lab_Section1, Lab_Section2, Lab_Section3, Lab_Section3_R, Lab_Section4, Lab_Section4_R, Lab_Section5, Lab_Section5_R,
        Glass_Column,Glass_Section1,
        Hospital_Column,Hospital_Section1, Hospital_Section2, Hospital_Section3,OpenSpace_Section
    }
    public enum ThemeType
    {
        Lab1,Lab2,Lab3,Hospital,Glass,OpenSpace
    }

}
