using Microsoft.VisualStudio.TestTools.UnitTesting;
using MidiChord;
using System;

namespace UnitTestMidiChord
{
    [TestClass]
    public class UnitTestComments
    {
        [TestMethod]
        public void When_Line_Is_Commented_Then_Line_Is_Ignored()
        {
            var parser = new ChordParser();

            string[] text =
            {
                "# Intro}",
                "# G D Em C",
                "G  A B A",
                "Hallo, how are you?"
            };

            parser.Parse(text);

            Assert.AreEqual(4, parser.GetNumberOfChords());
        }

        [TestMethod]
        public void When_Lines_Are_Commented_Then_Lines_Are_Ignored()
        {
            var parser = new ChordParser();

            string[] text =
            {
                "G D",
                "/",
                "A C D B",
                "/",
                "Walking down dusty roads",
                "C E D E"
            };

            parser.Parse(text);

            Assert.AreEqual(6, parser.GetNumberOfChords());
        }

        [TestMethod]
        public void When_Part_Of_Line_Is_Commented_Then_Part_Of_Line_Is_Ignored()
        {
            var parser = new ChordParser();

            string[] text =
            {
                "G D E / A B C / D",
                "Walking down dusty roads"
            };

            parser.Parse(text);

            Assert.AreEqual(4, parser.GetNumberOfChords());
        }

        [TestMethod]
        public void When_Start_Of_Line_Is_Commented_Then_Start_Part_Of_Line_Is_Ignored()
        {
            var parser = new ChordParser();

            string[] text =
            {
                "/ A B C D / D C",
                "Walking down dusty roads"
            };

            parser.Parse(text);

            Assert.AreEqual(2, parser.GetNumberOfChords());
        }

        [TestMethod]
        public void When_End_Of_Line_Is_Commented_At_Then_End_Of_Line_Is_Ignored()
        {
            var parser = new ChordParser();

            string[] text =
            {
                "A B C / D D C E /",
                "Walking down dusty roads"
            };

            parser.Parse(text);

            Assert.AreEqual(3, parser.GetNumberOfChords());
        }

        [TestMethod]
        public void When_Lines_And_Parts_Are_Commented_Then_Lines_And_Parts_Are_Ignored()
        {
            var parser = new ChordParser();

            string[] text =
            {
                "/",
                "{ Intro}",
                "G D Em C G D",
                "/",
                "",
                "",
                "{Verse 1}",
                "G D     / D D /",
                "Walking down dusty roads",
                "C"
            };

            parser.Parse(text);

            Assert.AreEqual(3, parser.GetNumberOfChords());
        }

    }
}
