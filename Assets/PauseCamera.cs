using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<SimplePauseScript>() != null && GetComponent<SimplePauseScript>().isPaused)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
