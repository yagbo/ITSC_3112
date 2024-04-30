/*
 
This class is to keep track of the current Move that a Pokemon knows. This is used to 
keep track of PP during battle and gameplay

 */

public class Move 
{
    public MoveBase Base { get; set; }  // The Base of the move, where all the information is stored
    public int PP { get; set; }         // PowerPoints, how many times a pokemon is allowed to use a certain move

    public Move(MoveBase Base)          // Move constructor that sets Base and PP to the provided Base and PP
    {
        this.Base = Base;
        PP = Base.PP;
    }
}
