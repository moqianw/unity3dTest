using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class uicontral : MonoBehaviour
{
    bool isload = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!isload)
            {
                StartCoroutine(loadUIScene());
                isload = true;
            }
            else
            {
                StartCoroutine(unloadUIScene());
                isload = false;
            }
        }
    }
    private IEnumerator loadUIScene()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("ui", LoadSceneMode.Additive);
        yield return operation;
    }
    private IEnumerator unloadUIScene()
    {
        AsyncOperation operation = SceneManager.UnloadSceneAsync("ui");
        yield return operation;
    }
}
