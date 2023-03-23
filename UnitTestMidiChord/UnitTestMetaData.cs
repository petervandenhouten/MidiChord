using Microsoft.VisualStudio.TestTools.UnitTesting;
using MidiChord;
using System;

namespace UnitTestMidiChord
{
    [TestClass]
    public class UnitTestMetaData
    {
        [TestMethod]
        public void When_Song_Has_Title_Command_Then_Title_Is_Parsed()
        {
            var parser = new ChordParser();

            string[] text =
            {
                "{title: Name Of the Song}"
            };

            parser.Parse(text);

            Assert.AreEqual("Name Of the Song", parser.Song.Title);

        }

        [TestMethod]
        public void When_Song_Has_No_Title_Command_Then_Title_Is_Empty()
        {
            var parser = new ChordParser();

            string[] text =
            {
                "{label}",
                "C D F E"
            };

            parser.Parse(text);

            Assert.IsTrue( string.IsNullOrEmpty(parser.Song.Title));

        }

    }
}
