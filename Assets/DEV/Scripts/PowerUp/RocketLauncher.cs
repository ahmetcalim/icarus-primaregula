
using UnityEngine;
using System.Linq;
public class RocketLauncher : MonoBehaviour
{
    public GameObject rocket;
    public bool canLaunch; float step;
    public GameObject explosion;
    public float speed = 1.0f;
    public Transform player;
    public GameObject[] transforms;
    public GameObject[] targets;
    public static int count = 0;
    private bool isTargetsValid;
    GameObject[] nTransforms;
    GameObject[] nClosest;
    public bool isOk;
    public Animator noTargetAnimator;
    public void Launch()
    {
        SpawnRockets();
    }
    private void Awake()
    {
        if (PlayerPrefs.GetInt("rocketCount") == 0)
        {
            PlayerPrefs.SetInt("rocketCount", 10);
        }
        transforms = new GameObject[PlayerPrefs.GetInt("rocketCount")];
        targets = new GameObject[PlayerPrefs.GetInt("rocketCount")];
    }
    private GameObject[] GetNearests()
    {
        transforms = new GameObject[PlayerPrefs.GetInt("rocketCount")];
        targets = new GameObject[PlayerPrefs.GetInt("rocketCount")];
        isOk = false;
        nTransforms = GameObject.FindGameObjectsWithTag("Obstacle").ToArray();
      
        nClosest = nTransforms.OrderBy(t =>
        
        (t.transform.position - player.transform.position).sqrMagnitude).Take(PlayerPrefs.GetInt("rocketCount")).ToArray();

        for (int i = 0; i < nClosest.Length; i++)
        {
            Debug.Log((nClosest[i].transform.position - player.transform.position).sqrMagnitude);
            if ((nClosest[i].transform.position - player.transform.position).sqrMagnitude > 60000 && (nClosest[i].transform.position - player.transform.position).sqrMagnitude < 1000)
            {
                nClosest[i] = null;
            }
            if (nClosest[i] != null)
            {
                nClosest[i].tag = "ObstacleSelected";

            }
        }
        
        return nClosest;
    }
    private void SpawnRockets()
    {
        targets = GetNearests();
        for (int i = 0; i < targets.Length; i++)
        {
            if (targets[i] != null)
            {
                isOk = true;
                
                transforms[i] = Instantiate(rocket, new Vector3(targets[i].transform.position.x, targets[i].transform.position.y, player.transform.position.z - 10f), Quaternion.identity);
                transforms[i].GetComponent<RocketBehaviour>().target = targets[i].transform;
                transforms[i].transform.rotation = Quaternion.Euler(new Vector3(-180f, 90f, 180f));
            }
        }
    }
}
