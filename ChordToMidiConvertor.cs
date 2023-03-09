using Sanford.Multimedia.Midi;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace MidiChord
{
    public class ChordToMidiConvertor : ChordPlayer
    {
        private Sanford.Multimedia.Midi.Sequence _sequence;

        public ChordToMidiConvertor(Dictionary<string, string[]> chordNotes)
            : base(chordNotes)
        {
        }

        private int get_division(int beatsPerMinutes)
        {
            float beatsPerSeconds = (float)beatsPerMinutes / 60;
            float division = 1000 * (1 / beatsPerSeconds);
            return 8333;
        }
        private void create_track_from_song()
        {
            int division = get_division(BeatsPerMinute);

            int beat_delay = GetBeatDelay(BeatsPerMinute);

            _sequence = new Sanford.Multimedia.Midi.Sequence(division);

            Track track = new Track();
            bool track_finised = false;

            while (!track_finised)
            {
                int beat_time = _beatIndex * beat_delay;

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
                                track.Insert(beat_time, midiEvent);
                            }
                        }
                        foreach (var midiEvent in midiChord.NotesOn)
                        {
                            track.Insert(beat_time, midiEvent);
                        }
                        _lastMidiChord = midiChord;
                    }
                }

                if (EnableMetronome == true)
                {
                    track.Insert(beat_time, _metronomeNoteOff.Result);
                    if (_beatIndex % 4 == 0)
                    {
                        track.Insert(beat_time, _metronomeFirstBeatInstrument.Result);
                    }
                    else
                    {
                        track.Insert(beat_time, _metronomeBeatInstument.Result);
                    }
                    track.Insert(beat_time, _metronomeNoteOn.Result);
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
                            track.Insert(beat_time, midiEvent);
                        }
                    }
                    track_finised = true;
                }
            }

            _sequence.Add(track);
        }

        public void Save(string filename)
        {
            create_track_from_song();

            _sequence.Save(filename);
        }
    }
}
