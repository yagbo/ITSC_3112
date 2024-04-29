using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 
This class uses a GameState enum to switch between FreeRoam and Battle mode
we use this to tell the playerController and battleSystem when it is time 
to start/end a battle, when we do this we turn on/off the battle camera and 
world camera.
 
 */

// GameState enum that switches between FreeRoam and Battle
public enum GameState { FreeRoam, Battle}


public class GameController : MonoBehaviour
{
    // properties that help us control when the cameras turn on and off
    [SerializeField] PlayerController playerController;
    [SerializeField] BattleSystem battleSystem;
    [SerializeField] Camera worldCamera;

    GameState state;

    // when the player encounters a wild pokemon we start battle
    // when the battle ends we end the battle
    private void Start()
    {
        playerController.onEncountered += StartBattle;
        battleSystem.OnBattleOver += EndBattle;
    }

    // when we start a battle we change the GameState, turn off world cam and turn on battle cam
    void StartBattle()
    {
        state = GameState.Battle;
        battleSystem.gameObject.SetActive(true);
        worldCamera.gameObject.SetActive(false);

        battleSystem.StartBattle();                 // makes a new battle every encounter
    }


    // when we end a battle we change the GameState to FreeRoam,
    // turn off the battle camera and turn on the world camera
    void EndBattle(bool won)
    {
        state = GameState.FreeRoam;
        battleSystem.gameObject.SetActive(false);
        worldCamera.gameObject.SetActive(true);
    }

    // if the GameState is FreeRoam then we Update the PlayerController
    // if the GameState is Battle then we update the battleSystem
    private void Update()
    {
        if (state == GameState.FreeRoam)
        {
            playerController.HandleUpdate();
        }
        else if (state == GameState.Battle) 
        { 
            battleSystem.HandleUpdate();
        }
    }
}
