using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject vrControllers;

    public Button Level1;
    public Button Level2;
    public Button Level3;
    public Button Level4;

    private bool isPaused = false;
    private void Start()
    {
        Time.timeScale = 1f; // Oyun zamanýný devam ettir.
        isPaused = false;
        pauseMenuUI.SetActive(false);
        //vrControllers.SetActive(true); // VR kontrollerini etkinleþtir.
        //mainCam.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked; // Fareyi kilitli hale getir.
        Cursor.visible = false; // Fareyi görünmez yap.


        Level1.onClick.AddListener(() => StartSelectedLevel(1));
        Level2.onClick.AddListener(() => StartSelectedLevel(2));
        Level3.onClick.AddListener(() => StartSelectedLevel(3));
        Level4.onClick.AddListener(() => StartSelectedLevel(4));
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    void Pause()
    {
        Time.timeScale = 0f; // Oyun zamanýný durdur.
        isPaused = true;
        pauseMenuUI.SetActive(true);
        //vrControllers.SetActive(false); // VR kontrollerini devre dýþý býrak.

        Cursor.lockState = CursorLockMode.None; // Fareyi serbest býrak.
        Cursor.visible = true; // Fareyi görünür yap.
    }

    public void Resume()
    {
        Time.timeScale = 1f; // Oyun zamanýný devam ettir.
        isPaused = false;
        pauseMenuUI.SetActive(false);
        //vrControllers.SetActive(true); // VR kontrollerini etkinleþtir.

        Cursor.lockState = CursorLockMode.Locked; // Fareyi kilitli hale getir.
        Cursor.visible = false; // Fareyi görünmez yap.
    }

    // Devam Et butonu için bir fonksiyon
    public void ResumeGame()
    {
        Resume();
    }

    public void RestartGame()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        Time.timeScale = 1f; // Oyun zamanýný devam ettir.
        isPaused = false;
        pauseMenuUI.SetActive(false);
        //vrControllers.SetActive(true); // VR kontrollerini etkinleþtir.

        Cursor.lockState = CursorLockMode.Locked; // Fareyi kilitli hale getir.
        Cursor.visible = false; // Fareyi görünmez yap.
    }

    public void ExitGame()
    {
        // Oyunu kapatmak için Application.Quit() fonksiyonunu kullanabilirsiniz.
        Application.Quit();
    }

    // Ana menüye dön butonu için bir fonksiyon
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void StartSelectedLevel(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }
}

