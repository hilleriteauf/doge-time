using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitreMenu
{
    private GameObject titre;

    private Vector2 posDep;
    private Vector2 posMil;
    private Vector2 posFin;

    private const float dureeAnim = 2;

    private Trajectoire traj;

    public GameObject GObject { get => titre; set => titre = value; }
    public Trajectoire Traj { get => traj; set => traj = value; }

    public TitreMenu(GameObject titre, List<Vector2> pos)
    {
        this.titre = titre;

        this.posDep = pos[0];
        this.posMil = pos[1];
        this.posFin = pos[2];

        this.titre.GetComponent<RectTransform>().position = this.posDep;

        this.traj = new Trajectoire(this.posDep.x, this.posMil.x, dureeAnim, 1);
    }

    public void Fin()
    {
        this.Traj = new Trajectoire(this.posMil.x, this.posFin.x, dureeAnim, 2);
    }
}
