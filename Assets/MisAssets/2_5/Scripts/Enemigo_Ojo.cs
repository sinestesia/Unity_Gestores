using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo_Ojo : MonoBehaviour
{
    Transform objetivo;

    private void Awake()
    {
        objetivo = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(objetivo.position);
    }
}
