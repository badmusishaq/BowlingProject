using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] Button endGameButton;

    // Start is called before the first frame update
    void Start()
    {
        endGameButton.onClick.AddListener(OnGameQuit);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnStartGameClicked()
    {
        SceneManager.LoadScene(1);
    }

    void OnGameQuit()
    {
        Debug.Log("Game ended!");
        Application.Quit();
    }
}
