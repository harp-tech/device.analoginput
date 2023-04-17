using Bonsai;
using Bonsai.Harp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Xml.Serialization;

namespace Harp.AnalogInput
{
    /// <summary>
    /// Generates events and processes commands for the AnalogInput device connected
    /// at the specified serial port.
    /// </summary>
    [Combinator(MethodName = nameof(Generate))]
    [WorkflowElementCategory(ElementCategory.Source)]
    [Description("Generates events and processes commands for the AnalogInput device.")]
    public partial class Device : Bonsai.Harp.Device, INamedElement
    {
        /// <summary>
        /// Represents the unique identity class of the <see cref="AnalogInput"/> device.
        /// This field is constant.
        /// </summary>
        public const int WhoAmI = 1236;

        /// <summary>
        /// Initializes a new instance of the <see cref="Device"/> class.
        /// </summary>
        public Device() : base(WhoAmI) { }

        string INamedElement.Name => nameof(AnalogInput);

        /// <summary>
        /// Gets a read-only mapping from address to register type.
        /// </summary>
        public static new IReadOnlyDictionary<int, Type> RegisterMap { get; } = new Dictionary<int, Type>
            (Bonsai.Harp.Device.RegisterMap.ToDictionary(entry => entry.Key, entry => entry.Value))
        {
            { 32, typeof(StartAcquisition) },
            { 33, typeof(AnalogData) },
            { 34, typeof(InputEvent) },
            { 37, typeof(RangeAndBandwidth) },
            { 38, typeof(SamplingFrequency) },
            { 39, typeof(DI0Mode) },
            { 40, typeof(DO0Mode) },
            { 41, typeof(DO0PulseDuration) },
            { 42, typeof(OutputSet) },
            { 43, typeof(OutputClear) },
            { 44, typeof(OutputToggle) },
            { 45, typeof(OutputState) },
            { 48, typeof(AcquisitionStartOutput) },
            { 58, typeof(DO0TargetChannel) },
            { 59, typeof(DO1TargetChannel) },
            { 60, typeof(DO2TargetChannel) },
            { 61, typeof(DO3TargetChannel) },
            { 66, typeof(DO0Threshold) },
            { 67, typeof(DO1Threshold) },
            { 68, typeof(DO2Threshold) },
            { 69, typeof(DO3Threshold) },
            { 74, typeof(DO0BufferRisingEdge) },
            { 75, typeof(DO1BufferRisingEdge) },
            { 76, typeof(DO2BufferRisingEdge) },
            { 77, typeof(DO3BufferRisingEdge) },
            { 82, typeof(DO0BufferFallingEdge) },
            { 83, typeof(DO1BufferFallingEdge) },
            { 84, typeof(DO2BufferFallingEdge) },
            { 85, typeof(DO3BufferFallingEdge) }
        };
    }

    /// <summary>
    /// Represents an operator that groups the sequence of <see cref="AnalogInput"/>" messages by register type.
    /// </summary>
    [Description("Groups the sequence of AnalogInput messages by register type.")]
    public partial class GroupByRegister : Combinator<HarpMessage, IGroupedObservable<Type, HarpMessage>>
    {
        /// <summary>
        /// Groups an observable sequence of <see cref="AnalogInput"/> messages
        /// by register type.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of observable groups, each of which corresponds to a unique
        /// <see cref="AnalogInput"/> register.
        /// </returns>
        public override IObservable<IGroupedObservable<Type, HarpMessage>> Process(IObservable<HarpMessage> source)
        {
            return source.GroupBy(message => Device.RegisterMap[message.Address]);
        }
    }

    /// <summary>
    /// Represents an operator that filters register-specific messages
    /// reported by the <see cref="AnalogInput"/> device.
    /// </summary>
    /// <seealso cref="StartAcquisition"/>
    /// <seealso cref="AnalogData"/>
    /// <seealso cref="InputEvent"/>
    /// <seealso cref="RangeAndBandwidth"/>
    /// <seealso cref="SamplingFrequency"/>
    /// <seealso cref="DI0Mode"/>
    /// <seealso cref="DO0Mode"/>
    /// <seealso cref="DO0PulseDuration"/>
    /// <seealso cref="OutputSet"/>
    /// <seealso cref="OutputClear"/>
    /// <seealso cref="OutputToggle"/>
    /// <seealso cref="OutputState"/>
    /// <seealso cref="AcquisitionStartOutput"/>
    /// <seealso cref="DO0TargetChannel"/>
    /// <seealso cref="DO1TargetChannel"/>
    /// <seealso cref="DO2TargetChannel"/>
    /// <seealso cref="DO3TargetChannel"/>
    /// <seealso cref="DO0Threshold"/>
    /// <seealso cref="DO1Threshold"/>
    /// <seealso cref="DO2Threshold"/>
    /// <seealso cref="DO3Threshold"/>
    /// <seealso cref="DO0BufferRisingEdge"/>
    /// <seealso cref="DO1BufferRisingEdge"/>
    /// <seealso cref="DO2BufferRisingEdge"/>
    /// <seealso cref="DO3BufferRisingEdge"/>
    /// <seealso cref="DO0BufferFallingEdge"/>
    /// <seealso cref="DO1BufferFallingEdge"/>
    /// <seealso cref="DO2BufferFallingEdge"/>
    /// <seealso cref="DO3BufferFallingEdge"/>
    [XmlInclude(typeof(StartAcquisition))]
    [XmlInclude(typeof(AnalogData))]
    [XmlInclude(typeof(InputEvent))]
    [XmlInclude(typeof(RangeAndBandwidth))]
    [XmlInclude(typeof(SamplingFrequency))]
    [XmlInclude(typeof(DI0Mode))]
    [XmlInclude(typeof(DO0Mode))]
    [XmlInclude(typeof(DO0PulseDuration))]
    [XmlInclude(typeof(OutputSet))]
    [XmlInclude(typeof(OutputClear))]
    [XmlInclude(typeof(OutputToggle))]
    [XmlInclude(typeof(OutputState))]
    [XmlInclude(typeof(AcquisitionStartOutput))]
    [XmlInclude(typeof(DO0TargetChannel))]
    [XmlInclude(typeof(DO1TargetChannel))]
    [XmlInclude(typeof(DO2TargetChannel))]
    [XmlInclude(typeof(DO3TargetChannel))]
    [XmlInclude(typeof(DO0Threshold))]
    [XmlInclude(typeof(DO1Threshold))]
    [XmlInclude(typeof(DO2Threshold))]
    [XmlInclude(typeof(DO3Threshold))]
    [XmlInclude(typeof(DO0BufferRisingEdge))]
    [XmlInclude(typeof(DO1BufferRisingEdge))]
    [XmlInclude(typeof(DO2BufferRisingEdge))]
    [XmlInclude(typeof(DO3BufferRisingEdge))]
    [XmlInclude(typeof(DO0BufferFallingEdge))]
    [XmlInclude(typeof(DO1BufferFallingEdge))]
    [XmlInclude(typeof(DO2BufferFallingEdge))]
    [XmlInclude(typeof(DO3BufferFallingEdge))]
    [Description("Filters register-specific messages reported by the AnalogInput device.")]
    public class FilterMessage : FilterMessageBuilder, INamedElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FilterMessage"/> class.
        /// </summary>
        public FilterMessage()
        {
            Register = new StartAcquisition();
        }

