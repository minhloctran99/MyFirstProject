using Unity.VisualScripting;
using UnityEngine;

public class FollowThing : MonoBehaviour
{
    [SerializeField] GameObject followthing;
    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = followthing.transform.position + new Vector3(0, 0, -20);        
    }
}
