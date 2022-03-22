using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmpScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.eulerAngles = new Vector3(Random.Range(0, 45), Random.Range(0, 45), Random.Range(0,90));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.transform.root.TryGetComponent(out SurveillanceCam eyescript) && other.gameObject.layer != 2)
        {
            eyescript.Pop();
        }
    }
}
