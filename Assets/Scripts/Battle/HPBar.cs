using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using System.Threading;
using UnityEngine;
using TMPro;

public class HPBar : MonoBehaviour
{

    [SerializeField] GameObject health;
    [SerializeField] TextMeshProUGUI hpText;


    // Start is called before the first frame update

    /*
     * 
     * this transforms the gameobject scale to x=.5, y=1
     * 
     * this line of code shows us how the transform.localScale property works
     * 
    

    void Start()
    {
        health.transform.localScale = new Vector3(1f,1f);
    }

    */

    // IMPORTANT    IMPORTANT    IMPORTANT    IMPORTANT    IMPORTANT    IMPORTANT    IMPORTANT    IMPORTANT    IMPORTANT    

    //  I STILL DON'T KNOW HOW THE HPNORMALIZED VARIALBE IS GRABBED, UNDERSTAND THIS BEFORE THE PRESENTATION IS DUE

    // HPNormalized is a decimal between 0-1. when it is passed in the battlehud class 

    // IMPORTANT    IMPORTANT    IMPORTANT    IMPORTANT    IMPORTANT    IMPORTANT    IMPORTANT    IMPORTANT    IMPORTANT    



    // this method is the same as the one above except instead of a setting x to a static .5f, we have a dynamic float variable
    public void SetHP(float hpNormalized)
    {

        health.transform.localScale = new Vector3(hpNormalized, 1f);

    }

    // method that makes the health bar move smooth
    public IEnumerator SetHPSmooth(float newHp)
    {
        float currHp = health.transform.localScale.x;   // current health
        float changeAmt = currHp - newHp;               // change in health

        // runs until curHp-newHp is a very small value... essentially runs the while loop until currentHp is the newHP
        // each iteration ticks the currentHp a very small amount, making it smooth
        while (currHp - newHp > Mathf.Epsilon)
        {
            currHp -= changeAmt * Time.deltaTime;    // currHp is iterated down by tiny increments
            health.transform.localScale = new Vector3(currHp, 1f);    // trransform the healthbar
            yield return null;                          // stops the coroutine until the next frame
        }
        health.transform.localScale = new Vector3(newHp, 1f);    // after the while loop transform the healthbar to the new hp

    }

    // this is supposed to make setHP smooth but it doesn't work, I think it would work better
    // if it was in the BattleDialogBox class but I don't want to spend more time on this
    public IEnumerator SetHPTextSmooth(Pokemon pokemon, int damageTaken)
    {
        int newHp = pokemon.HP;        
        int maxHp = pokemon.MaxHP;
        int currHp = pokemon.HP + damageTaken;          // this takes place after pokemon took damage, so currHP is hp after the attack
        // Gradually decrease the current HP value
        while (newHp < currHp)
        {
            currHp--; // Decrement the current HP value
            hpText.text = $"{currHp}/{maxHp}"; // Update the HP text
            yield return null; // Wait for the next frame
        }

        // Ensure the final HP text matches the new HP value
        hpText.text = $"{newHp}/{maxHp}";
    }


}
