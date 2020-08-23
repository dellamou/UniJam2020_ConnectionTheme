using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class buttonController : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject AchiMenu;
    public GameObject MainCam;
    public GameObject AchiCam;
    public GameObject MainSun;
    public GameObject AchiSun;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        AchiCam.SetActive(false);
        AchiMenu.SetActive(false);
        AchiSun.SetActive(false);
    }
    public void replay() {
		SceneManager.LoadScene("MainMaze");
	}
    public void play() {
        SceneManager.LoadScene("MainMaze");
    }
    public void achievements()
    {
        this.MainMenu.SetActive(false);
        MainCam.SetActive(false);
        AchiCam.SetActive(true);
        AchiSun.SetActive(true);
        MainSun.SetActive(false);
        this.AchiMenu.SetActive(true);
    }
    public void homePage()
    {
        this.AchiMenu.SetActive(false);
        MainCam.SetActive(true);
        AchiCam.SetActive(false);
        AchiSun.SetActive(false);
        MainSun.SetActive(true);
        this.MainMenu.SetActive(true);
    }
    public void badEnd()
    {
        SceneManager.LoadScene("BadEnd");
    }
    public void goodEnd()
    {
        SceneManager.LoadScene("GoodEnd");
    }
    public void trueEnd()
    {
        SceneManager.LoadScene("TrueEnd");
    }
    public void menu()
    {
        SceneManager.LoadScene("HomePage");
    }
}
