using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GotchaZone : MonoBehaviour
{
    public GameObject eye;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector3 delta = collision.gameObject.transform.position - eye.transform.position;


            if (Physics.Raycast(eye.transform.position + delta.normalized, delta, out RaycastHit hit, delta.magnitude))
            {
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    hit.collider.gameObject.GetComponent<PlayerController>().SeeYa(eye);
                }
            }

        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector3 delta = collision.gameObject.transform.position - eye.transform.position;

            //Returns true even when out of camera's viewangle, fix in surveil
            if (Physics.Raycast(eye.transform.position + delta.normalized, delta, out RaycastHit hit, delta.magnitude))
            {
                
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    eye.GetComponent<SurveillanceCam>().Spotted(hit.collider.gameObject);
                }
            }

        }
    }
}


