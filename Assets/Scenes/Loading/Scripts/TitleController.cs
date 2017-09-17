﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour {

    #region Mono Behaviour

    void Awake() {
        Screen.orientation = ScreenOrientation.Portrait;
    }

    #endregion

    #region Public Behaviour

    public void LoadNextScene () {
        Debug.Log("Tutorial: " + DataManager.GetIsTutorialPlayed());
        int nextSceneIndex = DataManager.GetIsTutorialPlayed() == true ? (int) GameScene.MainMenuScene : (int) GameScene.TutorialScene;
        SceneManager.LoadScene(nextSceneIndex);
    }

    #endregion

}
