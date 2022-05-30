using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jogador : MonoBehaviour
{
    public int pontos;
    public Carta[] cartas;
    int rodada;
    public GameObject[] slots;
    public Baralho baralho;

    void Start()
    {
        baralho = FindObjectOfType<Baralho>();
    }

    public void Reset()
    {
        rodada = 0;
    }
    // Update is called once per frame
    void Update()
    {
        //Ao clicar pega a posição do mouse no clique e passa como destino para o agente
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Carta carta = hit.collider.GetComponentInParent<Carta>();
                JogaCarta(carta);
            }
        }
    } 

    public void JogaCarta(Carta carta)
    {

        Carta vindo = baralho.GetCarta();
        print("Vindo " + vindo.numero + "  " + vindo.naipe);
        baralho.Descarta(carta);
        baralho.Descarta(vindo);
        for (int i = 0; i < 3; i++)
        {
            if (cartas[i] == carta)
            {
                cartas[i] = null;
            }
        }
        if (carta.manilha && !vindo.manilha)
        {
            print("Ganhou com manilha de " + carta.naipe);
            GanhouRodada();
        }
        else if (!carta.manilha && vindo.manilha)
        {
            print("Perdeu para manilha de " + vindo.naipe);
            baralho.BotGanhou();
        }
        else if (carta.manilha && vindo.manilha)
        {
            if (((int)carta.naipe) > ((int)vindo.naipe))
            {
                print("Ganhou com manilha de " + carta.naipe);
                GanhouRodada();
            }
            else
            {
                print("Perdeu para manilha de " + vindo.naipe);
            }
        }
        else if (carta.valor == vindo.valor)
        {
            print(carta.numero + "  " + carta.naipe);
            print("Melou");
            GanhouRodada();
            baralho.BotGanhou();
        }
        else
        {
            if (carta.valor > vindo.valor)
            {
                print(carta.numero + "  " + carta.naipe);
                print("Ganhou");
                GanhouRodada();
            }
            else
            {
                print(carta.numero + "  " + carta.naipe);
                print("Perdeu");
                baralho.BotGanhou();
            }
        }
    }
    
    public void GanhouRodada()
    {
        rodada++;
        if (rodada == 2)
        {
            Pontuo();
        }
    }
    public void Pontuo()
    {
        baralho.Descarta(baralho.tombo);
        baralho.tombo = null;
        pontos += baralho.truco;
        Reset();
        TerminaRodada();
    }

    public void TerminaRodada()
    {
        for (int i = 0; i < 3; i++)
        {
            if (cartas[i] != null)
            {
                baralho.Descarta(cartas[i]);
                cartas[i] = null;
            }
        }
        baralho.TerminaRodada();
    }

    public void Truco()
    {
        if(baralho.truco >= 12)
        {
            print("Tá valendo o jogo");
        }
        else
        {
            baralho.ChamaTruco(this);
        }
    }
}
