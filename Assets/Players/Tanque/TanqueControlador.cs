using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TanqueControlador : MonoBehaviour
{
    Vector2 ejesVirtuales;

    public float angulosRot;
    public float velMovimiento;
    public float velRotacion;

    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ejesVirtuales.x = Input.GetAxisRaw("Horizontal");
        ejesVirtuales.y = Input.GetAxisRaw("Vertical");

        if (ejesVirtuales.y != 0f)
        {

            angulosRot += ejesVirtuales.x * velRotacion * Time.deltaTime;
            transform.rotation = Quaternion.Euler(Vector3.up * angulosRot);
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + transform.forward * ejesVirtuales.y * velMovimiento * Time.fixedDeltaTime);
    }
}
