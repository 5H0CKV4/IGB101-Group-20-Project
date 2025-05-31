using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDeathCheck : MonoBehaviour
{
    public event EventHandler OnEnterFallDeathArea;
    public Transform playerObj;
    public Transform spawnPoint;
    public bool playerToSpawn;



    private void OnTriggerEnter(Collider otherObj)
    {
        if (otherObj.CompareTag("Player"))
        {
            OnEnterFallDeathArea?.Invoke(this, EventArgs.Empty);
        }
    }

}
