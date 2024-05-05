using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;


public class GameManager : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject vrControllers;
    public GameObject RayLine;

    public Button Level1;
    public Button Level2;
    public Button Level3;
    public Button Level4;

    
    public AudioSource source1;
    public AudioClip Level1G�revSesi;
    private List<GameObject> grabObj = new List<GameObject>();

    private bool isPaused = false;
    private void Start()
    {
        Time.timeScale = 1f; // Oyun zaman�n� devam ettir.
        isPaused = false;
        pauseMenuUI.SetActive(false);
        
        RayLine.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked; // Fareyi kilitli hale getir.
        Cursor.visible = false; // Fareyi g�r�nmez yap.

        // Ses kayd�n� ba�lat
        source1 = GetComponent<AudioSource>();
        source1.clip = Level1G�revSesi;
        source1.Play();

        // CanSlice tag'ine sahip t�m objeleri bul
        GameObject[] grabableObj = GameObject.FindGameObjectsWithTag("CanSlice");

        // Her bir objeyi devre d��� b�rak
        foreach (GameObject obj in grabableObj)
        {
            obj.SetActive(false);
            grabObj.Add(obj);
        }


        Level1.onClick.AddListener(() => StartSelectedLevel(1));
        Level2.onClick.AddListener(() => StartSelectedLevel(2));
        Level3.onClick.AddListener(() => StartSelectedLevel(3));
        Level4.onClick.AddListener(() => StartSelectedLevel(4));



    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) { 
                RayLine.SetActive(false);
                Resume();
            }
            else {
                RayLine.SetActive(true);
                Pause();
            }
        }

        // E�er ses kayd� tamamland�ysa ve oyun objeleri devre d���ysa
        if (!source1.isPlaying )
        {
            Debug.Log("DONE");
            // CanSlice tag'ine sahip t�m objeleri bul
            

            // Her bir objeyi devre d��� b�rak
            foreach (GameObject obj in grabObj)
            {
                obj.SetActive(true);
            }

        }
        

    }


    void Pause()
    {
        Time.timeScale = 0f; // Oyun zaman�n� durdur.
        isPaused = true;
        pauseMenuUI.SetActive(true);
        

        Cursor.lockState = CursorLockMode.None; // Fareyi serbest b�rak.
        Cursor.visible = true; // Fareyi g�r�n�r yap.
    }

    public void Resume()
    {
        Time.timeScale = 1f; // Oyun zaman�n� devam ettir.
        isPaused = false;
        pauseMenuUI.SetActive(false);
        

        Cursor.lockState = CursorLockMode.Locked; // Fareyi kilitli hale getir.
        Cursor.visible = false; // Fareyi g�r�nmez yap.
    }

    // Devam Et butonu i�in bir fonksiyon
    public void ResumeGame()
    {
        Resume();
    }

    public void RestartGame()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        Time.timeScale = 1f; // Oyun zaman�n� devam ettir.
        isPaused = false;
        pauseMenuUI.SetActive(false);
        

        Cursor.lockState = CursorLockMode.Locked; // Fareyi kilitli hale getir.
        Cursor.visible = false; // Fareyi g�r�nmez yap.
    }

    public void ExitGame()
    {
        
        Application.Quit();
    }

    // Ana men�ye d�n butonu i�in bir fonksiyon
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void StartSelectedLevel(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }
}

