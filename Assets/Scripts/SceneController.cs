using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject painelContext;
    [SerializeField] private GameObject painelScene;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        painelContext.SetActive(true);
    }

    public void CloseContextPanel(){
        painelContext.SetActive(false);
        painelScene.SetActive(true);
    }

    public void LoadMenuScene(){
        SceneManager.LoadScene("Module1");
    }
}
