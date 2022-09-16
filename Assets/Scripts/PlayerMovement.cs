using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody rb;
    
    Vector3 movement;
    Vector3 direction;
    
    KeyCode placeTurretKey = KeyCode.X;
    KeyCode takeTurretKey = KeyCode.Z;

    public Turret _turret;

    public GameSystem gameSystem;

    // Update is called once per frame
    void Update()
    {
        // input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(placeTurretKey)) 
        {
            PlaceTurret(_turret);        
        }
        if (Input.GetKeyDown(takeTurretKey))
        {
            TakeTurret();
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

    void PlaceTurret(Turret turret) 
    {
        var turretPosition = rb.position + direction * 2f;
        Instantiate(turret, turretPosition, Quaternion.identity);
    }

    void TakeTurret() 
    {
        var centerPosition = rb.position + direction * 2f;
        var boxSize = new Vector3(5, 5, 10);
        //var boxAngle = Vector3.Angle(direction, new Vector3(1, 0, 0));
        var objectsInFront = Physics.OverlapBox(centerPosition, boxSize);

        foreach (var obj in objectsInFront) 
        {
            if (!obj.TryGetComponent(out Turret turret)) continue;
            Destroy(turret.gameObject);
        }
    }
}
