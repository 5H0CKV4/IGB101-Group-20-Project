using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTest : MonoBehaviour
{
    Animation doorAnimation;

    // Start is called before the first frame update
    void Start()
    {
        doorAnimation = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("f"))
            doorAnimation.Play();
    }
}
