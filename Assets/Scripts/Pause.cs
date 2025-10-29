using UnityEngine;

public class Pause : MonoBehaviour
{
    private bool _isPause = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _isPause = !_isPause;

            if (_isPause)
            {
                Debug.Log("Pause");
                Time.timeScale = 0.0f;
            }
            else
            {
                Debug.Log("Un-Pause");
                Time.timeScale = 1.0f;
            }   
        }
    }
}
