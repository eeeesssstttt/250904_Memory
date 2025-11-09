using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryManager : MonoBehaviour
{
    public bool Victory { get; private set; }
    private string sceneName;
    private Canvas victoryTextDisplay; // Should create VictoryCanvas monobehaviour that insures it is a canvas that has a TextMeshProUGUI, if that is possible.
    private string victoryText;
    private float delayBeforeVictory;
    public void Initialize(string sceneName, Canvas victoryTextDisplay, string victoryText, float delayBeforeVictory)
    {
        this.sceneName = sceneName.Trim();
        this.victoryTextDisplay = victoryTextDisplay;
        this.victoryText = victoryText;
        this.delayBeforeVictory = delayBeforeVictory;
        // delay can be def in GameInitializer 
        // or in Victory if you're planning on different behaviors depending on victory type.
    }

    public void DisplayVictoryText()
    {
        StartCoroutine(_DisplayVictoryText());
    }

    public void HideVictoryText()
    {
        victoryTextDisplay.gameObject.SetActive(false);
        Victory = false;
    }

    public void LaunchVictoryScene()
    {
        StartCoroutine(_LaunchVictoryScene());
    }

    private IEnumerator _DisplayVictoryText()
    {
        yield return new WaitForSeconds(delayBeforeVictory);
        Debug.Log("victory");
        TMP_Text[] text = victoryTextDisplay.GetComponentsInChildren<TMP_Text>();
        if (text != null)
        {
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i].CompareTag("Edit"))
                {
                    text[i].SetText(victoryText);
                }
            }
        }
        victoryTextDisplay.gameObject.SetActive(true);
        Victory = true;
    }

    private IEnumerator _LaunchVictoryScene()
    // _ because something else launches it
    {
        yield return new WaitForSeconds(delayBeforeVictory);
        SceneManager.LoadScene(sceneName);
    }
}
