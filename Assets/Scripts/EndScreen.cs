using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{

    public TMP_Text endText;
    public GameObject textObj;

    public GameObject button;
    

    void Start()
    {
        textObj.SetActive(false);
        button.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }




    public void Text()
    {
        StartCoroutine (TextAnim() );
        textObj.SetActive(true);
    }

    public void returnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    IEnumerator TextAnim()
{       
        endText.SetText("The End?");
        
        yield return new WaitForSeconds(2);

        endText.SetText("Made By");

        yield return new WaitForSeconds(2);

        endText.SetText("Me");

        yield return new WaitForSeconds(2);

        button.SetActive(true);
        Cursor.lockState = CursorLockMode.None;

}
}
