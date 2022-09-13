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
        //TODO Adicionar logica e Os atributos que serao alterados 
        throw new System.NotImplementedException();
    }
    
}
