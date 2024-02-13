using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CambiaPlano : MonoBehaviour
{
    CinemachineVirtualCamera vc;

    private void Awake()
    {
        vc = transform.GetChild(0).GetComponent<CinemachineVirtualCamera>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) vc.m_Priority = 2;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) vc.m_Priority = 0;
    }
}
