using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerCountdown : MonoBehaviour
{

    public GameObject timerDisplay; // References the player HUD/GUI //
    public int secondsLeft = 60; // Set the time to how long you want it to count down //
    public bool takingAwayTime = false; // We are checking to see if it's taking time away from the timer //


    void Start()
    {
        timerDisplay.GetComponent<Text>().text = "00:" + secondsLeft; // getting a reference to the timer Display //
    }


    void Update()
    {
        if (takingAwayTime == false && secondsLeft > 0) // if the timer is taking time off the timer //
        {
            StartCoroutine(timerCountdown());
        }
    }

    IEnumerator timerCountdown()
    {
        takingAwayTime = true;
        yield return new WaitForSeconds(1); // delay of taking time away // 
        secondsLeft -= 1; // taking away a second off the timer //
        timerDisplay.GetComponent<Text>().text = "00:" + secondsLeft; // getting a reference to the timer Display //
        takingAwayTime = false; // finish taking away a second //
    }

}
