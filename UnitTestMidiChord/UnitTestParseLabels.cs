using Microsoft.VisualStudio.TestTools.UnitTesting;
using MidiChord;
using System.CodeDom;

namespace UnitTestMidiChord
{
    [TestClass]
    public class UnitTestParseLabels
    {
        [TestMethod]
        public void When_Labels_Are_Parsed_Then_Number_Is_As_Expected()
        {
            var parser = new ChordParser();

            string[] text =
            {
                "{intro}",
                "{verse 1}"
            };

            parser.Parse(text);

            Assert.AreEqual(2, parser.GetNumberOfLabels());
        }

        [TestMethod]
        public void When_Chords_Are_Parsed_Then_Items_Have_Correct_Labels()
        {
            var parser = new ChordParser();

            string[] text =
            {
                "{intro}",
                "C",
                "{verse 1}",
                "D"
            };

            var song = parser.Parse(text);

            Assert.IsNotNull(song);
            Assert.AreEqual(2, parser.GetNumberOfLabels());
            Assert.AreEqual(2, song.Count);
            Assert.AreEqual("intro", song[0].Part);
            Assert.AreEqual("verse 1", song[1].Part);


        }

    }
}
