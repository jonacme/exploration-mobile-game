using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move 
{
    public ActionNSpells ActionNSpells { get; set; }     // this is for the player

    public int Haste { get; set; }

    public Move(ActionNSpells pActionNSpells)
    {
        ActionNSpells = pActionNSpells;
        Haste = pActionNSpells.Haste;
    }

}
