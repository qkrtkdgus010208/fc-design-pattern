using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E01PortalController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            E01GameManager.Instance.LoadNextLevel();
        }
    }
}
