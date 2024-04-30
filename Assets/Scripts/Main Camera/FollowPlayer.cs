using UnityEngine;

/*
 
 this is a class that we can give to the Camera so that it follows the player, instead of the
frame being static. The camera will move exactly however much the player is moved. The offset 
adjusts the camera fromo the players position. Without offset the camera does weird things like falling forever
 
 */
public class FollowPlayer : MonoBehaviour
{
    public Transform player;                        // Drag the gameObject you want to follow into this field in the Inspector
    public Vector3 offset = new Vector3(0, 1, -3);  // Adjust this offset as needed

    // Update is called once per frame
    void Update () {
        if (player != null) {
            transform.position = player.position + offset;
        }
    }
}
