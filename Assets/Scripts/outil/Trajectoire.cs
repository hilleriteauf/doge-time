using System;
using UnityEngine;

public class Trajectoire
{
    private float coordDep;//Position de départ
    private float distance;//Distance à parcourir pour 
    private float temps;//Durée actuelle de l'animation
    private float duree;//Durée à atteindre pour l'animation
    private int typeAnim;//Défini le type de l'animation
    private bool animFini;//Bolléen qui détermine si l'animation est en cours ou finie

    public bool AnimFini { get => animFini; set => animFini = value; }

    public int TypeAnim { get => typeAnim; set => typeAnim = value; }
    public float CoordDep { get => coordDep; set => coordDep = value; }

    public Trajectoire(float coordDep, float coordFin, float duree, int typeAnim)
    {
        this.coordDep = coordDep;
        this.distance = Math.Abs(coordDep - coordFin);
        this.temps = 0;
        this.duree = duree;
        this.typeAnim = typeAnim;
        this.animFini = false;
    }

    public float UpdatePos(float coord)
    {
        if (!animFini)
        {
            temps += Time.deltaTime;
            if (temps >= duree)
            {
                animFini = true;
                return coordDep + distance;//On revient parfaitement à la position finale
            }
            else
            {
                switch (typeAnim)
                {
                    case 1:
                        return EaseOutExpo();
                    case 2:
                        return EaseInExpo(); 
                    default:
                        throw new Exception("type d'animation non correct");
                }
            }
        }
        else
            return coord;
    }

    public float EaseInExpo()
    {
        return (float)(distance * Math.Pow(2, 10 * (temps / duree - 1)) + coordDep);
    }

    public float EaseOutExpo()
    {
        return (float)(distance * (-Math.Pow(2, -10 * temps / duree) + 1) + coordDep);
    }
}
