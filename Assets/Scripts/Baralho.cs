using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baralho : MonoBehaviour
{
    public List<GameObject> cartas = new List<GameObject>();
    List<Carta> descarte = new List<Carta>();
    public Carta tombo;
    public GameObject cartaPrefab;
    public Jogador[] jogadores;
    
    void Start()
    {
        for (int i = 0; i < 40; i++)
        {
            if(i > 0)
            {
                Vector3 pos = this.transform.position + new Vector3(0,
                    0,
                    0.01f * i);
                GameObject gameObject = (GameObject)Instantiate(cartaPrefab, pos, Quaternion.identity);
                gameObject.transform.parent = this.gameObject.transform;
                Carta carta = gameObject.GetComponent<Carta>();

                if (i < 11)
                {
                    carta.Construtor(EnumNaipes.ouros, i);
                    cartas.Add(carta.gameObject);
                }
                else if (i > 10 && i < 21)
                {
                    carta.Construtor(EnumNaipes.espada, i - 10);
                    cartas.Add(carta.gameObject);
                }
                else if (i > 20 && i < 31)
                {
                    carta.Construtor(EnumNaipes.copas, i - 20);
                    cartas.Add(carta.gameObject);

                }
                else if (i > 30 && i < 41)
                {
                    carta.Construtor(EnumNaipes.paus, i - 30);
                    cartas.Add(carta.gameObject);

                }
            }
        }
    }

    public void DarCartas()
    {
        int index = Random.Range(0, cartas.Count);
        tombo = cartas[index].GetComponent<Carta>();
        cartas[index].transform.parent = tombo.transform;
        cartas[index].transform.position = new Vector3(0, 0, 0);
        cartas.Remove(cartas[index]);
        foreach (Jogador jogador in jogadores)
        {
            for (int i = 0; i < 3; i++)
            {
                index = Random.Range(0, cartas.Count);
                jogador.cartas[i] = cartas[index].GetComponent<Carta>();
                cartas[index].transform.parent = jogador.slots[i].transform;
                if (jogador.cartas[i].valor == tombo.valor + 1)
                {
                    jogador.cartas[i].manilha = true;
                }
                cartas[index].transform.position = new Vector3(0, 0, 0);
                cartas.Remove(cartas[index]);
            }
        }
    }
}
