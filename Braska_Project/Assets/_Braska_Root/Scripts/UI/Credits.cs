using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Credits: MonoBehaviour
{
    [SerializeField] string sceneToLoad3;
    public GameObject skip;

    private void Start()
    {
        Invoke(nameof(EndReached), 39f);
    }
    void Update()
    {
       
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            
                ScenesFade.Instance.FadeOutAndLoad(sceneToLoad3);
        
        }
       

    }
    void EndReached()
    {
        skip.gameObject.SetActive(false);
        ScenesFade.Instance.FadeOutAndLoad(sceneToLoad3);
    }
}
