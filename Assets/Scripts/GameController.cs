using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is a variable that can either be a 0 or a 1. 
// 0 represents FreeRoam, 1 represents Battle
// We can use this to change between World and Battle Camera
public enum GameState { FreeRoam, Battle}

/*
 
    This class is used to control if we see the Battle Screen or the World Screen
    we do this by changing the GameState enum from 0 to 1 whenever we enter battle
    and vise versa whenever we leave battle (when a pokemon faints)
 
 */

public class GameController : MonoBehaviour
{

    [SerializeField] PlayerController playerController; // variable that lets us make the player stop walking when we are in battle
    [SerializeField] BattleSystem battleSystem;         // variable that will let us turn on and off battle camera
    [SerializeField] Camera worldCamera;                // variable that will let us turn on and off world camera

    // GameState instance so we can change the GameState within class methods
    GameState state;


    // If the player walks on grass and encounters a pokemon we call the startBattle
    // if the battle ends we call the endBattle
    private void Start()
    {
        playerController.onEncountered += StartBattle;
        battleSystem.OnBattleOver += EndBattle;
    }

    // We change the GameState to Battle, deactivate the world camera and activate the battle camera
    void StartBattle()
    {
        state = GameState.Battle;                   // set GameState to battle
        battleSystem.gameObject.SetActive(true);    // turn on battle camera
        worldCamera.gameObject.SetActive(false);    // turn off world camera

        battleSystem.StartBattle();     // call the method from the battleSystem class that initializes all information for the battle
    }
    
    // method to end the battle
    void EndBattle(bool won)
    {
        state = GameState.FreeRoam;                 // set GameState to FreeRoam
        battleSystem.gameObject.SetActive(false);   // turn off battle camera
        worldCamera.gameObject.SetActive(true);     // turn on world camera
    }

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
