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
            { 32, typeof(AcquisitionState) },
            { 33, typeof(AnalogData) },
            { 34, typeof(DigitalInputState) },
            { 35, typeof(Reserved0) },
            { 36, typeof(Reserved1) },
            { 37, typeof(RangeAndFilter) },
            { 38, typeof(SamplingRate) },
            { 39, typeof(DI0Trigger) },
            { 40, typeof(DO0Sync) },
            { 41, typeof(DO0PulseWidth) },
            { 42, typeof(DigitalOutputSet) },
            { 43, typeof(DigitalOutputClear) },
            { 44, typeof(DigitalOutputToggle) },
            { 45, typeof(DigitalOutputState) },
            { 46, typeof(Reserved2) },
            { 47, typeof(Reserved3) },
            { 48, typeof(SyncOutput) },
            { 49, typeof(Reserved4) },
            { 50, typeof(Reserved5) },
            { 51, typeof(Reserved6) },
            { 52, typeof(Reserved7) },
            { 53, typeof(Reserved8) },
            { 54, typeof(Reserved9) },
            { 55, typeof(Reserved10) },
            { 56, typeof(Reserved11) },
            { 57, typeof(Reserved12) },
            { 58, typeof(DO0TargetChannel) },
            { 59, typeof(DO1TargetChannel) },
            { 60, typeof(DO2TargetChannel) },
            { 61, typeof(DO3TargetChannel) },
            { 62, typeof(Reserved13) },
            { 63, typeof(Reserved14) },
            { 64, typeof(Reserved15) },
            { 65, typeof(Reserved16) },
            { 66, typeof(DO0Threshold) },
            { 67, typeof(DO1Threshold) },
            { 68, typeof(DO2Threshold) },
            { 69, typeof(DO3Threshold) },
            { 70, typeof(Reserved17) },
            { 71, typeof(Reserved18) },
            { 72, typeof(Reserved19) },
            { 73, typeof(Reserved20) },
            { 74, typeof(DO0TimeAboveThreshold) },
            { 75, typeof(DO1TimeAboveThreshold) },
            { 76, typeof(DO2TimeAboveThreshold) },
            { 77, typeof(DO3TimeAboveThreshold) },
            { 78, typeof(Reserved21) },
            { 79, typeof(Reserved22) },
            { 80, typeof(Reserved23) },
            { 81, typeof(Reserved24) },
            { 82, typeof(DO0TimeBelowThreshold) },
            { 83, typeof(DO1TimeBelowThreshold) },
            { 84, typeof(DO2TimeBelowThreshold) },
            { 85, typeof(DO3TimeBelowThreshold) },
            { 86, typeof(Reserved25) },
            { 87, typeof(Reserved26) },
            { 88, typeof(Reserved27) },
            { 89, typeof(Reserved28) },
            { 90, typeof(Reserved29) }
        };

        /// <summary>
        /// Gets the contents of the metadata file describing the <see cref="AnalogInput"/>
        /// device registers.
        /// </summary>
        public static readonly string Metadata = GetDeviceMetadata();

        static string GetDeviceMetadata()
        {
            var deviceType = typeof(Device);
            using var metadataStream = deviceType.Assembly.GetManifestResourceStream($"{deviceType.Namespace}.device.yml");
            using var streamReader = new System.IO.StreamReader(metadataStream);
            return streamReader.ReadToEnd();
        }
    }

    /// <summary>
    /// Represents an operator that returns the contents of the metadata file
    /// describing the <see cref="AnalogInput"/> device registers.
    /// </summary>
    [Description("Returns the contents of the metadata file describing the AnalogInput device registers.")]
    public partial class GetMetadata : Source<string>
    {
        /// <summary>
        /// Returns an observable sequence with the contents of the metadata file
        /// describing the <see cref="AnalogInput"/> device registers.
        /// </summary>
        /// <returns>
        /// A sequence with a single <see cref="string"/> object representing the
        /// contents of the metadata file.
        /// </returns>
        public override IObservable<string> Generate()
        {
            return Observable.Return(Device.Metadata);
        }
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
    /// <seealso cref="AcquisitionState"/>
    /// <seealso cref="AnalogData"/>
    /// <seealso cref="DigitalInputState"/>
    /// <seealso cref="RangeAndFilter"/>
    /// <seealso cref="SamplingRate"/>
    /// <seealso cref="DI0Trigger"/>
    /// <seealso cref="DO0Sync"/>
    /// <seealso cref="DO0PulseWidth"/>
    /// <seealso cref="DigitalOutputSet"/>
    /// <seealso cref="DigitalOutputClear"/>
    /// <seealso cref="DigitalOutputToggle"/>
    /// <seealso cref="DigitalOutputState"/>
    /// <seealso cref="SyncOutput"/>
    /// <seealso cref="DO0TargetChannel"/>
    /// <seealso cref="DO1TargetChannel"/>
    /// <seealso cref="DO2TargetChannel"/>
    /// <seealso cref="DO3TargetChannel"/>
    /// <seealso cref="DO0Threshold"/>
    /// <seealso cref="DO1Threshold"/>
    /// <seealso cref="DO2Threshold"/>
    /// <seealso cref="DO3Threshold"/>
    /// <seealso cref="DO0TimeAboveThreshold"/>
    /// <seealso cref="DO1TimeAboveThreshold"/>
    /// <seealso cref="DO2TimeAboveThreshold"/>
    /// <seealso cref="DO3TimeAboveThreshold"/>
    /// <seealso cref="DO0TimeBelowThreshold"/>
    /// <seealso cref="DO1TimeBelowThreshold"/>
    /// <seealso cref="DO2TimeBelowThreshold"/>
    /// <seealso cref="DO3TimeBelowThreshold"/>
    [XmlInclude(typeof(AcquisitionState))]
    [XmlInclude(typeof(AnalogData))]
    [XmlInclude(typeof(DigitalInputState))]
    [XmlInclude(typeof(RangeAndFilter))]
    [XmlInclude(typeof(SamplingRate))]
    [XmlInclude(typeof(DI0Trigger))]
    [XmlInclude(typeof(DO0Sync))]
    [XmlInclude(typeof(DO0PulseWidth))]
    [XmlInclude(typeof(DigitalOutputSet))]
    [XmlInclude(typeof(DigitalOutputClear))]
    [XmlInclude(typeof(DigitalOutputToggle))]
    [XmlInclude(typeof(DigitalOutputState))]
    [XmlInclude(typeof(SyncOutput))]
    [XmlInclude(typeof(DO0TargetChannel))]
    [XmlInclude(typeof(DO1TargetChannel))]
    [XmlInclude(typeof(DO2TargetChannel))]
    [XmlInclude(typeof(DO3TargetChannel))]
    [XmlInclude(typeof(DO0Threshold))]
    [XmlInclude(typeof(DO1Threshold))]
    [XmlInclude(typeof(DO2Threshold))]
    [XmlInclude(typeof(DO3Threshold))]
    [XmlInclude(typeof(DO0TimeAboveThreshold))]
    [XmlInclude(typeof(DO1TimeAboveThreshold))]
    [XmlInclude(typeof(DO2TimeAboveThreshold))]
    [XmlInclude(typeof(DO3TimeAboveThreshold))]
    [XmlInclude(typeof(DO0TimeBelowThreshold))]
    [XmlInclude(typeof(DO1TimeBelowThreshold))]
    [XmlInclude(typeof(DO2TimeBelowThreshold))]
    [XmlInclude(typeof(DO3TimeBelowThreshold))]
    [Description("Filters register-specific messages reported by the AnalogInput device.")]
    public class FilterRegister : FilterRegisterBuilder, INamedElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FilterRegister"/> class.
        /// </summary>
        public FilterRegister()
        {
            Register = new AcquisitionState();
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
    /// <seealso cref="AcquisitionState"/>
    /// <seealso cref="AnalogData"/>
    /// <seealso cref="DigitalInputState"/>
    /// <seealso cref="RangeAndFilter"/>
    /// <seealso cref="SamplingRate"/>
    /// <seealso cref="DI0Trigger"/>
    /// <seealso cref="DO0Sync"/>
    /// <seealso cref="DO0PulseWidth"/>
    /// <seealso cref="DigitalOutputSet"/>
    /// <seealso cref="DigitalOutputClear"/>
    /// <seealso cref="DigitalOutputToggle"/>
    /// <seealso cref="DigitalOutputState"/>
    /// <seealso cref="SyncOutput"/>
    /// <seealso cref="DO0TargetChannel"/>
    /// <seealso cref="DO1TargetChannel"/>
    /// <seealso cref="DO2TargetChannel"/>
    /// <seealso cref="DO3TargetChannel"/>
    /// <seealso cref="DO0Threshold"/>
    /// <seealso cref="DO1Threshold"/>
    /// <seealso cref="DO2Threshold"/>
    /// <seealso cref="DO3Threshold"/>
    /// <seealso cref="DO0TimeAboveThreshold"/>
    /// <seealso cref="DO1TimeAboveThreshold"/>
    /// <seealso cref="DO2TimeAboveThreshold"/>
    /// <seealso cref="DO3TimeAboveThreshold"/>
    /// <seealso cref="DO0TimeBelowThreshold"/>
    /// <seealso cref="DO1TimeBelowThreshold"/>
    /// <seealso cref="DO2TimeBelowThreshold"/>
    /// <seealso cref="DO3TimeBelowThreshold"/>
    [XmlInclude(typeof(AcquisitionState))]
    [XmlInclude(typeof(AnalogData))]
    [XmlInclude(typeof(DigitalInputState))]
    [XmlInclude(typeof(RangeAndFilter))]
    [XmlInclude(typeof(SamplingRate))]
    [XmlInclude(typeof(DI0Trigger))]
    [XmlInclude(typeof(DO0Sync))]
    [XmlInclude(typeof(DO0PulseWidth))]
    [XmlInclude(typeof(DigitalOutputSet))]
    [XmlInclude(typeof(DigitalOutputClear))]
    [XmlInclude(typeof(DigitalOutputToggle))]
    [XmlInclude(typeof(DigitalOutputState))]
    [XmlInclude(typeof(SyncOutput))]
    [XmlInclude(typeof(DO0TargetChannel))]
    [XmlInclude(typeof(DO1TargetChannel))]
    [XmlInclude(typeof(DO2TargetChannel))]
    [XmlInclude(typeof(DO3TargetChannel))]
    [XmlInclude(typeof(DO0Threshold))]
    [XmlInclude(typeof(DO1Threshold))]
    [XmlInclude(typeof(DO2Threshold))]
    [XmlInclude(typeof(DO3Threshold))]
    [XmlInclude(typeof(DO0TimeAboveThreshold))]
    [XmlInclude(typeof(DO1TimeAboveThreshold))]
    [XmlInclude(typeof(DO2TimeAboveThreshold))]
    [XmlInclude(typeof(DO3TimeAboveThreshold))]
    [XmlInclude(typeof(DO0TimeBelowThreshold))]
    [XmlInclude(typeof(DO1TimeBelowThreshold))]
    [XmlInclude(typeof(DO2TimeBelowThreshold))]
    [XmlInclude(typeof(DO3TimeBelowThreshold))]
    [XmlInclude(typeof(TimestampedAcquisitionState))]
    [XmlInclude(typeof(TimestampedAnalogData))]
    [XmlInclude(typeof(TimestampedDigitalInputState))]
    [XmlInclude(typeof(TimestampedRangeAndFilter))]
    [XmlInclude(typeof(TimestampedSamplingRate))]
    [XmlInclude(typeof(TimestampedDI0Trigger))]
    [XmlInclude(typeof(TimestampedDO0Sync))]
    [XmlInclude(typeof(TimestampedDO0PulseWidth))]
    [XmlInclude(typeof(TimestampedDigitalOutputSet))]
    [XmlInclude(typeof(TimestampedDigitalOutputClear))]
    [XmlInclude(typeof(TimestampedDigitalOutputToggle))]
    [XmlInclude(typeof(TimestampedDigitalOutputState))]
    [XmlInclude(typeof(TimestampedSyncOutput))]
    [XmlInclude(typeof(TimestampedDO0TargetChannel))]
    [XmlInclude(typeof(TimestampedDO1TargetChannel))]
    [XmlInclude(typeof(TimestampedDO2TargetChannel))]
    [XmlInclude(typeof(TimestampedDO3TargetChannel))]
    [XmlInclude(typeof(TimestampedDO0Threshold))]
    [XmlInclude(typeof(TimestampedDO1Threshold))]
    [XmlInclude(typeof(TimestampedDO2Threshold))]
    [XmlInclude(typeof(TimestampedDO3Threshold))]
    [XmlInclude(typeof(TimestampedDO0TimeAboveThreshold))]
    [XmlInclude(typeof(TimestampedDO1TimeAboveThreshold))]
    [XmlInclude(typeof(TimestampedDO2TimeAboveThreshold))]
    [XmlInclude(typeof(TimestampedDO3TimeAboveThreshold))]
    [XmlInclude(typeof(TimestampedDO0TimeBelowThreshold))]
    [XmlInclude(typeof(TimestampedDO1TimeBelowThreshold))]
    [XmlInclude(typeof(TimestampedDO2TimeBelowThreshold))]
    [XmlInclude(typeof(TimestampedDO3TimeBelowThreshold))]
    [Description("Filters and selects specific messages reported by the AnalogInput device.")]
    public partial class Parse : ParseBuilder, INamedElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Parse"/> class.
        /// </summary>
        public Parse()
        {
            Register = new AcquisitionState();
        }

        string INamedElement.Name => $"{nameof(AnalogInput)}.{GetElementDisplayName(Register)}";
    }

    /// <summary>
    /// Represents an operator which formats a sequence of values as specific
    /// AnalogInput register messages.
    /// </summary>
    /// <seealso cref="AcquisitionState"/>
    /// <seealso cref="AnalogData"/>
    /// <seealso cref="DigitalInputState"/>
    /// <seealso cref="RangeAndFilter"/>
    /// <seealso cref="SamplingRate"/>
    /// <seealso cref="DI0Trigger"/>
    /// <seealso cref="DO0Sync"/>
    /// <seealso cref="DO0PulseWidth"/>
    /// <seealso cref="DigitalOutputSet"/>
    /// <seealso cref="DigitalOutputClear"/>
    /// <seealso cref="DigitalOutputToggle"/>
    /// <seealso cref="DigitalOutputState"/>
    /// <seealso cref="SyncOutput"/>
    /// <seealso cref="DO0TargetChannel"/>
    /// <seealso cref="DO1TargetChannel"/>
    /// <seealso cref="DO2TargetChannel"/>
    /// <seealso cref="DO3TargetChannel"/>
    /// <seealso cref="DO0Threshold"/>
    /// <seealso cref="DO1Threshold"/>
    /// <seealso cref="DO2Threshold"/>
    /// <seealso cref="DO3Threshold"/>
    /// <seealso cref="DO0TimeAboveThreshold"/>
    /// <seealso cref="DO1TimeAboveThreshold"/>
    /// <seealso cref="DO2TimeAboveThreshold"/>
    /// <seealso cref="DO3TimeAboveThreshold"/>
    /// <seealso cref="DO0TimeBelowThreshold"/>
    /// <seealso cref="DO1TimeBelowThreshold"/>
    /// <seealso cref="DO2TimeBelowThreshold"/>
    /// <seealso cref="DO3TimeBelowThreshold"/>
    [XmlInclude(typeof(AcquisitionState))]
    [XmlInclude(typeof(AnalogData))]
    [XmlInclude(typeof(DigitalInputState))]
    [XmlInclude(typeof(RangeAndFilter))]
    [XmlInclude(typeof(SamplingRate))]
    [XmlInclude(typeof(DI0Trigger))]
    [XmlInclude(typeof(DO0Sync))]
    [XmlInclude(typeof(DO0PulseWidth))]
    [XmlInclude(typeof(DigitalOutputSet))]
    [XmlInclude(typeof(DigitalOutputClear))]
    [XmlInclude(typeof(DigitalOutputToggle))]
    [XmlInclude(typeof(DigitalOutputState))]
    [XmlInclude(typeof(SyncOutput))]
    [XmlInclude(typeof(DO0TargetChannel))]
    [XmlInclude(typeof(DO1TargetChannel))]
    [XmlInclude(typeof(DO2TargetChannel))]
    [XmlInclude(typeof(DO3TargetChannel))]
    [XmlInclude(typeof(DO0Threshold))]
    [XmlInclude(typeof(DO1Threshold))]
    [XmlInclude(typeof(DO2Threshold))]
    [XmlInclude(typeof(DO3Threshold))]
    [XmlInclude(typeof(DO0TimeAboveThreshold))]
    [XmlInclude(typeof(DO1TimeAboveThreshold))]
    [XmlInclude(typeof(DO2TimeAboveThreshold))]
    [XmlInclude(typeof(DO3TimeAboveThreshold))]
    [XmlInclude(typeof(DO0TimeBelowThreshold))]
    [XmlInclude(typeof(DO1TimeBelowThreshold))]
    [XmlInclude(typeof(DO2TimeBelowThreshold))]
    [XmlInclude(typeof(DO3TimeBelowThreshold))]
    [Description("Formats a sequence of values as specific AnalogInput register messages.")]
    public partial class Format : FormatBuilder, INamedElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Format"/> class.
        /// </summary>
        public Format()
        {
            Register = new AcquisitionState();
        }

        string INamedElement.Name => $"{nameof(AnalogInput)}.{GetElementDisplayName(Register)}";
    }

    /// <summary>
    /// Represents a register that enables the data acquisition.
    /// </summary>
    [Description("Enables the data acquisition.")]
    public partial class AcquisitionState
    {
        /// <summary>
        /// Represents the address of the <see cref="AcquisitionState"/> register. This field is constant.
        /// </summary>
        public const int Address = 32;

        /// <summary>
        /// Represents the payload type of the <see cref="AcquisitionState"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="AcquisitionState"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="AcquisitionState"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static EnableFlag GetPayload(HarpMessage message)
        {
            return (EnableFlag)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="AcquisitionState"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<EnableFlag> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((EnableFlag)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="AcquisitionState"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="AcquisitionState"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, EnableFlag value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="AcquisitionState"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="AcquisitionState"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, EnableFlag value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// AcquisitionState register.
    /// </summary>
    /// <seealso cref="AcquisitionState"/>
    [Description("Filters and selects timestamped messages from the AcquisitionState register.")]
    public partial class TimestampedAcquisitionState
    {
        /// <summary>
        /// Represents the address of the <see cref="AcquisitionState"/> register. This field is constant.
        /// </summary>
        public const int Address = AcquisitionState.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="AcquisitionState"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<EnableFlag> GetPayload(HarpMessage message)
        {
            return AcquisitionState.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that value from a single read of all ADC channels.
    /// </summary>
    [Description("Value from a single read of all ADC channels.")]
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
    public partial class DigitalInputState
    {
        /// <summary>
        /// Represents the address of the <see cref="DigitalInputState"/> register. This field is constant.
        /// </summary>
        public const int Address = 34;

        /// <summary>
        /// Represents the payload type of the <see cref="DigitalInputState"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="DigitalInputState"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DigitalInputState"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static DigitalInputs GetPayload(HarpMessage message)
        {
            return (DigitalInputs)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DigitalInputState"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalInputs> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((DigitalInputs)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DigitalInputState"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DigitalInputState"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, DigitalInputs value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DigitalInputState"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DigitalInputState"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, DigitalInputs value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DigitalInputState register.
    /// </summary>
    /// <seealso cref="DigitalInputState"/>
    [Description("Filters and selects timestamped messages from the DigitalInputState register.")]
    public partial class TimestampedDigitalInputState
    {
        /// <summary>
        /// Represents the address of the <see cref="DigitalInputState"/> register. This field is constant.
        /// </summary>
        public const int Address = DigitalInputState.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DigitalInputState"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalInputs> GetPayload(HarpMessage message)
        {
            return DigitalInputState.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that reserved.
    /// </summary>
    [Description("Reserved")]
    internal partial class Reserved0
    {
        /// <summary>
        /// Represents the address of the <see cref="Reserved0"/> register. This field is constant.
        /// </summary>
        public const int Address = 35;

        /// <summary>
        /// Represents the payload type of the <see cref="Reserved0"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Reserved0"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;
    }

    /// <summary>
    /// Represents a register that reserved.
    /// </summary>
    [Description("Reserved")]
    internal partial class Reserved1
    {
        /// <summary>
        /// Represents the address of the <see cref="Reserved1"/> register. This field is constant.
        /// </summary>
        public const int Address = 36;

        /// <summary>
        /// Represents the payload type of the <see cref="Reserved1"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Reserved1"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;
    }

    /// <summary>
    /// Represents a register that sets the range and LowPass filter cutoff of the ADC.
    /// </summary>
    [Description("Sets the range and LowPass filter cutoff of the ADC.")]
    public partial class RangeAndFilter
    {
        /// <summary>
        /// Represents the address of the <see cref="RangeAndFilter"/> register. This field is constant.
        /// </summary>
        public const int Address = 37;

        /// <summary>
        /// Represents the payload type of the <see cref="RangeAndFilter"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="RangeAndFilter"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="RangeAndFilter"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static RangeAndFilterConfig GetPayload(HarpMessage message)
        {
            return (RangeAndFilterConfig)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="RangeAndFilter"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<RangeAndFilterConfig> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((RangeAndFilterConfig)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="RangeAndFilter"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="RangeAndFilter"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, RangeAndFilterConfig value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="RangeAndFilter"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="RangeAndFilter"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, RangeAndFilterConfig value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// RangeAndFilter register.
    /// </summary>
    /// <seealso cref="RangeAndFilter"/>
    [Description("Filters and selects timestamped messages from the RangeAndFilter register.")]
    public partial class TimestampedRangeAndFilter
    {
        /// <summary>
        /// Represents the address of the <see cref="RangeAndFilter"/> register. This field is constant.
        /// </summary>
        public const int Address = RangeAndFilter.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="RangeAndFilter"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<RangeAndFilterConfig> GetPayload(HarpMessage message)
        {
            return RangeAndFilter.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sets the sampling frequency of the ADC.
    /// </summary>
    [Description("Sets the sampling frequency of the ADC.")]
    public partial class SamplingRate
    {
        /// <summary>
        /// Represents the address of the <see cref="SamplingRate"/> register. This field is constant.
        /// </summary>
        public const int Address = 38;

        /// <summary>
        /// Represents the payload type of the <see cref="SamplingRate"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="SamplingRate"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="SamplingRate"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static SamplingRateMode GetPayload(HarpMessage message)
        {
            return (SamplingRateMode)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="SamplingRate"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<SamplingRateMode> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((SamplingRateMode)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="SamplingRate"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="SamplingRate"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, SamplingRateMode value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="SamplingRate"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="SamplingRate"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, SamplingRateMode value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// SamplingRate register.
    /// </summary>
    /// <seealso cref="SamplingRate"/>
    [Description("Filters and selects timestamped messages from the SamplingRate register.")]
    public partial class TimestampedSamplingRate
    {
        /// <summary>
        /// Represents the address of the <see cref="SamplingRate"/> register. This field is constant.
        /// </summary>
        public const int Address = SamplingRate.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="SamplingRate"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<SamplingRateMode> GetPayload(HarpMessage message)
        {
            return SamplingRate.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configuration of the digital input pin 0.
    /// </summary>
    [Description("Configuration of the digital input pin 0.")]
    public partial class DI0Trigger
    {
        /// <summary>
        /// Represents the address of the <see cref="DI0Trigger"/> register. This field is constant.
        /// </summary>
        public const int Address = 39;

        /// <summary>
        /// Represents the payload type of the <see cref="DI0Trigger"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="DI0Trigger"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DI0Trigger"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static TriggerConfig GetPayload(HarpMessage message)
        {
            return (TriggerConfig)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DI0Trigger"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<TriggerConfig> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((TriggerConfig)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DI0Trigger"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DI0Trigger"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, TriggerConfig value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DI0Trigger"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DI0Trigger"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, TriggerConfig value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DI0Trigger register.
    /// </summary>
    /// <seealso cref="DI0Trigger"/>
    [Description("Filters and selects timestamped messages from the DI0Trigger register.")]
    public partial class TimestampedDI0Trigger
    {
        /// <summary>
        /// Represents the address of the <see cref="DI0Trigger"/> register. This field is constant.
        /// </summary>
        public const int Address = DI0Trigger.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DI0Trigger"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<TriggerConfig> GetPayload(HarpMessage message)
        {
            return DI0Trigger.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configuration of the digital output pin 0.
    /// </summary>
    [Description("Configuration of the digital output pin 0.")]
    public partial class DO0Sync
    {
        /// <summary>
        /// Represents the address of the <see cref="DO0Sync"/> register. This field is constant.
        /// </summary>
        public const int Address = 40;

        /// <summary>
        /// Represents the payload type of the <see cref="DO0Sync"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="DO0Sync"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DO0Sync"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static SyncConfig GetPayload(HarpMessage message)
        {
            return (SyncConfig)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DO0Sync"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<SyncConfig> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((SyncConfig)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DO0Sync"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO0Sync"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, SyncConfig value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DO0Sync"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO0Sync"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, SyncConfig value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DO0Sync register.
    /// </summary>
    /// <seealso cref="DO0Sync"/>
    [Description("Filters and selects timestamped messages from the DO0Sync register.")]
    public partial class TimestampedDO0Sync
    {
        /// <summary>
        /// Represents the address of the <see cref="DO0Sync"/> register. This field is constant.
        /// </summary>
        public const int Address = DO0Sync.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DO0Sync"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<SyncConfig> GetPayload(HarpMessage message)
        {
            return DO0Sync.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that pulse duration (ms) for the digital output pin 0. The pulse will only be emitted when DO0Sync == Pulse.
    /// </summary>
    [Description("Pulse duration (ms) for the digital output pin 0. The pulse will only be emitted when DO0Sync == Pulse.")]
    public partial class DO0PulseWidth
    {
        /// <summary>
        /// Represents the address of the <see cref="DO0PulseWidth"/> register. This field is constant.
        /// </summary>
        public const int Address = 41;

        /// <summary>
        /// Represents the payload type of the <see cref="DO0PulseWidth"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="DO0PulseWidth"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DO0PulseWidth"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static byte GetPayload(HarpMessage message)
        {
            return message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DO0PulseWidth"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<byte> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadByte();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DO0PulseWidth"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO0PulseWidth"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, byte value)
        {
            return HarpMessage.FromByte(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DO0PulseWidth"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO0PulseWidth"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, byte value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DO0PulseWidth register.
    /// </summary>
    /// <seealso cref="DO0PulseWidth"/>
    [Description("Filters and selects timestamped messages from the DO0PulseWidth register.")]
    public partial class TimestampedDO0PulseWidth
    {
        /// <summary>
        /// Represents the address of the <see cref="DO0PulseWidth"/> register. This field is constant.
        /// </summary>
        public const int Address = DO0PulseWidth.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DO0PulseWidth"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<byte> GetPayload(HarpMessage message)
        {
            return DO0PulseWidth.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that set the specified digital output lines.
    /// </summary>
    [Description("Set the specified digital output lines.")]
    public partial class DigitalOutputSet
    {
        /// <summary>
        /// Represents the address of the <see cref="DigitalOutputSet"/> register. This field is constant.
        /// </summary>
        public const int Address = 42;

        /// <summary>
        /// Represents the payload type of the <see cref="DigitalOutputSet"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="DigitalOutputSet"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DigitalOutputSet"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static DigitalOutputs GetPayload(HarpMessage message)
        {
            return (DigitalOutputs)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DigitalOutputSet"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalOutputs> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((DigitalOutputs)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DigitalOutputSet"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DigitalOutputSet"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, DigitalOutputs value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DigitalOutputSet"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DigitalOutputSet"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, DigitalOutputs value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DigitalOutputSet register.
    /// </summary>
    /// <seealso cref="DigitalOutputSet"/>
    [Description("Filters and selects timestamped messages from the DigitalOutputSet register.")]
    public partial class TimestampedDigitalOutputSet
    {
        /// <summary>
        /// Represents the address of the <see cref="DigitalOutputSet"/> register. This field is constant.
        /// </summary>
        public const int Address = DigitalOutputSet.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DigitalOutputSet"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalOutputs> GetPayload(HarpMessage message)
        {
            return DigitalOutputSet.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that clear the specified digital output lines.
    /// </summary>
    [Description("Clear the specified digital output lines.")]
    public partial class DigitalOutputClear
    {
        /// <summary>
        /// Represents the address of the <see cref="DigitalOutputClear"/> register. This field is constant.
        /// </summary>
        public const int Address = 43;

        /// <summary>
        /// Represents the payload type of the <see cref="DigitalOutputClear"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="DigitalOutputClear"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DigitalOutputClear"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static DigitalOutputs GetPayload(HarpMessage message)
        {
            return (DigitalOutputs)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DigitalOutputClear"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalOutputs> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((DigitalOutputs)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DigitalOutputClear"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DigitalOutputClear"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, DigitalOutputs value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DigitalOutputClear"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DigitalOutputClear"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, DigitalOutputs value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DigitalOutputClear register.
    /// </summary>
    /// <seealso cref="DigitalOutputClear"/>
    [Description("Filters and selects timestamped messages from the DigitalOutputClear register.")]
    public partial class TimestampedDigitalOutputClear
    {
        /// <summary>
        /// Represents the address of the <see cref="DigitalOutputClear"/> register. This field is constant.
        /// </summary>
        public const int Address = DigitalOutputClear.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DigitalOutputClear"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalOutputs> GetPayload(HarpMessage message)
        {
            return DigitalOutputClear.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that toggle the specified digital output lines.
    /// </summary>
    [Description("Toggle the specified digital output lines")]
    public partial class DigitalOutputToggle
    {
        /// <summary>
        /// Represents the address of the <see cref="DigitalOutputToggle"/> register. This field is constant.
        /// </summary>
        public const int Address = 44;

        /// <summary>
        /// Represents the payload type of the <see cref="DigitalOutputToggle"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="DigitalOutputToggle"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DigitalOutputToggle"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static DigitalOutputs GetPayload(HarpMessage message)
        {
            return (DigitalOutputs)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DigitalOutputToggle"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalOutputs> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((DigitalOutputs)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DigitalOutputToggle"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DigitalOutputToggle"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, DigitalOutputs value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DigitalOutputToggle"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DigitalOutputToggle"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, DigitalOutputs value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DigitalOutputToggle register.
    /// </summary>
    /// <seealso cref="DigitalOutputToggle"/>
    [Description("Filters and selects timestamped messages from the DigitalOutputToggle register.")]
    public partial class TimestampedDigitalOutputToggle
    {
        /// <summary>
        /// Represents the address of the <see cref="DigitalOutputToggle"/> register. This field is constant.
        /// </summary>
        public const int Address = DigitalOutputToggle.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DigitalOutputToggle"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalOutputs> GetPayload(HarpMessage message)
        {
            return DigitalOutputToggle.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that write the state of all digital output lines. An event will be emitted when the value of any pin was changed by a threshold event.
    /// </summary>
    [Description("Write the state of all digital output lines. An event will be emitted when the value of any pin was changed by a threshold event.")]
    public partial class DigitalOutputState
    {
        /// <summary>
        /// Represents the address of the <see cref="DigitalOutputState"/> register. This field is constant.
        /// </summary>
        public const int Address = 45;

        /// <summary>
        /// Represents the payload type of the <see cref="DigitalOutputState"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="DigitalOutputState"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DigitalOutputState"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static DigitalOutputs GetPayload(HarpMessage message)
        {
            return (DigitalOutputs)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DigitalOutputState"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalOutputs> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((DigitalOutputs)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DigitalOutputState"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DigitalOutputState"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, DigitalOutputs value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DigitalOutputState"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DigitalOutputState"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, DigitalOutputs value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DigitalOutputState register.
    /// </summary>
    /// <seealso cref="DigitalOutputState"/>
    [Description("Filters and selects timestamped messages from the DigitalOutputState register.")]
    public partial class TimestampedDigitalOutputState
    {
        /// <summary>
        /// Represents the address of the <see cref="DigitalOutputState"/> register. This field is constant.
        /// </summary>
        public const int Address = DigitalOutputState.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DigitalOutputState"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalOutputs> GetPayload(HarpMessage message)
        {
            return DigitalOutputState.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that reserved.
    /// </summary>
    [Description("Reserved")]
    internal partial class Reserved2
    {
        /// <summary>
        /// Represents the address of the <see cref="Reserved2"/> register. This field is constant.
        /// </summary>
        public const int Address = 46;

        /// <summary>
        /// Represents the payload type of the <see cref="Reserved2"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Reserved2"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;
    }

    /// <summary>
    /// Represents a register that reserved.
    /// </summary>
    [Description("Reserved")]
    internal partial class Reserved3
    {
        /// <summary>
        /// Represents the address of the <see cref="Reserved3"/> register. This field is constant.
        /// </summary>
        public const int Address = 47;

        /// <summary>
        /// Represents the payload type of the <see cref="Reserved3"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Reserved3"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;
    }

    /// <summary>
    /// Represents a register that digital output that will be set when acquisition starts.
    /// </summary>
    [Description("Digital output that will be set when acquisition starts.")]
    public partial class SyncOutput
    {
        /// <summary>
        /// Represents the address of the <see cref="SyncOutput"/> register. This field is constant.
        /// </summary>
        public const int Address = 48;

        /// <summary>
        /// Represents the payload type of the <see cref="SyncOutput"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="SyncOutput"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="SyncOutput"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static StartSyncOutputTarget GetPayload(HarpMessage message)
        {
            return (StartSyncOutputTarget)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="SyncOutput"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<StartSyncOutputTarget> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((StartSyncOutputTarget)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="SyncOutput"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="SyncOutput"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, StartSyncOutputTarget value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="SyncOutput"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="SyncOutput"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, StartSyncOutputTarget value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// SyncOutput register.
    /// </summary>
    /// <seealso cref="SyncOutput"/>
    [Description("Filters and selects timestamped messages from the SyncOutput register.")]
    public partial class TimestampedSyncOutput
    {
        /// <summary>
        /// Represents the address of the <see cref="SyncOutput"/> register. This field is constant.
        /// </summary>
        public const int Address = SyncOutput.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="SyncOutput"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<StartSyncOutputTarget> GetPayload(HarpMessage message)
        {
            return SyncOutput.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that reserved.
    /// </summary>
    [Description("Reserved")]
    internal partial class Reserved4
    {
        /// <summary>
        /// Represents the address of the <see cref="Reserved4"/> register. This field is constant.
        /// </summary>
        public const int Address = 49;

        /// <summary>
        /// Represents the payload type of the <see cref="Reserved4"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Reserved4"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;
    }

    /// <summary>
    /// Represents a register that reserved.
    /// </summary>
    [Description("Reserved")]
    internal partial class Reserved5
    {
        /// <summary>
        /// Represents the address of the <see cref="Reserved5"/> register. This field is constant.
        /// </summary>
        public const int Address = 50;

        /// <summary>
        /// Represents the payload type of the <see cref="Reserved5"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Reserved5"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;
    }

    /// <summary>
    /// Represents a register that reserved.
    /// </summary>
    [Description("Reserved")]
    internal partial class Reserved6
    {
        /// <summary>
        /// Represents the address of the <see cref="Reserved6"/> register. This field is constant.
        /// </summary>
        public const int Address = 51;

        /// <summary>
        /// Represents the payload type of the <see cref="Reserved6"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Reserved6"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;
    }

    /// <summary>
    /// Represents a register that reserved.
    /// </summary>
    [Description("Reserved")]
    internal partial class Reserved7
    {
        /// <summary>
        /// Represents the address of the <see cref="Reserved7"/> register. This field is constant.
        /// </summary>
        public const int Address = 52;

        /// <summary>
        /// Represents the payload type of the <see cref="Reserved7"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Reserved7"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;
    }

    /// <summary>
    /// Represents a register that reserved.
    /// </summary>
    [Description("Reserved")]
    internal partial class Reserved8
    {
        /// <summary>
        /// Represents the address of the <see cref="Reserved8"/> register. This field is constant.
        /// </summary>
        public const int Address = 53;

        /// <summary>
        /// Represents the payload type of the <see cref="Reserved8"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Reserved8"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;
    }

    /// <summary>
    /// Represents a register that reserved.
    /// </summary>
    [Description("Reserved")]
    internal partial class Reserved9
    {
        /// <summary>
        /// Represents the address of the <see cref="Reserved9"/> register. This field is constant.
        /// </summary>
        public const int Address = 54;

        /// <summary>
        /// Represents the payload type of the <see cref="Reserved9"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Reserved9"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;
    }

    /// <summary>
    /// Represents a register that reserved.
    /// </summary>
    [Description("Reserved")]
    internal partial class Reserved10
    {
        /// <summary>
        /// Represents the address of the <see cref="Reserved10"/> register. This field is constant.
        /// </summary>
        public const int Address = 55;

        /// <summary>
        /// Represents the payload type of the <see cref="Reserved10"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Reserved10"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;
    }

    /// <summary>
    /// Represents a register that reserved.
    /// </summary>
    [Description("Reserved")]
    internal partial class Reserved11
    {
        /// <summary>
        /// Represents the address of the <see cref="Reserved11"/> register. This field is constant.
        /// </summary>
        public const int Address = 56;

        /// <summary>
        /// Represents the payload type of the <see cref="Reserved11"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Reserved11"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;
    }

    /// <summary>
    /// Represents a register that reserved.
    /// </summary>
    [Description("Reserved")]
    internal partial class Reserved12
    {
        /// <summary>
        /// Represents the address of the <see cref="Reserved12"/> register. This field is constant.
        /// </summary>
        public const int Address = 57;

        /// <summary>
        /// Represents the payload type of the <see cref="Reserved12"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Reserved12"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;
    }

    /// <summary>
    /// Represents a register that target ADC channel that will be used to trigger a threshold event on DO0 pin.
    /// </summary>
    [Description("Target ADC channel that will be used to trigger a threshold event on DO0 pin.")]
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
        public static AdcChannel GetPayload(HarpMessage message)
        {
            return (AdcChannel)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DO0TargetChannel"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<AdcChannel> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((AdcChannel)payload.Value, payload.Seconds);
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
        public static HarpMessage FromPayload(MessageType messageType, AdcChannel value)
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
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, AdcChannel value)
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
        public static Timestamped<AdcChannel> GetPayload(HarpMessage message)
        {
            return DO0TargetChannel.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that target ADC channel that will be used to trigger a threshold event on DO1 pin.
    /// </summary>
    [Description("Target ADC channel that will be used to trigger a threshold event on DO1 pin.")]
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
        public static AdcChannel GetPayload(HarpMessage message)
        {
            return (AdcChannel)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DO1TargetChannel"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<AdcChannel> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((AdcChannel)payload.Value, payload.Seconds);
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
        public static HarpMessage FromPayload(MessageType messageType, AdcChannel value)
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
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, AdcChannel value)
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
        public static Timestamped<AdcChannel> GetPayload(HarpMessage message)
        {
            return DO1TargetChannel.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that target ADC channel that will be used to trigger a threshold event on DO2 pin.
    /// </summary>
    [Description("Target ADC channel that will be used to trigger a threshold event on DO2 pin.")]
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
        public static AdcChannel GetPayload(HarpMessage message)
        {
            return (AdcChannel)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DO2TargetChannel"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<AdcChannel> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((AdcChannel)payload.Value, payload.Seconds);
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
        public static HarpMessage FromPayload(MessageType messageType, AdcChannel value)
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
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, AdcChannel value)
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
        public static Timestamped<AdcChannel> GetPayload(HarpMessage message)
        {
            return DO2TargetChannel.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that target ADC channel that will be used to trigger a threshold event on DO3 pin.
    /// </summary>
    [Description("Target ADC channel that will be used to trigger a threshold event on DO3 pin.")]
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
        public static AdcChannel GetPayload(HarpMessage message)
        {
            return (AdcChannel)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DO3TargetChannel"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<AdcChannel> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((AdcChannel)payload.Value, payload.Seconds);
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
        public static HarpMessage FromPayload(MessageType messageType, AdcChannel value)
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
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, AdcChannel value)
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
        public static Timestamped<AdcChannel> GetPayload(HarpMessage message)
        {
            return DO3TargetChannel.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that reserved.
    /// </summary>
    [Description("Reserved")]
    internal partial class Reserved13
    {
        /// <summary>
        /// Represents the address of the <see cref="Reserved13"/> register. This field is constant.
        /// </summary>
        public const int Address = 62;

        /// <summary>
        /// Represents the payload type of the <see cref="Reserved13"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Reserved13"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;
    }

    /// <summary>
    /// Represents a register that reserved.
    /// </summary>
    [Description("Reserved")]
    internal partial class Reserved14
    {
        /// <summary>
        /// Represents the address of the <see cref="Reserved14"/> register. This field is constant.
        /// </summary>
        public const int Address = 63;

        /// <summary>
        /// Represents the payload type of the <see cref="Reserved14"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Reserved14"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;
    }

    /// <summary>
    /// Represents a register that reserved.
    /// </summary>
    [Description("Reserved")]
    internal partial class Reserved15
    {
        /// <summary>
        /// Represents the address of the <see cref="Reserved15"/> register. This field is constant.
        /// </summary>
        public const int Address = 64;

        /// <summary>
        /// Represents the payload type of the <see cref="Reserved15"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Reserved15"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;
    }

    /// <summary>
    /// Represents a register that reserved.
    /// </summary>
    [Description("Reserved")]
    internal partial class Reserved16
    {
        /// <summary>
        /// Represents the address of the <see cref="Reserved16"/> register. This field is constant.
        /// </summary>
        public const int Address = 65;

        /// <summary>
        /// Represents the payload type of the <see cref="Reserved16"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Reserved16"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;
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
    /// Represents a register that reserved.
    /// </summary>
    [Description("Reserved")]
    internal partial class Reserved17
    {
        /// <summary>
        /// Represents the address of the <see cref="Reserved17"/> register. This field is constant.
        /// </summary>
        public const int Address = 70;

        /// <summary>
        /// Represents the payload type of the <see cref="Reserved17"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Reserved17"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;
    }

    /// <summary>
    /// Represents a register that reserved.
    /// </summary>
    [Description("Reserved")]
    internal partial class Reserved18
    {
        /// <summary>
        /// Represents the address of the <see cref="Reserved18"/> register. This field is constant.
        /// </summary>
        public const int Address = 71;

        /// <summary>
        /// Represents the payload type of the <see cref="Reserved18"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Reserved18"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;
    }

    /// <summary>
    /// Represents a register that reserved.
    /// </summary>
    [Description("Reserved")]
    internal partial class Reserved19
    {
        /// <summary>
        /// Represents the address of the <see cref="Reserved19"/> register. This field is constant.
        /// </summary>
        public const int Address = 72;

        /// <summary>
        /// Represents the payload type of the <see cref="Reserved19"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Reserved19"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;
    }

    /// <summary>
    /// Represents a register that reserved.
    /// </summary>
    [Description("Reserved")]
    internal partial class Reserved20
    {
        /// <summary>
        /// Represents the address of the <see cref="Reserved20"/> register. This field is constant.
        /// </summary>
        public const int Address = 73;

        /// <summary>
        /// Represents the payload type of the <see cref="Reserved20"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Reserved20"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;
    }

    /// <summary>
    /// Represents a register that time (ms) above threshold value that is required to trigger a DO0 pin event.
    /// </summary>
    [Description("Time (ms) above threshold value that is required to trigger a DO0 pin event.")]
    public partial class DO0TimeAboveThreshold
    {
        /// <summary>
        /// Represents the address of the <see cref="DO0TimeAboveThreshold"/> register. This field is constant.
        /// </summary>
        public const int Address = 74;

        /// <summary>
        /// Represents the payload type of the <see cref="DO0TimeAboveThreshold"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="DO0TimeAboveThreshold"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DO0TimeAboveThreshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DO0TimeAboveThreshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DO0TimeAboveThreshold"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO0TimeAboveThreshold"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DO0TimeAboveThreshold"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO0TimeAboveThreshold"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DO0TimeAboveThreshold register.
    /// </summary>
    /// <seealso cref="DO0TimeAboveThreshold"/>
    [Description("Filters and selects timestamped messages from the DO0TimeAboveThreshold register.")]
    public partial class TimestampedDO0TimeAboveThreshold
    {
        /// <summary>
        /// Represents the address of the <see cref="DO0TimeAboveThreshold"/> register. This field is constant.
        /// </summary>
        public const int Address = DO0TimeAboveThreshold.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DO0TimeAboveThreshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return DO0TimeAboveThreshold.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that time (ms) above threshold value that is required to trigger a DO1 pin event.
    /// </summary>
    [Description("Time (ms) above threshold value that is required to trigger a DO1 pin event.")]
    public partial class DO1TimeAboveThreshold
    {
        /// <summary>
        /// Represents the address of the <see cref="DO1TimeAboveThreshold"/> register. This field is constant.
        /// </summary>
        public const int Address = 75;

        /// <summary>
        /// Represents the payload type of the <see cref="DO1TimeAboveThreshold"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="DO1TimeAboveThreshold"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DO1TimeAboveThreshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DO1TimeAboveThreshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DO1TimeAboveThreshold"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO1TimeAboveThreshold"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DO1TimeAboveThreshold"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO1TimeAboveThreshold"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DO1TimeAboveThreshold register.
    /// </summary>
    /// <seealso cref="DO1TimeAboveThreshold"/>
    [Description("Filters and selects timestamped messages from the DO1TimeAboveThreshold register.")]
    public partial class TimestampedDO1TimeAboveThreshold
    {
        /// <summary>
        /// Represents the address of the <see cref="DO1TimeAboveThreshold"/> register. This field is constant.
        /// </summary>
        public const int Address = DO1TimeAboveThreshold.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DO1TimeAboveThreshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return DO1TimeAboveThreshold.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that time (ms) above threshold value that is required to trigger a DO2 pin event.
    /// </summary>
    [Description("Time (ms) above threshold value that is required to trigger a DO2 pin event.")]
    public partial class DO2TimeAboveThreshold
    {
        /// <summary>
        /// Represents the address of the <see cref="DO2TimeAboveThreshold"/> register. This field is constant.
        /// </summary>
        public const int Address = 76;

        /// <summary>
        /// Represents the payload type of the <see cref="DO2TimeAboveThreshold"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="DO2TimeAboveThreshold"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DO2TimeAboveThreshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DO2TimeAboveThreshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DO2TimeAboveThreshold"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO2TimeAboveThreshold"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DO2TimeAboveThreshold"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO2TimeAboveThreshold"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DO2TimeAboveThreshold register.
    /// </summary>
    /// <seealso cref="DO2TimeAboveThreshold"/>
    [Description("Filters and selects timestamped messages from the DO2TimeAboveThreshold register.")]
    public partial class TimestampedDO2TimeAboveThreshold
    {
        /// <summary>
        /// Represents the address of the <see cref="DO2TimeAboveThreshold"/> register. This field is constant.
        /// </summary>
        public const int Address = DO2TimeAboveThreshold.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DO2TimeAboveThreshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return DO2TimeAboveThreshold.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that time (ms) above threshold value that is required to trigger a DO3 pin event.
    /// </summary>
    [Description("Time (ms) above threshold value that is required to trigger a DO3 pin event.")]
    public partial class DO3TimeAboveThreshold
    {
        /// <summary>
        /// Represents the address of the <see cref="DO3TimeAboveThreshold"/> register. This field is constant.
        /// </summary>
        public const int Address = 77;

        /// <summary>
        /// Represents the payload type of the <see cref="DO3TimeAboveThreshold"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="DO3TimeAboveThreshold"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DO3TimeAboveThreshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DO3TimeAboveThreshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DO3TimeAboveThreshold"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO3TimeAboveThreshold"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DO3TimeAboveThreshold"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO3TimeAboveThreshold"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DO3TimeAboveThreshold register.
    /// </summary>
    /// <seealso cref="DO3TimeAboveThreshold"/>
    [Description("Filters and selects timestamped messages from the DO3TimeAboveThreshold register.")]
    public partial class TimestampedDO3TimeAboveThreshold
    {
        /// <summary>
        /// Represents the address of the <see cref="DO3TimeAboveThreshold"/> register. This field is constant.
        /// </summary>
        public const int Address = DO3TimeAboveThreshold.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DO3TimeAboveThreshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return DO3TimeAboveThreshold.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that reserved.
    /// </summary>
    [Description("Reserved")]
    internal partial class Reserved21
    {
        /// <summary>
        /// Represents the address of the <see cref="Reserved21"/> register. This field is constant.
        /// </summary>
        public const int Address = 78;

        /// <summary>
        /// Represents the payload type of the <see cref="Reserved21"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Reserved21"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;
    }

    /// <summary>
    /// Represents a register that reserved.
    /// </summary>
    [Description("Reserved")]
    internal partial class Reserved22
    {
        /// <summary>
        /// Represents the address of the <see cref="Reserved22"/> register. This field is constant.
        /// </summary>
        public const int Address = 79;

        /// <summary>
        /// Represents the payload type of the <see cref="Reserved22"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Reserved22"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;
    }

    /// <summary>
    /// Represents a register that reserved.
    /// </summary>
    [Description("Reserved")]
    internal partial class Reserved23
    {
        /// <summary>
        /// Represents the address of the <see cref="Reserved23"/> register. This field is constant.
        /// </summary>
        public const int Address = 80;

        /// <summary>
        /// Represents the payload type of the <see cref="Reserved23"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Reserved23"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;
    }

    /// <summary>
    /// Represents a register that reserved.
    /// </summary>
    [Description("Reserved")]
    internal partial class Reserved24
    {
        /// <summary>
        /// Represents the address of the <see cref="Reserved24"/> register. This field is constant.
        /// </summary>
        public const int Address = 81;

        /// <summary>
        /// Represents the payload type of the <see cref="Reserved24"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Reserved24"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;
    }

    /// <summary>
    /// Represents a register that time (ms) below threshold value that is required to trigger a DO0 pin event.
    /// </summary>
    [Description("Time (ms) below threshold value that is required to trigger a DO0 pin event.")]
    public partial class DO0TimeBelowThreshold
    {
        /// <summary>
        /// Represents the address of the <see cref="DO0TimeBelowThreshold"/> register. This field is constant.
        /// </summary>
        public const int Address = 82;

        /// <summary>
        /// Represents the payload type of the <see cref="DO0TimeBelowThreshold"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="DO0TimeBelowThreshold"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DO0TimeBelowThreshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DO0TimeBelowThreshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DO0TimeBelowThreshold"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO0TimeBelowThreshold"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DO0TimeBelowThreshold"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO0TimeBelowThreshold"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DO0TimeBelowThreshold register.
    /// </summary>
    /// <seealso cref="DO0TimeBelowThreshold"/>
    [Description("Filters and selects timestamped messages from the DO0TimeBelowThreshold register.")]
    public partial class TimestampedDO0TimeBelowThreshold
    {
        /// <summary>
        /// Represents the address of the <see cref="DO0TimeBelowThreshold"/> register. This field is constant.
        /// </summary>
        public const int Address = DO0TimeBelowThreshold.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DO0TimeBelowThreshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return DO0TimeBelowThreshold.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that time (ms) below threshold value that is required to trigger a DO1 pin event.
    /// </summary>
    [Description("Time (ms) below threshold value that is required to trigger a DO1 pin event.")]
    public partial class DO1TimeBelowThreshold
    {
        /// <summary>
        /// Represents the address of the <see cref="DO1TimeBelowThreshold"/> register. This field is constant.
        /// </summary>
        public const int Address = 83;

        /// <summary>
        /// Represents the payload type of the <see cref="DO1TimeBelowThreshold"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="DO1TimeBelowThreshold"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DO1TimeBelowThreshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DO1TimeBelowThreshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DO1TimeBelowThreshold"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO1TimeBelowThreshold"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DO1TimeBelowThreshold"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO1TimeBelowThreshold"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DO1TimeBelowThreshold register.
    /// </summary>
    /// <seealso cref="DO1TimeBelowThreshold"/>
    [Description("Filters and selects timestamped messages from the DO1TimeBelowThreshold register.")]
    public partial class TimestampedDO1TimeBelowThreshold
    {
        /// <summary>
        /// Represents the address of the <see cref="DO1TimeBelowThreshold"/> register. This field is constant.
        /// </summary>
        public const int Address = DO1TimeBelowThreshold.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DO1TimeBelowThreshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return DO1TimeBelowThreshold.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that time (ms) below threshold value that is required to trigger a DO2 pin event.
    /// </summary>
    [Description("Time (ms) below threshold value that is required to trigger a DO2 pin event.")]
    public partial class DO2TimeBelowThreshold
    {
        /// <summary>
        /// Represents the address of the <see cref="DO2TimeBelowThreshold"/> register. This field is constant.
        /// </summary>
        public const int Address = 84;

        /// <summary>
        /// Represents the payload type of the <see cref="DO2TimeBelowThreshold"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="DO2TimeBelowThreshold"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DO2TimeBelowThreshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DO2TimeBelowThreshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DO2TimeBelowThreshold"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO2TimeBelowThreshold"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DO2TimeBelowThreshold"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO2TimeBelowThreshold"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DO2TimeBelowThreshold register.
    /// </summary>
    /// <seealso cref="DO2TimeBelowThreshold"/>
    [Description("Filters and selects timestamped messages from the DO2TimeBelowThreshold register.")]
    public partial class TimestampedDO2TimeBelowThreshold
    {
        /// <summary>
        /// Represents the address of the <see cref="DO2TimeBelowThreshold"/> register. This field is constant.
        /// </summary>
        public const int Address = DO2TimeBelowThreshold.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DO2TimeBelowThreshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return DO2TimeBelowThreshold.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that time (ms) below threshold value that is required to trigger a DO3 pin event.
    /// </summary>
    [Description("Time (ms) below threshold value that is required to trigger a DO3 pin event.")]
    public partial class DO3TimeBelowThreshold
    {
        /// <summary>
        /// Represents the address of the <see cref="DO3TimeBelowThreshold"/> register. This field is constant.
        /// </summary>
        public const int Address = 85;

        /// <summary>
        /// Represents the payload type of the <see cref="DO3TimeBelowThreshold"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="DO3TimeBelowThreshold"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DO3TimeBelowThreshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DO3TimeBelowThreshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DO3TimeBelowThreshold"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO3TimeBelowThreshold"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DO3TimeBelowThreshold"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO3TimeBelowThreshold"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DO3TimeBelowThreshold register.
    /// </summary>
    /// <seealso cref="DO3TimeBelowThreshold"/>
    [Description("Filters and selects timestamped messages from the DO3TimeBelowThreshold register.")]
    public partial class TimestampedDO3TimeBelowThreshold
    {
        /// <summary>
        /// Represents the address of the <see cref="DO3TimeBelowThreshold"/> register. This field is constant.
        /// </summary>
        public const int Address = DO3TimeBelowThreshold.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DO3TimeBelowThreshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return DO3TimeBelowThreshold.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that reserved.
    /// </summary>
    [Description("Reserved")]
    internal partial class Reserved25
    {
        /// <summary>
        /// Represents the address of the <see cref="Reserved25"/> register. This field is constant.
        /// </summary>
        public const int Address = 86;

        /// <summary>
        /// Represents the payload type of the <see cref="Reserved25"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Reserved25"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;
    }

    /// <summary>
    /// Represents a register that reserved.
    /// </summary>
    [Description("Reserved")]
    internal partial class Reserved26
    {
        /// <summary>
        /// Represents the address of the <see cref="Reserved26"/> register. This field is constant.
        /// </summary>
        public const int Address = 87;

        /// <summary>
        /// Represents the payload type of the <see cref="Reserved26"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Reserved26"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;
    }

    /// <summary>
    /// Represents a register that reserved.
    /// </summary>
    [Description("Reserved")]
    internal partial class Reserved27
    {
        /// <summary>
        /// Represents the address of the <see cref="Reserved27"/> register. This field is constant.
        /// </summary>
        public const int Address = 88;

        /// <summary>
        /// Represents the payload type of the <see cref="Reserved27"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Reserved27"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;
    }

    /// <summary>
    /// Represents a register that reserved.
    /// </summary>
    [Description("Reserved")]
    internal partial class Reserved28
    {
        /// <summary>
        /// Represents the address of the <see cref="Reserved28"/> register. This field is constant.
        /// </summary>
        public const int Address = 89;

        /// <summary>
        /// Represents the payload type of the <see cref="Reserved28"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Reserved28"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;
    }

    /// <summary>
    /// Represents a register that reserved.
    /// </summary>
    [Description("Reserved")]
    internal partial class Reserved29
    {
        /// <summary>
        /// Represents the address of the <see cref="Reserved29"/> register. This field is constant.
        /// </summary>
        public const int Address = 90;

        /// <summary>
        /// Represents the payload type of the <see cref="Reserved29"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Reserved29"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;
    }

    /// <summary>
    /// Represents an operator which creates standard message payloads for the
    /// AnalogInput device.
    /// </summary>
    /// <seealso cref="CreateAcquisitionStatePayload"/>
    /// <seealso cref="CreateAnalogDataPayload"/>
    /// <seealso cref="CreateDigitalInputStatePayload"/>
    /// <seealso cref="CreateRangeAndFilterPayload"/>
    /// <seealso cref="CreateSamplingRatePayload"/>
    /// <seealso cref="CreateDI0TriggerPayload"/>
    /// <seealso cref="CreateDO0SyncPayload"/>
    /// <seealso cref="CreateDO0PulseWidthPayload"/>
    /// <seealso cref="CreateDigitalOutputSetPayload"/>
    /// <seealso cref="CreateDigitalOutputClearPayload"/>
    /// <seealso cref="CreateDigitalOutputTogglePayload"/>
    /// <seealso cref="CreateDigitalOutputStatePayload"/>
    /// <seealso cref="CreateSyncOutputPayload"/>
    /// <seealso cref="CreateDO0TargetChannelPayload"/>
    /// <seealso cref="CreateDO1TargetChannelPayload"/>
    /// <seealso cref="CreateDO2TargetChannelPayload"/>
    /// <seealso cref="CreateDO3TargetChannelPayload"/>
    /// <seealso cref="CreateDO0ThresholdPayload"/>
    /// <seealso cref="CreateDO1ThresholdPayload"/>
    /// <seealso cref="CreateDO2ThresholdPayload"/>
    /// <seealso cref="CreateDO3ThresholdPayload"/>
    /// <seealso cref="CreateDO0TimeAboveThresholdPayload"/>
    /// <seealso cref="CreateDO1TimeAboveThresholdPayload"/>
    /// <seealso cref="CreateDO2TimeAboveThresholdPayload"/>
    /// <seealso cref="CreateDO3TimeAboveThresholdPayload"/>
    /// <seealso cref="CreateDO0TimeBelowThresholdPayload"/>
    /// <seealso cref="CreateDO1TimeBelowThresholdPayload"/>
    /// <seealso cref="CreateDO2TimeBelowThresholdPayload"/>
    /// <seealso cref="CreateDO3TimeBelowThresholdPayload"/>
    [XmlInclude(typeof(CreateAcquisitionStatePayload))]
    [XmlInclude(typeof(CreateAnalogDataPayload))]
    [XmlInclude(typeof(CreateDigitalInputStatePayload))]
    [XmlInclude(typeof(CreateRangeAndFilterPayload))]
    [XmlInclude(typeof(CreateSamplingRatePayload))]
    [XmlInclude(typeof(CreateDI0TriggerPayload))]
    [XmlInclude(typeof(CreateDO0SyncPayload))]
    [XmlInclude(typeof(CreateDO0PulseWidthPayload))]
    [XmlInclude(typeof(CreateDigitalOutputSetPayload))]
    [XmlInclude(typeof(CreateDigitalOutputClearPayload))]
    [XmlInclude(typeof(CreateDigitalOutputTogglePayload))]
    [XmlInclude(typeof(CreateDigitalOutputStatePayload))]
    [XmlInclude(typeof(CreateSyncOutputPayload))]
    [XmlInclude(typeof(CreateDO0TargetChannelPayload))]
    [XmlInclude(typeof(CreateDO1TargetChannelPayload))]
    [XmlInclude(typeof(CreateDO2TargetChannelPayload))]
    [XmlInclude(typeof(CreateDO3TargetChannelPayload))]
    [XmlInclude(typeof(CreateDO0ThresholdPayload))]
    [XmlInclude(typeof(CreateDO1ThresholdPayload))]
    [XmlInclude(typeof(CreateDO2ThresholdPayload))]
    [XmlInclude(typeof(CreateDO3ThresholdPayload))]
    [XmlInclude(typeof(CreateDO0TimeAboveThresholdPayload))]
    [XmlInclude(typeof(CreateDO1TimeAboveThresholdPayload))]
    [XmlInclude(typeof(CreateDO2TimeAboveThresholdPayload))]
    [XmlInclude(typeof(CreateDO3TimeAboveThresholdPayload))]
    [XmlInclude(typeof(CreateDO0TimeBelowThresholdPayload))]
    [XmlInclude(typeof(CreateDO1TimeBelowThresholdPayload))]
    [XmlInclude(typeof(CreateDO2TimeBelowThresholdPayload))]
    [XmlInclude(typeof(CreateDO3TimeBelowThresholdPayload))]
    [XmlInclude(typeof(CreateTimestampedAcquisitionStatePayload))]
    [XmlInclude(typeof(CreateTimestampedAnalogDataPayload))]
    [XmlInclude(typeof(CreateTimestampedDigitalInputStatePayload))]
    [XmlInclude(typeof(CreateTimestampedRangeAndFilterPayload))]
    [XmlInclude(typeof(CreateTimestampedSamplingRatePayload))]
    [XmlInclude(typeof(CreateTimestampedDI0TriggerPayload))]
    [XmlInclude(typeof(CreateTimestampedDO0SyncPayload))]
    [XmlInclude(typeof(CreateTimestampedDO0PulseWidthPayload))]
    [XmlInclude(typeof(CreateTimestampedDigitalOutputSetPayload))]
    [XmlInclude(typeof(CreateTimestampedDigitalOutputClearPayload))]
    [XmlInclude(typeof(CreateTimestampedDigitalOutputTogglePayload))]
    [XmlInclude(typeof(CreateTimestampedDigitalOutputStatePayload))]
    [XmlInclude(typeof(CreateTimestampedSyncOutputPayload))]
    [XmlInclude(typeof(CreateTimestampedDO0TargetChannelPayload))]
    [XmlInclude(typeof(CreateTimestampedDO1TargetChannelPayload))]
    [XmlInclude(typeof(CreateTimestampedDO2TargetChannelPayload))]
    [XmlInclude(typeof(CreateTimestampedDO3TargetChannelPayload))]
    [XmlInclude(typeof(CreateTimestampedDO0ThresholdPayload))]
    [XmlInclude(typeof(CreateTimestampedDO1ThresholdPayload))]
    [XmlInclude(typeof(CreateTimestampedDO2ThresholdPayload))]
    [XmlInclude(typeof(CreateTimestampedDO3ThresholdPayload))]
    [XmlInclude(typeof(CreateTimestampedDO0TimeAboveThresholdPayload))]
    [XmlInclude(typeof(CreateTimestampedDO1TimeAboveThresholdPayload))]
    [XmlInclude(typeof(CreateTimestampedDO2TimeAboveThresholdPayload))]
    [XmlInclude(typeof(CreateTimestampedDO3TimeAboveThresholdPayload))]
    [XmlInclude(typeof(CreateTimestampedDO0TimeBelowThresholdPayload))]
    [XmlInclude(typeof(CreateTimestampedDO1TimeBelowThresholdPayload))]
    [XmlInclude(typeof(CreateTimestampedDO2TimeBelowThresholdPayload))]
    [XmlInclude(typeof(CreateTimestampedDO3TimeBelowThresholdPayload))]
    [Description("Creates standard message payloads for the AnalogInput device.")]
    public partial class CreateMessage : CreateMessageBuilder, INamedElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateMessage"/> class.
        /// </summary>
        public CreateMessage()
        {
            Payload = new CreateAcquisitionStatePayload();
        }

        string INamedElement.Name => $"{nameof(AnalogInput)}.{GetElementDisplayName(Payload)}";
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that enables the data acquisition.
    /// </summary>
    [DisplayName("AcquisitionStatePayload")]
    [Description("Creates a message payload that enables the data acquisition.")]
    public partial class CreateAcquisitionStatePayload
    {
        /// <summary>
        /// Gets or sets the value that enables the data acquisition.
        /// </summary>
        [Description("The value that enables the data acquisition.")]
        public EnableFlag AcquisitionState { get; set; }

        /// <summary>
        /// Creates a message payload for the AcquisitionState register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public EnableFlag GetPayload()
        {
            return AcquisitionState;
        }

        /// <summary>
        /// Creates a message that enables the data acquisition.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the AcquisitionState register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.AnalogInput.AcquisitionState.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that enables the data acquisition.
    /// </summary>
    [DisplayName("TimestampedAcquisitionStatePayload")]
    [Description("Creates a timestamped message payload that enables the data acquisition.")]
    public partial class CreateTimestampedAcquisitionStatePayload : CreateAcquisitionStatePayload
    {
        /// <summary>
        /// Creates a timestamped message that enables the data acquisition.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the AcquisitionState register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.AnalogInput.AcquisitionState.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that value from a single read of all ADC channels.
    /// </summary>
    [DisplayName("AnalogDataPayload")]
    [Description("Creates a message payload that value from a single read of all ADC channels.")]
    public partial class CreateAnalogDataPayload
    {
        /// <summary>
        /// Gets or sets a value to write on payload member Channel0.
        /// </summary>
        [Description("")]
        public short Channel0 { get; set; }

        /// <summary>
        /// Gets or sets a value to write on payload member Channel1.
        /// </summary>
        [Description("")]
        public short Channel1 { get; set; }

        /// <summary>
        /// Gets or sets a value to write on payload member Channel2.
        /// </summary>
        [Description("")]
        public short Channel2 { get; set; }

        /// <summary>
        /// Gets or sets a value to write on payload member Channel3.
        /// </summary>
        [Description("")]
        public short Channel3 { get; set; }

        /// <summary>
        /// Creates a message payload for the AnalogData register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public AnalogDataPayload GetPayload()
        {
            AnalogDataPayload value;
            value.Channel0 = Channel0;
            value.Channel1 = Channel1;
            value.Channel2 = Channel2;
            value.Channel3 = Channel3;
            return value;
        }

        /// <summary>
        /// Creates a message that value from a single read of all ADC channels.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the AnalogData register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.AnalogInput.AnalogData.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that value from a single read of all ADC channels.
    /// </summary>
    [DisplayName("TimestampedAnalogDataPayload")]
    [Description("Creates a timestamped message payload that value from a single read of all ADC channels.")]
    public partial class CreateTimestampedAnalogDataPayload : CreateAnalogDataPayload
    {
        /// <summary>
        /// Creates a timestamped message that value from a single read of all ADC channels.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the AnalogData register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.AnalogInput.AnalogData.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that state of the digital input pin 0.
    /// </summary>
    [DisplayName("DigitalInputStatePayload")]
    [Description("Creates a message payload that state of the digital input pin 0.")]
    public partial class CreateDigitalInputStatePayload
    {
        /// <summary>
        /// Gets or sets the value that state of the digital input pin 0.
        /// </summary>
        [Description("The value that state of the digital input pin 0.")]
        public DigitalInputs DigitalInputState { get; set; }

        /// <summary>
        /// Creates a message payload for the DigitalInputState register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public DigitalInputs GetPayload()
        {
            return DigitalInputState;
        }

        /// <summary>
        /// Creates a message that state of the digital input pin 0.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DigitalInputState register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.AnalogInput.DigitalInputState.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that state of the digital input pin 0.
    /// </summary>
    [DisplayName("TimestampedDigitalInputStatePayload")]
    [Description("Creates a timestamped message payload that state of the digital input pin 0.")]
    public partial class CreateTimestampedDigitalInputStatePayload : CreateDigitalInputStatePayload
    {
        /// <summary>
        /// Creates a timestamped message that state of the digital input pin 0.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DigitalInputState register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.AnalogInput.DigitalInputState.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that sets the range and LowPass filter cutoff of the ADC.
    /// </summary>
    [DisplayName("RangeAndFilterPayload")]
    [Description("Creates a message payload that sets the range and LowPass filter cutoff of the ADC.")]
    public partial class CreateRangeAndFilterPayload
    {
        /// <summary>
        /// Gets or sets the value that sets the range and LowPass filter cutoff of the ADC.
        /// </summary>
        [Description("The value that sets the range and LowPass filter cutoff of the ADC.")]
        public RangeAndFilterConfig RangeAndFilter { get; set; }

        /// <summary>
        /// Creates a message payload for the RangeAndFilter register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public RangeAndFilterConfig GetPayload()
        {
            return RangeAndFilter;
        }

        /// <summary>
        /// Creates a message that sets the range and LowPass filter cutoff of the ADC.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the RangeAndFilter register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.AnalogInput.RangeAndFilter.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that sets the range and LowPass filter cutoff of the ADC.
    /// </summary>
    [DisplayName("TimestampedRangeAndFilterPayload")]
    [Description("Creates a timestamped message payload that sets the range and LowPass filter cutoff of the ADC.")]
    public partial class CreateTimestampedRangeAndFilterPayload : CreateRangeAndFilterPayload
    {
        /// <summary>
        /// Creates a timestamped message that sets the range and LowPass filter cutoff of the ADC.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the RangeAndFilter register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.AnalogInput.RangeAndFilter.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that sets the sampling frequency of the ADC.
    /// </summary>
    [DisplayName("SamplingRatePayload")]
    [Description("Creates a message payload that sets the sampling frequency of the ADC.")]
    public partial class CreateSamplingRatePayload
    {
        /// <summary>
        /// Gets or sets the value that sets the sampling frequency of the ADC.
        /// </summary>
        [Description("The value that sets the sampling frequency of the ADC.")]
        public SamplingRateMode SamplingRate { get; set; }

        /// <summary>
        /// Creates a message payload for the SamplingRate register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public SamplingRateMode GetPayload()
        {
            return SamplingRate;
        }

        /// <summary>
        /// Creates a message that sets the sampling frequency of the ADC.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the SamplingRate register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.AnalogInput.SamplingRate.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that sets the sampling frequency of the ADC.
    /// </summary>
    [DisplayName("TimestampedSamplingRatePayload")]
    [Description("Creates a timestamped message payload that sets the sampling frequency of the ADC.")]
    public partial class CreateTimestampedSamplingRatePayload : CreateSamplingRatePayload
    {
        /// <summary>
        /// Creates a timestamped message that sets the sampling frequency of the ADC.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the SamplingRate register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.AnalogInput.SamplingRate.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that configuration of the digital input pin 0.
    /// </summary>
    [DisplayName("DI0TriggerPayload")]
    [Description("Creates a message payload that configuration of the digital input pin 0.")]
    public partial class CreateDI0TriggerPayload
    {
        /// <summary>
        /// Gets or sets the value that configuration of the digital input pin 0.
        /// </summary>
        [Description("The value that configuration of the digital input pin 0.")]
        public TriggerConfig DI0Trigger { get; set; }

        /// <summary>
        /// Creates a message payload for the DI0Trigger register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public TriggerConfig GetPayload()
        {
            return DI0Trigger;
        }

        /// <summary>
        /// Creates a message that configuration of the digital input pin 0.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DI0Trigger register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.AnalogInput.DI0Trigger.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that configuration of the digital input pin 0.
    /// </summary>
    [DisplayName("TimestampedDI0TriggerPayload")]
    [Description("Creates a timestamped message payload that configuration of the digital input pin 0.")]
    public partial class CreateTimestampedDI0TriggerPayload : CreateDI0TriggerPayload
    {
        /// <summary>
        /// Creates a timestamped message that configuration of the digital input pin 0.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DI0Trigger register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.AnalogInput.DI0Trigger.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that configuration of the digital output pin 0.
    /// </summary>
    [DisplayName("DO0SyncPayload")]
    [Description("Creates a message payload that configuration of the digital output pin 0.")]
    public partial class CreateDO0SyncPayload
    {
        /// <summary>
        /// Gets or sets the value that configuration of the digital output pin 0.
        /// </summary>
        [Description("The value that configuration of the digital output pin 0.")]
        public SyncConfig DO0Sync { get; set; }

        /// <summary>
        /// Creates a message payload for the DO0Sync register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public SyncConfig GetPayload()
        {
            return DO0Sync;
        }

        /// <summary>
        /// Creates a message that configuration of the digital output pin 0.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DO0Sync register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.AnalogInput.DO0Sync.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that configuration of the digital output pin 0.
    /// </summary>
    [DisplayName("TimestampedDO0SyncPayload")]
    [Description("Creates a timestamped message payload that configuration of the digital output pin 0.")]
    public partial class CreateTimestampedDO0SyncPayload : CreateDO0SyncPayload
    {
        /// <summary>
        /// Creates a timestamped message that configuration of the digital output pin 0.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DO0Sync register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.AnalogInput.DO0Sync.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that pulse duration (ms) for the digital output pin 0. The pulse will only be emitted when DO0Sync == Pulse.
    /// </summary>
    [DisplayName("DO0PulseWidthPayload")]
    [Description("Creates a message payload that pulse duration (ms) for the digital output pin 0. The pulse will only be emitted when DO0Sync == Pulse.")]
    public partial class CreateDO0PulseWidthPayload
    {
        /// <summary>
        /// Gets or sets the value that pulse duration (ms) for the digital output pin 0. The pulse will only be emitted when DO0Sync == Pulse.
        /// </summary>
        [Range(min: 1, max: 250)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that pulse duration (ms) for the digital output pin 0. The pulse will only be emitted when DO0Sync == Pulse.")]
        public byte DO0PulseWidth { get; set; } = 1;

        /// <summary>
        /// Creates a message payload for the DO0PulseWidth register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public byte GetPayload()
        {
            return DO0PulseWidth;
        }

        /// <summary>
        /// Creates a message that pulse duration (ms) for the digital output pin 0. The pulse will only be emitted when DO0Sync == Pulse.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DO0PulseWidth register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.AnalogInput.DO0PulseWidth.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that pulse duration (ms) for the digital output pin 0. The pulse will only be emitted when DO0Sync == Pulse.
    /// </summary>
    [DisplayName("TimestampedDO0PulseWidthPayload")]
    [Description("Creates a timestamped message payload that pulse duration (ms) for the digital output pin 0. The pulse will only be emitted when DO0Sync == Pulse.")]
    public partial class CreateTimestampedDO0PulseWidthPayload : CreateDO0PulseWidthPayload
    {
        /// <summary>
        /// Creates a timestamped message that pulse duration (ms) for the digital output pin 0. The pulse will only be emitted when DO0Sync == Pulse.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DO0PulseWidth register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.AnalogInput.DO0PulseWidth.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that set the specified digital output lines.
    /// </summary>
    [DisplayName("DigitalOutputSetPayload")]
    [Description("Creates a message payload that set the specified digital output lines.")]
    public partial class CreateDigitalOutputSetPayload
    {
        /// <summary>
        /// Gets or sets the value that set the specified digital output lines.
        /// </summary>
        [Description("The value that set the specified digital output lines.")]
        public DigitalOutputs DigitalOutputSet { get; set; }

        /// <summary>
        /// Creates a message payload for the DigitalOutputSet register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public DigitalOutputs GetPayload()
        {
            return DigitalOutputSet;
        }

        /// <summary>
        /// Creates a message that set the specified digital output lines.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DigitalOutputSet register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.AnalogInput.DigitalOutputSet.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that set the specified digital output lines.
    /// </summary>
    [DisplayName("TimestampedDigitalOutputSetPayload")]
    [Description("Creates a timestamped message payload that set the specified digital output lines.")]
    public partial class CreateTimestampedDigitalOutputSetPayload : CreateDigitalOutputSetPayload
    {
        /// <summary>
        /// Creates a timestamped message that set the specified digital output lines.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DigitalOutputSet register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.AnalogInput.DigitalOutputSet.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that clear the specified digital output lines.
    /// </summary>
    [DisplayName("DigitalOutputClearPayload")]
    [Description("Creates a message payload that clear the specified digital output lines.")]
    public partial class CreateDigitalOutputClearPayload
    {
        /// <summary>
        /// Gets or sets the value that clear the specified digital output lines.
        /// </summary>
        [Description("The value that clear the specified digital output lines.")]
        public DigitalOutputs DigitalOutputClear { get; set; }

        /// <summary>
        /// Creates a message payload for the DigitalOutputClear register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public DigitalOutputs GetPayload()
        {
            return DigitalOutputClear;
        }

        /// <summary>
        /// Creates a message that clear the specified digital output lines.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DigitalOutputClear register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.AnalogInput.DigitalOutputClear.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that clear the specified digital output lines.
    /// </summary>
    [DisplayName("TimestampedDigitalOutputClearPayload")]
    [Description("Creates a timestamped message payload that clear the specified digital output lines.")]
    public partial class CreateTimestampedDigitalOutputClearPayload : CreateDigitalOutputClearPayload
    {
        /// <summary>
        /// Creates a timestamped message that clear the specified digital output lines.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DigitalOutputClear register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.AnalogInput.DigitalOutputClear.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that toggle the specified digital output lines.
    /// </summary>
    [DisplayName("DigitalOutputTogglePayload")]
    [Description("Creates a message payload that toggle the specified digital output lines.")]
    public partial class CreateDigitalOutputTogglePayload
    {
        /// <summary>
        /// Gets or sets the value that toggle the specified digital output lines.
        /// </summary>
        [Description("The value that toggle the specified digital output lines.")]
        public DigitalOutputs DigitalOutputToggle { get; set; }

        /// <summary>
        /// Creates a message payload for the DigitalOutputToggle register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public DigitalOutputs GetPayload()
        {
            return DigitalOutputToggle;
        }

        /// <summary>
        /// Creates a message that toggle the specified digital output lines.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DigitalOutputToggle register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.AnalogInput.DigitalOutputToggle.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that toggle the specified digital output lines.
    /// </summary>
    [DisplayName("TimestampedDigitalOutputTogglePayload")]
    [Description("Creates a timestamped message payload that toggle the specified digital output lines.")]
    public partial class CreateTimestampedDigitalOutputTogglePayload : CreateDigitalOutputTogglePayload
    {
        /// <summary>
        /// Creates a timestamped message that toggle the specified digital output lines.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DigitalOutputToggle register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.AnalogInput.DigitalOutputToggle.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that write the state of all digital output lines. An event will be emitted when the value of any pin was changed by a threshold event.
    /// </summary>
    [DisplayName("DigitalOutputStatePayload")]
    [Description("Creates a message payload that write the state of all digital output lines. An event will be emitted when the value of any pin was changed by a threshold event.")]
    public partial class CreateDigitalOutputStatePayload
    {
        /// <summary>
        /// Gets or sets the value that write the state of all digital output lines. An event will be emitted when the value of any pin was changed by a threshold event.
        /// </summary>
        [Description("The value that write the state of all digital output lines. An event will be emitted when the value of any pin was changed by a threshold event.")]
        public DigitalOutputs DigitalOutputState { get; set; }

        /// <summary>
        /// Creates a message payload for the DigitalOutputState register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public DigitalOutputs GetPayload()
        {
            return DigitalOutputState;
        }

        /// <summary>
        /// Creates a message that write the state of all digital output lines. An event will be emitted when the value of any pin was changed by a threshold event.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DigitalOutputState register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.AnalogInput.DigitalOutputState.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that write the state of all digital output lines. An event will be emitted when the value of any pin was changed by a threshold event.
    /// </summary>
    [DisplayName("TimestampedDigitalOutputStatePayload")]
    [Description("Creates a timestamped message payload that write the state of all digital output lines. An event will be emitted when the value of any pin was changed by a threshold event.")]
    public partial class CreateTimestampedDigitalOutputStatePayload : CreateDigitalOutputStatePayload
    {
        /// <summary>
        /// Creates a timestamped message that write the state of all digital output lines. An event will be emitted when the value of any pin was changed by a threshold event.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DigitalOutputState register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.AnalogInput.DigitalOutputState.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that digital output that will be set when acquisition starts.
    /// </summary>
    [DisplayName("SyncOutputPayload")]
    [Description("Creates a message payload that digital output that will be set when acquisition starts.")]
    public partial class CreateSyncOutputPayload
    {
        /// <summary>
        /// Gets or sets the value that digital output that will be set when acquisition starts.
        /// </summary>
        [Description("The value that digital output that will be set when acquisition starts.")]
        public StartSyncOutputTarget SyncOutput { get; set; }

        /// <summary>
        /// Creates a message payload for the SyncOutput register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public StartSyncOutputTarget GetPayload()
        {
            return SyncOutput;
        }

        /// <summary>
        /// Creates a message that digital output that will be set when acquisition starts.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the SyncOutput register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.AnalogInput.SyncOutput.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that digital output that will be set when acquisition starts.
    /// </summary>
    [DisplayName("TimestampedSyncOutputPayload")]
    [Description("Creates a timestamped message payload that digital output that will be set when acquisition starts.")]
    public partial class CreateTimestampedSyncOutputPayload : CreateSyncOutputPayload
    {
        /// <summary>
        /// Creates a timestamped message that digital output that will be set when acquisition starts.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the SyncOutput register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.AnalogInput.SyncOutput.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that target ADC channel that will be used to trigger a threshold event on DO0 pin.
    /// </summary>
    [DisplayName("DO0TargetChannelPayload")]
    [Description("Creates a message payload that target ADC channel that will be used to trigger a threshold event on DO0 pin.")]
    public partial class CreateDO0TargetChannelPayload
    {
        /// <summary>
        /// Gets or sets the value that target ADC channel that will be used to trigger a threshold event on DO0 pin.
        /// </summary>
        [Description("The value that target ADC channel that will be used to trigger a threshold event on DO0 pin.")]
        public AdcChannel DO0TargetChannel { get; set; }

        /// <summary>
        /// Creates a message payload for the DO0TargetChannel register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public AdcChannel GetPayload()
        {
            return DO0TargetChannel;
        }

        /// <summary>
        /// Creates a message that target ADC channel that will be used to trigger a threshold event on DO0 pin.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DO0TargetChannel register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.AnalogInput.DO0TargetChannel.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that target ADC channel that will be used to trigger a threshold event on DO0 pin.
    /// </summary>
    [DisplayName("TimestampedDO0TargetChannelPayload")]
    [Description("Creates a timestamped message payload that target ADC channel that will be used to trigger a threshold event on DO0 pin.")]
    public partial class CreateTimestampedDO0TargetChannelPayload : CreateDO0TargetChannelPayload
    {
        /// <summary>
        /// Creates a timestamped message that target ADC channel that will be used to trigger a threshold event on DO0 pin.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DO0TargetChannel register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.AnalogInput.DO0TargetChannel.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that target ADC channel that will be used to trigger a threshold event on DO1 pin.
    /// </summary>
    [DisplayName("DO1TargetChannelPayload")]
    [Description("Creates a message payload that target ADC channel that will be used to trigger a threshold event on DO1 pin.")]
    public partial class CreateDO1TargetChannelPayload
    {
        /// <summary>
        /// Gets or sets the value that target ADC channel that will be used to trigger a threshold event on DO1 pin.
        /// </summary>
        [Description("The value that target ADC channel that will be used to trigger a threshold event on DO1 pin.")]
        public AdcChannel DO1TargetChannel { get; set; }

        /// <summary>
        /// Creates a message payload for the DO1TargetChannel register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public AdcChannel GetPayload()
        {
            return DO1TargetChannel;
        }

        /// <summary>
        /// Creates a message that target ADC channel that will be used to trigger a threshold event on DO1 pin.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DO1TargetChannel register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.AnalogInput.DO1TargetChannel.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that target ADC channel that will be used to trigger a threshold event on DO1 pin.
    /// </summary>
    [DisplayName("TimestampedDO1TargetChannelPayload")]
    [Description("Creates a timestamped message payload that target ADC channel that will be used to trigger a threshold event on DO1 pin.")]
    public partial class CreateTimestampedDO1TargetChannelPayload : CreateDO1TargetChannelPayload
    {
        /// <summary>
        /// Creates a timestamped message that target ADC channel that will be used to trigger a threshold event on DO1 pin.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DO1TargetChannel register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.AnalogInput.DO1TargetChannel.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that target ADC channel that will be used to trigger a threshold event on DO2 pin.
    /// </summary>
    [DisplayName("DO2TargetChannelPayload")]
    [Description("Creates a message payload that target ADC channel that will be used to trigger a threshold event on DO2 pin.")]
    public partial class CreateDO2TargetChannelPayload
    {
        /// <summary>
        /// Gets or sets the value that target ADC channel that will be used to trigger a threshold event on DO2 pin.
        /// </summary>
        [Description("The value that target ADC channel that will be used to trigger a threshold event on DO2 pin.")]
        public AdcChannel DO2TargetChannel { get; set; }

        /// <summary>
        /// Creates a message payload for the DO2TargetChannel register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public AdcChannel GetPayload()
        {
            return DO2TargetChannel;
        }

        /// <summary>
        /// Creates a message that target ADC channel that will be used to trigger a threshold event on DO2 pin.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DO2TargetChannel register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.AnalogInput.DO2TargetChannel.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that target ADC channel that will be used to trigger a threshold event on DO2 pin.
    /// </summary>
    [DisplayName("TimestampedDO2TargetChannelPayload")]
    [Description("Creates a timestamped message payload that target ADC channel that will be used to trigger a threshold event on DO2 pin.")]
    public partial class CreateTimestampedDO2TargetChannelPayload : CreateDO2TargetChannelPayload
    {
        /// <summary>
        /// Creates a timestamped message that target ADC channel that will be used to trigger a threshold event on DO2 pin.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DO2TargetChannel register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.AnalogInput.DO2TargetChannel.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that target ADC channel that will be used to trigger a threshold event on DO3 pin.
    /// </summary>
    [DisplayName("DO3TargetChannelPayload")]
    [Description("Creates a message payload that target ADC channel that will be used to trigger a threshold event on DO3 pin.")]
    public partial class CreateDO3TargetChannelPayload
    {
        /// <summary>
        /// Gets or sets the value that target ADC channel that will be used to trigger a threshold event on DO3 pin.
        /// </summary>
        [Description("The value that target ADC channel that will be used to trigger a threshold event on DO3 pin.")]
        public AdcChannel DO3TargetChannel { get; set; }

        /// <summary>
        /// Creates a message payload for the DO3TargetChannel register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public AdcChannel GetPayload()
        {
            return DO3TargetChannel;
        }

        /// <summary>
        /// Creates a message that target ADC channel that will be used to trigger a threshold event on DO3 pin.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DO3TargetChannel register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.AnalogInput.DO3TargetChannel.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that target ADC channel that will be used to trigger a threshold event on DO3 pin.
    /// </summary>
    [DisplayName("TimestampedDO3TargetChannelPayload")]
    [Description("Creates a timestamped message payload that target ADC channel that will be used to trigger a threshold event on DO3 pin.")]
    public partial class CreateTimestampedDO3TargetChannelPayload : CreateDO3TargetChannelPayload
    {
        /// <summary>
        /// Creates a timestamped message that target ADC channel that will be used to trigger a threshold event on DO3 pin.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DO3TargetChannel register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.AnalogInput.DO3TargetChannel.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that value used to threshold an ADC read, and trigger DO0 pin.
    /// </summary>
    [DisplayName("DO0ThresholdPayload")]
    [Description("Creates a message payload that value used to threshold an ADC read, and trigger DO0 pin.")]
    public partial class CreateDO0ThresholdPayload
    {
        /// <summary>
        /// Gets or sets the value that value used to threshold an ADC read, and trigger DO0 pin.
        /// </summary>
        [Description("The value that value used to threshold an ADC read, and trigger DO0 pin.")]
        public short DO0Threshold { get; set; }

        /// <summary>
        /// Creates a message payload for the DO0Threshold register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public short GetPayload()
        {
            return DO0Threshold;
        }

        /// <summary>
        /// Creates a message that value used to threshold an ADC read, and trigger DO0 pin.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DO0Threshold register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.AnalogInput.DO0Threshold.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that value used to threshold an ADC read, and trigger DO0 pin.
    /// </summary>
    [DisplayName("TimestampedDO0ThresholdPayload")]
    [Description("Creates a timestamped message payload that value used to threshold an ADC read, and trigger DO0 pin.")]
    public partial class CreateTimestampedDO0ThresholdPayload : CreateDO0ThresholdPayload
    {
        /// <summary>
        /// Creates a timestamped message that value used to threshold an ADC read, and trigger DO0 pin.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DO0Threshold register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.AnalogInput.DO0Threshold.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that value used to threshold an ADC read, and trigger DO1 pin.
    /// </summary>
    [DisplayName("DO1ThresholdPayload")]
    [Description("Creates a message payload that value used to threshold an ADC read, and trigger DO1 pin.")]
    public partial class CreateDO1ThresholdPayload
    {
        /// <summary>
        /// Gets or sets the value that value used to threshold an ADC read, and trigger DO1 pin.
        /// </summary>
        [Description("The value that value used to threshold an ADC read, and trigger DO1 pin.")]
        public short DO1Threshold { get; set; }

        /// <summary>
        /// Creates a message payload for the DO1Threshold register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public short GetPayload()
        {
            return DO1Threshold;
        }

        /// <summary>
        /// Creates a message that value used to threshold an ADC read, and trigger DO1 pin.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DO1Threshold register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.AnalogInput.DO1Threshold.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that value used to threshold an ADC read, and trigger DO1 pin.
    /// </summary>
    [DisplayName("TimestampedDO1ThresholdPayload")]
    [Description("Creates a timestamped message payload that value used to threshold an ADC read, and trigger DO1 pin.")]
    public partial class CreateTimestampedDO1ThresholdPayload : CreateDO1ThresholdPayload
    {
        /// <summary>
        /// Creates a timestamped message that value used to threshold an ADC read, and trigger DO1 pin.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DO1Threshold register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.AnalogInput.DO1Threshold.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that value used to threshold an ADC read, and trigger DO2 pin.
    /// </summary>
    [DisplayName("DO2ThresholdPayload")]
    [Description("Creates a message payload that value used to threshold an ADC read, and trigger DO2 pin.")]
    public partial class CreateDO2ThresholdPayload
    {
        /// <summary>
        /// Gets or sets the value that value used to threshold an ADC read, and trigger DO2 pin.
        /// </summary>
        [Description("The value that value used to threshold an ADC read, and trigger DO2 pin.")]
        public short DO2Threshold { get; set; }

        /// <summary>
        /// Creates a message payload for the DO2Threshold register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public short GetPayload()
        {
            return DO2Threshold;
        }

        /// <summary>
        /// Creates a message that value used to threshold an ADC read, and trigger DO2 pin.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DO2Threshold register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.AnalogInput.DO2Threshold.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that value used to threshold an ADC read, and trigger DO2 pin.
    /// </summary>
    [DisplayName("TimestampedDO2ThresholdPayload")]
    [Description("Creates a timestamped message payload that value used to threshold an ADC read, and trigger DO2 pin.")]
    public partial class CreateTimestampedDO2ThresholdPayload : CreateDO2ThresholdPayload
    {
        /// <summary>
        /// Creates a timestamped message that value used to threshold an ADC read, and trigger DO2 pin.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DO2Threshold register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.AnalogInput.DO2Threshold.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that value used to threshold an ADC read, and trigger DO3 pin.
    /// </summary>
    [DisplayName("DO3ThresholdPayload")]
    [Description("Creates a message payload that value used to threshold an ADC read, and trigger DO3 pin.")]
    public partial class CreateDO3ThresholdPayload
    {
        /// <summary>
        /// Gets or sets the value that value used to threshold an ADC read, and trigger DO3 pin.
        /// </summary>
        [Description("The value that value used to threshold an ADC read, and trigger DO3 pin.")]
        public short DO3Threshold { get; set; }

        /// <summary>
        /// Creates a message payload for the DO3Threshold register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public short GetPayload()
        {
            return DO3Threshold;
        }

        /// <summary>
        /// Creates a message that value used to threshold an ADC read, and trigger DO3 pin.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DO3Threshold register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.AnalogInput.DO3Threshold.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that value used to threshold an ADC read, and trigger DO3 pin.
    /// </summary>
    [DisplayName("TimestampedDO3ThresholdPayload")]
    [Description("Creates a timestamped message payload that value used to threshold an ADC read, and trigger DO3 pin.")]
    public partial class CreateTimestampedDO3ThresholdPayload : CreateDO3ThresholdPayload
    {
        /// <summary>
        /// Creates a timestamped message that value used to threshold an ADC read, and trigger DO3 pin.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DO3Threshold register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.AnalogInput.DO3Threshold.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that time (ms) above threshold value that is required to trigger a DO0 pin event.
    /// </summary>
    [DisplayName("DO0TimeAboveThresholdPayload")]
    [Description("Creates a message payload that time (ms) above threshold value that is required to trigger a DO0 pin event.")]
    public partial class CreateDO0TimeAboveThresholdPayload
    {
        /// <summary>
        /// Gets or sets the value that time (ms) above threshold value that is required to trigger a DO0 pin event.
        /// </summary>
        [Description("The value that time (ms) above threshold value that is required to trigger a DO0 pin event.")]
        public ushort DO0TimeAboveThreshold { get; set; } = 0;

        /// <summary>
        /// Creates a message payload for the DO0TimeAboveThreshold register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return DO0TimeAboveThreshold;
        }

        /// <summary>
        /// Creates a message that time (ms) above threshold value that is required to trigger a DO0 pin event.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DO0TimeAboveThreshold register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.AnalogInput.DO0TimeAboveThreshold.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that time (ms) above threshold value that is required to trigger a DO0 pin event.
    /// </summary>
    [DisplayName("TimestampedDO0TimeAboveThresholdPayload")]
    [Description("Creates a timestamped message payload that time (ms) above threshold value that is required to trigger a DO0 pin event.")]
    public partial class CreateTimestampedDO0TimeAboveThresholdPayload : CreateDO0TimeAboveThresholdPayload
    {
        /// <summary>
        /// Creates a timestamped message that time (ms) above threshold value that is required to trigger a DO0 pin event.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DO0TimeAboveThreshold register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.AnalogInput.DO0TimeAboveThreshold.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that time (ms) above threshold value that is required to trigger a DO1 pin event.
    /// </summary>
    [DisplayName("DO1TimeAboveThresholdPayload")]
    [Description("Creates a message payload that time (ms) above threshold value that is required to trigger a DO1 pin event.")]
    public partial class CreateDO1TimeAboveThresholdPayload
    {
        /// <summary>
        /// Gets or sets the value that time (ms) above threshold value that is required to trigger a DO1 pin event.
        /// </summary>
        [Description("The value that time (ms) above threshold value that is required to trigger a DO1 pin event.")]
        public ushort DO1TimeAboveThreshold { get; set; } = 0;

        /// <summary>
        /// Creates a message payload for the DO1TimeAboveThreshold register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return DO1TimeAboveThreshold;
        }

        /// <summary>
        /// Creates a message that time (ms) above threshold value that is required to trigger a DO1 pin event.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DO1TimeAboveThreshold register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.AnalogInput.DO1TimeAboveThreshold.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that time (ms) above threshold value that is required to trigger a DO1 pin event.
    /// </summary>
    [DisplayName("TimestampedDO1TimeAboveThresholdPayload")]
    [Description("Creates a timestamped message payload that time (ms) above threshold value that is required to trigger a DO1 pin event.")]
    public partial class CreateTimestampedDO1TimeAboveThresholdPayload : CreateDO1TimeAboveThresholdPayload
    {
        /// <summary>
        /// Creates a timestamped message that time (ms) above threshold value that is required to trigger a DO1 pin event.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DO1TimeAboveThreshold register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.AnalogInput.DO1TimeAboveThreshold.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that time (ms) above threshold value that is required to trigger a DO2 pin event.
    /// </summary>
    [DisplayName("DO2TimeAboveThresholdPayload")]
    [Description("Creates a message payload that time (ms) above threshold value that is required to trigger a DO2 pin event.")]
    public partial class CreateDO2TimeAboveThresholdPayload
    {
        /// <summary>
        /// Gets or sets the value that time (ms) above threshold value that is required to trigger a DO2 pin event.
        /// </summary>
        [Description("The value that time (ms) above threshold value that is required to trigger a DO2 pin event.")]
        public ushort DO2TimeAboveThreshold { get; set; } = 0;

        /// <summary>
        /// Creates a message payload for the DO2TimeAboveThreshold register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return DO2TimeAboveThreshold;
        }

        /// <summary>
        /// Creates a message that time (ms) above threshold value that is required to trigger a DO2 pin event.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DO2TimeAboveThreshold register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.AnalogInput.DO2TimeAboveThreshold.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that time (ms) above threshold value that is required to trigger a DO2 pin event.
    /// </summary>
    [DisplayName("TimestampedDO2TimeAboveThresholdPayload")]
    [Description("Creates a timestamped message payload that time (ms) above threshold value that is required to trigger a DO2 pin event.")]
    public partial class CreateTimestampedDO2TimeAboveThresholdPayload : CreateDO2TimeAboveThresholdPayload
    {
        /// <summary>
        /// Creates a timestamped message that time (ms) above threshold value that is required to trigger a DO2 pin event.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DO2TimeAboveThreshold register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.AnalogInput.DO2TimeAboveThreshold.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that time (ms) above threshold value that is required to trigger a DO3 pin event.
    /// </summary>
    [DisplayName("DO3TimeAboveThresholdPayload")]
    [Description("Creates a message payload that time (ms) above threshold value that is required to trigger a DO3 pin event.")]
    public partial class CreateDO3TimeAboveThresholdPayload
    {
        /// <summary>
        /// Gets or sets the value that time (ms) above threshold value that is required to trigger a DO3 pin event.
        /// </summary>
        [Description("The value that time (ms) above threshold value that is required to trigger a DO3 pin event.")]
        public ushort DO3TimeAboveThreshold { get; set; } = 0;

        /// <summary>
        /// Creates a message payload for the DO3TimeAboveThreshold register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return DO3TimeAboveThreshold;
        }

        /// <summary>
        /// Creates a message that time (ms) above threshold value that is required to trigger a DO3 pin event.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DO3TimeAboveThreshold register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.AnalogInput.DO3TimeAboveThreshold.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that time (ms) above threshold value that is required to trigger a DO3 pin event.
    /// </summary>
    [DisplayName("TimestampedDO3TimeAboveThresholdPayload")]
    [Description("Creates a timestamped message payload that time (ms) above threshold value that is required to trigger a DO3 pin event.")]
    public partial class CreateTimestampedDO3TimeAboveThresholdPayload : CreateDO3TimeAboveThresholdPayload
    {
        /// <summary>
        /// Creates a timestamped message that time (ms) above threshold value that is required to trigger a DO3 pin event.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DO3TimeAboveThreshold register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.AnalogInput.DO3TimeAboveThreshold.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that time (ms) below threshold value that is required to trigger a DO0 pin event.
    /// </summary>
    [DisplayName("DO0TimeBelowThresholdPayload")]
    [Description("Creates a message payload that time (ms) below threshold value that is required to trigger a DO0 pin event.")]
    public partial class CreateDO0TimeBelowThresholdPayload
    {
        /// <summary>
        /// Gets or sets the value that time (ms) below threshold value that is required to trigger a DO0 pin event.
        /// </summary>
        [Description("The value that time (ms) below threshold value that is required to trigger a DO0 pin event.")]
        public ushort DO0TimeBelowThreshold { get; set; } = 0;

        /// <summary>
        /// Creates a message payload for the DO0TimeBelowThreshold register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return DO0TimeBelowThreshold;
        }

        /// <summary>
        /// Creates a message that time (ms) below threshold value that is required to trigger a DO0 pin event.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DO0TimeBelowThreshold register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.AnalogInput.DO0TimeBelowThreshold.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that time (ms) below threshold value that is required to trigger a DO0 pin event.
    /// </summary>
    [DisplayName("TimestampedDO0TimeBelowThresholdPayload")]
    [Description("Creates a timestamped message payload that time (ms) below threshold value that is required to trigger a DO0 pin event.")]
    public partial class CreateTimestampedDO0TimeBelowThresholdPayload : CreateDO0TimeBelowThresholdPayload
    {
        /// <summary>
        /// Creates a timestamped message that time (ms) below threshold value that is required to trigger a DO0 pin event.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DO0TimeBelowThreshold register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.AnalogInput.DO0TimeBelowThreshold.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that time (ms) below threshold value that is required to trigger a DO1 pin event.
    /// </summary>
    [DisplayName("DO1TimeBelowThresholdPayload")]
    [Description("Creates a message payload that time (ms) below threshold value that is required to trigger a DO1 pin event.")]
    public partial class CreateDO1TimeBelowThresholdPayload
    {
        /// <summary>
        /// Gets or sets the value that time (ms) below threshold value that is required to trigger a DO1 pin event.
        /// </summary>
        [Description("The value that time (ms) below threshold value that is required to trigger a DO1 pin event.")]
        public ushort DO1TimeBelowThreshold { get; set; } = 0;

        /// <summary>
        /// Creates a message payload for the DO1TimeBelowThreshold register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return DO1TimeBelowThreshold;
        }

        /// <summary>
        /// Creates a message that time (ms) below threshold value that is required to trigger a DO1 pin event.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DO1TimeBelowThreshold register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.AnalogInput.DO1TimeBelowThreshold.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that time (ms) below threshold value that is required to trigger a DO1 pin event.
    /// </summary>
    [DisplayName("TimestampedDO1TimeBelowThresholdPayload")]
    [Description("Creates a timestamped message payload that time (ms) below threshold value that is required to trigger a DO1 pin event.")]
    public partial class CreateTimestampedDO1TimeBelowThresholdPayload : CreateDO1TimeBelowThresholdPayload
    {
        /// <summary>
        /// Creates a timestamped message that time (ms) below threshold value that is required to trigger a DO1 pin event.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DO1TimeBelowThreshold register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.AnalogInput.DO1TimeBelowThreshold.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that time (ms) below threshold value that is required to trigger a DO2 pin event.
    /// </summary>
    [DisplayName("DO2TimeBelowThresholdPayload")]
    [Description("Creates a message payload that time (ms) below threshold value that is required to trigger a DO2 pin event.")]
    public partial class CreateDO2TimeBelowThresholdPayload
    {
        /// <summary>
        /// Gets or sets the value that time (ms) below threshold value that is required to trigger a DO2 pin event.
        /// </summary>
        [Description("The value that time (ms) below threshold value that is required to trigger a DO2 pin event.")]
        public ushort DO2TimeBelowThreshold { get; set; } = 0;

        /// <summary>
        /// Creates a message payload for the DO2TimeBelowThreshold register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return DO2TimeBelowThreshold;
        }

        /// <summary>
        /// Creates a message that time (ms) below threshold value that is required to trigger a DO2 pin event.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DO2TimeBelowThreshold register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.AnalogInput.DO2TimeBelowThreshold.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that time (ms) below threshold value that is required to trigger a DO2 pin event.
    /// </summary>
    [DisplayName("TimestampedDO2TimeBelowThresholdPayload")]
    [Description("Creates a timestamped message payload that time (ms) below threshold value that is required to trigger a DO2 pin event.")]
    public partial class CreateTimestampedDO2TimeBelowThresholdPayload : CreateDO2TimeBelowThresholdPayload
    {
        /// <summary>
        /// Creates a timestamped message that time (ms) below threshold value that is required to trigger a DO2 pin event.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DO2TimeBelowThreshold register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.AnalogInput.DO2TimeBelowThreshold.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that time (ms) below threshold value that is required to trigger a DO3 pin event.
    /// </summary>
    [DisplayName("DO3TimeBelowThresholdPayload")]
    [Description("Creates a message payload that time (ms) below threshold value that is required to trigger a DO3 pin event.")]
    public partial class CreateDO3TimeBelowThresholdPayload
    {
        /// <summary>
        /// Gets or sets the value that time (ms) below threshold value that is required to trigger a DO3 pin event.
        /// </summary>
        [Description("The value that time (ms) below threshold value that is required to trigger a DO3 pin event.")]
        public ushort DO3TimeBelowThreshold { get; set; } = 0;

        /// <summary>
        /// Creates a message payload for the DO3TimeBelowThreshold register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return DO3TimeBelowThreshold;
        }

        /// <summary>
        /// Creates a message that time (ms) below threshold value that is required to trigger a DO3 pin event.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DO3TimeBelowThreshold register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.AnalogInput.DO3TimeBelowThreshold.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that time (ms) below threshold value that is required to trigger a DO3 pin event.
    /// </summary>
    [DisplayName("TimestampedDO3TimeBelowThresholdPayload")]
    [Description("Creates a timestamped message payload that time (ms) below threshold value that is required to trigger a DO3 pin event.")]
    public partial class CreateTimestampedDO3TimeBelowThresholdPayload : CreateDO3TimeBelowThresholdPayload
    {
        /// <summary>
        /// Creates a timestamped message that time (ms) below threshold value that is required to trigger a DO3 pin event.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DO3TimeBelowThreshold register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.AnalogInput.DO3TimeBelowThreshold.FromPayload(timestamp, messageType, GetPayload());
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
        /// <param name="channel0"></param>
        /// <param name="channel1"></param>
        /// <param name="channel2"></param>
        /// <param name="channel3"></param>
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
        /// 
        /// </summary>
        public short Channel0;

        /// <summary>
        /// 
        /// </summary>
        public short Channel1;

        /// <summary>
        /// 
        /// </summary>
        public short Channel2;

        /// <summary>
        /// 
        /// </summary>
        public short Channel3;

        /// <summary>
        /// Returns a <see cref="string"/> that represents the payload of
        /// the AnalogData register.
        /// </summary>
        /// <returns>
        /// A <see cref="string"/> that represents the payload of the
        /// AnalogData register.
        /// </returns>
        public override string ToString()
        {
            return "AnalogDataPayload { " +
                "Channel0 = " + Channel0 + ", " +
                "Channel1 = " + Channel1 + ", " +
                "Channel2 = " + Channel2 + ", " +
                "Channel3 = " + Channel3 + " " +
            "}";
        }
    }

    /// <summary>
    /// Available digital input lines.
    /// </summary>
    [Flags]
    public enum DigitalInputs : byte
    {
        None = 0x0,
        DI0 = 0x1
    }

    /// <summary>
    /// Specifies the state of port digital output lines.
    /// </summary>
    [Flags]
    public enum DigitalOutputs : byte
    {
        None = 0x0,
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
    /// Available settings to set the range (Volt) and LowPass filter cutoff (Hz) of the ADC.
    /// </summary>
    public enum RangeAndFilterConfig : byte
    {
        Range5VLowPass1500Hz = 6,
        Range5VLowPass3000Hz = 5,
        Range5VLowPass6000Hz = 4,
        Range5VLowPass10300Hz = 3,
        Range5VLowPass13700Hz = 2,
        Range5VLowPass15000Hz = 1,
        Range10VLowPass1500Hz = 22,
        Range10VLowPass3000Hz = 21,
        Range10VLowPass6000Hz = 20,
        Range10VLowPass11900Hz = 19,
        Range10VLowPass18500Hz = 18,
        Range10VLowPass22000Hz = 17
    }

    /// <summary>
    /// Available sampling frequency settings of the ADC.
    /// </summary>
    public enum SamplingRateMode : byte
    {
        SamplingRate1000Hz = 0,
        SamplingRate2000Hz = 1
    }

    /// <summary>
    /// Available configurations for when using DI0 as an acquisition trigger.
    /// </summary>
    public enum TriggerConfig : byte
    {
        None = 0,
        StartOnRisingEdge = 1,
        StartOnFallingEdge = 2,
        SampleOnRisingEdge = 3
    }

    /// <summary>
    /// Available configurations when using DO0 pin to report firmware events.
    /// </summary>
    public enum SyncConfig : byte
    {
        None = 0,
        Heartbeat = 1,
        Pulse = 2
    }

    /// <summary>
    /// Available digital output pins that are able to be triggered on acquisition start.
    /// </summary>
    public enum StartSyncOutputTarget : byte
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
    public enum AdcChannel : byte
    {
        Channel0 = 0,
        Channel1 = 1,
        Channel2 = 2,
        Channel3 = 3,
        None = 8
    }
}
