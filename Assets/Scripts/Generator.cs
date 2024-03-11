using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public static Generator Instance;

    [SerializeField] private GameObject celda;
    [SerializeField] private int width, height;
    [SerializeField] private int nBombs;
    [SerializeField] public Canvas canvas;
     public Canvas victoria;
     public Canvas derrota;

    private GameObject[][] map;
    public bool winner = true;
    public int nTest = 0;

    private int x, y;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    public void EasyMap()
    {
        width = 5;
        height = 5;
        nBombs = 3;

        //canvas.gameObject.active = false;
        generateMap();
    }

    public void MediumMap()
    {
        width = 6;
        height = 6;
        nBombs = 5;


        generateMap();
    }

    public void HardMap()
    {
        width = 6;
        height = 6;
        nBombs = 8;

        generateMap();
    }

    public int getWidth()
    {
        return width;
    }

    public int getHeight()
    {
        return height;
    }

    public int getBombs()
    {
        return nBombs;
    }

    public int getNTest()
    {
        return nTest;
    }

    public void addTest()
    {
        nTest++;
    }


    public void generateMap()
    {
        //Generamos el mapa interno
        map = new GameObject[width][];
        for (int i = 0; i < map.Length; i++)
        {
            map[i] = new GameObject[height];
        }

        //Generamos la pantalla de juego
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                map[i][j] = Instantiate(celda, new Vector3(i, j, 0), Quaternion.identity);
                map[i][j].GetComponent<Celda>().setX(i);
                map[i][j].GetComponent<Celda>().setY(j);
            }
        }

        //Situamos la cámara en el centro
        Camera.main.transform.position = new Vector3(((float)width / 2) - 0.5f, ((float)height / 2) - 0.5f, -10);

        //Situamos las bombas aleatoriamente
        for (int i = 0; i < nBombs; i++)
        {
            x = Random.Range(0, width);
            y = Random.Range(0, height);
            if (!map[x][y].GetComponent<Celda>().isBomb())
            {
                map[x][y].GetComponent<Celda>().setBomb(true);
            }
            else
            {
                //le restamos menos a la i para que haga una iteración más
                i--;
            }
        }
    }

    public int getBombsAround(int x, int y)
    {
        int contador = 0;

        //la primera comprobacion es para que no se salga de los limites del array y pete
        //casilla superior izquierda
        if (x > 0 && y < height - 1 && map[x - 1][y + 1].GetComponent<Celda>().isBomb())
            contador++;

        //casilla superior, o sea, si existen casillas por encima
        if(y < height-1 && map[x][y+1].GetComponent<Celda>().isBomb())
            contador++;

        //casilla superior derecha, compruebo que no estoy en el limite derecho
        if(x < width-1 && y < height-1 && map[x + 1][y+1].GetComponent <Celda>().isBomb())
            contador++;

        //casilla izquierda
        if(x > 0 && map[x - 1][y].GetComponent <Celda>().isBomb())
            contador++;

        //casilla derecha
        if(x < width-1 && map[x + 1][y].GetComponent<Celda>().isBomb())
            contador++;

        //Inferior izquieda
        if (x > 0 && y > 0 && map[x - 1][y - 1].GetComponent<Celda>().isBomb())
            contador++;

        //casilla inferior, o sea, si existen casillas por debajo
        if (y > 0 && map[x][y - 1].GetComponent<Celda>().isBomb())
            contador++;

        //casilla inferior derecha, compruebo que no estoy en el limite derecho
        if (x < width - 1 && y > 0 && map[x + 1][y - 1].GetComponent<Celda>().isBomb())
            contador++;

        //si queremos usar bucles
        //for(int i = x-1; i<=x + 1; i++)
        //{
        //    for(int j = y-1; j >= y + 1; j++)
        //    {
        //        if(i>=0 && i< width && j>= 0 && j< height && i!=x && j != y)
        //        {
        //            if (map[i][j].GetComponent<Celda>().isBomb()) contador++;
        //        }

        //    }
        //}

        return contador;
    }

    public void Win(bool win)
    {
        win = winner;
    }

    public bool SetWinner(bool win)
    {
        return win = winner;
    }

    public void DestroyMap()
    {
        GameObject[] cuadrados = GameObject.FindGameObjectsWithTag("cuadrado");

        foreach(GameObject cuadrado in cuadrados)
        {
            Destroy(cuadrado);
        }
    }
    
}
