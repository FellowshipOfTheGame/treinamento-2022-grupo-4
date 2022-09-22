using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class CommonBullet : MonoBehaviour
{
    public float damage;
    public void Awake()
    {
        Destroy(this.gameObject,10f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            //Damage
        }
    }
}
