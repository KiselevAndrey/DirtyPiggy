using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtySplash : Unit
{
    public void GetDirty()
    {
        for (int i = 0; i < Cell.Units.Count; i++)
        {
            if(Cell.Units[i] != this)
                Cell.Units[i].BecomeDirty();
        }
    }
}
