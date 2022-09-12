using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    
    Vector2 movement;
    Vector2 direction;
    
    KeyCode placeTurretKey = KeyCode.X;
    KeyCode takeTurretKey = KeyCode.Z;

    public Turret _turret;

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
        if (movement != Vector2.zero) 
        {
            direction = movement.normalized;
        }
    }

    void PlaceTurret(Turret turret) 
    {
        var turretPosition = rb.position + direction * 2f;
        Object.Instantiate(turret, turretPosition, Quaternion.identity);
    }

    void TakeTurret() 
    {
        var centerPosition = rb.position + direction * 2f;
        var boxSize = new Vector2(5, 5);
        var boxAngle = Vector2.Angle(direction, new Vector2(1, 0));
        var objectsInFront = Physics2D.OverlapBoxAll(centerPosition, boxSize, boxAngle);

        foreach (var obj in objectsInFront) 
        {
            if (!obj.TryGetComponent<Turret>(out Turret turret)) continue;
            Debug.Log(turret);
            Object.Destroy(turret, 2f);
        }
    }
}
