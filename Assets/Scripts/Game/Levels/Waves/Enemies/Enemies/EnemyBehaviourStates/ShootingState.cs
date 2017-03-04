﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyBehaviourStates {

  public class ShootingState : BaseState {
    
    #region Fields

    Vector2 shootingPosition;

    #endregion
    
    #region State Behaviour

    public override void Enter() {
      base.Enter();
      shootingPosition = BoardManager.GetRandomEnemyShotPosition();
      EventManager.TriggerEvent(new EnemyAttackEvent(enemyController.Enemy.EnemyType, shootingPosition));
      if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Disable"))
       StartCoroutine(ShootingRoutine());
    }

    public override void Exit() {
      base.Exit();
    }

    public override void Play() {
      transform.position = Vector2.Lerp(transform.position, shootingPosition, Config.ENEMY_MAX_SPEED * Time.timeScale);
    }

    protected override void AddListeners() {
      EventManager.StartListening<GestureInput>(OnGestureInput);
    }

    protected override void RemoveListeners() {
      EventManager.StopListening<GestureInput>(OnGestureInput);
    }

    #endregion

    #region Event Behaviour

    void OnGestureInput(GestureInput gestureInput) {

      if (gestureInput.Score < Config.GESTURE_MIN_SCORE)       // Low score
        EventManager.TriggerEvent(new WrongGestureInput(gestureInput));

      if ((int) gestureInput.Type != (int) enemyController.Enemy.EnemyType) { // Wrong gesture
        EventManager.TriggerEvent(new WrongGestureInput(gestureInput));

      } else { // Hit
        EventManager.TriggerEvent(new RightGestureInput(gestureInput)); 
        RemoveListeners();
        enemyController.DisableRoutine();
      }

    }

    #endregion

    #region Private Behaviour

    private IEnumerator ShootingRoutine() {
      yield return new WaitForSeconds(0.2f);
      animator.Play("Shooting");
      yield return new WaitForSeconds(1f);
      transform.rotation = QuaternionToPlayer();
      GetComponent<SpriteRenderer>().flipY = true;
      laser.Play();
      EventManager.TriggerEvent(new EnemyShotEvent(transform.position));
      yield return new WaitForSeconds(0.8f);
      GetComponent<SpriteRenderer>().flipY = false;
    }

    private Quaternion QuaternionToPlayer() {

      Quaternion quaternion = Quaternion.identity;
      Vector2 moveDirection = (Vector2) transform.position - (Vector2) player.transform.position; 

      if (moveDirection != Vector2.zero) {
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        quaternion = Quaternion.AngleAxis(angle + 90, Vector3.forward);
      }

      return quaternion;

    }

    #endregion

  }

}