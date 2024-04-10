using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundHazard : MonoBehaviour
{
    float timer = 0;
    // set this up in the inspector!
    public float damageTime = 2f;
    public float damageAmount = 4f;
    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<PlayerStats>().TakeDamage(2.0f * timeOn);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        timeOn = 0f;
    }*/

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer.Equals(0))
        {
            /*Debug.Log(timer);
            if (timer >= damageTime)
            {
                timer -= damageTime;
                collision.gameObject.GetComponent<PlayerStats>().TakeDamage(damageAmount);
            }
            timer += Time.deltaTime;*/

            InvokeRepeating("DamagePlayer", damageTime, damageTime);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        CancelInvoke();
    }

    void DamagePlayer()
    {
        FindObjectOfType<PlayerStats>().TakeDamage(damageAmount);
    }
}
