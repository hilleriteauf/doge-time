using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public Camera cam;

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

        TitreMenu titrePlay =     new(cam, textPlay, pos[0]);
        TitreMenu titreOption =   new(cam, textOption, pos[1]);
        TitreMenu titreCredit =   new(cam, textCredit, pos[2]);
        TitreMenu titreQuit =     new(cam, textQuit, pos[3]);

        listTitre = new List<TitreMenu>
        {
            titrePlay,
            titreOption,
            titreCredit,
            titreQuit
        };

        anim = 1;
    }

    // Update is called once per frame
    void Update()
    {
        switch (anim)
        {
            case 1:
                /*if (listTitre[0].Traj.AnimFini)
                    anim = 2;
                else
                {*/
                Vector2 vAux;
                for (int i = 0; i < listTitre.Count; i++)//MAJ des positions des titres
                {
                    vAux = MethodeStatic.getPositionRect(listTitre[i].GObject);
                    vAux.x = listTitre[i].Traj.UpdatePos(listTitre[i].GObject.GetComponent<Transform>().position.x);
                    listTitre[i].GObject.GetComponent<RectTransform>().position = vAux;
                }
                //}
                break;
            /*case 2:
                if (true)
                {
                    anim = 3;
                    for (int i = 0; i < listTitre.Count; i++)
                        listTitre[i].Fin();//Animation de fin
                }
                break;
            case 3:
                if (listTitre[0].Traj.AnimFini)
                    anim = 1;//TODO envoyer vers la bonne scène
                else
                {
                    for (int i = 0; i < listTitre.Count; i++)//MAJ des positions des titres
                        listTitre[i].GObject.GetComponent<Transform>().position = listTitre[i].Traj.UpdatePos(listTitre[i].GObject.GetComponent<Transform>().position);
                }
                break;*/
            default:
                throw new Exception("Erreur animation titre menu");
        }
    }
}