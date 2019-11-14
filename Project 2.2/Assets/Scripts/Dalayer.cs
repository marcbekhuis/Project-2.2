using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Dalayer : MonoBehaviour
{
    public UnityEvent Event = new UnityEvent();

    public void StartDelay(float delay)
    {
        StartCoroutine(Delay(delay));
    }

    private IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Event.Invoke();
    }
}
