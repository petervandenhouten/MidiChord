using Microsoft.VisualStudio.TestTools.UnitTesting;
using MidiChord;
using System;

namespace UnitTestMidiChord
{
    [TestClass]
    public class UnitTestChords
    {
        [TestMethod]
        public void When_Line_With_Only_Chords_Then_Chords_Are_Parsed_As_Whole_Notes()
        {
            var parser = new ChordParser();

            parser.Parse(new string[] { "C D F G" });
            Assert.AreEqual(4, parser.MeasureCount);
            Assert.AreEqual(16, parser.BeatCount);
            Assert.AreEqual(4, parser.GetNumberOfChords());

            parser.Parse(new string[] { "C * F G" });
            Assert.AreEqual(4, parser.MeasureCount);
            Assert.AreEqual(16, parser.BeatCount);
            Assert.AreEqual(3, parser.GetNumberOfChords());
        }

        [TestMethod]
        public void When_Line_With_Chords_Inside_Brackets_Then_Chords_Are_Parsed_As_Quarter_Notes()
        {
            var parser = new ChordParser();

            parser.Parse(new string[] { "|C D F G|" });
            Assert.AreEqual(1, parser.MeasureCount);
            Assert.AreEqual(4, parser.BeatCount);
            Assert.AreEqual(4, parser.GetNumberOfChords());

            parser.Parse(new string[] { "|C D * G|" });
            Assert.AreEqual(1, parser.MeasureCount);
            Assert.AreEqual(4, parser.BeatCount);
            Assert.AreEqual(3, parser.GetNumberOfChords());
        }

        [TestMethod]
        public void When_Line_With_Chords_Inside_And_Outside_Brackets_Then_Chords_Are_Parsed_As_Whole_And_Quarter_Notes()
        {
            var parser = new ChordParser();

            parser.Parse(new string[] { "|C D F G| C D A B" });
            Assert.AreEqual(5, parser.MeasureCount);
            Assert.AreEqual(20, parser.BeatCount);
            Assert.AreEqual(8, parser.GetNumberOfChords());

            parser.Parse(new string[] { "C D A B |C D F G|" });
            Assert.AreEqual(5, parser.MeasureCount);
            Assert.AreEqual(20, parser.BeatCount);
            Assert.AreEqual(8, parser.GetNumberOfChords());

            parser.Parse(new string[] { "C D |C D F G| A B" });
            Assert.AreEqual(5, parser.MeasureCount);
            Assert.AreEqual(20, parser.BeatCount);
            Assert.AreEqual(8, parser.GetNumberOfChords());

            parser.Parse(new string[] { "C D |C D| A" });
            Assert.AreEqual(4, parser.MeasureCount);
            Assert.AreEqual(14, parser.BeatCount);
            Assert.AreEqual(5, parser.GetNumberOfChords());
        }
    }
}