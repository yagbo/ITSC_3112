using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 
This class is to keep track of the current Move that a Pokemon knows. This is used to 
keep track of PP during battle and gameplay

 */

public class Move 
{
    public MoveBase Base { get; set; }
    public int PP { get; set; }

    public Move(MoveBase Base)
    {
        this.Base = Base;
        PP = Base.PP;
    }
}
