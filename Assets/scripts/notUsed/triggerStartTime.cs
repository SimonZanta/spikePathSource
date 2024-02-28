using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerStartTime : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject.Find("player").SendMessage("start");
    }
}
