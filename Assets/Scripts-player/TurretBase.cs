using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBase : MonoBehaviour
{
    public bool HasTurret = false;
    public Turret Turret;
    public void PlaceTurret(Turret turret)
    {
        Turret = Instantiate(turret, transform.position, Quaternion.identity, transform);
        HasTurret = true;
    }
    public Turret RemoveTurret()
    {
        Destroy(Turret.gameObject);
        HasTurret = false;
        return Turret;
    }
}
