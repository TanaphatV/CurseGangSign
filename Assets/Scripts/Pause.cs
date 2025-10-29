using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    private bool _isPause = false;
    public GameObject pauseScreen;
    public Slider sensitivitySlider;
    public Slider masterVolSlider;
    public Slider bgmVolSlider;
    public Slider sfxVolSlider;

    [Header("MANUALLY ASSIGN THESE FUCK YOU")]
    public MouseLook mouseLook;
    public AudioManager audioManager;

    void Start()
    {
        pauseScreen.SetActive(false); // FUCK YOU
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _isPause = !_isPause;

            if (_isPause)
            {
                Debug.Log("Pause");
                Time.timeScale = 0.0f;
                Cursor.lockState = CursorLockMode.None;
                pauseScreen.SetActive(true);
            }
            else
            {
                Debug.Log("Un-Pause");
                Time.timeScale = 1.0f;
                Cursor.lockState = CursorLockMode.Locked;
                pauseScreen.SetActive(false);
            }   
        }
    }

    public void SetMouseSensitivity()
    {
        mouseLook.mouseSensitivity = sensitivitySlider.value;
    }

    public void ChangeMasterVol()
    {
        audioManager.AdjustMasterVolume(masterVolSlider.value);
    }

    public void ChangeBGMVol()
    {
        audioManager.AdjustBGMVolume(bgmVolSlider.value);
    }

    public void ChangeSFXVol()
    {
        audioManager.AdjustSFXVolume(sfxVolSlider.value);
    }
}
