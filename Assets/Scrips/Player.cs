using JetBrains.Annotations;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int level = 10;
    public string gender = "Male";
   
    void Start()
    {
        
        Debug.Log("Player started");
        Debug.Log("Player Gender:" + gender);
        Debug.Log("Player Level:" + level);
    }
    
    void Update()
    {
        
    }
}
