using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Profe_PrimeraPersonaControlador : MonoBehaviour
{
    public float velocidad;
    public Vector2 ejesVirtuales;
    Vector3 dirMovimiento;
    Vector3 dirRotacion;
    Quaternion rot;

    Transform cam;
    Transform cmFollow;

    Rigidbody rb;

    private void Awake()
    {
        cam = Camera.main.transform;
        cmFollow = transform.GetChild(1);
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ejesVirtuales.x = Input.GetAxisRaw("Horizontal");
        ejesVirtuales.y = Input.GetAxisRaw("Vertical");

        dirMovimiento = cam.right * ejesVirtuales.x + cam.forward * ejesVirtuales.y;
        dirMovimiento.y = 0f;
        dirMovimiento.Normalize();

        dirRotacion = cam.forward;
        dirRotacion.y = 0f;
        dirRotacion.Normalize();

        rot = Quaternion.LookRotation(dirRotacion);
        if (dirRotacion.magnitude != 0f)
        {
            transform.rotation = rot;
            cmFollow.forward = cam.forward;
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + dirMovimiento * velocidad * Time.deltaTime);
    }
}
