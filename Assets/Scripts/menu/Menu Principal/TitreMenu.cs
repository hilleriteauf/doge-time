using TMPro;
using UnityEngine;

public class TitreMenu
{
    private TextMeshProUGUI titre;

    private Vector2 posMil;
    private Vector2 posFin;

    private readonly Vector2 echelleNormale = new(1, 1);
    private const float agrandissement = 1.2f;
    private Vector2 echelleAgrandissement;//Agrandissement du texte lors de la sélection

    private const float dureeAnim = 2;

    private Trajectoire traj;

    public TextMeshProUGUI Titre { get => titre; set => titre = value; }
    public Trajectoire Traj { get => traj; set => traj = value; }
    public Vector2 PosMil { get => posMil; set => posMil = value; }

    public TitreMenu(TextMeshProUGUI titre, float posMil, float posFin)
    {
        this.titre = titre;
        Vector2 posDep = MethodeStatic.GetPositionRect(titre);

        this.posMil = new Vector2(posMil, posDep.y);
        this.posFin = new Vector2(posFin, posDep.y);

        this.echelleAgrandissement = this.echelleNormale * agrandissement;

        this.traj = new Trajectoire(posDep.x, this.posMil.x, dureeAnim, 1);
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
