using System.Collections.Generic;
using UnityEngine;

/*
# Pokemon Class Documentation


The `Pokemon` class represents a Pokémon entity within a game. This is the pokemon object
that is created when we create a pokemon in unity. We can add them by right clicking,
and creating a Pokemon game object, then we will add all the required information. 
This object is also used to keep track of the HP and PP of the moves it has learned.

*/


public class Pokemon
{
    public PokemonBase Base { get; set; }           // PokemonBase
    public int Level { get; set; }                  // level

    public int HP {  get; set; }                    // currentHP
    public List<Move> Moves { get; set; }           // list of moves 
    public int Damage { get; set; }                 // damage done
    
    // Pokemon Constructor
    public Pokemon(PokemonBase pbase, int plevel)
    {
        Base = pbase;  
        Level = plevel;
        HP = MaxHP;
        
        // teach Pokemon Moves and store in a List of Moves
        Moves = new List<Move>();

        // when we create a pokemon we are allowed to pre-enter the moves it will learn at a certain Level.
        // when the game loads, the pokemon will load in with the moves it's allowed to learn.
        foreach (var move in Base.LearnableMoves)
        {
            if (move.level <= Level)
            {
                Moves.Add(new Move(move.moveBase));

                if (Moves.Count >= 4)
                {
                    break;
                }
            }
        }
    }


    // constructor, this is so that we can get the stats of a pokemon during the game.
    public int Attack
    {
        get { return Mathf.FloorToInt((Base.Attack * Level) / 100f) + 5; }
    }

    public int Defense
    {
        get { return Mathf.FloorToInt((Base.Defense * Level) / 100f) + 5; }
    }

    public int SpAttack
    {
        get { return Mathf.FloorToInt((Base.SpAttack * Level) / 100f) + 5; }
    }

    public int SpDefense
    {
        get { return Mathf.FloorToInt((Base.SpDefense * Level) / 100f) + 5; }
    }

    public int Speed
    {
        get { return Mathf.FloorToInt((Base.Speed * Level) / 100f) + 10; }
    }

    public int MaxHP
    {
        get { return Mathf.FloorToInt((Base.MaxHp * Level) / 100f) + 10; }
    }

    // method that calculates how much damage an attack will do to a defending pokemon
    public DamageDetails TakeDamage(Move move, Pokemon attacker)
    {
        float critical = 1f;
        if (UnityEngine.Random.Range(0,100) <= 25)
        {
            critical = 2f;
        }

        float type = TypeChart.GetEffectiveness(move.Base.Type, this.Base.Type1) * TypeChart.GetEffectiveness(move.Base.Type, this.Base.Type2);

        var damageDetails = new DamageDetails()
        {
            TypeEffectiveness = type,
            Critical = critical,
            Fainted = false
        };

        float attack = (move.Base.IsSpecial) ? attacker.SpAttack : attacker.Attack;
        float defense = (move.Base.IsSpecial) ? SpDefense : Defense;
        float modifiers = UnityEngine.Random.Range(.85f, 1f) * type * critical;
        float a = (2 * attacker.Level + 10) / 250f;
        float d = a * move.Base.Power * ((float)attack / defense) /*+ 2*/;
        int damage = Mathf.FloorToInt(d * modifiers);
        Damage = damage;
        HP -= damage;

        if (HP <= 0)
        {
            HP = 0;
            damageDetails.Fainted = true;
        }

        return damageDetails;
    }


    // selects a random move for the enemy pokemon
    public Move GetRandomMove()
    {
        int r = UnityEngine.Random.Range(0, Moves.Count);
        return Moves[r];
    }
}

// class that keeps track of if a pokemon fainted, if the move was a critical hit and if it was super effective or not
public class DamageDetails
{
    public bool Fainted { get; set; }
    public float Critical { get; set; }
    public float TypeEffectiveness { get; set; }
}
