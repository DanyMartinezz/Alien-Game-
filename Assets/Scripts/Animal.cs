using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] bool movingRight;
    [SerializeField] int contadorDisparos;

    float minX, maxX;
    int puntosDeVida = 5;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 esquinaInfDer = Camera.main.ViewportToWorldPoint(new Vector2(1, 0));
        Vector2 esquinaInfIzq = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        maxX = esquinaInfDer.x;
        minX = esquinaInfIzq.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(movingRight)
        {
            Vector2 movimiento = new Vector2(speed * Time.deltaTime, 0);
            transform.Translate(movimiento);
        }
        else
        {
            Vector2 movimiento = new Vector2(-speed * Time.deltaTime, 0);
            transform.Translate(movimiento);
        }
        

        if(transform.position.x >= maxX)
        {
            movingRight = false;
        }
        else if(transform.position.x <= minX)
        {
            movingRight = true;
        }
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(collision.gameObject.CompareTag("Disparo") )
        {
            //1. Encontrar el objeto llamado "GameManager"
            //2. Encontrar el componente de ese objeto de tipo "GameManager"
            //3. Llamar la función CaptureAnimal()
            GameObject gm = GameObject.Find("GameManager");
            GameManager script = gm.GetComponent<GameManager>();


            bool validarVictoria = false;

            int nivelactual = script.getNivel();

            switch(nivelactual)
            {
                case 1:
                    Destroy(this.gameObject);
                    validarVictoria = true;
                    break;
                case 2:
                        if(contadorDisparos >= 1)
                        {
                            Destroy(this.gameObject);
                            validarVictoria = true;
                        }
                        else
                        {
                            contadorDisparos = contadorDisparos + 1;
                        }
                    break;
                case 3:
                    if (contadorDisparos >= 2)
                    {
                        Destroy(this.gameObject);
                        validarVictoria = true;
                    }
                    else
                    {
                        contadorDisparos = contadorDisparos + 1;
                    }
                    break;
            }

            if (validarVictoria == true)
            {
                script.CaptureAnimal();
            }

        }
    }

}