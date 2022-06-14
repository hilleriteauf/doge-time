using CSharpSynth.Midi;

namespace Assets.Scripts.MIDI
{
    public class PlayableNote
    {
        public MidiEvent onEvent { get; }
        public MidiEvent offEvent { get; }

        public short Octave;

        public MusicNote ExpectedNote { get; }

        // A définir lorsque le joueur place une note dans le modèle
        private MusicNote _placedNote;
        public MusicNote PlacedNote
        {
            get
            {
                return _placedNote;
            }
            set
            {
                _placedNote = AddDieseToPlacedNote(value);

                if (value == MusicNote.Null)
                {
                    this.onEvent.enabled = false;
                    offEvent.enabled = false;
                    return;
                }
                else
                {
                    onEvent.parameter1 = GetMidiNoteValueFromMusicNote(_placedNote, Octave);
                    offEvent.parameter1 = onEvent.parameter1;
                    onEvent.enabled = true;
                    offEvent.enabled = true;
                }
            }
        }

        public PlayableNote(MidiEvent onEvent, MidiEvent offEvent)
        {
            this.onEvent = onEvent;
            this.offEvent = offEvent;

            Octave = GetOctaveFromMidiNoteValue(onEvent.parameter1);
            ExpectedNote = GetMusicNoteFromMidiNoteValue(onEvent.parameter1);

            PlacedNote = MusicNote.Null;
        }

        private MusicNote AddDieseToPlacedNote(MusicNote placedNote)
        {
            if ((int)ExpectedNote % 10 == 0)
            {
                return placedNote;
            }

            switch (placedNote)
            {
                case MusicNote.Do:
                    return MusicNote.DoDiese;
                case MusicNote.Re:
                    return MusicNote.ReDiese;
                case MusicNote.Fa:
                    return MusicNote.FaDiese;
                case MusicNote.Sol:
                    return MusicNote.SolDiese;
                case MusicNote.La:
                    return MusicNote.LaDiese;
                default:
                    return placedNote;
            }
        }

        private static byte GetMidiNoteValueFromMusicNote(MusicNote MusicNote, short Octave)
        {
            byte value = 0;

            switch (MusicNote)
            {
                case MusicNote.Do:
                    value = 0;
                    break;
                case MusicNote.DoDiese:
                    value = 1;
                    break;
                case MusicNote.Re:
                    value = 2;
                    break;
                case MusicNote.ReDiese:
                    value = 3;
                    break;
                case MusicNote.Mi:
                    value = 4;
                    break;
                case MusicNote.Fa:
                    value = 5;
                    break;
                case MusicNote.FaDiese:
                    value = 6;
                    break;
                case MusicNote.Sol:
                    value = 7;
                    break;
                case MusicNote.SolDiese:
                    value = 8;
                    break;
                case MusicNote.La:
                    value = 9;
                    break;
                case MusicNote.LaDiese:
                    value = 10;
                    break;
                case MusicNote.Si:
                    value = 11;
                    break;
            }

            value += (byte)((Octave + 1) * 12);

            return value;
        }

        private static short GetOctaveFromMidiNoteValue(byte value)
        {
            return (short)((value / 12) - 1);
        }

        private static MusicNote GetMusicNoteFromMidiNoteValue(byte value)
        {
            value %= 12;

            switch (value)
            {
                case 0:
                    return MusicNote.Do;
                case 1:
                    return MusicNote.DoDiese;
                case 2:
                    return MusicNote.Re;
                case 3:
                    return MusicNote.ReDiese;
                case 4:
                    return MusicNote.Mi;
                case 5:
                    return MusicNote.Fa;
                case 6:
                    return MusicNote.FaDiese;
                case 7:
                    return MusicNote.Sol;
                case 8:
                    return MusicNote.SolDiese;
                case 9:
                    return MusicNote.La;
                case 10:
                    return MusicNote.LaDiese;
                case 11:
                    return MusicNote.Si;
                default:
                    return MusicNote.Null;
            }
        }
        
    }
}