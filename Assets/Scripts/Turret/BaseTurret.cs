using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseTurret : MonoBehaviour
{
    [SerializeField]protected float _damage;
    [SerializeField]protected float _range;

    [SerializeField]protected float _rateOfFire;
    protected WaitForSeconds _delay;
   
    //talvez repensar
    protected Vector3 _targetPosition;
    protected bool _loaded;

    protected List<GameObject> _targets;
    [SerializeField]protected GameObject _currTarget;

    #region Base Behaviour
        private void Start()
        {
            _loaded = true;
            _rateOfFire = 1.5f;
            _delay = new WaitForSeconds(1 / _rateOfFire);
            
            _currTarget = null; 
            _targets = new List<GameObject>();
        }

        private void Update()
        {
            if (!_currTarget)
            {
                if (_targets.Count > 0)
                {
                    _currTarget = _targets[0];
                    _targets.RemoveAt(0);
                }
            }
            else if(_loaded)
                Shoot();
        }
        
        private void OnTriggerEnter(Collider other) {
            GameObject otherGameObject = other.gameObject;
            if (otherGameObject.CompareTag("Enemy"))
                _targets.Add(otherGameObject);
        }
        
        private void OnTriggerExit(Collider other){
            GameObject otherGameObject = other.gameObject;
            if (otherGameObject.CompareTag("Enemy"))
            {
                if (otherGameObject == _currTarget)
                    _currTarget = null;
                else
                    _targets.Remove(otherGameObject);
            }
        }
    #endregion
    
    #region Turret Specific Behaviour
        protected IEnumerator Reload()
        {
            yield return _delay;
            _loaded = true;
        }
        
        private void Shoot() {
            _loaded = false;
            _shoot();
            StartCoroutine(Reload());
        }
        
        protected abstract void _shoot();
        protected abstract void _upgrade();
    #endregion
   
    #region Debug Stuff
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            if(_targets != null)
                foreach (var target in _targets)
                    if(target) Gizmos.DrawLine(this.transform.position,target.transform.position);

            if (_currTarget)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(this.transform.position,_currTarget.transform.position);
            }
        }
    #endregion 
}
