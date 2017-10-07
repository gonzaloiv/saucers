﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyTypeLabelSpawner : MonoBehaviour {

    #region Fields

    // Same order as EnemyType
    [SerializeField] private GameObject[] gesturePrefabs;
    private GameObjectArrayPool gesturePool;

    private List<Enemy> currentEnemies;
    private GameObject[] gestures;

    private IEnumerator showGesturesRoutine;
    private IEnumerator showGestureRoutine;
    private IEnumerator hideGesturesRoutine;

    #endregion

    #region Mono Behaviour

    void Awake () {
        gesturePool = new GameObjectArrayPool("GesturePool", gesturePrefabs, 16, transform);
    }

    #endregion

    #region Public Behaviour

    public void Init () {
        if (showGesturesRoutine != null)
            StopCoroutine(showGesturesRoutine);
        currentEnemies = new List<Enemy>();
    }

    public void AddGesture (Enemy enemy) {
        currentEnemies.Add(enemy);
    }

    public void SetGestureByIndex (int index, Enemy enemy) {
        currentEnemies[index] = enemy;
    }

    public void ShowGestures (float time) {
        showGesturesRoutine = ShowGesturesRoutine(time);
        StartCoroutine(showGesturesRoutine);
    }

    public void ShowGesture (int index, float time) {
        showGestureRoutine = ShowGestureRoutine(index, time);
        StartCoroutine(showGestureRoutine);
    }

    public void HideGestures () {
        hideGesturesRoutine = HideGesturesRoutine();      
        StartCoroutine(hideGesturesRoutine);
    }

    #endregion

    #region Private Behaviour

    private IEnumerator ShowGesturesRoutine (float time) {
        gestures = new GameObject[currentEnemies.Count];
        for (int i = 0; i < currentEnemies.Count; i++) {
            gestures[i] = gesturePool.PopObject((int) currentEnemies[i].EnemyType - 1);
            gestures[i].transform.position = currentEnemies[i].Position + new Vector2(0, -0.7f);
            gestures[i].SetActive(true);
        }
        yield return new WaitForSeconds(time);
        for (int i = 0; i < gestures.Length; i++)
            gestures[i].SetActive(false);
    }

    private IEnumerator ShowGestureRoutine (int index, float time) {
        GameObject gesture = new GameObject();
        gesture = gesturePool.PopObject((int) currentEnemies[index].EnemyType - 1);
        gesture.transform.position = currentEnemies[index].Position + new Vector2(0, -0.7f);
        gesture.SetActive(true);
        yield return new WaitForSeconds(time);
        gesture.SetActive(false);
    }

    private IEnumerator HideGesturesRoutine () {
        for (int i = 0; i < gestures.Length; i++) {
            gestures[i].SetActive(false);
            yield return new WaitForSeconds(.15f);
        }
    }

    #endregion
	
}