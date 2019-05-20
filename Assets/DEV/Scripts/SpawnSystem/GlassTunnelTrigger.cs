using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassTunnelTrigger : MonoBehaviour
{
    public int index;
    Player player;
    Camera cam;
    private void Start()
    {
        cam = Camera.main;
        player = FindObjectOfType<Player>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GlassTunnel"))
        {
            if (index == 1)
            {
                Player.IsGlassTunnelActive = true;
                player.ActivateGlassTunnel(true);
            }
            if (index==0 && Player.IsGlassTunnelActive == true)
            {
                Player.IsGlassTunnelActive = false;
                player.ActivateGlassTunnel(false);
            }
            
        }
    }
}
