using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerEndTime : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        FindObjectOfType<AudioManager>().Play("complete");
    }
}