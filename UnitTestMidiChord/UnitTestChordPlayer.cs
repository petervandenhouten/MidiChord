using Microsoft.VisualStudio.TestTools.UnitTesting;
using MidiChord;
using System;

namespace UnitTestMidiChord
{
    [TestClass]
    public class UnitTestChordPlayer
    {
        [TestMethod]
        public void When_Song_Is_Played_Then_Beats_Have_Correct_Chords()
        {
            var parser = new ChordParser();

            string[] text =
            {
                "C D F G"
            };

            var song = parser.Parse(text);
            Assert.IsNotNull(song);
            Assert.AreEqual(4, song.Count);

            Assert.AreEqual(0, song[0].BeatIndex);
            Assert.AreEqual(4, song[1].BeatIndex);
            Assert.AreEqual(8, song[2].BeatIndex);
            Assert.AreEqual(12, song[3].BeatIndex);

            var player = new ChordPlayer(new ChordList(), new DrumList());
            player.SetSong(song);

            var m0 = player.GetMidiCommandsAtBeat(0);
            var m4 = player.GetMidiCommandsAtBeat(4);
            var m8 = player.GetMidiCommandsAtBeat(8);
            var m12 = player.GetMidiCommandsAtBeat(12);

            Assert.AreEqual("C", m0.ChordName);
            Assert.AreEqual("D", m4.ChordName);
            Assert.AreEqual("F", m8.ChordName);
            Assert.AreEqual("G", m12.ChordName);

        }

        [TestMethod]
        public void When_Song_Is_Played_Then_Beats_Have_Correct_LineNumbers()
        {
            var parser = new ChordParser();

            string[] text =
            {
                "C D",          // line 0
                "{chorus}",     // line 1
                "# Comment",    // line 2
                "E",            // line 3
                "F",            // line 4
            };

            var song = parser.Parse(text);
            Assert.IsNotNull(song);
            Assert.AreEqual(4, song.Count);

            Assert.AreEqual(0, song[0].BeatIndex);
            Assert.AreEqual(4, song[1].BeatIndex);
            Assert.AreEqual(8, song[2].BeatIndex);
            Assert.AreEqual(12, song[3].BeatIndex);

            var player = new ChordPlayer(new ChordList(), new DrumList());
            player.SetSong(song);

            var m0 = player.GetMidiCommandsAtBeat(0);
            var m4 = player.GetMidiCommandsAtBeat(4);
            var m8 = player.GetMidiCommandsAtBeat(8);
            var m12 = player.GetMidiCommandsAtBeat(12);

            Assert.AreEqual(0, m0.ParserLine);
            Assert.AreEqual(0, m4.ParserLine);
            Assert.AreEqual(3, m8.ParserLine);
            Assert.AreEqual(4, m12.ParserLine);

            Assert.AreEqual("C", m0.ChordName);
            Assert.AreEqual("D", m4.ChordName);
            Assert.AreEqual("E", m8.ChordName);
            Assert.AreEqual("F", m12.ChordName);
        }

        [TestMethod]
        public void When_Song_With_Drums_Is_Played_Then_Beats_Have_Correct_LineNumbers()
        {
            var parser = new ChordParser();

            string[] text =
            {
                "C D",                  // line 0
                "{drum left: b b b b}", // line 1
                "{chorus}",             // line 2
                "# Comment",            // line 3
                "E",                    // line 4
                "F",                    // line 5
            };

            var song = parser.Parse(text);
            Assert.IsNotNull(song);
            Assert.AreEqual(5, song.Count);
            Assert.AreEqual(4, parser.GetNumberOfChords());
            Assert.AreEqual(1, parser.GetNumberOfDrumPatterns());

            Assert.AreEqual(0, song[0].BeatIndex);
            Assert.AreEqual(4, song[1].BeatIndex);
            Assert.AreEqual(8, song[2].BeatIndex); // the entry for the drum
            Assert.AreEqual(8, song[3].BeatIndex);
            Assert.AreEqual(12, song[4].BeatIndex);

            var player = new ChordPlayer(new ChordList(), new DrumList());
            player.SetSong(song);
            Assert.AreEqual(15, player.LastBeatIndex);

            var m0 = player.GetMidiCommandsAtBeat(0);
            var m1 = player.GetMidiCommandsAtBeat(1);
            var m4 = player.GetMidiCommandsAtBeat(4);
            var m7 = player.GetMidiCommandsAtBeat(7);
            var m8 = player.GetMidiCommandsAtBeat(8);
            var m9 = player.GetMidiCommandsAtBeat(9);
            var m12 = player.GetMidiCommandsAtBeat(12);
            var m15 = player.GetMidiCommandsAtBeat(15);

            Assert.AreEqual(0, m0.ParserLine);
            Assert.AreEqual(0, m1.ParserLine);
            Assert.AreEqual(0, m4.ParserLine);
            Assert.AreEqual(0, m7.ParserLine);
            Assert.AreEqual(4, m8.ParserLine);
            Assert.AreEqual(4, m9.ParserLine);
            Assert.AreEqual(5, m12.ParserLine);
            Assert.AreEqual(5, m15.ParserLine);
        }

        [TestMethod]
        public void When_Song_Is_Played_Then_Beats_Have_Correct_Label()
        {
            var parser = new ChordParser();

            string[] text =
            {
                "C",
                "{verse}",
                "E",
                "D",
                "{chorus}",
                "F",       
            };

            var song = parser.Parse(text);
            Assert.IsNotNull(song);
            Assert.AreEqual(4, song.Count);

            Assert.AreEqual(0, song[0].BeatIndex);
            Assert.AreEqual(4, song[1].BeatIndex);
            Assert.AreEqual(8, song[2].BeatIndex);
            Assert.AreEqual(12, song[3].BeatIndex);

            var player = new ChordPlayer(new ChordList(), new DrumList());
            player.SetSong(song);
            Assert.AreEqual(15, player.LastBeatIndex);

            var m0 = player.GetMidiCommandsAtBeat(0);
            var m1 = player.GetMidiCommandsAtBeat(1);
            var m4 = player.GetMidiCommandsAtBeat(4);
            var m5 = player.GetMidiCommandsAtBeat(5);
            var m8 = player.GetMidiCommandsAtBeat(8);
            var m9 = player.GetMidiCommandsAtBeat(9);
            var m12 = player.GetMidiCommandsAtBeat(12);
            var m13 = player.GetMidiCommandsAtBeat(13);

            Assert.AreEqual("",         m0.Part);
            Assert.AreEqual("",         m1.Part);
            Assert.AreEqual("verse",    m4.Part);
            Assert.AreEqual("verse",    m5.Part);
            Assert.AreEqual("verse",    m8.Part);
            Assert.AreEqual("verse",    m9.Part);
            Assert.AreEqual("chorus",   m12.Part);
            Assert.AreEqual("chorus",   m13.Part);
        }

    }
}
