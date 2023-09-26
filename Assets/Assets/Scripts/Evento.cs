using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evento : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Pokemon Unite/Rayquaza:
        //En pokemon unite ocurren muchos eventos durantes una partida, pero explicaré el evento mas importante del juego, Rayquaza.
        //Cuando el temporizador marca que solo quedan 2 minutos de partida, ocurre lo siguiente:
        //El ambiente se oscurece, dando una estetica de boss final.
        //La cantidad de tanto anotados se duplica, osea que si yo voy a una base enemiga y tengo 25 puntos, en lugar de anotar 25 tantos en la base, se anotarán 50 tantos.
        //Aparece Rayquaza, el ultimo objetivo de la partida. Al aparecer, todos los pokemon salvajes repartidos, evolucionan, dando mas experiencia al derrotarlos. 
        //Mientras la barra de vida de Rayquaza va bajando, desencadenará ataques a todos los jugadores cercanos. Su frecuencia de ataque subirá mientras mas baje su barra de vida.
        //Si un jugador le da el golpe final, desencadenará efectos a sus 4 aliados del equipo.
        //El escudo Vendaval, proporciona un escudo equivalente a su 30% de vida maxima, y tambien podrán anotar tantos 3 veces mas rapido. Solo mientras aun tenga el escudo, el cual puede ser destruido por el equipo rival.
        //Si un equipo tiene sus bases completas, pueden optar por defender el rayquaza, para que el equipo rival no tenga oportunidad de ganar.
        //En cambio, si un equipo tiene sus bases destruidas, puede coordinarse con su equipo, para ir por rayquaza, vencerlo y obtener el buffo correspondiente. Escudo y velocidad de anotación, y asi poder voltear la partida.
        //Un evento que puede ocasionar un derrota aplastante si no se sabe aprovechar, o una victoria masiva si saben como aprovecharlo.
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
