using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool gameHasEnded = false;
    public float FallAreaRestartTime;
    public float SpikeRestartTime;

    public float MenuLoadTime;

    public void FallArea()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            Invoke("Restart", FallAreaRestartTime);
        }
    }

    public void Spikes()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            Invoke("Restart", SpikeRestartTime);
        }
    }

    private void Update()
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

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}