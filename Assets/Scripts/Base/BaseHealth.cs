using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class BaseHealth : MonoBehaviour
{
    [SerializeField][Range(0,50)]private int maxHealth, currHealth;
    private void OnTriggerEnter(Collider other)
    {
       if(other.gameObject.CompareTag("Enemy"))
       {
           Destroy(other.gameObject);
           currHealth--;

           if (currHealth <= 0)
           {
                print("Perdeu! :(");   
           }
       }
    }
}
