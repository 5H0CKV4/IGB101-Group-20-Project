using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public event EventHandler OnItemPickedUp;
    internal int numberOfPickUps;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            numberOfPickUps += 1;
            OnItemPickedUp?.Invoke(this, EventArgs.Empty);
        }
    }

    private void OnTriggerEnter(Collider otherObj)
    {
        if (otherObj.transform.CompareTag("PickableObject"))
        {
            numberOfPickUps += 1;
            OnItemPickedUp?.Invoke(this, EventArgs.Empty);
            //Debug.Log(numberOfPickUps);
            Destroy(otherObj.gameObject);
        }
    }

}
