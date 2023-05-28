using Microsoft.VisualStudio.TestTools.UnitTesting;
using MidiChord;
using System;

namespace UnitTestMidiChord
{
    [TestClass]
    public class UnitTestParseLineNumbers
    {
        [TestMethod]
        public void When_Chords_Are_Parsed_Then_Output_Have_Correct_LineNumbers()
        {
            var parser = new ChordParser();

            string[] text =
            {
                "C",    // line 0
                "D",    // line 1
                "",
                "Just some text without chords",
                "",
                "F"     // line 5
            };

            var song = parser.Parse(text);

            Assert.IsNotNull(song);
            Assert.AreEqual(3, song.Count);
            Assert.AreEqual(3, parser.GetNumberOfChords());
            Assert.AreEqual(0, song[0].LineNumber);
            Assert.AreEqual(1, song[1].LineNumber);
            Assert.AreEqual(5, song[2].LineNumber);

        }
    }
}
