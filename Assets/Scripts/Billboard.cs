using UnityEngine;

public class Billboard : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        var _dir = Camera.main.transform.position - transform.position;
        transform.rotation = Quaternion.Euler(0,Quaternion.LookRotation(_dir).eulerAngles.y,0);
    }
}
