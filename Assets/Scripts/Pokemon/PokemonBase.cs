using System.Collections.Generic;
using UnityEngine;

/*
 
 this is an object class that helps define pokemon.
 
 
 
 */
// allows us to create new pokemon objects within Unity menu (right click and it should appear)
[CreateAssetMenu(fileName = "Pokemon", menuName = "Pokemon/Create new Pokemon")]

public class PokemonBase : ScriptableObject
{

    // Properties that help define a Pokémon; these are entered when creating the Pokémon
    [SerializeField] string pokemonName;      // The name of the Pokémon
    [TextArea]                                  // A multiline text field for description
    [SerializeField] string description;      // A brief description of the Pokémon
    [SerializeField] public Sprite frontSprite;       // The sprite representing the front view of the Pokémon
    [SerializeField] public Sprite backSprite;        // The sprite representing the back view of the Pokémon
    [SerializeField] PokemonType type1;       // The primary type of the Pokémon
    [SerializeField] PokemonType type2;       // The secondary type of the Pokémon (if any)

    // Base stats of the Pokémon
    [SerializeField] int maxHp;                // Maximum hit points (HP)
    [SerializeField] int attack;               // Attack stat
    [SerializeField] int defense;              // Defense stat
    [SerializeField] int spAttack;             // Special attack stat       these weren't used
    [SerializeField] int spDefense;            // Special defense stat      these weren't used
    [SerializeField] int speed;                // Speed stat                these weren't used

    [SerializeField] List<LearnableMove> learnableMoves; // List of moves that the Pokémon can learn



    // constructor so that we can use the properties in other bits of code
    public string Name
    {
        get { return pokemonName; }
    }

    public string Description
    {
        get { return description; }
    }

    public Sprite FrontSprite
    {
        get { return frontSprite; }
    }

    public Sprite BackSprite
    {
        get { return backSprite; }
    }

    public PokemonType Type1
    {
        get { return type1; }
    }

    public PokemonType Type2
    {
        get { return type2; }
    }

    public int MaxHp
    {
        get { return maxHp; }
    }

    public int Attack
    {
        get { return attack; }
    }

    public int Defense
    {
        get { return defense; }
    }

    public int SpAttack
    {
        get { return spAttack; }
    }

    public int SpDefense
    {
        get { return spDefense; }
    }

    public int Speed
    {
        get { return speed; }
    }

    public List<LearnableMove> LearnableMoves
    {
        get { return learnableMoves; }
    }

}

[System.Serializable]


// the learnable move class helps us create moves and determine when they can be learned.
// Each pokemon has a list of learnableMoves
public class LearnableMove
{
    [SerializeField] public int level;          // level at which move can be learned
    [SerializeField] public MoveBase moveBase;  // moveBase of the move

}



// this shows the PokemonType, makes it much easier to define a pokemon type in unity
public enum PokemonType
{
    None,
    Normal, 
    Fire,
    Water,  
    Electric,
    Grass,
    Ice,
    Fighting,
    Poison,
    Ground,
    Flying,
    Psychic,
    Bug,
    Rock,
    Ghost,
    Dragon,
    Dark,
    Steel
}


/*
 
The TypeChart class helps us determine the TypeAdvantage of a move against a defending Pokemon. 
For example, fire moves are super effective against grass types, so they will do extra damage
             grass moves are less effective against fire types, so they will do less damage
 
 */
