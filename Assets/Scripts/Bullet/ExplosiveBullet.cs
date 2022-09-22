using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class ExplosiveBullet : MonoBehaviour
{
    public float damage,explosionRange;
   
    
    public BoxCollider bxColl;
    public SphereCollider spColl;

    public void Awake()
    { 
        spColl.radius = explosionRange/transform.lossyScale.x; 
        Destroy(this.gameObject,10f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (bxColl.enabled)
            {
                bxColl.enabled = false;
                spColl.enabled = true;
                Destroy(this.gameObject,0.12f);
            }
            else
            {
                print("BOOOM");
                //dar dano
            }
        }
    }

}
