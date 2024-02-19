using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Centinela : MonoBehaviour
{
    Animator anim;
    Transform objetivo;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        objetivo = GameObject.FindWithTag("Player").transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnAnimatorIK()
    {
        anim.SetLookAtPosition(objetivo.position + Vector3.up * 1.5f);
        anim.SetLookAtWeight(1f, 0.5f, 1f);
    }
}
