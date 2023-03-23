using Microsoft.VisualStudio.TestTools.UnitTesting;
using MidiChord;

namespace UnitTestMidiChord
{
    [TestClass]
    public class UnitTestInstrument
    {
        [TestMethod]
        public void When_Parser_Is_Created_Then_AcousticGrandPiano_Is_The_Default_Instrument()
        {
            var parser = new ChordParser();
            Assert.AreEqual("AcousticGrandPiano", parser.Instrument);
        }

        [TestMethod]
        public void When_Data_Has_Instrument_Command_Then_Name_Of_Instrument_Is_Found()
        {
            var parser = new ChordParser();

            parser.Parse(new string[] { "{instrument: Marimba}" });
            Assert.AreEqual("Marimba", parser.Instrument);
            Assert.AreEqual(1, parser.GetNumberOfInstrumentChanges());
        }

        [TestMethod]
        public void When_Data_Has_Command_With_Unknown_Instrument_Then_Name_Of_Instrument_Is_Not_Found()
        {
            var parser = new ChordParser();

            parser.Parse(new string[] { "{instrument: X}" });
            Assert.AreEqual("AcousticGrandPiano", parser.Instrument);
            Assert.AreEqual(0, parser.GetNumberOfInstrumentChanges());
        }

    }
}
