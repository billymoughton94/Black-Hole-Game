using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title_Controller : MonoBehaviour {

    [SerializeField] 
    private GameObject mainPanel;
    [SerializeField] 
    private GameObject instructionPanel;
    [SerializeField] 
    private GameObject introPanel;

    private float readingTime = 5.0f;
    private bool waited;

    void Start() {
        waited = false;
    }
    

    public void startPressed() {
        mainPanel.SetActive(false);
        introPanel.SetActive(true);
        StartCoroutine("LoadSceneAsync");
    }

    public void instructionsPressed() {
        mainPanel.SetActive(false);
        instructionPanel.SetActive(true);
    }

    public void backPressed() {
        mainPanel.SetActive(true);
        instructionPanel.SetActive(false);
    }
    
    IEnumerator LoadSceneAsync() {
        yield return new WaitForSeconds(3);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("SampleScene");
        while (!asyncLoad.isDone) {
            yield return null;
        }
    }

}
