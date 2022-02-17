using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GotchaZone : MonoBehaviour
{
    public GameObject eye;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 delta = collision.gameObject.transform.position - eye.transform.position;

            RaycastHit2D hit = Physics2D.Raycast((Vector2)eye.transform.position + delta.normalized, delta, delta.magnitude);
            if (hit)
            {
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    hit.collider.gameObject.GetComponent<PlayerController>().SeeYa(eye);
                }
            }

        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 delta = collision.gameObject.transform.position - eye.transform.position;
            Debug.Log("player collided");
            RaycastHit2D hit = Physics2D.Raycast((Vector2)eye.transform.position, delta, delta.magnitude);
            if (hit)
            {
                if (hit.collider.gameObject.CompareTag("Player"))
                {

                    hit.collider.gameObject.GetComponent<PlayerController>().SeeYa(eye);
                }
            }

        }
    }
}
