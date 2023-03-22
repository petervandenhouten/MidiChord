using Sanford.Multimedia.Midi;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MidiChord
{
    public class ChordToMidiConvertor : ChordPlayer
    {
        private Sanford.Multimedia.Midi.Sequence _sequence;

        public ChordToMidiConvertor(ChordList chordNotes)
            : base(chordNotes)
        {
        }

        private int get_division()
        {
            // Minimum PPQN value is 24, aka resolution of midifile
            return 24;
        }
        private void create_track_from_song()
        {
            int division = get_division(); 

            int beat_delay = GetBeatTimeInMs(BeatsPerMinute);

            _sequence = new Sanford.Multimedia.Midi.Sequence(division);

            Track track_chords    = new Track();
            Track track_metronome = new Track();
            bool track_finised = false;
            
            while (!track_finised)
            {
                int beat_time = _beatIndex * division;

                track_chords.Insert(0, GetMidiProgram(SongInstrument));

                // find ALL notes on the beat index
                foreach (var entry in _data)
                {
                    MidiChord midiChord = GetMidiChord(entry.Data);

                    if (entry.BeatIndex == _beatIndex)
                    {
                        if (_lastMidiChord != null)
                        {
                            foreach (var midiEvent in _lastMidiChord.NotesOff)
                            {
                                track_chords.Insert(beat_time, midiEvent);
                            }
                        }
                        foreach (var midiEvent in midiChord.NotesOn)
                        {
                            track_chords.Insert(beat_time, midiEvent);
                        }
                        _lastMidiChord = midiChord;
                    }
                }

                if (EnableMetronome == true)
                {
                    track_metronome.Insert(beat_time, _metronomeNoteOff.Result);
                    if (_beatIndex % 4 == 0)
                    {
                        track_metronome.Insert(beat_time, _metronomeFirstBeatInstrument.Result);
                    }
                    else
                    {
                        track_metronome.Insert(beat_time, _metronomeBeatInstument.Result);
                    }
                    track_metronome.Insert(beat_time, _metronomeNoteOn.Result);
                }

                //// Notify client
                //if (BeatTick != null)
                //{
                //    BeatTick(_beatIndex, _lastBeatIndex, parserPosition);
                //}

                base._beatIndex++;

                if (_beatIndex > _lastBeatIndex)
                {
                    if (_lastMidiChord != null)
                    {
                        foreach (var midiEvent in _lastMidiChord.NotesOff)
                        {
                            track_chords.Insert(beat_time, midiEvent);
                        }
                    }
                    track_finised = true;
                }
            }

            _sequence.Add(track_chords);
            _sequence.Add(track_metronome);
        }

        public void Save(string filename)
        {
            create_track_from_song();

            try
            {
                _sequence.Save(filename);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
