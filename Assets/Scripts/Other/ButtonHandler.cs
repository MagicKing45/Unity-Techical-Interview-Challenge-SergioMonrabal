using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ButtonHandler : MonoBehaviour
{
    public GameObject EndMenu;
    public void Dead() 
    {
        Cursor.lockState = CursorLockMode.None;
        EndMenu.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Dead";
        EndMenu.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.red;
        EndMenu.SetActive(true);
    }
    public void Win() 
    {
        Cursor.lockState = CursorLockMode.None;
        EndMenu.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "¡Win!";
        EndMenu.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.green;
        EndMenu.SetActive(true);
    }
    public void TryAgain() 
    {
        SceneManager.LoadScene("GameScene");
    }
    public void Exit()
    {
        Application.Quit();
    }
}
