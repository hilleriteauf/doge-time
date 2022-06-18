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

    //Ecran avec (0,0) en bas à gauche
    private const float longEcran = 1920;
    private const float hautEcran = 1080;

    //Titre avec (0,0) au milieu
    private const float longTitre = 900;
    private const float hautTitre = 150;

    private const int nbTitre = 4;
    private const float espaceEntreTitre = (hautEcran - nbTitre * hautTitre) / (nbTitre + 1);

    private float[] posY;

    private const float posDepX = 0 - longTitre / 2;
    private const float posMilX = longEcran / 2;
    private const float posFinX = longEcran + longTitre / 2;

    private float distDecalageTitre = 700;

    List<TitreMenu> listTitre;

    private int anim;// 1 = départ, 2 = en attente, 3 = fin

    private int choixScene;// 0 = Menu, 1 = Jeu, 2 = Option, 3 = Crédit, 4 = Quit

    void Start()
    {
        posY = new float[nbTitre];
        float auxPosY = espaceEntreTitre + hautTitre / 2; ;
        for (int i = nbTitre - 1; i >= 0; i--)
        {
            posY[i] = auxPosY;
            auxPosY += espaceEntreTitre + hautTitre;
        }

        List<List<Vector2>> pos = new();
        for (int i = 0; i < nbTitre; i++)
        {
            pos.Add(new List<Vector2>());

            pos[i].Add(new Vector2(posDepX-i*distDecalageTitre, posY[i]));
            pos[i].Add(new Vector2(posMilX, posY[i]));
            pos[i].Add(new Vector2(posFinX+i*distDecalageTitre, posY[i]));
        }

        TitreMenu titrePlay =     new(textPlay, pos[0]);
        TitreMenu titreOption =   new(textOption, pos[1]);
        TitreMenu titreCredit =   new(textCredit, pos[2]);
        TitreMenu titreQuit =     new(textQuit, pos[3]);

        listTitre = new List<TitreMenu>
        {
            titrePlay,
            titreOption,
            titreCredit,
            titreQuit
        };

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
                break;
            case 3://Sortie des titres du menu
                if (listTitre[0].Traj.AnimFini)
                {
                    if (choixScene == 4)
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
            vAux = MethodeStatic.getPositionRect(listTitre[i].GObject);
            vAux.x = listTitre[i].Traj.UpdatePos(listTitre[i].GObject.GetComponent<Transform>().position.x);
            listTitre[i].GObject.GetComponent<RectTransform>().position = vAux;
        }
    }

    public void ActionPlay()
    {
        choixScene = 1;
        AnimationSortie();
    }

    public void ActionOption()
    {
        choixScene = 2;
        AnimationSortie();
    }

    public void ActionCredit()
    {
        choixScene = 3;
        AnimationSortie();
    }

    public void ActionQuit()
    {
        choixScene = 4;
        AnimationSortie();
    }

    public void AnimationSortie()
    {
        anim = 3;
        for (int i = 0; i < listTitre.Count; i++)
            listTitre[i].Fin();//Animation de fin
    }
}