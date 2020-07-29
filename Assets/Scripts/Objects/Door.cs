using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public void OpenDoor()
    {
        this.transform.gameObject.SetActive(false);
    }

    public void CloseDoor()
    {
        this.transform.gameObject.SetActive(true);
    }
}
