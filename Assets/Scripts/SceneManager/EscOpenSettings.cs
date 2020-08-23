using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class EscOpenSettings : MonoBehaviour
{
    public GameObject BGM;
    public GameObject SoundFX;

    private void Start()
    {
    }
    public void replay() {
		SceneManager.LoadScene("MainMaze");
	}
    public void BackToHomePage()
    {
        SceneManager.LoadScene("HomePage");
    }

    public void MuteBGM()
    {
        BGM.SetActive(!BGM.activeSelf);
    }

    public void MuteFX()
    {
        SoundFX.SetActive(!SoundFX.activeSelf);
    }
}
