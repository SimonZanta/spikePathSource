using UnityEngine;

public class fallArea : MonoBehaviour
{
    private PlayerScript playerScr;

    private disable disable;

    private void Start()
    {
        playerScr = GetComponent<PlayerScript>();

        disable = GetComponent<disable>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "fallArea")
        {
            FindObjectOfType<GameManager>().FallArea();
        }

        if (collision.gameObject.tag == "spikes")
        {
            FindObjectOfType<GameManager>().Spikes();
            playerScr.enabled = false;
            FindObjectOfType<PlayerScript>().Death();
            playerScr.jumpForce = 0;
            playerScr.speed = 0;
        }

        if (collision.gameObject.tag == "chodba-disabled")
        {
            FindObjectOfType<disable>().disabling();
        }
        if (collision.gameObject.tag == "chodba-enabled")
        {
            FindObjectOfType<disable>().enabling();
        }
    }
}