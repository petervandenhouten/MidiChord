﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using MidiChord;
using System;

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
                "G            [D  * Dsus4  D]",
                "Walking down dusty roads"
            };

            parser.ParseText(text);

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

            parser.ParseText(text);

            Assert.AreEqual(4, parser.GetNumberOfChords());
        }

    }
}
