
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private float delayBeforeLoading = 38f;
    [SerializeField] private string sceneNameToLoad;
    private float timeElapsed;

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed > delayBeforeLoading )
        {
            SceneManager.LoadScene(sceneNameToLoad);
        }


    }
}
