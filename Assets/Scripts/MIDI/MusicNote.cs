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

    public abstract class MusicNoteHelper
    {
        public static Color GetMusicNoteColor(MusicNote musicNote)
        {
            switch ((int)musicNote / 10)
            {
                case 0:
                    return new Color(1f, 0f, 0f);
                case 1:
                    return new Color(0.7333f, 0.4470f, 0.0039f);
                case 2:
                    return new Color(1f, 0.7490f, 0.3686f);
                case 3:
                    return new Color(0.6117f, 0.9333f, 0.01960f);
                case 4:
                    return new Color(0.0196f, 0.6941f, 0.9921f);
                case 5:
                    return new Color(0.4274f, 0.0039f, 0.5843f);
                case 6:
                    return new Color(1f, 0f, 0.9960f);
                default:
                    return Color.white;

            }
        }
    }
}