using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject textPlay;
    public GameObject textOption;
    public GameObject textCredit;
    public GameObject textQuit;


    private int choixTitre;// 0 = Play, 1 = Option, 2 = Credit, 3 = Quit

    //Ecran avec (0,0) en bas à gauche
    private float longEcran;
    private float hautEcran;

    private Vector2 echelle;

    //Titre avec (0,0) au milieu
    private const int longTitre = 900;
    private const int hautTitre = 150;

    private const int nbTitre = 4;
    private float espaceEntreTitre;

    private float[] posY;

    private float posDepX;
    private float posMilX;
    private float posFinX;

    private const float distDecalageTitre = 700;//Décalage pour avoir un effet de différentes vitesses pour l'animation

    List<TitreMenu> listTitre;

    private int anim;// 1 = départ, 2 = en attente, 3 = fin

    private int choixScene;// 0 = Menu, 1 = Choix niveau, 2 = Option, 3 = Crédit, 4 = Jeu, 5 = Quit
    private const int choixSceneQuit = 5;

    void Start()
    {
        longEcran = Screen.width;
        hautEcran = Screen.height;

        echelle = MethodeStatic.getScale();

        espaceEntreTitre = (hautEcran - nbTitre * hautTitre * echelle.y) / (nbTitre + 1);

        posDepX = 0 - longTitre / 2;
        posMilX = longEcran / 2;
        posFinX = longEcran + longTitre / 2;

        posY = new float[nbTitre];
        float auxPosY = espaceEntreTitre + hautTitre * echelle.y / 2; ;
        for (int i = nbTitre - 1; i >= 0; i--)
        {
            posY[i] = auxPosY;
            auxPosY += espaceEntreTitre + hautTitre * echelle.y;
        }

        List<List<Vector2>> pos = new();
        for (int i = 0; i < nbTitre; i++)
        {
            pos.Add(new List<Vector2>());

            pos[i].Add(new Vector2(posDepX-i*distDecalageTitre, posY[i]));
            pos[i].Add(new Vector2(posMilX, posY[i]));
            pos[i].Add(new Vector2(posFinX+i*distDecalageTitre, posY[i]));
        }

        TitreMenu titrePlay =     new(textPlay, pos[0], this.echelle);
        TitreMenu titreOption =   new(textOption, pos[1], this.echelle);
        TitreMenu titreCredit =   new(textCredit, pos[2], this.echelle);
        TitreMenu titreQuit =     new(textQuit, pos[3], this.echelle);

        listTitre = new List<TitreMenu>
        {
            titrePlay,
            titreOption,
            titreCredit,
            titreQuit
        };

        choixTitre = 0;

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
                    choixTitre = (choixTitre - 1 + nbTitre) % nbTitre;

                if (Input.GetKeyDown(KeyCode.DownArrow))
                    choixTitre = (choixTitre + 1) % nbTitre;

                for (int i = 0; i < nbTitre; ++i)
                {
                    if (Survole(listTitre[i].PosMil))
                    {
                        choixTitre = i;
                        if (Input.GetMouseButtonDown(0))
                        {
                            choixScene = choixTitre + 1;
                            AnimationSortie();
                        }
                    }

                    listTitre[i].SupSelection();
                }

                if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
                {
                    choixScene = choixTitre + 1;
                    AnimationSortie();
                }

                listTitre[choixTitre].Selection();

                break;
            case 3://Sortie des titres du menu
                if (listTitre[0].Traj.AnimFini)
                {
                    if (choixScene == choixSceneQuit)
                        Application.Quit();
                    else
                        SceneManager.LoadScene(choixScene);
                }
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
        for (int i = 0; i < listTitre.Count; i++)//MAJ des positions des titres
        {
            vAux = MethodeStatic.getPositionRect(listTitre[i].Titre);
            vAux.x = listTitre[i].Traj.UpdatePos(listTitre[i].Titre.GetComponent<Transform>().position.x);
            listTitre[i].Titre.GetComponent<RectTransform>().position = vAux;
        }
    }

    public bool Survole(Vector2 posTitre)
    {
        Vector3 mouse = Input.mousePosition;
        return posTitre.x - longTitre / 2 <= mouse.x
            && mouse.x <= posTitre.x + longTitre / 2
            && posTitre.y - hautTitre / 2 <= mouse.y
            && mouse.y <= posTitre.y + hautTitre / 2;
    }

    public void AnimationSortie()
    {
        anim = 3;
        for (int i = 0; i < listTitre.Count; i++)
            listTitre[i].Fin();//Animation de fin
    }
}