using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartTransition : MonoBehaviour {

    public GameObject anyKeyUI;
    public GameObject mainTitleUI;
    public GameObject mainOptionsUI;
    public GameObject aboutUI;
    public Button backButton;
    public Button aboutButton;
    public Button playButton;
    public Button exitButton;

	bool onStart;

	// Use this for initialization
	void Start () {
        onStart = true;
        backButton.onClick.AddListener(BackToMainTask);
        aboutButton.onClick.AddListener(ShowAboutTask);
        playButton.onClick.AddListener(StartGameTask);
        exitButton.onClick.AddListener(ExitGameTask);

        //Start Screen UI
        mainTitleUI.active = true;
        anyKeyUI.active = true;

        //Menu Screen UI
        mainOptionsUI.active = false;

        //About Screen Deactivate
        aboutUI.active = false;
	}
	
	void Update () {
        if (onStart == true & Input.anyKey) {
            onStart = false;

            //Start Screen Deactivate
            anyKeyUI.active = false;

            //Menu Screen Activate
            mainOptionsUI.active = true;
        }
	}

    void BackToMainTask() {
        //About Screen Deactivate
        aboutUI.active = false;

        //Menu Screen Activate
        mainOptionsUI.active = true;
        mainTitleUI.active = true;
    }

    void ShowAboutTask() {
        //About Screen Activate
        aboutUI.active = true;

        //Menu Screen Dectivate
        mainOptionsUI.active = false;
        mainTitleUI.active = false;
    }

    void StartGameTask() {
        SceneManager.LoadScene("offline");
    }

    void ExitGameTask() {
        Application.Quit();
    }
}
