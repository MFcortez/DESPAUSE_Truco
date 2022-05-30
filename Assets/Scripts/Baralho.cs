using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baralho : MonoBehaviour
{
    public List<GameObject> cartas = new List<GameObject>();
    public List<Carta> descarte = new List<Carta>();
    int manilha;
    public Carta tombo;
    public GameObject cartaPrefab, 
        gameObjectTombo;
    public Jogador[] jogadores;
    public int truco;

    int rodada;
    int pontos;
    
    void Start()
    {
        truco = 1;
        for (int i = 0; i < 41; i++)
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
        tombo = GetCarta();
        tombo.transform.position = gameObjectTombo.transform.position;
        manilha = tombo.valor;
        if(manilha == 13)
        {
            manilha = 4;
        }
        else
        {
            manilha++;
        }
        cartas.Remove(tombo.gameObject);
        foreach (Jogador jogador in jogadores)
        {
            for (int i = 0; i < 3; i++)
            {
                Carta carta = GetCarta();
                jogador.cartas[i] = carta;

                if (jogador.cartas[i].valor == manilha)
                {
                    carta.SetaManilha();
                }
                carta.transform.position = jogador.slots[i].transform.position;
            }
        }
    }

    public void Descarta(Carta carta)
    {
        descarte.Add(carta);
        Vector3 pos = this.transform.position + new Vector3(0,
            0,
            0.01f);
        carta.transform.position = pos;
    }

    public Carta GetCarta()
    {
        int index = Random.Range(0, cartas.Count);
        Carta carta = cartas[index].GetComponent<Carta>();
        if (tombo != null)
        {
            int manilha = tombo.valor;
            manilha++;
            if (carta.valor == manilha)
            {
                carta.manilha = true;
            }
        }
        cartas.Remove(carta.gameObject);
        return carta;
    }

    public void BotGanhou()
    {
        rodada++;
        if(rodada == 2)
        {
            pontos+= truco;
            Descarta(tombo);
            tombo = null;
            foreach (Jogador jogador in jogadores)
            {
                jogador.TerminaRodada();
                jogador.Reset();
            }
        }
    }

    public void TerminaRodada()
    {
        foreach(Carta carta in descarte)
        {
            cartas.Add(carta.gameObject);
            carta.Reset();
        }
        descarte.Clear();
        truco = 1;
        rodada = 0;
    }

    public void ChamaTruco(Jogador jogador)
    {
        int chanceDeAceitar = Random.Range(1, 10);
        if(chanceDeAceitar > 6)
        {
            if (truco == 1)
            {
                truco = 3;
            }
            else
            {
                truco *= 2;
            }
            print("TRUUUUUUUUUUUCO!!!!, valendo: " + truco);
        }
        else
        {
            
            jogador.Pontuo();
            print("Oponente fugiu, ganhou: " + truco );
        }
    }
}
