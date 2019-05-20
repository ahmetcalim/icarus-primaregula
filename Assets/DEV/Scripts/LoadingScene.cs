using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    public Image loadingBar;
    AsyncOperation asyncOperation;
    // Start is called before the first frame update
    private void Awake()
    {
        asyncOperation = SceneManager.LoadSceneAsync(2);
        asyncOperation.allowSceneActivation = false;
    }
    void Start()
    {
        StartCoroutine(StartGame());
    }
    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(LoadScene());
    }
    IEnumerator LoadScene()
    {
        yield return null;

       
        Debug.Log("Pro :" + asyncOperation.progress);
        //When the load is still in progress, output the Text and progress bar
        while (!asyncOperation.isDone)
        {
            loadingBar.fillAmount = asyncOperation.progress;
            // Check if the load has finished
            if (asyncOperation.progress >= 0.9f)
            {

                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
