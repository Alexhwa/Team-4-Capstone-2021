using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCover : MonoBehaviour
{
    private int inCover = 0;

    public void SetInCover (bool val)
    {
        if (val)
            inCover++;
        else
            inCover--;
    }

    public bool InCover()
    {
        if (inCover > 0)
            return true;
        else
            return false;
    }

}
