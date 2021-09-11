using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject gameOverMenu;
    [SerializeField] int numAnimals;
    [SerializeField] int nivel;
    int contadorBoton;

    private GameObject[] animales;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PauseGame();
    }

    public void Parartiempo()
    {
        contadorBoton = contadorBoton + 1;

 
        GameObject boton = GameObject.Find("btnParar");

        Button miBoton = boton.GetComponent<Button>();

        miBoton.enabled = false;

        animales = GameObject.FindGameObjectsWithTag("Animal");

        for (int i = 0; i < animales.Length; i++)
        {
            GameObject obj = animales[i];
            Animal miAnimal = obj.GetComponent<Animal>();
            miAnimal.speed = (miAnimal.speed / 2);
        }

        StartCoroutine(volverNormalidad());

    }

    public IEnumerator volverNormalidad()
    {
        yield return new WaitForSeconds(3);

        GameObject boton = GameObject.Find("btnParar");

        Button miBoton = boton.GetComponent<Button>();

        miBoton.enabled = true;

        animales = GameObject.FindGameObjectsWithTag("Animal");

        for (int i = 0; i < animales.Length; i++)
        {
            GameObject obj = animales[i];
            Animal miAnimal = obj.GetComponent<Animal>();
            miAnimal.speed = (miAnimal.speed * 2);
        }

        if (contadorBoton == 3)
        {

            miBoton.enabled = false;
        }
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void cargarNivel(int nivel)
    {
        SceneManager.LoadScene(nivel);
    }

    public int getNivel()
    {
        return nivel;
    }

    void PauseGame()
    {
        //pausar y "despausar" el juego
        if(Input.GetKeyDown(KeyCode.P))
        {
            if(Time.timeScale == 1)
            {
                Time.timeScale = 0;
                pauseMenu.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                pauseMenu.SetActive(false);
            }
        }
        //setActive
        //variable serializada
    }

    public void CaptureAnimal()
    {
        numAnimals = numAnimals - 1;
        if(numAnimals < 1)
        {
            //ganamos!
            //Time.timeScale = 0;
            gameOverMenu.SetActive(true);
        }
    }
}
