using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastCamara : MonoBehaviour
{
    #region 1) DEFINICION VARIABLES
    Transform cam;

    public float longitudRayo;
    #endregion

    #region 2) FUNCIONES PREDET. UNITY
    private void Awake()
    {
        cam = Camera.main.transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        CrearRayo();
    }
    #endregion

    #region 3) METODOS ORIGINALES
    void CrearRayo()
    {
        Ray ray = new Ray(cam.position, cam.forward);
        RaycastHit hit;

        bool result = Physics.Raycast(ray, out hit, longitudRayo);

        if (result)
        {
            Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red);
        }
        else
        {
            Debug.DrawRay(ray.origin, ray.direction * longitudRayo, Color.blue);
        }
    }
    #endregion
}