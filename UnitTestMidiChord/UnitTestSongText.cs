using Microsoft.VisualStudio.TestTools.UnitTesting;
using MidiChord;
using System;

namespace UnitTestMidiChord
{
    [TestClass]
    public class UnitTestSongText
    {
        [TestMethod]
        public void When_SongText_Then_Chords_Are_Detected()
        {
            var parser = new ChordParser();

            parser.Parse(new string[] { "[C] Hello [D] How [F] Are [G] you?" });
            Assert.AreEqual(4, parser.MeasureCount);
            Assert.AreEqual(16, parser.BeatCount);
            Assert.AreEqual(4, parser.GetNumberOfChords());


        }

        [TestMethod]
        public void When_SongText_Has_Comments_Then_Part_Is_Ignored()
        {
            var parser = new ChordParser();

            parser.Parse(new string[] { "[C] Hello /[D] How [F]/ Are [G] you?" });
            Assert.AreEqual(2, parser.GetNumberOfChords());
        }

        [TestMethod]
        public void When_Start_Of_SongText_Has_Comments_Then_Start_Is_Ignored()
        {
            var parser = new ChordParser();

            parser.Parse(new string[] { "/[C] Hello /[D] How [F] Are [G] you?" });
            Assert.AreEqual(3, parser.GetNumberOfChords());
        }


        [TestMethod]
        public void When_End_Of_SongText_Has_Comments_Then_End_Is_Ignored()
        {
            var parser = new ChordParser();

            parser.Parse(new string[] { "[C] Hello [D] How /[F] Are [G] you?/" });
            Assert.AreEqual(2, parser.GetNumberOfChords());
        }

    }
}
