using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class placeholder_Move : MonoBehaviour
{
    private Vector3 _initialPosition;
    private float t;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        _initialPosition = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = 5*(new Vector3(Mathf.Sin(t),0f,0f));
        t += Time.deltaTime;
    }
}
