using UnityEngine;


/*
# MoveBase Documentation

This is an object class that can be used to define moves

## Usage

1. **Create New Move:**
   - In Unity Editor, right-click in the Project window.
   - From the context menu, navigate to `Pokemon` > `Create new move`.
   - This will create a new `MoveBase` asset that can be configured with desired properties.

2. **Configuring Move Properties:**
   - Once created, select the move asset from the Project window.
   - In the Inspector window, you can edit properties such as move name, description, type, power, accuracy, and PP.

3. **Integration with Pokémon System:**
   - Use the created move assets within your Pokémon system to assign moves to Pokémon entities.
   - You can access move properties within the code for gameplay mechanics, such as dealing damage, applying status effects, etc.

*/


// allows us to create new pokemon objects within Unity menu (right click and it should appear)
[CreateAssetMenu(fileName = "Move", menuName = "Pokemon/Create new move")]

public class MoveBase : ScriptableObject
{

    // [SerializeField] is used so that it will show up as a menu option in Unity Editor
    [SerializeField] string moveName;       // name of the move

    [TextArea]                              // this is here so that we have room for description
    [SerializeField] string description;    

    [SerializeField] PokemonType type;      // the type of the move
    [SerializeField] int power;             // the power of the move
    [SerializeField] int accuracy;          // the accuracy of the move
    [SerializeField] int pp;                // power points
    



    // constructor for these properties so that we can access them

    public string Name
    {
        get { return moveName; }
    }

    public string Description
    {
        get { return description; }
    }

    public PokemonType Type
    {
        get { return type; }
    }

    public int Power
    {
        get { return power; }
    }

    public int Accuracy
    {
        get { return accuracy; }
    }

    public int PP
    {
        get { return pp; }
    }

// This is whether a move is special or physical
    public bool IsSpecial
    {
    get{
    // These types are typically used for special moves
    if (type== PokemonType.Fire|| type== PokemonType.Water ||type== PokemonType.Grass
    ||type== PokemonType.Ice||type== PokemonType.Electric||type== PokemonType.Dragon){
      return true;
    }
    else{
    return false;
    }
    }
    }
}
