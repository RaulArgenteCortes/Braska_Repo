using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Cinematicas : MonoBehaviour
{
    public VideoPlayer vid;
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (vid.isPlaying)
            {
                vid.Pause();
            }
            else
            {
                vid.Play();
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (vid.isPlaying)
            {
                vid.Stop();
            }
            SceneManager.LoadScene("SCN_MainMenu");
        }
    }
}
