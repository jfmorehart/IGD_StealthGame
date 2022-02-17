using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorScript : MonoBehaviour
{
    private Animator animator;
    private AnimatorControllerParameter isUnlocked;
    private int unlockedID;
    private bool opened;
    private bool playertouching;

    private void Start()
    {
        animator = GetComponent<Animator>();
        isUnlocked = animator.GetParameter(0);
        unlockedID = isUnlocked.nameHash;
        animator.SetBool(unlockedID, true);
        opened = true;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playertouching)
        {
            if (opened)
            {
                opened = false;
                animator.SetBool(unlockedID, false);
            }
            else if (!opened)
            {
                opened = true;
                animator.SetBool(unlockedID, true);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playertouching = true;

        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playertouching = false;
        }
    }

}
