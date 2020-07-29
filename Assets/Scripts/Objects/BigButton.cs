using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BigButton : MonoBehaviour
{
    [Header("Insert Functions Here:")]
    public UnityEvent onHeld;
    public UnityEvent onReleased;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        onHeld.Invoke();
        Debug.Log("Bonk");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        onReleased.Invoke();
        Debug.Log("No Bonk");
    }
}
