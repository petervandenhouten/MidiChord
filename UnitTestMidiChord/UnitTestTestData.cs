using Microsoft.VisualStudio.TestTools.UnitTesting;
using MidiChord;
using System;
using System.Runtime.InteropServices;
using System.Security.Policy;

namespace UnitTestMidiChord
{
    [TestClass]
    public class UnitTestTestData
    {
        [TestMethod]
        public void TestData_Intro_Time_Aint_Nothing()
        {
            var parser = new ChordParser();

            string[] text =
            {
                "{Intro}",
                "G D Em C G D",
                "",
                "{ Verse 1 }",
                "G            |D  * Dsus4  D|",
                "Walking down dusty roads"
            };

            parser.Parse(text);

            Assert.AreEqual(2, parser.GetNumberOfLabelledParts());
            Assert.AreEqual(10, parser.GetNumberOfChords());
        }

        [TestMethod]
        public void TestData_SongText_After_Chord()
        {
            var parser = new ChordParser();

            string[] text =
            {
                "[D G C D] Hallo How are you?",
            };

            parser.Parse(text);

            Assert.AreEqual(4, parser.GetNumberOfChords());
        }

        [TestMethod]
        public void TestData_Riders_On_The_Storm()
        {
            var parser = new ChordParser();

            string[] text =
            {
                "[Em] Riders on the[A] storm[Em] [A]",
                "[Em] Riders on the[A] storm[Em] [A]",
                "In-[Am]to this house we're born [C] [D]",
                "In-[Em] to this world we're [A] thrown [Em] [A]",
                "Like a [D] dog without a bone",
                "An[C] actor out on loan",
                "[Em] Riders on the[A] storm [Em][A]"
            };

            parser.Parse(text);

            Assert.AreEqual(21, parser.GetNumberOfChords());
        }

    }
}
