﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.EventSystems;

public class CreditsScreenController : MonoBehaviour {

    #region Fields

    [SerializeField] private GameObject title;
    [SerializeField] private List<GameObject> creditsUFOs;

    private Text[] creditsText;
    private List<GameObject> objects = new List<GameObject>();

    #endregion

    #region Events

    public delegate void CreditsEventHandler ();
    public static event CreditsEventHandler CreditsEvent = delegate {};

    #endregion

    #region Mono Behaviour

    void Awake () {
        objects.Add(title);
        creditsText = GetComponentsInChildren<Text>();
        creditsUFOs.ForEach(x => objects.Add(x));
        for (int i = 0; i < creditsText.Length; i++)
            objects.Add(creditsText[i].gameObject);
    }

    void OnEnable () {
        CreditsEvent.Invoke();
        StartCoroutine(ScrollingTextRoutine());
    }

    void Update () {
        if (objects.Last().transform.position.y >= -4)
            SceneManager.LoadScene((int) GameScene.GameScene);
    }

    #endregion

    #region Private Behaviour

    private IEnumerator ScrollingTextRoutine () {
        yield return new WaitForSeconds(0.9f);
        while (gameObject.activeSelf) {
            objects.ForEach(x => x.transform.position = new Vector2(x.transform.position.x, x.transform.position.y + 0.01f));
            yield return null;
        }
    }

    #endregion


}
