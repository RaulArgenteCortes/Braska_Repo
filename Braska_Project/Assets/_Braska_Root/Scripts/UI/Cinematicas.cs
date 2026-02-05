using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Cinematicas : MonoBehaviour
{
    public VideoPlayer vid;
    [SerializeField] string sceneToLoad3;
    public GameObject skip;

    void Start()
    {
        vid.loopPointReached += EndReached;
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
                skip.gameObject.SetActive(false);

                vid.Stop();
            }
            ScenesFade.Instance.FadeOutAndLoad(sceneToLoad3);

        }
    }
    void EndReached(VideoPlayer vp)
    {
        skip.gameObject.SetActive(false);
        ScenesFade.Instance.FadeOutAndLoad(sceneToLoad3);
    }
}
