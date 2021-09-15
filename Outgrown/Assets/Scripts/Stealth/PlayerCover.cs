using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCover : MonoBehaviour
{
    private bool inCover = false;

    public void SetInCover (bool val)
    {
        inCover = val;
    }

    public bool InCover() => inCover;

}
