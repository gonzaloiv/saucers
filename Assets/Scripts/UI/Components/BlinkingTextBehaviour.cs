﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkingTextBehaviour : MonoBehaviour {

    #region Private Behaviour

    [SerializeField] private float animationTime = 0.3f;

    private Text pauseScreenLabel;
    private IEnumerator blinkingRoutine;

    #endregion

    #region Mono Behaviour

    void Awake () {
        pauseScreenLabel = GetComponent<Text>();
        blinkingRoutine = BlinkingRoutine();
    }

    void OnEnable () {
        Play();
    }

    void OnDisable () {
        Stop();
    }

    #endregion

    #region Public Behaviour

    public void Play () {
        StartCoroutine(blinkingRoutine);
    }

    public void Stop () {
        StopCoroutine(blinkingRoutine);
    }

    #endregion

    #region Private Behaviour

    private IEnumerator BlinkingRoutine () {
        float timeToWait;
        while (gameObject.activeInHierarchy) {
            pauseScreenLabel.enabled = true;
            timeToWait = Time.realtimeSinceStartup + animationTime;
            while (Time.realtimeSinceStartup < timeToWait)
                yield return 0;
            pauseScreenLabel.enabled = false;
            timeToWait = Time.realtimeSinceStartup + animationTime;
            while (Time.realtimeSinceStartup < timeToWait)
                yield return 0;
        }
    }

    #endregion

}
