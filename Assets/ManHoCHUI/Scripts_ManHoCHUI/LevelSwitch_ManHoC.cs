using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSwitch_ManhoC : MonoBehaviour
{

    public event EventHandler OnPlayerEnterExit;
    public string nextLevel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider otherObj)
    {
        if (otherObj.transform.CompareTag("Player"))
        {
            OnPlayerEnterExit?.Invoke(this, EventArgs.Empty);

        }
    }
}
