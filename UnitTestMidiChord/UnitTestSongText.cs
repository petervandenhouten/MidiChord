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

            parser.ParseText(new string[] { "[C] Hello [D] How [F] Are [G] you?" });
            Assert.AreEqual(4, parser.MeasureCount);
            Assert.AreEqual(16, parser.BeatCount);
            Assert.AreEqual(4, parser.GetNumberOfChords());


        }
    }
}
