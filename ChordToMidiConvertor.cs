using Sanford.Multimedia.Midi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MidiChord
{
    public class ChordToMidiConvertor : ChordPlayer
    {
        private Sanford.Multimedia.Midi.Sequence _sequence;

        public ChordToMidiConvertor(ChordList chordNotes, DrumList drumNotes)
            : base(chordNotes, drumNotes)
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
            Track track_drum     = new Track();
            bool track_finised = false;
            
            while (!track_finised)
            {
                int beat_time = _beatIndex * division;

                track_chords    .Insert(0, _chordMetaText);
                track_metronome .Insert(0, _metronomeMetaText);
                track_drum      .Insert(0, _drumMetaText);

                track_chords    .Insert(1, GetMidiProgram(SongInstrument));
                //track_drum      .Insert(1, _drumInstument);

                // find ALL notes on the beat index
                foreach (var entry in _data)
                {
                    if (entry.Type == SongItem.SongItemType.MEASURE_CHORD ||
                        entry.Type == SongItem.SongItemType.BEAT_CHORD)
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
                    else if (entry.Type == SongItem.SongItemType.START_DRUM_PATTERN)
                    {
                        DrumPattern drumPattern = GetDrumPattern(entry.Data);

                        if (drumPattern != null)
                        {
                            if (entry.BeatIndex <= _beatIndex)
                            {
                                // Activate pattern
                                int drumPatternIndex = getDrumIndex(entry.ItemInstrument);
                                if (drumPatternIndex >= 0)
                                {
                                    _drumPatterns[drumPatternIndex] = drumPattern;
                                }
                            }
                        }
                    }
                    else if (entry.Type == SongItem.SongItemType.STOP_DRUM_PATTERN)
                    {
                        if (entry.BeatIndex <= _beatIndex)
                        {
                            // deactivate
                            int drumPatternIndex = getDrumIndex(entry.ItemInstrument);
                            if (drumPatternIndex >= 0)
                            {
                                _drumPatterns[drumPatternIndex] = null;
                            }
                        }
                    }

                }

                createDrumTrack(track_drum, beat_time);

                if (EnableMetronome == true)
                {
                    track_metronome.Insert(beat_time, _metronomeNoteOff);
                    if (_beatIndex % 4 == 0)
                    {
                        track_metronome.Insert(beat_time, _metronomeFirstBeatInstrument);
                    }
                    else
                    {
                        track_metronome.Insert(beat_time, _metronomeBeatInstument);
                    }
                    track_metronome.Insert(beat_time, _metronomeNoteOn);
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
            _sequence.Add(track_drum);
            _sequence.Add(track_metronome);
        }

        private void createDrumTrack(Track drum_track, int beattime)
        {
            for (int drum = 0; drum < 3; drum++)
            {
                if (_drumPatterns[drum] != null)
                {
                    int beat_in_measure = (base._beatIndex % 4);
                    if (beat_in_measure < _drumPatterns[drum].NotesOn.Count)
                    {
                        var midiEvent = _drumPatterns[drum].NotesOn[beat_in_measure];
                        if (midiEvent != null)
                        {
                            drum_track.Insert(beattime, midiEvent);
                            Debug.WriteLine("Insert drum event: " + midiEvent.Command.ToString());
                        }
                    }
                    if (beat_in_measure < _drumPatterns[drum].NotesOff.Count)
                    {
                        var midiEvent = _drumPatterns[drum].NotesOff[beat_in_measure];
                        if (midiEvent != null)
                        {
                            drum_track.Insert(beattime+3, midiEvent);
                            Debug.WriteLine("Insert drum event: " + midiEvent.Command.ToString());
                        }
                    }
                }
            }
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
