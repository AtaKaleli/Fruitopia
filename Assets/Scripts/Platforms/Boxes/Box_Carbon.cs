using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box_Carbon : Box
{
    protected override void Start()
    {
        base.Start();
        hitPoint = 3;
    }
}