        string INamedElement.Name
        {
            get => $"{nameof(AnalogInput)}.{GetElementDisplayName(Register)}";
        }
    }

    /// <summary>
    /// Represents an operator which filters and selects specific messages
    /// reported by the AnalogInput device.
    /// </summary>
    /// <seealso cref="StartAcquisition"/>
    /// <seealso cref="AnalogData"/>
    /// <seealso cref="InputEvent"/>
    /// <seealso cref="RangeAndBandwidth"/>
    /// <seealso cref="SamplingFrequency"/>
    /// <seealso cref="DI0Mode"/>
    /// <seealso cref="DO0Mode"/>
    /// <seealso cref="DO0PulseDuration"/>
    /// <seealso cref="OutputSet"/>
    /// <seealso cref="OutputClear"/>
    /// <seealso cref="OutputToggle"/>
    /// <seealso cref="OutputState"/>
    /// <seealso cref="AcquisitionStartOutput"/>
    /// <seealso cref="DO0TargetChannel"/>
    /// <seealso cref="DO1TargetChannel"/>
    /// <seealso cref="DO2TargetChannel"/>
    /// <seealso cref="DO3TargetChannel"/>
    /// <seealso cref="DO0Threshold"/>
    /// <seealso cref="DO1Threshold"/>
    /// <seealso cref="DO2Threshold"/>
    /// <seealso cref="DO3Threshold"/>
    /// <seealso cref="DO0BufferRisingEdge"/>
    /// <seealso cref="DO1BufferRisingEdge"/>
    /// <seealso cref="DO2BufferRisingEdge"/>
    /// <seealso cref="DO3BufferRisingEdge"/>
    /// <seealso cref="DO0BufferFallingEdge"/>
    /// <seealso cref="DO1BufferFallingEdge"/>
    /// <seealso cref="DO2BufferFallingEdge"/>
    /// <seealso cref="DO3BufferFallingEdge"/>
    [XmlInclude(typeof(StartAcquisition))]
    [XmlInclude(typeof(AnalogData))]
    [XmlInclude(typeof(InputEvent))]
    [XmlInclude(typeof(RangeAndBandwidth))]
    [XmlInclude(typeof(SamplingFrequency))]
    [XmlInclude(typeof(DI0Mode))]
    [XmlInclude(typeof(DO0Mode))]
    [XmlInclude(typeof(DO0PulseDuration))]
    [XmlInclude(typeof(OutputSet))]
    [XmlInclude(typeof(OutputClear))]
    [XmlInclude(typeof(OutputToggle))]
    [XmlInclude(typeof(OutputState))]
    [XmlInclude(typeof(AcquisitionStartOutput))]
    [XmlInclude(typeof(DO0TargetChannel))]
    [XmlInclude(typeof(DO1TargetChannel))]
    [XmlInclude(typeof(DO2TargetChannel))]
    [XmlInclude(typeof(DO3TargetChannel))]
    [XmlInclude(typeof(DO0Threshold))]
    [XmlInclude(typeof(DO1Threshold))]
    [XmlInclude(typeof(DO2Threshold))]
    [XmlInclude(typeof(DO3Threshold))]
    [XmlInclude(typeof(DO0BufferRisingEdge))]
    [XmlInclude(typeof(DO1BufferRisingEdge))]
    [XmlInclude(typeof(DO2BufferRisingEdge))]
    [XmlInclude(typeof(DO3BufferRisingEdge))]
    [XmlInclude(typeof(DO0BufferFallingEdge))]
    [XmlInclude(typeof(DO1BufferFallingEdge))]
    [XmlInclude(typeof(DO2BufferFallingEdge))]
    [XmlInclude(typeof(DO3BufferFallingEdge))]
    [XmlInclude(typeof(TimestampedStartAcquisition))]
    [XmlInclude(typeof(TimestampedAnalogData))]
    [XmlInclude(typeof(TimestampedInputEvent))]
    [XmlInclude(typeof(TimestampedRangeAndBandwidth))]
    [XmlInclude(typeof(TimestampedSamplingFrequency))]
    [XmlInclude(typeof(TimestampedDI0Mode))]
    [XmlInclude(typeof(TimestampedDO0Mode))]
    [XmlInclude(typeof(TimestampedDO0PulseDuration))]
    [XmlInclude(typeof(TimestampedOutputSet))]
    [XmlInclude(typeof(TimestampedOutputClear))]
    [XmlInclude(typeof(TimestampedOutputToggle))]
    [XmlInclude(typeof(TimestampedOutputState))]
    [XmlInclude(typeof(TimestampedAcquisitionStartOutput))]
    [XmlInclude(typeof(TimestampedDO0TargetChannel))]
    [XmlInclude(typeof(TimestampedDO1TargetChannel))]
    [XmlInclude(typeof(TimestampedDO2TargetChannel))]
    [XmlInclude(typeof(TimestampedDO3TargetChannel))]
    [XmlInclude(typeof(TimestampedDO0Threshold))]
    [XmlInclude(typeof(TimestampedDO1Threshold))]
    [XmlInclude(typeof(TimestampedDO2Threshold))]
    [XmlInclude(typeof(TimestampedDO3Threshold))]
    [XmlInclude(typeof(TimestampedDO0BufferRisingEdge))]
    [XmlInclude(typeof(TimestampedDO1BufferRisingEdge))]
    [XmlInclude(typeof(TimestampedDO2BufferRisingEdge))]
    [XmlInclude(typeof(TimestampedDO3BufferRisingEdge))]
    [XmlInclude(typeof(TimestampedDO0BufferFallingEdge))]
    [XmlInclude(typeof(TimestampedDO1BufferFallingEdge))]
    [XmlInclude(typeof(TimestampedDO2BufferFallingEdge))]
    [XmlInclude(typeof(TimestampedDO3BufferFallingEdge))]
    [Description("Filters and selects specific messages reported by the AnalogInput device.")]
    public partial class Parse : ParseBuilder, INamedElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Parse"/> class.
        /// </summary>
        public Parse()
        {
            Register = new StartAcquisition();
        }

        string INamedElement.Name => $"{nameof(AnalogInput)}.{GetElementDisplayName(Register)}";
    }

    /// <summary>
    /// Represents an operator which formats a sequence of values as specific
    /// AnalogInput register messages.
    /// </summary>
    /// <seealso cref="StartAcquisition"/>
    /// <seealso cref="AnalogData"/>
    /// <seealso cref="InputEvent"/>
    /// <seealso cref="RangeAndBandwidth"/>
    /// <seealso cref="SamplingFrequency"/>
    /// <seealso cref="DI0Mode"/>
    /// <seealso cref="DO0Mode"/>
    /// <seealso cref="DO0PulseDuration"/>
    /// <seealso cref="OutputSet"/>
    /// <seealso cref="OutputClear"/>
    /// <seealso cref="OutputToggle"/>
    /// <seealso cref="OutputState"/>
    /// <seealso cref="AcquisitionStartOutput"/>
    /// <seealso cref="DO0TargetChannel"/>
    /// <seealso cref="DO1TargetChannel"/>
    /// <seealso cref="DO2TargetChannel"/>
    /// <seealso cref="DO3TargetChannel"/>
    /// <seealso cref="DO0Threshold"/>
    /// <seealso cref="DO1Threshold"/>
    /// <seealso cref="DO2Threshold"/>
    /// <seealso cref="DO3Threshold"/>
    /// <seealso cref="DO0BufferRisingEdge"/>
    /// <seealso cref="DO1BufferRisingEdge"/>
    /// <seealso cref="DO2BufferRisingEdge"/>
    /// <seealso cref="DO3BufferRisingEdge"/>
    /// <seealso cref="DO0BufferFallingEdge"/>
    /// <seealso cref="DO1BufferFallingEdge"/>
    /// <seealso cref="DO2BufferFallingEdge"/>
    /// <seealso cref="DO3BufferFallingEdge"/>
    [XmlInclude(typeof(StartAcquisition))]
    [XmlInclude(typeof(AnalogData))]
    [XmlInclude(typeof(InputEvent))]
    [XmlInclude(typeof(RangeAndBandwidth))]
    [XmlInclude(typeof(SamplingFrequency))]
    [XmlInclude(typeof(DI0Mode))]
    [XmlInclude(typeof(DO0Mode))]
    [XmlInclude(typeof(DO0PulseDuration))]
    [XmlInclude(typeof(OutputSet))]
    [XmlInclude(typeof(OutputClear))]
    [XmlInclude(typeof(OutputToggle))]
    [XmlInclude(typeof(OutputState))]
    [XmlInclude(typeof(AcquisitionStartOutput))]
    [XmlInclude(typeof(DO0TargetChannel))]
    [XmlInclude(typeof(DO1TargetChannel))]
    [XmlInclude(typeof(DO2TargetChannel))]
    [XmlInclude(typeof(DO3TargetChannel))]
    [XmlInclude(typeof(DO0Threshold))]
    [XmlInclude(typeof(DO1Threshold))]
    [XmlInclude(typeof(DO2Threshold))]
    [XmlInclude(typeof(DO3Threshold))]
    [XmlInclude(typeof(DO0BufferRisingEdge))]
    [XmlInclude(typeof(DO1BufferRisingEdge))]
    [XmlInclude(typeof(DO2BufferRisingEdge))]
    [XmlInclude(typeof(DO3BufferRisingEdge))]
    [XmlInclude(typeof(DO0BufferFallingEdge))]
    [XmlInclude(typeof(DO1BufferFallingEdge))]
    [XmlInclude(typeof(DO2BufferFallingEdge))]
    [XmlInclude(typeof(DO3BufferFallingEdge))]
    [Description("Formats a sequence of values as specific AnalogInput register messages.")]
    public partial class Format : FormatBuilder, INamedElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Format"/> class.
        /// </summary>
        public Format()
        {
            Register = new StartAcquisition();
        }

        string INamedElement.Name => $"{nameof(AnalogInput)}.{GetElementDisplayName(Register)}";
    }

    /// <summary>
    /// Represents a register that enables the data acquisition.
    /// </summary>
    [Description("Enables the data acquisition")]
    public partial class StartAcquisition
    {
        /// <summary>
        /// Represents the address of the <see cref="StartAcquisition"/> register. This field is constant.
        /// </summary>
        public const int Address = 32;

        /// <summary>
        /// Represents the payload type of the <see cref="StartAcquisition"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="StartAcquisition"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="StartAcquisition"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static EnableFlag GetPayload(HarpMessage message)
        {
            return (EnableFlag)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="StartAcquisition"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<EnableFlag> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((EnableFlag)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="StartAcquisition"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="StartAcquisition"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, EnableFlag value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="StartAcquisition"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="StartAcquisition"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, EnableFlag value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// StartAcquisition register.
    /// </summary>
    /// <seealso cref="StartAcquisition"/>
    [Description("Filters and selects timestamped messages from the StartAcquisition register.")]
    public partial class TimestampedStartAcquisition
    {
        /// <summary>
        /// Represents the address of the <see cref="StartAcquisition"/> register. This field is constant.
        /// </summary>
        public const int Address = StartAcquisition.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="StartAcquisition"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<EnableFlag> GetPayload(HarpMessage message)
        {
            return StartAcquisition.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that value from a single read of all ADC channels.
    /// </summary>
    [Description("Value from a single read of all ADC channels")]
    public partial class AnalogData
    {
        /// <summary>
        /// Represents the address of the <see cref="AnalogData"/> register. This field is constant.
        /// </summary>
        public const int Address = 33;

        /// <summary>
        /// Represents the payload type of the <see cref="AnalogData"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.S16;

        /// <summary>
        /// Represents the length of the <see cref="AnalogData"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 4;

        static AnalogDataPayload ParsePayload(short[] payload)
        {
            AnalogDataPayload result;
            result.Channel0 = payload[0];
            result.Channel1 = payload[1];
            result.Channel2 = payload[2];
            result.Channel3 = payload[3];
            return result;
        }

        static short[] FormatPayload(AnalogDataPayload value)
        {
            short[] result;
            result = new short[4];
            result[0] = value.Channel0;
            result[1] = value.Channel1;
            result[2] = value.Channel2;
            result[3] = value.Channel3;
            return result;
        }

        /// <summary>
        /// Returns the payload data for <see cref="AnalogData"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static AnalogDataPayload GetPayload(HarpMessage message)
        {
            return ParsePayload(message.GetPayloadArray<short>());
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="AnalogData"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<AnalogDataPayload> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadArray<short>();
            return Timestamped.Create(ParsePayload(payload.Value), payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="AnalogData"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="AnalogData"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, AnalogDataPayload value)
        {
            return HarpMessage.FromInt16(Address, messageType, FormatPayload(value));
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="AnalogData"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="AnalogData"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, AnalogDataPayload value)
        {
            return HarpMessage.FromInt16(Address, timestamp, messageType, FormatPayload(value));
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// AnalogData register.
    /// </summary>
    /// <seealso cref="AnalogData"/>
    [Description("Filters and selects timestamped messages from the AnalogData register.")]
    public partial class TimestampedAnalogData
    {
        /// <summary>
        /// Represents the address of the <see cref="AnalogData"/> register. This field is constant.
        /// </summary>
        public const int Address = AnalogData.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="AnalogData"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<AnalogDataPayload> GetPayload(HarpMessage message)
        {
            return AnalogData.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that state of the digital input pin 0.
    /// </summary>
    [Description("State of the digital input pin 0.")]
    public partial class InputEvent
    {
        /// <summary>
        /// Represents the address of the <see cref="InputEvent"/> register. This field is constant.
        /// </summary>
        public const int Address = 34;

        /// <summary>
        /// Represents the payload type of the <see cref="InputEvent"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="InputEvent"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="InputEvent"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static DigitalInputState GetPayload(HarpMessage message)
        {
            return (DigitalInputState)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="InputEvent"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalInputState> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((DigitalInputState)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="InputEvent"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="InputEvent"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, DigitalInputState value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="InputEvent"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="InputEvent"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, DigitalInputState value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// InputEvent register.
    /// </summary>
    /// <seealso cref="InputEvent"/>
    [Description("Filters and selects timestamped messages from the InputEvent register.")]
    public partial class TimestampedInputEvent
    {
        /// <summary>
        /// Represents the address of the <see cref="InputEvent"/> register. This field is constant.
        /// </summary>
        public const int Address = InputEvent.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="InputEvent"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalInputState> GetPayload(HarpMessage message)
        {
            return InputEvent.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sets the range and bandwidth of the ADC.
    /// </summary>
    [Description("Sets the range and bandwidth of the ADC.")]
    public partial class RangeAndBandwidth
    {
        /// <summary>
        /// Represents the address of the <see cref="RangeAndBandwidth"/> register. This field is constant.
        /// </summary>
        public const int Address = 37;

        /// <summary>
        /// Represents the payload type of the <see cref="RangeAndBandwidth"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="RangeAndBandwidth"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="RangeAndBandwidth"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static RangeAndBandwidthConfig GetPayload(HarpMessage message)
        {
            return (RangeAndBandwidthConfig)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="RangeAndBandwidth"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<RangeAndBandwidthConfig> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((RangeAndBandwidthConfig)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="RangeAndBandwidth"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="RangeAndBandwidth"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, RangeAndBandwidthConfig value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="RangeAndBandwidth"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="RangeAndBandwidth"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, RangeAndBandwidthConfig value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// RangeAndBandwidth register.
    /// </summary>
    /// <seealso cref="RangeAndBandwidth"/>
    [Description("Filters and selects timestamped messages from the RangeAndBandwidth register.")]
    public partial class TimestampedRangeAndBandwidth
    {
        /// <summary>
        /// Represents the address of the <see cref="RangeAndBandwidth"/> register. This field is constant.
        /// </summary>
        public const int Address = RangeAndBandwidth.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="RangeAndBandwidth"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<RangeAndBandwidthConfig> GetPayload(HarpMessage message)
        {
            return RangeAndBandwidth.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sets the sampling frequency of the ADC.
    /// </summary>
    [Description("Sets the sampling frequency of the ADC.")]
    public partial class SamplingFrequency
    {
        /// <summary>
        /// Represents the address of the <see cref="SamplingFrequency"/> register. This field is constant.
        /// </summary>
        public const int Address = 38;

        /// <summary>
        /// Represents the payload type of the <see cref="SamplingFrequency"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="SamplingFrequency"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="SamplingFrequency"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static SamplingFrequencyConfig GetPayload(HarpMessage message)
        {
            return (SamplingFrequencyConfig)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="SamplingFrequency"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<SamplingFrequencyConfig> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((SamplingFrequencyConfig)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="SamplingFrequency"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="SamplingFrequency"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, SamplingFrequencyConfig value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="SamplingFrequency"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="SamplingFrequency"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, SamplingFrequencyConfig value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// SamplingFrequency register.
    /// </summary>
    /// <seealso cref="SamplingFrequency"/>
    [Description("Filters and selects timestamped messages from the SamplingFrequency register.")]
    public partial class TimestampedSamplingFrequency
    {
        /// <summary>
        /// Represents the address of the <see cref="SamplingFrequency"/> register. This field is constant.
        /// </summary>
        public const int Address = SamplingFrequency.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="SamplingFrequency"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<SamplingFrequencyConfig> GetPayload(HarpMessage message)
        {
            return SamplingFrequency.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configuration of the digital input pin 0.
    /// </summary>
    [Description("Configuration of the digital input pin 0.")]
    public partial class DI0Mode
    {
        /// <summary>
        /// Represents the address of the <see cref="DI0Mode"/> register. This field is constant.
        /// </summary>
        public const int Address = 39;

        /// <summary>
        /// Represents the payload type of the <see cref="DI0Mode"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="DI0Mode"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DI0Mode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static DI0Configuration GetPayload(HarpMessage message)
        {
            return (DI0Configuration)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DI0Mode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DI0Configuration> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((DI0Configuration)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DI0Mode"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DI0Mode"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, DI0Configuration value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DI0Mode"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DI0Mode"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, DI0Configuration value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DI0Mode register.
    /// </summary>
    /// <seealso cref="DI0Mode"/>
    [Description("Filters and selects timestamped messages from the DI0Mode register.")]
    public partial class TimestampedDI0Mode
    {
        /// <summary>
        /// Represents the address of the <see cref="DI0Mode"/> register. This field is constant.
        /// </summary>
        public const int Address = DI0Mode.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DI0Mode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DI0Configuration> GetPayload(HarpMessage message)
        {
            return DI0Mode.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configuration of the digital output pin 0.
    /// </summary>
    [Description("Configuration of the digital output pin 0.")]
    public partial class DO0Mode
    {
        /// <summary>
        /// Represents the address of the <see cref="DO0Mode"/> register. This field is constant.
        /// </summary>
        public const int Address = 40;

        /// <summary>
        /// Represents the payload type of the <see cref="DO0Mode"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="DO0Mode"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DO0Mode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static DO0Configuration GetPayload(HarpMessage message)
        {
            return (DO0Configuration)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DO0Mode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DO0Configuration> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((DO0Configuration)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DO0Mode"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO0Mode"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, DO0Configuration value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DO0Mode"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO0Mode"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, DO0Configuration value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DO0Mode register.
    /// </summary>
    /// <seealso cref="DO0Mode"/>
    [Description("Filters and selects timestamped messages from the DO0Mode register.")]
    public partial class TimestampedDO0Mode
    {
        /// <summary>
        /// Represents the address of the <see cref="DO0Mode"/> register. This field is constant.
        /// </summary>
        public const int Address = DO0Mode.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DO0Mode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DO0Configuration> GetPayload(HarpMessage message)
        {
            return DO0Mode.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that pulse duration (ms) for the digital output pin 0. The pulse will only be emitted when DO0Mode == Pulse.
    /// </summary>
    [Description("Pulse duration (ms) for the digital output pin 0. The pulse will only be emitted when DO0Mode == Pulse.")]
    public partial class DO0PulseDuration
    {
        /// <summary>
        /// Represents the address of the <see cref="DO0PulseDuration"/> register. This field is constant.
        /// </summary>
        public const int Address = 41;

        /// <summary>
        /// Represents the payload type of the <see cref="DO0PulseDuration"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="DO0PulseDuration"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DO0PulseDuration"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static byte GetPayload(HarpMessage message)
        {
            return message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DO0PulseDuration"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<byte> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadByte();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DO0PulseDuration"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO0PulseDuration"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, byte value)
        {
            return HarpMessage.FromByte(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DO0PulseDuration"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO0PulseDuration"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, byte value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DO0PulseDuration register.
    /// </summary>
    /// <seealso cref="DO0PulseDuration"/>
    [Description("Filters and selects timestamped messages from the DO0PulseDuration register.")]
    public partial class TimestampedDO0PulseDuration
    {
        /// <summary>
        /// Represents the address of the <see cref="DO0PulseDuration"/> register. This field is constant.
        /// </summary>
        public const int Address = DO0PulseDuration.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DO0PulseDuration"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<byte> GetPayload(HarpMessage message)
        {
            return DO0PulseDuration.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that set the specified digital output lines.
    /// </summary>
    [Description("Set the specified digital output lines.")]
    public partial class OutputSet
    {
        /// <summary>
        /// Represents the address of the <see cref="OutputSet"/> register. This field is constant.
        /// </summary>
        public const int Address = 42;

        /// <summary>
        /// Represents the payload type of the <see cref="OutputSet"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="OutputSet"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="OutputSet"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static DigitalOutputs GetPayload(HarpMessage message)
        {
            return (DigitalOutputs)message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="OutputSet"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalOutputs> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadUInt16();
            return Timestamped.Create((DigitalOutputs)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="OutputSet"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="OutputSet"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, DigitalOutputs value)
        {
            return HarpMessage.FromUInt16(Address, messageType, (ushort)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="OutputSet"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="OutputSet"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, DigitalOutputs value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, (ushort)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// OutputSet register.
    /// </summary>
    /// <seealso cref="OutputSet"/>
    [Description("Filters and selects timestamped messages from the OutputSet register.")]
    public partial class TimestampedOutputSet
    {
        /// <summary>
        /// Represents the address of the <see cref="OutputSet"/> register. This field is constant.
        /// </summary>
        public const int Address = OutputSet.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="OutputSet"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalOutputs> GetPayload(HarpMessage message)
        {
            return OutputSet.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that clear the specified digital output lines.
    /// </summary>
    [Description("Clear the specified digital output lines.")]
    public partial class OutputClear
    {
        /// <summary>
        /// Represents the address of the <see cref="OutputClear"/> register. This field is constant.
        /// </summary>
        public const int Address = 43;

        /// <summary>
        /// Represents the payload type of the <see cref="OutputClear"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="OutputClear"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="OutputClear"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static DigitalOutputs GetPayload(HarpMessage message)
        {
            return (DigitalOutputs)message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="OutputClear"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalOutputs> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadUInt16();
            return Timestamped.Create((DigitalOutputs)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="OutputClear"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="OutputClear"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, DigitalOutputs value)
        {
            return HarpMessage.FromUInt16(Address, messageType, (ushort)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="OutputClear"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="OutputClear"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, DigitalOutputs value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, (ushort)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// OutputClear register.
    /// </summary>
    /// <seealso cref="OutputClear"/>
    [Description("Filters and selects timestamped messages from the OutputClear register.")]
    public partial class TimestampedOutputClear
    {
        /// <summary>
        /// Represents the address of the <see cref="OutputClear"/> register. This field is constant.
        /// </summary>
        public const int Address = OutputClear.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="OutputClear"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalOutputs> GetPayload(HarpMessage message)
        {
            return OutputClear.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that toggle the specified digital output lines.
    /// </summary>
    [Description("Toggle the specified digital output lines")]
    public partial class OutputToggle
    {
        /// <summary>
        /// Represents the address of the <see cref="OutputToggle"/> register. This field is constant.
        /// </summary>
        public const int Address = 44;

        /// <summary>
        /// Represents the payload type of the <see cref="OutputToggle"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="OutputToggle"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="OutputToggle"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static DigitalOutputs GetPayload(HarpMessage message)
        {
            return (DigitalOutputs)message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="OutputToggle"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalOutputs> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadUInt16();
            return Timestamped.Create((DigitalOutputs)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="OutputToggle"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="OutputToggle"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, DigitalOutputs value)
        {
            return HarpMessage.FromUInt16(Address, messageType, (ushort)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="OutputToggle"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="OutputToggle"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, DigitalOutputs value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, (ushort)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// OutputToggle register.
    /// </summary>
    /// <seealso cref="OutputToggle"/>
    [Description("Filters and selects timestamped messages from the OutputToggle register.")]
    public partial class TimestampedOutputToggle
    {
        /// <summary>
        /// Represents the address of the <see cref="OutputToggle"/> register. This field is constant.
        /// </summary>
        public const int Address = OutputToggle.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="OutputToggle"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalOutputs> GetPayload(HarpMessage message)
        {
            return OutputToggle.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that write the state of all digital output lines. An event will be emitted when the value of any pin was changed by a threshold crossing event.
    /// </summary>
    [Description("Write the state of all digital output lines. An event will be emitted when the value of any pin was changed by a threshold crossing event.")]
    public partial class OutputState
    {
        /// <summary>
        /// Represents the address of the <see cref="OutputState"/> register. This field is constant.
        /// </summary>
        public const int Address = 45;

        /// <summary>
        /// Represents the payload type of the <see cref="OutputState"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="OutputState"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="OutputState"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static DigitalOutputs GetPayload(HarpMessage message)
        {
            return (DigitalOutputs)message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="OutputState"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalOutputs> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadUInt16();
            return Timestamped.Create((DigitalOutputs)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="OutputState"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="OutputState"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, DigitalOutputs value)
        {
            return HarpMessage.FromUInt16(Address, messageType, (ushort)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="OutputState"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="OutputState"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, DigitalOutputs value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, (ushort)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// OutputState register.
    /// </summary>
    /// <seealso cref="OutputState"/>
    [Description("Filters and selects timestamped messages from the OutputState register.")]
    public partial class TimestampedOutputState
    {
        /// <summary>
        /// Represents the address of the <see cref="OutputState"/> register. This field is constant.
        /// </summary>
        public const int Address = OutputState.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="OutputState"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalOutputs> GetPayload(HarpMessage message)
        {
            return OutputState.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that digital output that will be set when the acquisition starts.
    /// </summary>
    [Description("Digital output that will be set when the acquisition starts.")]
    public partial class AcquisitionStartOutput
    {
        /// <summary>
        /// Represents the address of the <see cref="AcquisitionStartOutput"/> register. This field is constant.
        /// </summary>
        public const int Address = 48;

        /// <summary>
        /// Represents the payload type of the <see cref="AcquisitionStartOutput"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="AcquisitionStartOutput"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="AcquisitionStartOutput"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static AcquisitionStartTargets GetPayload(HarpMessage message)
        {
            return (AcquisitionStartTargets)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="AcquisitionStartOutput"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<AcquisitionStartTargets> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((AcquisitionStartTargets)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="AcquisitionStartOutput"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="AcquisitionStartOutput"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, AcquisitionStartTargets value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="AcquisitionStartOutput"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="AcquisitionStartOutput"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, AcquisitionStartTargets value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// AcquisitionStartOutput register.
    /// </summary>
    /// <seealso cref="AcquisitionStartOutput"/>
    [Description("Filters and selects timestamped messages from the AcquisitionStartOutput register.")]
    public partial class TimestampedAcquisitionStartOutput
    {
        /// <summary>
        /// Represents the address of the <see cref="AcquisitionStartOutput"/> register. This field is constant.
        /// </summary>
        public const int Address = AcquisitionStartOutput.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="AcquisitionStartOutput"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<AcquisitionStartTargets> GetPayload(HarpMessage message)
        {
            return AcquisitionStartOutput.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that target channel that will be used to trigger a threshold event on DO0 pin.
    /// </summary>
    [Description("Target channel that will be used to trigger a threshold event on DO0 pin.")]
    public partial class DO0TargetChannel
    {
        /// <summary>
        /// Represents the address of the <see cref="DO0TargetChannel"/> register. This field is constant.
        /// </summary>
        public const int Address = 58;

        /// <summary>
        /// Represents the payload type of the <see cref="DO0TargetChannel"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="DO0TargetChannel"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DO0TargetChannel"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ThresholdOnChannel GetPayload(HarpMessage message)
        {
            return (ThresholdOnChannel)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DO0TargetChannel"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ThresholdOnChannel> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((ThresholdOnChannel)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DO0TargetChannel"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO0TargetChannel"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ThresholdOnChannel value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DO0TargetChannel"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO0TargetChannel"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ThresholdOnChannel value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DO0TargetChannel register.
    /// </summary>
    /// <seealso cref="DO0TargetChannel"/>
    [Description("Filters and selects timestamped messages from the DO0TargetChannel register.")]
    public partial class TimestampedDO0TargetChannel
    {
        /// <summary>
        /// Represents the address of the <see cref="DO0TargetChannel"/> register. This field is constant.
        /// </summary>
        public const int Address = DO0TargetChannel.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DO0TargetChannel"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ThresholdOnChannel> GetPayload(HarpMessage message)
        {
            return DO0TargetChannel.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that target channel that will be used to trigger a threshold event on DO1 pin.
    /// </summary>
    [Description("Target channel that will be used to trigger a threshold event on DO1 pin.")]
    public partial class DO1TargetChannel
    {
        /// <summary>
        /// Represents the address of the <see cref="DO1TargetChannel"/> register. This field is constant.
        /// </summary>
        public const int Address = 59;

        /// <summary>
        /// Represents the payload type of the <see cref="DO1TargetChannel"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="DO1TargetChannel"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DO1TargetChannel"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ThresholdOnChannel GetPayload(HarpMessage message)
        {
            return (ThresholdOnChannel)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DO1TargetChannel"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ThresholdOnChannel> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((ThresholdOnChannel)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DO1TargetChannel"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO1TargetChannel"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ThresholdOnChannel value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DO1TargetChannel"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO1TargetChannel"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ThresholdOnChannel value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DO1TargetChannel register.
    /// </summary>
    /// <seealso cref="DO1TargetChannel"/>
    [Description("Filters and selects timestamped messages from the DO1TargetChannel register.")]
    public partial class TimestampedDO1TargetChannel
    {
        /// <summary>
        /// Represents the address of the <see cref="DO1TargetChannel"/> register. This field is constant.
        /// </summary>
        public const int Address = DO1TargetChannel.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DO1TargetChannel"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ThresholdOnChannel> GetPayload(HarpMessage message)
        {
            return DO1TargetChannel.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that target channel that will be used to trigger a threshold event on DO2 pin.
    /// </summary>
    [Description("Target channel that will be used to trigger a threshold event on DO2 pin.")]
    public partial class DO2TargetChannel
    {
        /// <summary>
        /// Represents the address of the <see cref="DO2TargetChannel"/> register. This field is constant.
        /// </summary>
        public const int Address = 60;

        /// <summary>
        /// Represents the payload type of the <see cref="DO2TargetChannel"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="DO2TargetChannel"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DO2TargetChannel"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ThresholdOnChannel GetPayload(HarpMessage message)
        {
            return (ThresholdOnChannel)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DO2TargetChannel"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ThresholdOnChannel> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((ThresholdOnChannel)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DO2TargetChannel"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO2TargetChannel"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ThresholdOnChannel value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DO2TargetChannel"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO2TargetChannel"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ThresholdOnChannel value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DO2TargetChannel register.
    /// </summary>
    /// <seealso cref="DO2TargetChannel"/>
    [Description("Filters and selects timestamped messages from the DO2TargetChannel register.")]
    public partial class TimestampedDO2TargetChannel
    {
        /// <summary>
        /// Represents the address of the <see cref="DO2TargetChannel"/> register. This field is constant.
        /// </summary>
        public const int Address = DO2TargetChannel.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DO2TargetChannel"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ThresholdOnChannel> GetPayload(HarpMessage message)
        {
            return DO2TargetChannel.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that target channel that will be used to trigger a threshold event on DO3 pin.
    /// </summary>
    [Description("Target channel that will be used to trigger a threshold event on DO3 pin.")]
    public partial class DO3TargetChannel
    {
        /// <summary>
        /// Represents the address of the <see cref="DO3TargetChannel"/> register. This field is constant.
        /// </summary>
        public const int Address = 61;

        /// <summary>
        /// Represents the payload type of the <see cref="DO3TargetChannel"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="DO3TargetChannel"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DO3TargetChannel"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ThresholdOnChannel GetPayload(HarpMessage message)
        {
            return (ThresholdOnChannel)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DO3TargetChannel"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ThresholdOnChannel> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((ThresholdOnChannel)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DO3TargetChannel"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO3TargetChannel"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ThresholdOnChannel value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DO3TargetChannel"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO3TargetChannel"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ThresholdOnChannel value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DO3TargetChannel register.
    /// </summary>
    /// <seealso cref="DO3TargetChannel"/>
    [Description("Filters and selects timestamped messages from the DO3TargetChannel register.")]
    public partial class TimestampedDO3TargetChannel
    {
        /// <summary>
        /// Represents the address of the <see cref="DO3TargetChannel"/> register. This field is constant.
        /// </summary>
        public const int Address = DO3TargetChannel.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DO3TargetChannel"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ThresholdOnChannel> GetPayload(HarpMessage message)
        {
            return DO3TargetChannel.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that value used to threshold an ADC read, and trigger DO0 pin.
    /// </summary>
    [Description("Value used to threshold an ADC read, and trigger DO0 pin.")]
    public partial class DO0Threshold
    {
        /// <summary>
        /// Represents the address of the <see cref="DO0Threshold"/> register. This field is constant.
        /// </summary>
        public const int Address = 66;

        /// <summary>
        /// Represents the payload type of the <see cref="DO0Threshold"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.S16;

        /// <summary>
        /// Represents the length of the <see cref="DO0Threshold"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DO0Threshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static short GetPayload(HarpMessage message)
        {
            return message.GetPayloadInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DO0Threshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<short> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DO0Threshold"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO0Threshold"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, short value)
        {
            return HarpMessage.FromInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DO0Threshold"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO0Threshold"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, short value)
        {
            return HarpMessage.FromInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DO0Threshold register.
    /// </summary>
    /// <seealso cref="DO0Threshold"/>
    [Description("Filters and selects timestamped messages from the DO0Threshold register.")]
    public partial class TimestampedDO0Threshold
    {
        /// <summary>
        /// Represents the address of the <see cref="DO0Threshold"/> register. This field is constant.
        /// </summary>
        public const int Address = DO0Threshold.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DO0Threshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<short> GetPayload(HarpMessage message)
        {
            return DO0Threshold.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that value used to threshold an ADC read, and trigger DO1 pin.
    /// </summary>
    [Description("Value used to threshold an ADC read, and trigger DO1 pin.")]
    public partial class DO1Threshold
    {
        /// <summary>
        /// Represents the address of the <see cref="DO1Threshold"/> register. This field is constant.
        /// </summary>
        public const int Address = 67;

        /// <summary>
        /// Represents the payload type of the <see cref="DO1Threshold"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.S16;

        /// <summary>
        /// Represents the length of the <see cref="DO1Threshold"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DO1Threshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static short GetPayload(HarpMessage message)
        {
            return message.GetPayloadInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DO1Threshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<short> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DO1Threshold"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO1Threshold"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, short value)
        {
            return HarpMessage.FromInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DO1Threshold"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO1Threshold"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, short value)
        {
            return HarpMessage.FromInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DO1Threshold register.
    /// </summary>
    /// <seealso cref="DO1Threshold"/>
    [Description("Filters and selects timestamped messages from the DO1Threshold register.")]
    public partial class TimestampedDO1Threshold
    {
        /// <summary>
        /// Represents the address of the <see cref="DO1Threshold"/> register. This field is constant.
        /// </summary>
        public const int Address = DO1Threshold.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DO1Threshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<short> GetPayload(HarpMessage message)
        {
            return DO1Threshold.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that value used to threshold an ADC read, and trigger DO2 pin.
    /// </summary>
    [Description("Value used to threshold an ADC read, and trigger DO2 pin.")]
    public partial class DO2Threshold
    {
        /// <summary>
        /// Represents the address of the <see cref="DO2Threshold"/> register. This field is constant.
        /// </summary>
        public const int Address = 68;

        /// <summary>
        /// Represents the payload type of the <see cref="DO2Threshold"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.S16;

        /// <summary>
        /// Represents the length of the <see cref="DO2Threshold"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DO2Threshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static short GetPayload(HarpMessage message)
        {
            return message.GetPayloadInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DO2Threshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<short> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DO2Threshold"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO2Threshold"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, short value)
        {
            return HarpMessage.FromInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DO2Threshold"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO2Threshold"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, short value)
        {
            return HarpMessage.FromInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DO2Threshold register.
    /// </summary>
    /// <seealso cref="DO2Threshold"/>
    [Description("Filters and selects timestamped messages from the DO2Threshold register.")]
    public partial class TimestampedDO2Threshold
    {
        /// <summary>
        /// Represents the address of the <see cref="DO2Threshold"/> register. This field is constant.
        /// </summary>
        public const int Address = DO2Threshold.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DO2Threshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<short> GetPayload(HarpMessage message)
        {
            return DO2Threshold.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that value used to threshold an ADC read, and trigger DO3 pin.
    /// </summary>
    [Description("Value used to threshold an ADC read, and trigger DO3 pin.")]
    public partial class DO3Threshold
    {
        /// <summary>
        /// Represents the address of the <see cref="DO3Threshold"/> register. This field is constant.
        /// </summary>
        public const int Address = 69;

        /// <summary>
        /// Represents the payload type of the <see cref="DO3Threshold"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.S16;

        /// <summary>
        /// Represents the length of the <see cref="DO3Threshold"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DO3Threshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static short GetPayload(HarpMessage message)
        {
            return message.GetPayloadInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DO3Threshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<short> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DO3Threshold"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO3Threshold"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, short value)
        {
            return HarpMessage.FromInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DO3Threshold"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO3Threshold"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, short value)
        {
            return HarpMessage.FromInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DO3Threshold register.
    /// </summary>
    /// <seealso cref="DO3Threshold"/>
    [Description("Filters and selects timestamped messages from the DO3Threshold register.")]
    public partial class TimestampedDO3Threshold
    {
        /// <summary>
        /// Represents the address of the <see cref="DO3Threshold"/> register. This field is constant.
        /// </summary>
        public const int Address = DO3Threshold.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DO3Threshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<short> GetPayload(HarpMessage message)
        {
            return DO3Threshold.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that time (ms) above threshold value that is required to trigger a DO0 pin event.
    /// </summary>
    [Description("Time (ms) above threshold value that is required to trigger a DO0 pin event.")]
    public partial class DO0BufferRisingEdge
    {
        /// <summary>
        /// Represents the address of the <see cref="DO0BufferRisingEdge"/> register. This field is constant.
        /// </summary>
        public const int Address = 74;

        /// <summary>
        /// Represents the payload type of the <see cref="DO0BufferRisingEdge"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="DO0BufferRisingEdge"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DO0BufferRisingEdge"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DO0BufferRisingEdge"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DO0BufferRisingEdge"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO0BufferRisingEdge"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DO0BufferRisingEdge"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO0BufferRisingEdge"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DO0BufferRisingEdge register.
    /// </summary>
    /// <seealso cref="DO0BufferRisingEdge"/>
    [Description("Filters and selects timestamped messages from the DO0BufferRisingEdge register.")]
    public partial class TimestampedDO0BufferRisingEdge
    {
        /// <summary>
        /// Represents the address of the <see cref="DO0BufferRisingEdge"/> register. This field is constant.
        /// </summary>
        public const int Address = DO0BufferRisingEdge.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DO0BufferRisingEdge"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return DO0BufferRisingEdge.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that time (ms) above threshold value that is required to trigger a DO1 pin event.
    /// </summary>
    [Description("Time (ms) above threshold value that is required to trigger a DO1 pin event.")]
    public partial class DO1BufferRisingEdge
    {
        /// <summary>
        /// Represents the address of the <see cref="DO1BufferRisingEdge"/> register. This field is constant.
        /// </summary>
        public const int Address = 75;

        /// <summary>
        /// Represents the payload type of the <see cref="DO1BufferRisingEdge"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="DO1BufferRisingEdge"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DO1BufferRisingEdge"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DO1BufferRisingEdge"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DO1BufferRisingEdge"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO1BufferRisingEdge"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DO1BufferRisingEdge"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO1BufferRisingEdge"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DO1BufferRisingEdge register.
    /// </summary>
    /// <seealso cref="DO1BufferRisingEdge"/>
    [Description("Filters and selects timestamped messages from the DO1BufferRisingEdge register.")]
    public partial class TimestampedDO1BufferRisingEdge
    {
        /// <summary>
        /// Represents the address of the <see cref="DO1BufferRisingEdge"/> register. This field is constant.
        /// </summary>
        public const int Address = DO1BufferRisingEdge.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DO1BufferRisingEdge"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return DO1BufferRisingEdge.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that time (ms) above threshold value that is required to trigger a DO2 pin event.
    /// </summary>
    [Description("Time (ms) above threshold value that is required to trigger a DO2 pin event.")]
    public partial class DO2BufferRisingEdge
    {
        /// <summary>
        /// Represents the address of the <see cref="DO2BufferRisingEdge"/> register. This field is constant.
        /// </summary>
        public const int Address = 76;

        /// <summary>
        /// Represents the payload type of the <see cref="DO2BufferRisingEdge"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="DO2BufferRisingEdge"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DO2BufferRisingEdge"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DO2BufferRisingEdge"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DO2BufferRisingEdge"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO2BufferRisingEdge"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DO2BufferRisingEdge"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO2BufferRisingEdge"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DO2BufferRisingEdge register.
    /// </summary>
    /// <seealso cref="DO2BufferRisingEdge"/>
    [Description("Filters and selects timestamped messages from the DO2BufferRisingEdge register.")]
    public partial class TimestampedDO2BufferRisingEdge
    {
        /// <summary>
        /// Represents the address of the <see cref="DO2BufferRisingEdge"/> register. This field is constant.
        /// </summary>
        public const int Address = DO2BufferRisingEdge.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DO2BufferRisingEdge"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return DO2BufferRisingEdge.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that time (ms) above threshold value that is required to trigger a DO3 pin event.
    /// </summary>
    [Description("Time (ms) above threshold value that is required to trigger a DO3 pin event.")]
    public partial class DO3BufferRisingEdge
    {
        /// <summary>
        /// Represents the address of the <see cref="DO3BufferRisingEdge"/> register. This field is constant.
        /// </summary>
        public const int Address = 77;

        /// <summary>
        /// Represents the payload type of the <see cref="DO3BufferRisingEdge"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="DO3BufferRisingEdge"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DO3BufferRisingEdge"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DO3BufferRisingEdge"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DO3BufferRisingEdge"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO3BufferRisingEdge"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DO3BufferRisingEdge"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO3BufferRisingEdge"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DO3BufferRisingEdge register.
    /// </summary>
    /// <seealso cref="DO3BufferRisingEdge"/>
    [Description("Filters and selects timestamped messages from the DO3BufferRisingEdge register.")]
    public partial class TimestampedDO3BufferRisingEdge
    {
        /// <summary>
        /// Represents the address of the <see cref="DO3BufferRisingEdge"/> register. This field is constant.
        /// </summary>
        public const int Address = DO3BufferRisingEdge.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DO3BufferRisingEdge"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return DO3BufferRisingEdge.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that time (ms) below threshold value that is required to trigger a DO0 pin event.
    /// </summary>
    [Description("Time (ms) below threshold value that is required to trigger a DO0 pin event.")]
    public partial class DO0BufferFallingEdge
    {
        /// <summary>
        /// Represents the address of the <see cref="DO0BufferFallingEdge"/> register. This field is constant.
        /// </summary>
        public const int Address = 82;

        /// <summary>
        /// Represents the payload type of the <see cref="DO0BufferFallingEdge"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="DO0BufferFallingEdge"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DO0BufferFallingEdge"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DO0BufferFallingEdge"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DO0BufferFallingEdge"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO0BufferFallingEdge"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DO0BufferFallingEdge"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO0BufferFallingEdge"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DO0BufferFallingEdge register.
    /// </summary>
    /// <seealso cref="DO0BufferFallingEdge"/>
    [Description("Filters and selects timestamped messages from the DO0BufferFallingEdge register.")]
    public partial class TimestampedDO0BufferFallingEdge
    {
        /// <summary>
        /// Represents the address of the <see cref="DO0BufferFallingEdge"/> register. This field is constant.
        /// </summary>
        public const int Address = DO0BufferFallingEdge.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DO0BufferFallingEdge"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return DO0BufferFallingEdge.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that time (ms) below threshold value that is required to trigger a DO1 pin event.
    /// </summary>
    [Description("Time (ms) below threshold value that is required to trigger a DO1 pin event.")]
    public partial class DO1BufferFallingEdge
    {
        /// <summary>
        /// Represents the address of the <see cref="DO1BufferFallingEdge"/> register. This field is constant.
        /// </summary>
        public const int Address = 83;

        /// <summary>
        /// Represents the payload type of the <see cref="DO1BufferFallingEdge"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="DO1BufferFallingEdge"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DO1BufferFallingEdge"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DO1BufferFallingEdge"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DO1BufferFallingEdge"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO1BufferFallingEdge"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DO1BufferFallingEdge"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO1BufferFallingEdge"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DO1BufferFallingEdge register.
    /// </summary>
    /// <seealso cref="DO1BufferFallingEdge"/>
    [Description("Filters and selects timestamped messages from the DO1BufferFallingEdge register.")]
    public partial class TimestampedDO1BufferFallingEdge
    {
        /// <summary>
        /// Represents the address of the <see cref="DO1BufferFallingEdge"/> register. This field is constant.
        /// </summary>
        public const int Address = DO1BufferFallingEdge.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DO1BufferFallingEdge"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return DO1BufferFallingEdge.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that time (ms) below threshold value that is required to trigger a DO2 pin event.
    /// </summary>
    [Description("Time (ms) below threshold value that is required to trigger a DO2 pin event.")]
    public partial class DO2BufferFallingEdge
    {
        /// <summary>
        /// Represents the address of the <see cref="DO2BufferFallingEdge"/> register. This field is constant.
        /// </summary>
        public const int Address = 84;

        /// <summary>
        /// Represents the payload type of the <see cref="DO2BufferFallingEdge"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="DO2BufferFallingEdge"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DO2BufferFallingEdge"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DO2BufferFallingEdge"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DO2BufferFallingEdge"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO2BufferFallingEdge"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DO2BufferFallingEdge"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO2BufferFallingEdge"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DO2BufferFallingEdge register.
    /// </summary>
    /// <seealso cref="DO2BufferFallingEdge"/>
    [Description("Filters and selects timestamped messages from the DO2BufferFallingEdge register.")]
    public partial class TimestampedDO2BufferFallingEdge
    {
        /// <summary>
        /// Represents the address of the <see cref="DO2BufferFallingEdge"/> register. This field is constant.
        /// </summary>
        public const int Address = DO2BufferFallingEdge.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DO2BufferFallingEdge"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return DO2BufferFallingEdge.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that time (ms) below threshold value that is required to trigger a DO3 pin event.
    /// </summary>
    [Description("Time (ms) below threshold value that is required to trigger a DO3 pin event.")]
    public partial class DO3BufferFallingEdge
    {
        /// <summary>
        /// Represents the address of the <see cref="DO3BufferFallingEdge"/> register. This field is constant.
        /// </summary>
        public const int Address = 85;

        /// <summary>
        /// Represents the payload type of the <see cref="DO3BufferFallingEdge"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="DO3BufferFallingEdge"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DO3BufferFallingEdge"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DO3BufferFallingEdge"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DO3BufferFallingEdge"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO3BufferFallingEdge"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DO3BufferFallingEdge"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO3BufferFallingEdge"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DO3BufferFallingEdge register.
    /// </summary>
    /// <seealso cref="DO3BufferFallingEdge"/>
    [Description("Filters and selects timestamped messages from the DO3BufferFallingEdge register.")]
    public partial class TimestampedDO3BufferFallingEdge
    {
        /// <summary>
        /// Represents the address of the <see cref="DO3BufferFallingEdge"/> register. This field is constant.
        /// </summary>
        public const int Address = DO3BufferFallingEdge.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DO3BufferFallingEdge"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return DO3BufferFallingEdge.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents an operator which creates standard message payloads for the
    /// AnalogInput device.
    /// </summary>
    /// <seealso cref="CreateStartAcquisitionPayload"/>
    /// <seealso cref="CreateAnalogDataPayload"/>
    /// <seealso cref="CreateInputEventPayload"/>
    /// <seealso cref="CreateRangeAndBandwidthPayload"/>
    /// <seealso cref="CreateSamplingFrequencyPayload"/>
    /// <seealso cref="CreateDI0ModePayload"/>
    /// <seealso cref="CreateDO0ModePayload"/>
    /// <seealso cref="CreateDO0PulseDurationPayload"/>
    /// <seealso cref="CreateOutputSetPayload"/>
    /// <seealso cref="CreateOutputClearPayload"/>
    /// <seealso cref="CreateOutputTogglePayload"/>
    /// <seealso cref="CreateOutputStatePayload"/>
    /// <seealso cref="CreateAcquisitionStartOutputPayload"/>
    /// <seealso cref="CreateDO0TargetChannelPayload"/>
    /// <seealso cref="CreateDO1TargetChannelPayload"/>
    /// <seealso cref="CreateDO2TargetChannelPayload"/>
    /// <seealso cref="CreateDO3TargetChannelPayload"/>
    /// <seealso cref="CreateDO0ThresholdPayload"/>
    /// <seealso cref="CreateDO1ThresholdPayload"/>
    /// <seealso cref="CreateDO2ThresholdPayload"/>
    /// <seealso cref="CreateDO3ThresholdPayload"/>
    /// <seealso cref="CreateDO0BufferRisingEdgePayload"/>
    /// <seealso cref="CreateDO1BufferRisingEdgePayload"/>
    /// <seealso cref="CreateDO2BufferRisingEdgePayload"/>
    /// <seealso cref="CreateDO3BufferRisingEdgePayload"/>
    /// <seealso cref="CreateDO0BufferFallingEdgePayload"/>
    /// <seealso cref="CreateDO1BufferFallingEdgePayload"/>
    /// <seealso cref="CreateDO2BufferFallingEdgePayload"/>
    /// <seealso cref="CreateDO3BufferFallingEdgePayload"/>
    [XmlInclude(typeof(CreateStartAcquisitionPayload))]
    [XmlInclude(typeof(CreateAnalogDataPayload))]
    [XmlInclude(typeof(CreateInputEventPayload))]
    [XmlInclude(typeof(CreateRangeAndBandwidthPayload))]
    [XmlInclude(typeof(CreateSamplingFrequencyPayload))]
    [XmlInclude(typeof(CreateDI0ModePayload))]
    [XmlInclude(typeof(CreateDO0ModePayload))]
    [XmlInclude(typeof(CreateDO0PulseDurationPayload))]
    [XmlInclude(typeof(CreateOutputSetPayload))]
    [XmlInclude(typeof(CreateOutputClearPayload))]
    [XmlInclude(typeof(CreateOutputTogglePayload))]
    [XmlInclude(typeof(CreateOutputStatePayload))]
    [XmlInclude(typeof(CreateAcquisitionStartOutputPayload))]
    [XmlInclude(typeof(CreateDO0TargetChannelPayload))]
    [XmlInclude(typeof(CreateDO1TargetChannelPayload))]
    [XmlInclude(typeof(CreateDO2TargetChannelPayload))]
    [XmlInclude(typeof(CreateDO3TargetChannelPayload))]
    [XmlInclude(typeof(CreateDO0ThresholdPayload))]
    [XmlInclude(typeof(CreateDO1ThresholdPayload))]
    [XmlInclude(typeof(CreateDO2ThresholdPayload))]
    [XmlInclude(typeof(CreateDO3ThresholdPayload))]
    [XmlInclude(typeof(CreateDO0BufferRisingEdgePayload))]
    [XmlInclude(typeof(CreateDO1BufferRisingEdgePayload))]
    [XmlInclude(typeof(CreateDO2BufferRisingEdgePayload))]
    [XmlInclude(typeof(CreateDO3BufferRisingEdgePayload))]
    [XmlInclude(typeof(CreateDO0BufferFallingEdgePayload))]
    [XmlInclude(typeof(CreateDO1BufferFallingEdgePayload))]
    [XmlInclude(typeof(CreateDO2BufferFallingEdgePayload))]
    [XmlInclude(typeof(CreateDO3BufferFallingEdgePayload))]
    [Description("Creates standard message payloads for the AnalogInput device.")]
    public partial class CreateMessage : CreateMessageBuilder, INamedElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateMessage"/> class.
        /// </summary>
        public CreateMessage()
        {
            Payload = new CreateStartAcquisitionPayload();
        }

        string INamedElement.Name => $"{nameof(AnalogInput)}.{GetElementDisplayName(Payload)}";
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that enables the data acquisition.
    /// </summary>
    [DisplayName("StartAcquisitionPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that enables the data acquisition.")]
    public partial class CreateStartAcquisitionPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that enables the data acquisition.
        /// </summary>
        [Description("The value that enables the data acquisition.")]
        public EnableFlag Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that enables the data acquisition.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that enables the data acquisition.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => StartAcquisition.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that value from a single read of all ADC channels.
    /// </summary>
    [DisplayName("AnalogDataPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that value from a single read of all ADC channels.")]
    public partial class CreateAnalogDataPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets a value that channel0.
        /// </summary>
        [Description("Channel0")]
        public short Channel0 { get; set; }

        /// <summary>
        /// Gets or sets a value that channel1.
        /// </summary>
        [Description("Channel1")]
        public short Channel1 { get; set; }

        /// <summary>
        /// Gets or sets a value that channel2.
        /// </summary>
        [Description("Channel2")]
        public short Channel2 { get; set; }

        /// <summary>
        /// Gets or sets a value that channel3.
        /// </summary>
        [Description("Channel3")]
        public short Channel3 { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that value from a single read of all ADC channels.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that value from a single read of all ADC channels.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ =>
            {
                AnalogDataPayload value;
                value.Channel0 = Channel0;
                value.Channel1 = Channel1;
                value.Channel2 = Channel2;
                value.Channel3 = Channel3;
                return AnalogData.FromPayload(MessageType, value);
            });
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that state of the digital input pin 0.
    /// </summary>
    [DisplayName("InputEventPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that state of the digital input pin 0.")]
    public partial class CreateInputEventPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that state of the digital input pin 0.
        /// </summary>
        [Description("The value that state of the digital input pin 0.")]
        public DigitalInputState Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that state of the digital input pin 0.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that state of the digital input pin 0.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => InputEvent.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sets the range and bandwidth of the ADC.
    /// </summary>
    [DisplayName("RangeAndBandwidthPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the range and bandwidth of the ADC.")]
    public partial class CreateRangeAndBandwidthPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sets the range and bandwidth of the ADC.
        /// </summary>
        [Description("The value that sets the range and bandwidth of the ADC.")]
        public RangeAndBandwidthConfig Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the range and bandwidth of the ADC.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that sets the range and bandwidth of the ADC.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => RangeAndBandwidth.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sets the sampling frequency of the ADC.
    /// </summary>
    [DisplayName("SamplingFrequencyPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the sampling frequency of the ADC.")]
    public partial class CreateSamplingFrequencyPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sets the sampling frequency of the ADC.
        /// </summary>
        [Description("The value that sets the sampling frequency of the ADC.")]
        public SamplingFrequencyConfig Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the sampling frequency of the ADC.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that sets the sampling frequency of the ADC.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => SamplingFrequency.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that configuration of the digital input pin 0.
    /// </summary>
    [DisplayName("DI0ModePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that configuration of the digital input pin 0.")]
    public partial class CreateDI0ModePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that configuration of the digital input pin 0.
        /// </summary>
        [Description("The value that configuration of the digital input pin 0.")]
        public DI0Configuration Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that configuration of the digital input pin 0.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that configuration of the digital input pin 0.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => DI0Mode.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that configuration of the digital output pin 0.
    /// </summary>
    [DisplayName("DO0ModePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that configuration of the digital output pin 0.")]
    public partial class CreateDO0ModePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that configuration of the digital output pin 0.
        /// </summary>
        [Description("The value that configuration of the digital output pin 0.")]
        public DO0Configuration Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that configuration of the digital output pin 0.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that configuration of the digital output pin 0.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => DO0Mode.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that pulse duration (ms) for the digital output pin 0. The pulse will only be emitted when DO0Mode == Pulse.
    /// </summary>
    [DisplayName("DO0PulseDurationPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that pulse duration (ms) for the digital output pin 0. The pulse will only be emitted when DO0Mode == Pulse.")]
    public partial class CreateDO0PulseDurationPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that pulse duration (ms) for the digital output pin 0. The pulse will only be emitted when DO0Mode == Pulse.
        /// </summary>
        [Range(min: 1, max: 255)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that pulse duration (ms) for the digital output pin 0. The pulse will only be emitted when DO0Mode == Pulse.")]
        public byte Value { get; set; } = 1;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that pulse duration (ms) for the digital output pin 0. The pulse will only be emitted when DO0Mode == Pulse.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that pulse duration (ms) for the digital output pin 0. The pulse will only be emitted when DO0Mode == Pulse.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => DO0PulseDuration.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that set the specified digital output lines.
    /// </summary>
    [DisplayName("OutputSetPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that set the specified digital output lines.")]
    public partial class CreateOutputSetPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that set the specified digital output lines.
        /// </summary>
        [Description("The value that set the specified digital output lines.")]
        public DigitalOutputs Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that set the specified digital output lines.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that set the specified digital output lines.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => OutputSet.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that clear the specified digital output lines.
    /// </summary>
    [DisplayName("OutputClearPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that clear the specified digital output lines.")]
    public partial class CreateOutputClearPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that clear the specified digital output lines.
        /// </summary>
        [Description("The value that clear the specified digital output lines.")]
        public DigitalOutputs Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that clear the specified digital output lines.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that clear the specified digital output lines.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => OutputClear.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that toggle the specified digital output lines.
    /// </summary>
    [DisplayName("OutputTogglePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that toggle the specified digital output lines.")]
    public partial class CreateOutputTogglePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that toggle the specified digital output lines.
        /// </summary>
        [Description("The value that toggle the specified digital output lines.")]
        public DigitalOutputs Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that toggle the specified digital output lines.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that toggle the specified digital output lines.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => OutputToggle.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that write the state of all digital output lines. An event will be emitted when the value of any pin was changed by a threshold crossing event.
    /// </summary>
    [DisplayName("OutputStatePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that write the state of all digital output lines. An event will be emitted when the value of any pin was changed by a threshold crossing event.")]
    public partial class CreateOutputStatePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that write the state of all digital output lines. An event will be emitted when the value of any pin was changed by a threshold crossing event.
        /// </summary>
        [Description("The value that write the state of all digital output lines. An event will be emitted when the value of any pin was changed by a threshold crossing event.")]
        public DigitalOutputs Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that write the state of all digital output lines. An event will be emitted when the value of any pin was changed by a threshold crossing event.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that write the state of all digital output lines. An event will be emitted when the value of any pin was changed by a threshold crossing event.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => OutputState.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that digital output that will be set when the acquisition starts.
    /// </summary>
    [DisplayName("AcquisitionStartOutputPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that digital output that will be set when the acquisition starts.")]
    public partial class CreateAcquisitionStartOutputPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that digital output that will be set when the acquisition starts.
        /// </summary>
        [Description("The value that digital output that will be set when the acquisition starts.")]
        public AcquisitionStartTargets Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that digital output that will be set when the acquisition starts.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that digital output that will be set when the acquisition starts.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => AcquisitionStartOutput.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that target channel that will be used to trigger a threshold event on DO0 pin.
    /// </summary>
    [DisplayName("DO0TargetChannelPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that target channel that will be used to trigger a threshold event on DO0 pin.")]
    public partial class CreateDO0TargetChannelPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that target channel that will be used to trigger a threshold event on DO0 pin.
        /// </summary>
        [Description("The value that target channel that will be used to trigger a threshold event on DO0 pin.")]
        public ThresholdOnChannel Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that target channel that will be used to trigger a threshold event on DO0 pin.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that target channel that will be used to trigger a threshold event on DO0 pin.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => DO0TargetChannel.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that target channel that will be used to trigger a threshold event on DO1 pin.
    /// </summary>
    [DisplayName("DO1TargetChannelPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that target channel that will be used to trigger a threshold event on DO1 pin.")]
    public partial class CreateDO1TargetChannelPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that target channel that will be used to trigger a threshold event on DO1 pin.
        /// </summary>
        [Description("The value that target channel that will be used to trigger a threshold event on DO1 pin.")]
        public ThresholdOnChannel Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that target channel that will be used to trigger a threshold event on DO1 pin.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that target channel that will be used to trigger a threshold event on DO1 pin.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => DO1TargetChannel.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that target channel that will be used to trigger a threshold event on DO2 pin.
    /// </summary>
    [DisplayName("DO2TargetChannelPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that target channel that will be used to trigger a threshold event on DO2 pin.")]
    public partial class CreateDO2TargetChannelPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that target channel that will be used to trigger a threshold event on DO2 pin.
        /// </summary>
        [Description("The value that target channel that will be used to trigger a threshold event on DO2 pin.")]
        public ThresholdOnChannel Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that target channel that will be used to trigger a threshold event on DO2 pin.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that target channel that will be used to trigger a threshold event on DO2 pin.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => DO2TargetChannel.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that target channel that will be used to trigger a threshold event on DO3 pin.
    /// </summary>
    [DisplayName("DO3TargetChannelPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that target channel that will be used to trigger a threshold event on DO3 pin.")]
    public partial class CreateDO3TargetChannelPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that target channel that will be used to trigger a threshold event on DO3 pin.
        /// </summary>
        [Description("The value that target channel that will be used to trigger a threshold event on DO3 pin.")]
        public ThresholdOnChannel Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that target channel that will be used to trigger a threshold event on DO3 pin.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that target channel that will be used to trigger a threshold event on DO3 pin.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => DO3TargetChannel.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that value used to threshold an ADC read, and trigger DO0 pin.
    /// </summary>
    [DisplayName("DO0ThresholdPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that value used to threshold an ADC read, and trigger DO0 pin.")]
    public partial class CreateDO0ThresholdPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that value used to threshold an ADC read, and trigger DO0 pin.
        /// </summary>
        [Description("The value that value used to threshold an ADC read, and trigger DO0 pin.")]
        public short Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that value used to threshold an ADC read, and trigger DO0 pin.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that value used to threshold an ADC read, and trigger DO0 pin.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => DO0Threshold.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that value used to threshold an ADC read, and trigger DO1 pin.
    /// </summary>
    [DisplayName("DO1ThresholdPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that value used to threshold an ADC read, and trigger DO1 pin.")]
    public partial class CreateDO1ThresholdPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that value used to threshold an ADC read, and trigger DO1 pin.
        /// </summary>
        [Description("The value that value used to threshold an ADC read, and trigger DO1 pin.")]
        public short Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that value used to threshold an ADC read, and trigger DO1 pin.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that value used to threshold an ADC read, and trigger DO1 pin.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => DO1Threshold.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that value used to threshold an ADC read, and trigger DO2 pin.
    /// </summary>
    [DisplayName("DO2ThresholdPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that value used to threshold an ADC read, and trigger DO2 pin.")]
    public partial class CreateDO2ThresholdPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that value used to threshold an ADC read, and trigger DO2 pin.
        /// </summary>
        [Description("The value that value used to threshold an ADC read, and trigger DO2 pin.")]
        public short Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that value used to threshold an ADC read, and trigger DO2 pin.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that value used to threshold an ADC read, and trigger DO2 pin.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => DO2Threshold.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that value used to threshold an ADC read, and trigger DO3 pin.
    /// </summary>
    [DisplayName("DO3ThresholdPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that value used to threshold an ADC read, and trigger DO3 pin.")]
    public partial class CreateDO3ThresholdPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that value used to threshold an ADC read, and trigger DO3 pin.
        /// </summary>
        [Description("The value that value used to threshold an ADC read, and trigger DO3 pin.")]
        public short Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that value used to threshold an ADC read, and trigger DO3 pin.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that value used to threshold an ADC read, and trigger DO3 pin.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => DO3Threshold.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that time (ms) above threshold value that is required to trigger a DO0 pin event.
    /// </summary>
    [DisplayName("DO0BufferRisingEdgePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that time (ms) above threshold value that is required to trigger a DO0 pin event.")]
    public partial class CreateDO0BufferRisingEdgePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that time (ms) above threshold value that is required to trigger a DO0 pin event.
        /// </summary>
        [Description("The value that time (ms) above threshold value that is required to trigger a DO0 pin event.")]
        public ushort Value { get; set; } = 0;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that time (ms) above threshold value that is required to trigger a DO0 pin event.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that time (ms) above threshold value that is required to trigger a DO0 pin event.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => DO0BufferRisingEdge.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that time (ms) above threshold value that is required to trigger a DO1 pin event.
    /// </summary>
    [DisplayName("DO1BufferRisingEdgePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that time (ms) above threshold value that is required to trigger a DO1 pin event.")]
    public partial class CreateDO1BufferRisingEdgePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that time (ms) above threshold value that is required to trigger a DO1 pin event.
        /// </summary>
        [Description("The value that time (ms) above threshold value that is required to trigger a DO1 pin event.")]
        public ushort Value { get; set; } = 0;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that time (ms) above threshold value that is required to trigger a DO1 pin event.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that time (ms) above threshold value that is required to trigger a DO1 pin event.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => DO1BufferRisingEdge.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that time (ms) above threshold value that is required to trigger a DO2 pin event.
    /// </summary>
    [DisplayName("DO2BufferRisingEdgePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that time (ms) above threshold value that is required to trigger a DO2 pin event.")]
    public partial class CreateDO2BufferRisingEdgePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that time (ms) above threshold value that is required to trigger a DO2 pin event.
        /// </summary>
        [Description("The value that time (ms) above threshold value that is required to trigger a DO2 pin event.")]
        public ushort Value { get; set; } = 0;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that time (ms) above threshold value that is required to trigger a DO2 pin event.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that time (ms) above threshold value that is required to trigger a DO2 pin event.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => DO2BufferRisingEdge.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that time (ms) above threshold value that is required to trigger a DO3 pin event.
    /// </summary>
    [DisplayName("DO3BufferRisingEdgePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that time (ms) above threshold value that is required to trigger a DO3 pin event.")]
    public partial class CreateDO3BufferRisingEdgePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that time (ms) above threshold value that is required to trigger a DO3 pin event.
        /// </summary>
        [Description("The value that time (ms) above threshold value that is required to trigger a DO3 pin event.")]
        public ushort Value { get; set; } = 0;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that time (ms) above threshold value that is required to trigger a DO3 pin event.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that time (ms) above threshold value that is required to trigger a DO3 pin event.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => DO3BufferRisingEdge.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that time (ms) below threshold value that is required to trigger a DO0 pin event.
    /// </summary>
    [DisplayName("DO0BufferFallingEdgePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that time (ms) below threshold value that is required to trigger a DO0 pin event.")]
    public partial class CreateDO0BufferFallingEdgePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that time (ms) below threshold value that is required to trigger a DO0 pin event.
        /// </summary>
        [Description("The value that time (ms) below threshold value that is required to trigger a DO0 pin event.")]
        public ushort Value { get; set; } = 0;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that time (ms) below threshold value that is required to trigger a DO0 pin event.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that time (ms) below threshold value that is required to trigger a DO0 pin event.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => DO0BufferFallingEdge.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that time (ms) below threshold value that is required to trigger a DO1 pin event.
    /// </summary>
    [DisplayName("DO1BufferFallingEdgePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that time (ms) below threshold value that is required to trigger a DO1 pin event.")]
    public partial class CreateDO1BufferFallingEdgePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that time (ms) below threshold value that is required to trigger a DO1 pin event.
        /// </summary>
        [Description("The value that time (ms) below threshold value that is required to trigger a DO1 pin event.")]
        public ushort Value { get; set; } = 0;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that time (ms) below threshold value that is required to trigger a DO1 pin event.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that time (ms) below threshold value that is required to trigger a DO1 pin event.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => DO1BufferFallingEdge.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that time (ms) below threshold value that is required to trigger a DO2 pin event.
    /// </summary>
    [DisplayName("DO2BufferFallingEdgePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that time (ms) below threshold value that is required to trigger a DO2 pin event.")]
    public partial class CreateDO2BufferFallingEdgePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that time (ms) below threshold value that is required to trigger a DO2 pin event.
        /// </summary>
        [Description("The value that time (ms) below threshold value that is required to trigger a DO2 pin event.")]
        public ushort Value { get; set; } = 0;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that time (ms) below threshold value that is required to trigger a DO2 pin event.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that time (ms) below threshold value that is required to trigger a DO2 pin event.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => DO2BufferFallingEdge.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that time (ms) below threshold value that is required to trigger a DO3 pin event.
    /// </summary>
    [DisplayName("DO3BufferFallingEdgePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that time (ms) below threshold value that is required to trigger a DO3 pin event.")]
    public partial class CreateDO3BufferFallingEdgePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that time (ms) below threshold value that is required to trigger a DO3 pin event.
        /// </summary>
        [Description("The value that time (ms) below threshold value that is required to trigger a DO3 pin event.")]
        public ushort Value { get; set; } = 0;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that time (ms) below threshold value that is required to trigger a DO3 pin event.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that time (ms) below threshold value that is required to trigger a DO3 pin event.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => DO3BufferFallingEdge.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents the payload of the AnalogData register.
    /// </summary>
    public struct AnalogDataPayload
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AnalogDataPayload"/> structure.
        /// </summary>
        /// <param name="channel0">Channel0</param>
        /// <param name="channel1">Channel1</param>
        /// <param name="channel2">Channel2</param>
        /// <param name="channel3">Channel3</param>
        public AnalogDataPayload(
            short channel0,
            short channel1,
            short channel2,
            short channel3)
        {
            Channel0 = channel0;
            Channel1 = channel1;
            Channel2 = channel2;
            Channel3 = channel3;
        }

        /// <summary>
        /// Channel0
        /// </summary>
        public short Channel0;

        /// <summary>
        /// Channel1
        /// </summary>
        public short Channel1;

        /// <summary>
        /// Channel2
        /// </summary>
        public short Channel2;

        /// <summary>
        /// Channel3
        /// </summary>
        public short Channel3;
    }

    /// <summary>
    /// Specifies the state of port digital output lines.
    /// </summary>
    [Flags]
    public enum DigitalOutputs : byte
    {
        DO0 = 0x1,
        DO1 = 0x2,
        DO2 = 0x4,
        DO3 = 0x8,
        DO0Changed = 0x10,
        DO1Changed = 0x20,
        DO2Changed = 0x40,
        DO4Changed = 0x80
    }

    /// <summary>
    /// The state of an abstract functionality.
    /// </summary>
    public enum EnableFlag : byte
    {
        Disabled = 0,
        Enabled = 1
    }

    /// <summary>
    /// The state of a digital input pin.
    /// </summary>
    public enum DigitalInputState : byte
    {
        Low = 0,
        High = 1
    }

    /// <summary>
    /// The available settings for set the range and bandwidth of the ADC.
    /// </summary>
    public enum RangeAndBandwidthConfig : byte
    {
        Range5V_Bandwidth1k5 = 6,
        Range5V_Bandwidth3k = 5,
        Range5V_Bandwidth6k = 4,
        Range5V_Bandwidth10k3 = 3,
        Range5V_Bandwidth13k7 = 2,
        Range5V_Bandwidth15k = 1,
        Range10V_Bandwidth1k5 = 22,
        Range10V_Bandwidth3k = 21,
        Range10V_Bandwidth6k = 20,
        Range10V_Bandwidth11k9 = 19,
        Range10V_Bandwidth18k5 = 18,
        Range10V_Bandwidth22k = 17
    }

    /// <summary>
    /// The available settings for set the sampling frequency of the ADC.
    /// </summary>
    public enum SamplingFrequencyConfig : byte
    {
        Frequency1k = 0,
        Frequency2k = 1
    }

    /// <summary>
    /// Available configurations for DI0 pin.
    /// </summary>
    public enum DI0Configuration : byte
    {
        Input = 0,
        StartOnRisingEdge = 1,
        StartOnFallingEdge = 2,
        SampleOnRisingEdge = 3,
        SampleOnFallingEdge = 4
    }

    /// <summary>
    /// Available configurations for DO0 pin.
    /// </summary>
    public enum DO0Configuration : byte
    {
        Output = 0,
        ToggleEachSecond = 1,
        Pulse = 2
    }

    /// <summary>
    /// Available digital output pins that are able to be triggered on acquisition start.
    /// </summary>
    public enum AcquisitionStartTargets : byte
    {
        None = 0,
        DO0 = 1,
        DO1 = 2,
        DO2 = 3,
        DO3 = 4
    }

    /// <summary>
    /// Available target analog channels to be targeted for threshold events.
    /// </summary>
    public enum ThresholdOnChannel : byte
    {
        Channel0 = 0,
        Channel1 = 1,
        Channel2 = 2,
        Channel3 = 3,
        None = 8
    }
}
