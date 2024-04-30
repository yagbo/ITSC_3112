using System;
using System.Collections;
using UnityEngine;


/*
 
Class that controls the player sprite. depending on which direction we move it changes the sprite to the according direction
If we run into a wall it stops us and if we're walking in grass theres a chance we encounter a pokemon
 
 */

public class PlayerController : MonoBehaviour
{

    public LayerMask solidObjectsLayer;     // Layer that has walls we cannot run through
    public LayerMask GrassLayer;            // grass layer, if we walk through we may encounter a pokemon
    public float moveSpeed;                 // how fast the player moves

    public event Action onEncountered;      // event based variable that tells us if we encountered a pokemon, makes changing into the BattleSystem easy


    private Vector2 input;                  // grabs the current position of the player
    private bool isMoving;                  // if we are still moving then we start the running animation
    private Animator animator;              // calls the Animator from Unity Editor that controls what sprite goes with what direction

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    
    // Update is called once per frame
    // we changed this to HandleUpdate so that we can manually call the function instead of it being automatically called every frame
    public void HandleUpdate()
    {
    // if the player isn't currently moving, get them to move
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            // prevents diagonal movement
            if (input.x != 0) input.y = 0;

         
            if (input != Vector2.zero)
            {
                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);
                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;

               // if a tile can be walked on, allow the player to move on it
                if (IsWalkable(targetPos) == true)
                {
                    StartCoroutine(Move(targetPos));
                }
            }
        }
        animator.SetBool("isMoving", isMoving);
    }

// This is the method that moves the player to the target position
    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;

        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPos;

        isMoving = false;

        CheckForEncounters(targetPos);
    }

// This method checks if  tile can be moved on
    private bool IsWalkable(Vector3 targetPos)
    {
        // checks for solid object at target position
        // the function physics3D takes parameters (next position, radius of tiles, layer) to see if it will colide with the layer or not
        if (Physics2D.OverlapCircle(targetPos, 0.2f, solidObjectsLayer) != null)
        {
            UnityEngine.Debug.Log("you ran into a wall");

            return false;
        }
        return true;
    }

    // This method checks for random encounters
    private void CheckForEncounters(Vector3 targetPos)
    { 
        if (Physics2D.OverlapCircle(targetPos, 0.2f, GrassLayer) != null)
        {
            if(UnityEngine.Random.Range(1,101) <= 10)
            {
                animator.SetBool("isMoving", false);    // makes player stop walking when we get into a battle
                onEncountered();
                
            }   
        }
    }
}

