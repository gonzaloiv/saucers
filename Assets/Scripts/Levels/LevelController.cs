﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

  #region Fields

  [SerializeField] private GameObject wavePrefab;
  private WaveController waveController;

  [SerializeField] private GameObject playerPrefab;
  private PlayerSpawner playerSpawner;

  private Level currentLevel;
  private List<GameObject> currentLevelObjects;

  #endregion

  #region Mono Behaviour

  void Awake() {
    currentLevel = Data.Level1;
    waveController = Instantiate(wavePrefab, transform).GetComponent<WaveController>();
    playerSpawner = Instantiate(playerPrefab, transform).GetComponent<PlayerSpawner>();
  }

  void OnDisable() {
    StopAllCoroutines();
  }

  #endregion

  #region Public Behaviour

  public void Level() {
    currentLevelObjects = new List<GameObject>();
    playerSpawner.SpawnPlayer();
    StartCoroutine(LevelRoutine());
  }

  #endregion

  #region Private Behaviour

  private IEnumerator LevelRoutine() {
    while (currentLevel.HasMoreWaves()) {
      waveController.Wave(currentLevel.CurrentWave()).ForEach(x => currentLevelObjects.Add(x));
      yield return new WaitForSeconds(1);
    }
  }

  #endregion
	
}