public class TypeChart
{
    static float[][] chart =
    {                        /*NOR, FIR,  WAT, ELE, GRA, ICE, FIG, POI, GRO, FLY, PSY, BUG, ROC, GHO, DRA, DAR, STE*/
        /* NOR */ new float[] { 1f,  1f,   1f,  1f,  1f,  1f,  1f,  1f,  1f,  1f,  1f,  1f, 0.5f, 0f,  1f,  1f, 0.5f },
        /* FIR */ new float[] { 1f, 0.5f, 0.5f, 1f,  2f,  2f,  1f,  1f,  1f,  1f,  1f,  2f, 0.5f, 1f, 0.5f, 1f,  2f },
        /* WAT */ new float[] { 1f,  2f,  0.5f, 1f, 0.5f, 1f,  1f,  1f,  2f,  1f,  1f,  1f,  2f,  1f, 0.5f, 1f,  1f },
        /* ELE */ new float[] { 1f,  1f,   2f, 0.5f,0.5f, 1f,  1f,  1f,  0f,  2f,  1f,  1f,  1f,  1f, 0.5f, 1f,  1f },
        /* GRA */ new float[] { 1f, 0.5f,  2f,  1f, 0.5f, 1f,  1f, 0.5f, 2f, 0.5f, 1f, 0.5f, 2f,  1f, 0.5f, 1f, 0.5f },
        /* ICE */ new float[] { 1f, 0.5f, 0.5f, 1f,  2f, 0.5f, 1f,  1f,  2f,  2f,  1f,  1f,  1f,  1f,  2f,  1f, 0.5f },
        /* FIG */ new float[] { 2f,  1f,   1f,  1f,  1f,  2f,  1f, 0.5f, 1f, 0.5f, 0.5f, 0.5f, 2f, 0f, 1f,  2f,  2f },
        /* POI */ new float[] { 1f,  1f,   1f,  1f,  2f,  1f,  1f, 0.5f, 0.5f, 1f, 1f,  1f, 0.5f, 0.5f, 1f, 1f,  0f },
        /* GRO */ new float[] { 1f,  2f,   1f,  2f, 0.5f, 1f,  1f,  2f,  1f,  0f,  1f, 0.5f, 2f,  1f,  1f,  1f,  2f },
        /* FLY */ new float[] { 1f,  1f,   1f, 0.5f, 2f,  1f,  2f,  1f,  1f,  1f,  1f,  2f, 0.5f, 1f,  1f,  1f, 0.5f },
        /* PSY */ new float[] { 1f,  1f,   1f,  1f,  1f,  1f,  2f,  2f,  1f,  1f, 0.5f, 1f,  1f,  1f,  1f,  0f, 0.5f },
        /* BUG */ new float[] { 1f, 0.5f,  1f,  1f,  2f, 1f, 0.5f, 0.5f, 1f, 0.5f, 2f,  1f,  1f, 0.5f, 1f,  2f, 0.5f },
        /* ROC */ new float[] { 1f,  2f,   1f,  1f,  1f,  2f, 0.5f, 1f, 0.5f, 2f,  1f,  2f,  1f,  1f,  1f,  1f, 0.5f },
        /* GHO */ new float[] { 0f,  1f,   1f,  1f,  1f,  1f,  1f,  1f,  1f,  1f,  2f,  1f,  1f,  2f, 1f, 0.5f,  1f },
        /* DRA */ new float[] { 1f,  1f,   1f,  1f,  1f,  1f,  1f,  1f,  1f,  1f,  1f,  1f,  1f,  1f,  2f,  1f,  0.5f},
        /* DAR */ new float[] { 1f,  1f,   1f,  1f,  1f,  1f, 0.5f, 1f,  1f,  1f,  2f,  1f,  1f,  2f, 1f, 0.5f, 0.5f },
        /* STE */ new float[] { 1f, 0.5f, 0.5f, 0.5f, 1f, 2f,  1f,  1f,  1f,  1f,  1f,  1f,  2f,  1f,  1f,  1f,  0.5f }
    };

    // method that takes type of the Attacking move and the defending pokemon and returns the corresponding number from the chart above
    public static float GetEffectiveness(PokemonType attackType, PokemonType defenseType)
    {
        if (attackType == PokemonType.None || defenseType == PokemonType.None)      // if a pokemons secondary type is None, then we return 1
        {                                                // when we call the method we will multiply GetEffectiveness(attack, defenseType1) * GetEffectiveness(attack, defenseType2) 
            return 1;                                    // this if statement makes it so that a Pokemon with only 1 type will still recieve the correct Type Multiplier
        }
        int row = (int)attackType - 1;          // we decrement 1 because None is in position 0 but the chart doesn't account for None
        int col = (int)defenseType - 1;         // we decrement 1 because None is in position 0 but the chart doesn't account for None

        return chart[row][col];                 // return the type advantage multiplier
    }
}

