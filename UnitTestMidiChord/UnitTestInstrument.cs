using Microsoft.VisualStudio.TestTools.UnitTesting;
using MidiChord;

namespace UnitTestMidiChord
{
    [TestClass]
    public class UnitTestInstrument
    {
        [TestMethod]
        public void When_Data_Has_Instrument_Command_Then_Name_Of_Instrument_Is_Found()
        {
            var parser = new ChordParser();

            parser.ParseText(new string[] { "{instrument: Piano}" });
            Assert.AreEqual("Piano", parser.Instrument);
            Assert.AreEqual(1, parser.GetNumberOfInstrumentChanges());
        }

        [TestMethod]
        public void When_Data_Has_Command_With_Unknown_Instrument_Then_Name_Of_Instrument_Is_Not_Found()
        {
            var parser = new ChordParser();

            parser.ParseText(new string[] { "{instrument: X}" });
            Assert.IsTrue( string.IsNullOrEmpty(parser.Instrument));
            Assert.AreEqual(0, parser.GetNumberOfInstrumentChanges());
        }

    }
}
