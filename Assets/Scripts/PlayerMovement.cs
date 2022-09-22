using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody rb;
    
    Vector3 movement;
    Vector3 direction;
    
    KeyCode placeTurretKey = KeyCode.X;
    KeyCode takeTurretKey = KeyCode.Z;
    KeyCode moveSelector = KeyCode.Tab;
    KeyCode buyTurret = KeyCode.Q;

    [SerializeField]
    private Inventory _inventory;
    [SerializeField]
    private GameSystem _gameSystem;

    // Update is called once per frame
    void Update()
    {
        // input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(placeTurretKey)) 
        {
            PlaceTurret();
        }
        if (Input.GetKeyDown(takeTurretKey))
        {
            TakeTurret();
        }
        if (Input.GetKeyDown(moveSelector))
        {
            _gameSystem.MoveSelector();
        }
        if (Input.GetKeyDown(buyTurret))
        {
            _gameSystem.BuyTurret();
        }

    }

    void FixedUpdate()
    {
        // movement
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        if (movement != Vector3.zero) 
        {
            direction = movement.normalized;
        }
    }

    void PlaceTurret() 
    {
        var turretBase = TryGetTurretBase();
        if (turretBase == null || turretBase.HasTurret) return;

        var turret = _gameSystem.GetSelectedTurret();
        if (_inventory.TryRemoveTurret(turret))
        {
            turretBase.PlaceTurret(turret);
        }
        else
        {
            Debug.Log("No turret");
        }
    }

    void TakeTurret() 
    {
        var turretBase = TryGetTurretBase();
        if (turretBase == null || !turretBase.HasTurret) return;

        if (_inventory.TryAddTurret(turretBase.Turret))
        {
            turretBase.RemoveTurret();
        }
        else
        {
            Debug.Log("Too much turrets");
        }
        
    }

    TurretBase TryGetTurretBase()
    {
        var centerPosition = rb.position + direction * 2f;
        var boxSize = new Vector3(5, 5, 10);
        var objectsInFront = Physics.OverlapBox(centerPosition, boxSize);

        TurretBase turretBase = null;
        objectsInFront.FirstOrDefault(obj => obj.TryGetComponent<TurretBase>(out turretBase));
        return turretBase;
    }
}
