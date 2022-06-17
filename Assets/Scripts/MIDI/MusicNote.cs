using System.Collections;
using UnityEngine;

namespace Assets.Scripts.MIDI
{
    public enum MusicNote : short
    {
        Null = -1,
        Do = 0,
        DoDiese = 1,
        Re = 10,
        ReDiese = 11,
        Mi = 20,
        Fa = 30,
        FaDiese = 31,
        Sol = 40,
        SolDiese = 41,
        La = 50,
        LaDiese = 51,
        Si = 60
    }
}