using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class __Turret : MonoBehaviour
{
    public int Cost => 1;
    public int Weight => 5;

    public int Type;
    public override int GetHashCode()
    {
        return $"Base Turret - {Type} - Raw".GetHashCode();
    }

    public override bool Equals(object other)
    {
        return other.GetHashCode() == this.GetHashCode();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
