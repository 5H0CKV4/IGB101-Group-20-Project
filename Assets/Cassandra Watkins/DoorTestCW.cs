using UnityEngine;

public class DoorTestCW : MonoBehaviour
    {
    public Animation animation;
    //Start is called before the first frame update
    private void Start()
    {
        
    }

    //Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown("f"))
            GetComponent<Animation>().Play();
    }
}
