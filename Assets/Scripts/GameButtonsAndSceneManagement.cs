using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameButtonsAndSceneManagement : MonoBehaviour
{
    [SerializeField] private InputActionAsset m_inputFiles;
    private InputAction m_gettingToGameAction;
    private InputAction m_quittingTheGameAction;

    void Start()
    {
        m_gettingToGameAction = m_inputFiles.FindAction("GetBackToGame");
        m_quittingTheGameAction = m_inputFiles.FindAction("QuitTheGame");
    }
    // Update is called once per frame
    void Update()
    {
        bool enterButtonPressed = m_gettingToGameAction.WasPressedThisFrame();
        if (enterButtonPressed)
        {
            SceneManager.LoadScene("Game");
        }
        bool escapeButtonPressed = m_gettingToGameAction.WasPressedThisFrame();
        if (escapeButtonPressed)
        {
            Application.Quit();
        }
    }
    public void OnClickedPlay()
    {
        SceneManager.LoadScene("Game");
    }
    public void OnClickedQuit()
    {
        Application.Quit();
    }
}
