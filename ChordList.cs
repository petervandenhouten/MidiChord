using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MidiChord
{
    public class ChordList
    {
        private Dictionary<string, string[]> _chordNotes;
        const string chordsFilename = @"chords.txt";

        public ChordList()
        {
            bool loaded = false;
            if ( File.Exists(chordsFilename) ) 
            {
                loaded = LoadChords(chordsFilename);
            }
            if (!loaded)
            {
                _chordNotes = getNotesOfChords();
            }
        }

        public string[] GetAllChords()
        {
            return _chordNotes.Keys.ToArray();
        }

        //public string[] GetChord(string chord)
        //{
        //    if ( ContainsChord(chord))
        //    {
        //        return _chordNotes[chord];
        //    }
        //    return null; // or string[] ?
        //}

        public string[] GetChord(string chord, int transpose=0)
        {
            if (ContainsChord(chord))
            {
                return _chordNotes[chord];
            }
            return null; // or string[] ?
        }

        public bool ContainsChord(string chord)
        {
            if (string.IsNullOrEmpty(chord)) return false;
            return _chordNotes.ContainsKey(chord);
        }

        private bool LoadChords(string chordsFilename)
        {
            if (_chordNotes == null)
            {
                _chordNotes = new Dictionary<string, string[]>();
            }
            _chordNotes.Clear();

            try
            {
                System.IO.TextReader readFile = new StreamReader(chordsFilename);
                string txt = readFile.ReadToEnd();

                readFile.Close();
                readFile = null;

                // parse chords
                string[] lines = txt.Split('\n');

                foreach (string line in lines)
                {
                    string cleanLine = line.Replace("\r", "").Replace("\n", "").Trim();

                    if (!string.IsNullOrWhiteSpace(cleanLine))
                    {
                        string chordName = cleanLine.Split(':')[0];
                        string notes = cleanLine.Split(':')[1];

                        string[] noteArray = notes.Split(',');
                        List<string> cleanNoteArray = new List<string>();
                        foreach (string str in noteArray)
                        {
                            cleanNoteArray.Add(str.Replace('\r', ' ').Trim());
                        }

                        _chordNotes.Add(chordName, cleanNoteArray.ToArray());
                    }
                }

            }
            catch (IOException ex)
            {
                MessageBox.Show("Cannot read chords.txt file. " + ex.Message);
                return false;
            }

            return true;
        }


        private Dictionary<string, string[]> getNotesOfChords()
        {
            return new Dictionary<string, string[]>
            {
                ["A"] = new string[] { "A", "C#", "E" },
                ["A7"] = new string[] { "A", "C#", "E", "G" },
                ["Am"] = new string[] { "A", "C", "E" },
                ["Am7"] = new string[] { "A", "C", "E", "G" },

                ["A#"] = new string[] { "Bb", "D", "F" },
                ["Bb"] = new string[] { "Bb", "D", "F" },

                ["B"] = new string[] { "A", "D#", "F#" },

                ["C"] = new string[] { "C", "E", "G" },
                ["C7"] = new string[] { "C", "E", "G", "Bb" },

                ["C#"] = new string[] { "Db", "F", "Ab" },
                ["Db"] = new string[] { "Db", "F", "Ab" },

                ["D"] = new string[] { "D", "F#", "A" },
                ["D7"] = new string[] { "D", "F#", "A", "C" },

                ["D#"] = new string[] { "Eb", "G", "Bb" },
                ["Eb"] = new string[] { "Eb", "G", "Bb" },

                ["E"] = new string[] { "E", "G#", "B" },
                ["Em"] = new string[] { "E", "G", "B" },

                ["F"] = new string[] { "F", "A", "C" },
                ["Fm"] = new string[] { "F", "Ab", "C" },

                ["F#"] = new string[] { "F#", "A#", "C#" },
                ["Gb"] = new string[] { "F#", "A#", "C#" },

                ["G"] = new string[] { "G", "B", "D" },

                ["G#"] = new string[] { "Ab", "C", "Eb" },
                ["Ab"] = new string[] { "Ab", "C", "Eb" },

                ["Do"] = new string[] { "C", "E", "G" }
            };
        }

        internal string GetChordsInOneString()
        {
            string oneline = "";
            foreach(var chord in _chordNotes.Keys)
            {
                oneline += chord + " ";
            }
            return oneline;
        }
    }
}
