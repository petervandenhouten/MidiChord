using Sanford.Multimedia.Midi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidiChord
{
    public class MidiCommandFactory
    {
        public ChannelMessage Instrument(GeneralMidiInstrument instrument)
        {
            ChannelMessageBuilder builder = new ChannelMessageBuilder();
            builder.Command = ChannelCommand.ProgramChange;
            builder.MidiChannel = 0;
            builder.Data1 = (int)instrument;
            builder.Build();

            return builder.Result;
        }

        public ChannelMessage NoteCOn()
        {
            var builder = new ChannelMessageBuilder();
            builder.Command = ChannelCommand.NoteOn;
            builder.MidiChannel = 0;
            builder.Data1 = 60; // C
            builder.Data2 = 127;
            builder.Build();

            return builder.Result;
        }

        public ChannelMessage NoteOn(int channel, int node, int volume)
        {
            var builder = new ChannelMessageBuilder();
        
            builder.Command = ChannelCommand.NoteOn;
            builder.MidiChannel = channel;
            builder.Data1 = node;
            builder.Data2 = volume;
            builder.Build();

            return builder.Result;
        }

        public ChannelMessage NoteOff(int channel, int node)
        {
            var builder = new ChannelMessageBuilder();

            builder.Command = ChannelCommand.NoteOff;
            builder.MidiChannel = channel;
            builder.Data1 = node;
            builder.Data2 = 0;
            builder.Build();

            return builder.Result;
        }
    }
}
