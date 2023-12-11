using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint2Script : MonoBehaviour
{

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            LabirinthState.checkPoint2Passed = true;
            GameObject.Destroy(this.gameObject);
        }
    }
}
