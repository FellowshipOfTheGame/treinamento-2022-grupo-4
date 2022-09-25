using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableStorage : MonoBehaviour
{
    public int inimigosRemanescentes=0;
    public int round=0;
    public int kills=0;

    private void Start()
    {
        inimigosRemanescentes = 0;
        round = 0;
        kills = 0;
    }
    

}
