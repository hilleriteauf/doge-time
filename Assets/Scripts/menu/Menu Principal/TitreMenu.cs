using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TitreMenu
{
    private GameObject titre;

    private Vector2 posDep;
    private Vector2 posMil;
    private Vector2 posFin;

    private Vector3 echelleNormale;
    private Vector3 echelleAgrandissement;
    private readonly float textAgrandissement = 1.2f;//Agrandissement du texte lors de la sélection

    private const float dureeAnim = 2;

    private Trajectoire traj;

    public GameObject Titre { get => titre; set => titre = value; }
    public Trajectoire Traj { get => traj; set => traj = value; }
    public Vector2 PosMil { get => posMil; set => posMil = value; }

    public TitreMenu(GameObject titre, List<Vector2> pos, Vector3 echelle)
    {
        this.titre = titre;

        this.posDep = pos[0];
        this.posMil = pos[1];
        this.posFin = pos[2];

        this.titre.GetComponent<RectTransform>().position = this.posDep;
        
        this.echelleNormale = echelle;
        this.echelleAgrandissement = echelle * this.textAgrandissement;
        this.titre.GetComponent<RectTransform>().localScale = echelle;

        this.traj = new Trajectoire(this.posDep.x, this.posMil.x, dureeAnim, 1);
    }

    public void Fin()
    {
        this.Traj = new Trajectoire(this.posMil.x, this.posFin.x, dureeAnim, 2);
    }

    public void SupSelection()
    {
        this.titre.GetComponent<Transform>().localScale = this.echelleNormale;
        this.titre.GetComponent<TextMeshProUGUI>().color = Color.white;
    }

    public void Selection()
    {
        this.titre.GetComponent<Transform>().localScale = this.echelleAgrandissement;
        this.titre.GetComponent<TextMeshProUGUI>().color = Color.yellow;
    }
}
