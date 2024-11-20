using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (FindObjectOfType<RadioCountManager>().IsTimerEnded())
        {
            Debug.Log("You win");
        }
    }
}
