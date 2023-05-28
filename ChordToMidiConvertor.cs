using Sanford.Multimedia.Midi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Reflection;
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
            SendNoteOffForDrums = true;
        }

        private int get_division()
        {
            // Minimum PPQN value is 24, aka resolution of midifile
            return 24;
        }
        private void create_track_from_song()
        {
            int division = get_division(); 

            //int beat_delay = GetBeatTimeInMs(BeatsPerMinute);

            _sequence = new Sanford.Multimedia.Midi.Sequence(division);

            Track track_chords    = new Track();
            Track track_metronome = new Track();
            Track track_drum      = new Track();
            bool track_finised = false;
            
            while (!track_finised)
            {
                int beat_time = CurrentBeatIndex * division;

                track_chords    .Insert(0, _chordMetaText    );
                track_metronome .Insert(0, _metronomeMetaText);
                track_drum      .Insert(0, _drumMetaText     );

                track_chords.Insert(1, GetDefaultInstrument());
                //track_drum      .Insert(1, _drumInstument);

                var midi = GetMidiCommandsAtBeat(CurrentBeatIndex);
                saveMidiCommands(track_chords,      beat_time, midi.MetaData    );
                saveMidiCommands(track_chords,      beat_time, midi.Chords      );
                saveMidiCommands(track_metronome,   beat_time, midi.Metronome   );
                saveMidiDrumCommands(track_drum,    beat_time, midi.Drums       );

                NextBeatIndex();

                if (midi.EndOfSong)
                {
                    track_finised = true;
                }
            }

            _sequence.Add(track_chords);
            _sequence.Add(track_drum);
            _sequence.Add(track_metronome);
        }

        private void saveMidiDrumCommands(Track track, int beattime, List<IMidiMessage> midi)
        {
            foreach (var cmd in midi)
            {
                int note_off_delay = 0;

                var message = cmd as ChannelMessage;
                if ( message != null )
                {
                    if ( message.Command == ChannelCommand.NoteOff )
                    {
                        note_off_delay = 3;
                    }
                }
                track.Insert(beattime+ note_off_delay, cmd);
            }
        }

        private void saveMidiCommands(Track track, int beattime, List<IMidiMessage> midi)
        {
            foreach (var cmd in midi)
            {
                track.Insert(beattime, cmd);
            }
        }

        private IMidiMessage GetDefaultInstrument()
        {
            var factory = new MidiCommandFactory();
            return factory.Instrument(SongInstrument);
        }

        //private void createDrumTrack(Track drum_track, int beattime)
        //{
        //    for (int drum = 0; drum < 3; drum++)
        //    {
        //        if (_drumPatterns[drum] != null)
        //        {
        //            int beat_in_measure = (CurrentBeatIndex % 4);
        //            if (beat_in_measure < _drumPatterns[drum].NotesOn.Count)
        //            {
        //                var midiEvent = _drumPatterns[drum].NotesOn[beat_in_measure];
        //                if (midiEvent != null)
        //                {
        //                    drum_track.Insert(beattime, midiEvent);
        //                    Debug.WriteLine("Insert drum event: " + midiEvent.Command.ToString());
        //                }
        //            }
        //            if (beat_in_measure < _drumPatterns[drum].NotesOff.Count)
        //            {
        //                var midiEvent = _drumPatterns[drum].NotesOff[beat_in_measure];
        //                if (midiEvent != null)
        //                {
        //                    drum_track.Insert(beattime+3, midiEvent);
        //                    Debug.WriteLine("Insert drum event: " + midiEvent.Command.ToString());
        //                }
        //            }
        //        }
        //    }
        //}

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
