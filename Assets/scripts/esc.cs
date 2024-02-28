using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class esc : MonoBehaviour
{
    public float MenuLoadTime;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Invoke("Load", MenuLoadTime);
        }
    }

    private void Load()
    {
        SceneManager.LoadScene(0);
    }
}