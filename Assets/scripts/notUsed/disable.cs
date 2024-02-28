using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class disable : MonoBehaviour
{
    TilemapRenderer tilemap;

    void Start()
    {
        tilemap = GetComponent<TilemapRenderer>();
    }

    public void disabling()
    {
        tilemap.enabled = false;
    }

    public void enabling()
    {
        tilemap.enabled = true;
    }
}