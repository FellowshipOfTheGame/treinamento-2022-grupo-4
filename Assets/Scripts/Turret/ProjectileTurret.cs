using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ProjectileTurret : BaseTurret
{
    protected override void _shoot()
    {
        print("Hello World");
    }

    protected override void _upgrade()
    {
        throw new System.NotImplementedException();
    }
}
