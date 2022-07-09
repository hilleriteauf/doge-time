using TMPro;
using UnityEngine;

public class TitreMenu
{
    private TextMeshProUGUI titre;

    private Vector2 posDeb;
    private Vector2 posMil;
    private Vector2 posFin;

    private readonly Vector2 echelleNormale = new(1, 1);
    private const float agrandissement = 1.2f;
    private Vector2 echelleAgrandissement;//Agrandissement du texte lors de la sélection

    private float dureeAnim;
    private const float decalageFin = 1;

    private Trajectoire traj;

    public TextMeshProUGUI Titre { get => titre; set => titre = value; }
    public Trajectoire Traj { get => traj; set => traj = value; }
    public Vector2 PosMil { get => posMil; set => posMil = value; }

    public TitreMenu(TextMeshProUGUI titre, float posY, float dureeAnim)
    {
        this.dureeAnim = dureeAnim;
        this.titre = titre;

        this.posDeb = new Vector2(- MethodeStatic.GetSizeRect(titre).x, posY);
        this.posMil = new Vector2(Screen.width / 2, posY);
        this.posFin = new Vector2(Screen.width + MethodeStatic.GetSizeRect(titre).x, posY);

        this.titre.GetComponent<RectTransform>().position = this.posDeb;

        this.echelleAgrandissement = this.echelleNormale * agrandissement;

        this.traj = new Trajectoire(this.posDeb.x, this.posMil.x, this.dureeAnim, 1);
    }

    public void Fin()
    {
        this.Traj = new Trajectoire(this.posMil.x, this.posFin.x, this.dureeAnim - decalageFin, 2);
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
