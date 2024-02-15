using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PointClick_Controlador : MonoBehaviour
{
    Camera cam;

    [Range(100f, 250f)]
    public float rayLongitud;

    private void Awake()
    {
        cam = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            bool resultado = Physics.Raycast (ray, out hit, rayLongitud);
            if (resultado)
            {
                Debug.DrawRay (ray.origin, ray.direction * hit.distance, Color.red);
            }
            else
            {
                Debug.DrawRay(ray.origin, ray.direction * rayLongitud, Color.blue);
            }
        }
        
    }
}
