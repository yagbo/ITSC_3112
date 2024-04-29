using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public LayerMask solidObjectsLayer;
    public LayerMask GrassLayer;
    public float moveSpeed;

    public event Action onEncountered;


    private Vector2 input;
    private bool isMoving;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    
    // Update is called once per frame
    // we changed this to HandleUpdate so that we can manually call the function instead of it being automatically called every frame
    public void HandleUpdate()
    {
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

                if (IsWalkable(targetPos) == true)
                {
                    StartCoroutine(Move(targetPos));
                }
            }
        }
        animator.SetBool("isMoving", isMoving);
    }

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

