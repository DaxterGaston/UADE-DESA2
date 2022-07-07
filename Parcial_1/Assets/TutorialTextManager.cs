using TMPro;
using UnityEngine;

public class TutorialTextManager : MonoBehaviour
{
    [SerializeField] private string _textToDisplay;
    [SerializeField] TextMeshProUGUI _tutorialText;
    [SerializeField] GameObject _tutorialTextWindow;
    // Start is called before the first frame update
    void Start()
    {
        _tutorialTextWindow.SetActive(true);
        _tutorialText.text = _textToDisplay;
    }
}
