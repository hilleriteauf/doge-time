using System;
using UnityEngine;

public class Trajectoire : MonoBehaviour
{
    private Vector2 posDep;//Position de départ
    private readonly float distance;//Distance à parcourir pour 
    private float temps;//Durée actuelle de l'animation
    private readonly float duree;//Durée à atteindre pour l'animation
    private readonly int typeAnim;//Défini le type de l'animation
    private bool animFini;//Bolléen qui détermine si l'animation est en cours ou finie

    public Trajectoire(Vector2 posDep, Vector2 posFin, float duree, int typeAnim)
    {
        this.posDep = posDep;
        this.distance = (float)Math.Sqrt(Math.Pow(2, posDep.x-posFin.y) + Math.Pow(2, posDep.y-posFin.y));
        this.temps = 0;
        this.duree = duree;
        this.typeAnim = typeAnim;
        this.animFini = false;
    }

    public Vector2 UpdatePos(Vector2 posActu)
    {
        if (!animFini)
        {
            temps += Time.deltaTime;
            if (temps < duree)
                animFini = true;
            switch (typeAnim)
            {
                case 1:
                    return EaseInExpo();
                case 2:
                    return EaseOutExpo();
                default:
                    throw new Exception("type d'animation non correct");
            }
        }
        else
            return posActu;
    }

    public Vector2 EaseInExpo()
    {
        return new Vector2((float)(distance * Math.Pow(2, 10 * (temps / duree - 1)) + posDep.x), (float)(distance * Math.Pow(2, 10 * (temps / duree - 1)) + posDep.y));
    }

    public Vector2 EaseOutExpo()
    {
        return new Vector2((float)(distance * (-Math.Pow(2, -10 * temps / duree) + 1) + posDep.x), (float)(distance * (-Math.Pow(2, -10 * temps / duree) + 1) + posDep.y));
    }
}
