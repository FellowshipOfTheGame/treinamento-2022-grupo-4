using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ProjectileTurret : BaseTurret
{
    public GameObject bulletPrefab;
    [SerializeField]private float bulletSpeed;
    [SerializeField] private float bulletSpeedScaling;
    protected override void _shoot()
    {
        var bullet = Instantiate(bulletPrefab);
        
        //TODO discutir acerca da implementacao disso
        bullet.transform.position = this.transform.position;
        bullet.GetComponent<Rigidbody>().velocity = 
            bulletSpeed*(_currTarget.transform.position - this.transform.position).normalized ;
        
        //TODO Rotacao da bala pra virar pro inimigo?
        //TODO Inicializacao da bala com efeitos?
    }

    protected override void _upgrade()
    {
        if(current_level < max_level)
        {
            _damage *= damageScaling;
            
            _rateOfFire *= rateOfFireScaling;
            _delay = new WaitForSeconds(1/_rateOfFire);
            
            _range *= rangeScaling;
            rangeTrigger.radius = _range;

            bulletSpeed *= bulletSpeedScaling;
            
            current_level++;
        }
    }

    public override void Upgrade()
    {
        //TODO logica de requisitos.
        _upgrade();
    }

}
