using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Profe_VigaPinchos : MonoBehaviour
{
    public float velRotacion;
    public float sentidoRotacion;

    private void Start()
    {
        sentidoRotacion = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown (KeyCode.Space)) sentidoRotacion *= -1f;

        transform.Rotate(Vector3.up * sentidoRotacion * velRotacion * Time.deltaTime);
    }
}
