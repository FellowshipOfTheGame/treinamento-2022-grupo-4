using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class Turret : MonoBehaviour
{
    //Parte associada ao inventario
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
    
    
    [SerializeField]private int currentLevel = 1, maxLevel = 4;
    
    [SerializeField][Range(0f,20.0f)]private float damage;
    
    //n muda ate um upgrade
    [SerializeField] [Range(0, 20f)] float range;
    private SphereCollider _rangeTrigger;

    [SerializeField][Range(0f,20f)]private float rateOfFire;
    
    public GameObject bulletPrefab;
    public GameObject barrelEnd;
    [SerializeField] [Range(0f, 40f)] private float bulletSpeed;
    private Vector3 _targetPosition;
    private bool _loaded;

    private List<GameObject> _targets;
    [SerializeField]private GameObject currTarget;
    
    [SerializeField][Range(0f,5.0f)]private float damageScaling, rateOfFireScaling, rangeScaling,bulletSpeedScaling;
    private void Start()
    {
        _loaded = true;
        rateOfFire = 1.5f;
        
        _rangeTrigger = GetComponent<SphereCollider>();
        _rangeTrigger.radius = range;
        
        currTarget = null;
        _targets = new List<GameObject>();

    }

    private void Update()
    {
        //Procura alvo
        if (!currTarget)
        {
            if (_targets.Count > 0)
            {
                currTarget = _targets[0];
                _targets.RemoveAt(0);
            }
        }
        else
        {
            //this.transform.LookAt(currTarget.transform);
             
            if(_loaded)
                _shoot();
        }

    }
    
    private void OnTriggerEnter(Collider other) {
        GameObject otherGameObject = other.gameObject;
        if (otherGameObject.CompareTag("Enemy"))
            _targets.Add(otherGameObject);
    }
    
    private void OnTriggerExit(Collider other){
        GameObject otherGameObject = other.gameObject;
        
        if (otherGameObject.CompareTag("Enemy"))
            if (otherGameObject == currTarget)     currTarget = null;
            else                                    _targets.Remove(otherGameObject);
    }
    
    private IEnumerator _reload()
    {
        yield return new WaitForSeconds(1/rateOfFire);
        _loaded = true;
    }
        
    private void _shoot() {
        _loaded = false;
        
        //instanciamento do projetil.
        var bullet = Instantiate(bulletPrefab);
        bullet.transform.position = barrelEnd.transform.position;
        bullet.GetComponent<Rigidbody>().velocity = 
            bulletSpeed*(currTarget.transform.position - transform.position).normalized ;
        
        StartCoroutine(_reload());
    }

    private void _upgrade()
    {
        if(currentLevel < maxLevel)
        {
            damage *= damageScaling;
            
            rateOfFire *= rateOfFireScaling;
            
            range *= rangeScaling;
            _rangeTrigger.radius = range;

            bulletSpeed *= bulletSpeedScaling;
            
            currentLevel++;
        }
    }
    
    //Para ser acessada pelo jogador
    public void Upgrade()
    {
        //if(resources){
        //remove resources
        _upgrade();
        //}
    }
     
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.green;
    //    if(_targets != null)
    //        foreach (var target in _targets)
    //            if(target) Gizmos.DrawLine(this.transform.position,target.transform.position);

    //    if (currTarget)
    //    {
    //        Gizmos.color = Color.red;
    //        Gizmos.DrawLine(this.transform.position,currTarget.transform.position);
    //    }
    //}
}
