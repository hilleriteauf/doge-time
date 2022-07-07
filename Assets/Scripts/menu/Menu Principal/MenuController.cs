using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject canvas;

    //Ecran avec (0,0) en bas à gauche
    private Vector2 screenSize;

    //Titre avec (0,0) au milieu
    private Vector2 titleSize;
    private float espaceEntreTitre;
    private const float distDecalageTitre = 700;//Décalage pour avoir un effet de différentes vitesses pour l'animation

    List<TitreMenu> listTitre;

    private int nbTitre;

    private readonly String[] scene = new String[4] { "ChoixNiveau", "Option", "Credit", "Quit" };
    private int choixScene;//Index qui indique le choix de la scène

    private int anim;// 1 = départ, 2 = en attente, 3 = fin

    void Start()
    {
        TextMeshProUGUI[] tabTitle = canvas.GetComponentsInChildren<TextMeshProUGUI>();
        nbTitre = tabTitle.Length;

        screenSize = new(Screen.width, Screen.height);

        titleSize = MethodeStatic.MultiplicationVector2(MethodeStatic.GetSizeRect(tabTitle[0]), MethodeStatic.GetScaleRect(canvas));

        espaceEntreTitre = (screenSize.y - nbTitre * titleSize.y) / (nbTitre + 1);

        float posMil = screenSize.x / 2;//Position de l'animation "en attente"
        float[] posFin = new float[nbTitre];//Position de l'animation "fin"

        float auxPosY = espaceEntreTitre + titleSize.y / 2;



        for (int i = nbTitre - 1; i >= 0; i--)
        {
            tabTitle[i].GetComponent<RectTransform>().position = new Vector2(-titleSize.x / 2 - i * distDecalageTitre, auxPosY);//Position de départ du titre
            posFin[i] = screenSize.x + titleSize.x / 2 + i * distDecalageTitre;//position de fin du titre (même hauteur)
            auxPosY += espaceEntreTitre + titleSize.y;
        }

        listTitre = new List<TitreMenu>();

        for (int i = 0; i < nbTitre; ++i)
            listTitre.Add(new TitreMenu(tabTitle[i], posMil, posFin[i]));

        choixScene = 0;

        anim = 1;

        choixScene = 0;
    }

    // Update is called once per frame
    void Update()
    {
        switch (anim)
        {
            case 1://Arrivé des titres du menu
                if (listTitre[0].Traj.AnimFini)
                    anim = 2;
                else
                    UpdateCoord();
                break;
            case 2://Attente du choix du menu
                if (Input.GetKeyDown(KeyCode.UpArrow))
                    choixScene = (choixScene - 1 + nbTitre) % nbTitre;

                if (Input.GetKeyDown(KeyCode.DownArrow))
                    choixScene = (choixScene + 1) % nbTitre;

                for (int i = 0; i < nbTitre; ++i)
                {
                    if ((Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0) && Survole(listTitre[i].PosMil))
                        choixScene = i;

                    if (Input.GetMouseButtonDown(0) && Survole(listTitre[i].PosMil))
                    {
                        choixScene = i;
                        AnimationSortie();
                    }

                    listTitre[i].SupSelection();
                }

                if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
                    AnimationSortie();

                listTitre[choixScene].Selection();

                break;
            case 3://Sortie des titres du menu
                if (listTitre[0].Traj.AnimFini)
                    MethodeStatic.ActiveScene(scene[choixScene]);
                else
                    UpdateCoord();
                break;
            default:
                throw new Exception("Erreur animation titre menu : " + anim);
        }
    }

    public void UpdateCoord()
    {
        Vector2 vAux;
        for (int i = 0; i < nbTitre; i++)//MAJ des positions des titres
        {
            vAux = MethodeStatic.GetPositionRect(listTitre[i].Titre);
            vAux.x = listTitre[i].Traj.UpdatePos(listTitre[i].Titre.GetComponent<Transform>().position.x);
            listTitre[i].Titre.GetComponent<RectTransform>().position = vAux;
        }
    }

    public bool Survole(Vector2 posTitre)
    {
        Vector3 mouse = Input.mousePosition;
        return posTitre.x - titleSize.x / 2 <= mouse.x
            && mouse.x <= posTitre.x + titleSize.x / 2
            && posTitre.y - titleSize.y / 2 <= mouse.y
            && mouse.y <= posTitre.y + titleSize.y / 2;
    }

    public void AnimationSortie()
    {
        anim = 3;
        for (int i = 0; i < nbTitre; i++)
            listTitre[i].Fin();//Animation de fin
    }
}