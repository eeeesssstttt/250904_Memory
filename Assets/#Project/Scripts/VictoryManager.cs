using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryManager : MonoBehaviour
{
    private string sceneName;
    private float delayBeforeLoadScene;
    public void Initialize(string sceneName, float delayBeforeLoadScene)
    {
        this.sceneName = sceneName.Trim();
        this.delayBeforeLoadScene = delayBeforeLoadScene;
        // delay can be def in GameInitializer 
        // or in Victory if you're planning on different behaviors depending on victory type.
    }

    public void LaunchVictory()
    {
        StartCoroutine(_LaunchVictory());
    }

    private IEnumerator _LaunchVictory()
    // _ because something else launches it
    {
        yield return new WaitForSeconds(delayBeforeLoadScene);
        SceneManager.LoadScene(sceneName);
    }
}
