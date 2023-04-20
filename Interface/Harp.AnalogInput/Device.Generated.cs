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
            { 34, typeof(DI0) },
            { 37, typeof(RangeAndFilterCutoff) },
            { 38, typeof(SamplingFrequency) },
            { 39, typeof(DI0Mode) },
            { 40, typeof(DO0Mode) },
            { 41, typeof(DO0PulseWidth) },
            { 42, typeof(DOSet) },
            { 43, typeof(DOClear) },
            { 44, typeof(DOToggle) },
            { 45, typeof(DOState) },
            { 48, typeof(AcquisitionStartDO) }
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
    /// <seealso cref="DI0"/>
    /// <seealso cref="RangeAndFilterCutoff"/>
    /// <seealso cref="SamplingFrequency"/>
    /// <seealso cref="DI0Mode"/>
    /// <seealso cref="DO0Mode"/>
    /// <seealso cref="DO0PulseWidth"/>
    /// <seealso cref="DOSet"/>
    /// <seealso cref="DOClear"/>
    /// <seealso cref="DOToggle"/>
    /// <seealso cref="DOState"/>
    /// <seealso cref="AcquisitionStartDO"/>
    [XmlInclude(typeof(StartAcquisition))]
    [XmlInclude(typeof(AnalogData))]
    [XmlInclude(typeof(DI0))]
    [XmlInclude(typeof(RangeAndFilterCutoff))]
    [XmlInclude(typeof(SamplingFrequency))]
    [XmlInclude(typeof(DI0Mode))]
    [XmlInclude(typeof(DO0Mode))]
    [XmlInclude(typeof(DO0PulseWidth))]
    [XmlInclude(typeof(DOSet))]
    [XmlInclude(typeof(DOClear))]
    [XmlInclude(typeof(DOToggle))]
    [XmlInclude(typeof(DOState))]
    [XmlInclude(typeof(AcquisitionStartDO))]
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
    /// <seealso cref="DI0"/>
    /// <seealso cref="RangeAndFilterCutoff"/>
    /// <seealso cref="SamplingFrequency"/>
    /// <seealso cref="DI0Mode"/>
    /// <seealso cref="DO0Mode"/>
    /// <seealso cref="DO0PulseWidth"/>
    /// <seealso cref="DOSet"/>
    /// <seealso cref="DOClear"/>
    /// <seealso cref="DOToggle"/>
    /// <seealso cref="DOState"/>
    /// <seealso cref="AcquisitionStartDO"/>
    [XmlInclude(typeof(StartAcquisition))]
    [XmlInclude(typeof(AnalogData))]
    [XmlInclude(typeof(DI0))]
    [XmlInclude(typeof(RangeAndFilterCutoff))]
    [XmlInclude(typeof(SamplingFrequency))]
    [XmlInclude(typeof(DI0Mode))]
    [XmlInclude(typeof(DO0Mode))]
    [XmlInclude(typeof(DO0PulseWidth))]
    [XmlInclude(typeof(DOSet))]
    [XmlInclude(typeof(DOClear))]
    [XmlInclude(typeof(DOToggle))]
    [XmlInclude(typeof(DOState))]
    [XmlInclude(typeof(AcquisitionStartDO))]
    [XmlInclude(typeof(TimestampedStartAcquisition))]
    [XmlInclude(typeof(TimestampedAnalogData))]
    [XmlInclude(typeof(TimestampedDI0))]
    [XmlInclude(typeof(TimestampedRangeAndFilterCutoff))]
    [XmlInclude(typeof(TimestampedSamplingFrequency))]
    [XmlInclude(typeof(TimestampedDI0Mode))]
    [XmlInclude(typeof(TimestampedDO0Mode))]
    [XmlInclude(typeof(TimestampedDO0PulseWidth))]
    [XmlInclude(typeof(TimestampedDOSet))]
    [XmlInclude(typeof(TimestampedDOClear))]
    [XmlInclude(typeof(TimestampedDOToggle))]
    [XmlInclude(typeof(TimestampedDOState))]
    [XmlInclude(typeof(TimestampedAcquisitionStartDO))]
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
    /// <seealso cref="DI0"/>
    /// <seealso cref="RangeAndFilterCutoff"/>
    /// <seealso cref="SamplingFrequency"/>
    /// <seealso cref="DI0Mode"/>
    /// <seealso cref="DO0Mode"/>
    /// <seealso cref="DO0PulseWidth"/>
    /// <seealso cref="DOSet"/>
    /// <seealso cref="DOClear"/>
    /// <seealso cref="DOToggle"/>
    /// <seealso cref="DOState"/>
    /// <seealso cref="AcquisitionStartDO"/>
    [XmlInclude(typeof(StartAcquisition))]
    [XmlInclude(typeof(AnalogData))]
    [XmlInclude(typeof(DI0))]
    [XmlInclude(typeof(RangeAndFilterCutoff))]
    [XmlInclude(typeof(SamplingFrequency))]
    [XmlInclude(typeof(DI0Mode))]
    [XmlInclude(typeof(DO0Mode))]
    [XmlInclude(typeof(DO0PulseWidth))]
    [XmlInclude(typeof(DOSet))]
    [XmlInclude(typeof(DOClear))]
    [XmlInclude(typeof(DOToggle))]
    [XmlInclude(typeof(DOState))]
    [XmlInclude(typeof(AcquisitionStartDO))]
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
    [Description("Enables the data acquisition.")]
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
    public partial class DI0
    {
        /// <summary>
        /// Represents the address of the <see cref="DI0"/> register. This field is constant.
        /// </summary>
        public const int Address = 34;

        /// <summary>
        /// Represents the payload type of the <see cref="DI0"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="DI0"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DI0"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static DigitalState GetPayload(HarpMessage message)
        {
            return (DigitalState)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DI0"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalState> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((DigitalState)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DI0"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DI0"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, DigitalState value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DI0"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DI0"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, DigitalState value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DI0 register.
    /// </summary>
    /// <seealso cref="DI0"/>
    [Description("Filters and selects timestamped messages from the DI0 register.")]
    public partial class TimestampedDI0
    {
        /// <summary>
        /// Represents the address of the <see cref="DI0"/> register. This field is constant.
        /// </summary>
        public const int Address = DI0.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DI0"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalState> GetPayload(HarpMessage message)
        {
            return DI0.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sets the range and LowPass filter cutoff of the ADC.
    /// </summary>
    [Description("Sets the range and LowPass filter cutoff of the ADC.")]
    public partial class RangeAndFilterCutoff
    {
        /// <summary>
        /// Represents the address of the <see cref="RangeAndFilterCutoff"/> register. This field is constant.
        /// </summary>
        public const int Address = 37;

        /// <summary>
        /// Represents the payload type of the <see cref="RangeAndFilterCutoff"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="RangeAndFilterCutoff"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="RangeAndFilterCutoff"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static RangeAndFilterConfig GetPayload(HarpMessage message)
        {
            return (RangeAndFilterConfig)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="RangeAndFilterCutoff"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<RangeAndFilterConfig> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((RangeAndFilterConfig)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="RangeAndFilterCutoff"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="RangeAndFilterCutoff"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, RangeAndFilterConfig value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="RangeAndFilterCutoff"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="RangeAndFilterCutoff"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, RangeAndFilterConfig value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// RangeAndFilterCutoff register.
    /// </summary>
    /// <seealso cref="RangeAndFilterCutoff"/>
    [Description("Filters and selects timestamped messages from the RangeAndFilterCutoff register.")]
    public partial class TimestampedRangeAndFilterCutoff
    {
        /// <summary>
        /// Represents the address of the <see cref="RangeAndFilterCutoff"/> register. This field is constant.
        /// </summary>
        public const int Address = RangeAndFilterCutoff.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="RangeAndFilterCutoff"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<RangeAndFilterConfig> GetPayload(HarpMessage message)
        {
            return RangeAndFilterCutoff.GetTimestampedPayload(message);
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
        public static DI0ModeConfig GetPayload(HarpMessage message)
        {
            return (DI0ModeConfig)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DI0Mode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DI0ModeConfig> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((DI0ModeConfig)payload.Value, payload.Seconds);
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
        public static HarpMessage FromPayload(MessageType messageType, DI0ModeConfig value)
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
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, DI0ModeConfig value)
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
        public static Timestamped<DI0ModeConfig> GetPayload(HarpMessage message)
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
        public static DO0ModeConfig GetPayload(HarpMessage message)
        {
            return (DO0ModeConfig)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DO0Mode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DO0ModeConfig> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((DO0ModeConfig)payload.Value, payload.Seconds);
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
        public static HarpMessage FromPayload(MessageType messageType, DO0ModeConfig value)
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
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, DO0ModeConfig value)
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
        public static Timestamped<DO0ModeConfig> GetPayload(HarpMessage message)
        {
            return DO0Mode.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that pulse duration (ms) for the digital output pin 0. The pulse will only be emitted when DO0Mode == Pulse.
    /// </summary>
    [Description("Pulse duration (ms) for the digital output pin 0. The pulse will only be emitted when DO0Mode == Pulse.")]
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
    public partial class DOSet
    {
        /// <summary>
        /// Represents the address of the <see cref="DOSet"/> register. This field is constant.
        /// </summary>
        public const int Address = 42;

        /// <summary>
        /// Represents the payload type of the <see cref="DOSet"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="DOSet"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DOSet"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static DigitalOutputs GetPayload(HarpMessage message)
        {
            return (DigitalOutputs)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DOSet"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalOutputs> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((DigitalOutputs)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DOSet"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DOSet"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, DigitalOutputs value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DOSet"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DOSet"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, DigitalOutputs value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DOSet register.
    /// </summary>
    /// <seealso cref="DOSet"/>
    [Description("Filters and selects timestamped messages from the DOSet register.")]
    public partial class TimestampedDOSet
    {
        /// <summary>
        /// Represents the address of the <see cref="DOSet"/> register. This field is constant.
        /// </summary>
        public const int Address = DOSet.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DOSet"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalOutputs> GetPayload(HarpMessage message)
        {
            return DOSet.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that clear the specified digital output lines.
    /// </summary>
    [Description("Clear the specified digital output lines.")]
    public partial class DOClear
    {
        /// <summary>
        /// Represents the address of the <see cref="DOClear"/> register. This field is constant.
        /// </summary>
        public const int Address = 43;

        /// <summary>
        /// Represents the payload type of the <see cref="DOClear"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="DOClear"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DOClear"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static DigitalOutputs GetPayload(HarpMessage message)
        {
            return (DigitalOutputs)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DOClear"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalOutputs> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((DigitalOutputs)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DOClear"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DOClear"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, DigitalOutputs value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DOClear"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DOClear"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, DigitalOutputs value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DOClear register.
    /// </summary>
    /// <seealso cref="DOClear"/>
    [Description("Filters and selects timestamped messages from the DOClear register.")]
    public partial class TimestampedDOClear
    {
        /// <summary>
        /// Represents the address of the <see cref="DOClear"/> register. This field is constant.
        /// </summary>
        public const int Address = DOClear.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DOClear"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalOutputs> GetPayload(HarpMessage message)
        {
            return DOClear.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that toggle the specified digital output lines.
    /// </summary>
    [Description("Toggle the specified digital output lines")]
    public partial class DOToggle
    {
        /// <summary>
        /// Represents the address of the <see cref="DOToggle"/> register. This field is constant.
        /// </summary>
        public const int Address = 44;

        /// <summary>
        /// Represents the payload type of the <see cref="DOToggle"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="DOToggle"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DOToggle"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static DigitalOutputs GetPayload(HarpMessage message)
        {
            return (DigitalOutputs)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DOToggle"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalOutputs> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((DigitalOutputs)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DOToggle"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DOToggle"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, DigitalOutputs value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DOToggle"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DOToggle"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, DigitalOutputs value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DOToggle register.
    /// </summary>
    /// <seealso cref="DOToggle"/>
    [Description("Filters and selects timestamped messages from the DOToggle register.")]
    public partial class TimestampedDOToggle
    {
        /// <summary>
        /// Represents the address of the <see cref="DOToggle"/> register. This field is constant.
        /// </summary>
        public const int Address = DOToggle.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DOToggle"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalOutputs> GetPayload(HarpMessage message)
        {
            return DOToggle.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that write the state of all digital output lines. An event will be emitted when the value of any pin was changed by a threshold crossing event.
    /// </summary>
    [Description("Write the state of all digital output lines. An event will be emitted when the value of any pin was changed by a threshold crossing event.")]
    public partial class DOState
    {
        /// <summary>
        /// Represents the address of the <see cref="DOState"/> register. This field is constant.
        /// </summary>
        public const int Address = 45;

        /// <summary>
        /// Represents the payload type of the <see cref="DOState"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="DOState"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DOState"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static DigitalOutputs GetPayload(HarpMessage message)
        {
            return (DigitalOutputs)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DOState"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalOutputs> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((DigitalOutputs)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DOState"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DOState"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, DigitalOutputs value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DOState"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DOState"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, DigitalOutputs value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DOState register.
    /// </summary>
    /// <seealso cref="DOState"/>
    [Description("Filters and selects timestamped messages from the DOState register.")]
    public partial class TimestampedDOState
    {
        /// <summary>
        /// Represents the address of the <see cref="DOState"/> register. This field is constant.
        /// </summary>
        public const int Address = DOState.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DOState"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalOutputs> GetPayload(HarpMessage message)
        {
            return DOState.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that digital output that will be set when the acquisition starts.
    /// </summary>
    [Description("Digital output that will be set when the acquisition starts.")]
    public partial class AcquisitionStartDO
    {
        /// <summary>
        /// Represents the address of the <see cref="AcquisitionStartDO"/> register. This field is constant.
        /// </summary>
        public const int Address = 48;

        /// <summary>
        /// Represents the payload type of the <see cref="AcquisitionStartDO"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="AcquisitionStartDO"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="AcquisitionStartDO"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static AcquisitionStartDOTargets GetPayload(HarpMessage message)
        {
            return (AcquisitionStartDOTargets)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="AcquisitionStartDO"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<AcquisitionStartDOTargets> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((AcquisitionStartDOTargets)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="AcquisitionStartDO"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="AcquisitionStartDO"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, AcquisitionStartDOTargets value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="AcquisitionStartDO"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="AcquisitionStartDO"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, AcquisitionStartDOTargets value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// AcquisitionStartDO register.
    /// </summary>
    /// <seealso cref="AcquisitionStartDO"/>
    [Description("Filters and selects timestamped messages from the AcquisitionStartDO register.")]
    public partial class TimestampedAcquisitionStartDO
    {
        /// <summary>
        /// Represents the address of the <see cref="AcquisitionStartDO"/> register. This field is constant.
        /// </summary>
        public const int Address = AcquisitionStartDO.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="AcquisitionStartDO"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<AcquisitionStartDOTargets> GetPayload(HarpMessage message)
        {
            return AcquisitionStartDO.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents an operator which creates standard message payloads for the
    /// AnalogInput device.
    /// </summary>
    /// <seealso cref="CreateStartAcquisitionPayload"/>
    /// <seealso cref="CreateAnalogDataPayload"/>
    /// <seealso cref="CreateDI0Payload"/>
    /// <seealso cref="CreateRangeAndFilterCutoffPayload"/>
    /// <seealso cref="CreateSamplingFrequencyPayload"/>
    /// <seealso cref="CreateDI0ModePayload"/>
    /// <seealso cref="CreateDO0ModePayload"/>
    /// <seealso cref="CreateDO0PulseWidthPayload"/>
    /// <seealso cref="CreateDOSetPayload"/>
    /// <seealso cref="CreateDOClearPayload"/>
    /// <seealso cref="CreateDOTogglePayload"/>
    /// <seealso cref="CreateDOStatePayload"/>
    /// <seealso cref="CreateAcquisitionStartDOPayload"/>
    [XmlInclude(typeof(CreateStartAcquisitionPayload))]
    [XmlInclude(typeof(CreateAnalogDataPayload))]
    [XmlInclude(typeof(CreateDI0Payload))]
    [XmlInclude(typeof(CreateRangeAndFilterCutoffPayload))]
    [XmlInclude(typeof(CreateSamplingFrequencyPayload))]
    [XmlInclude(typeof(CreateDI0ModePayload))]
    [XmlInclude(typeof(CreateDO0ModePayload))]
    [XmlInclude(typeof(CreateDO0PulseWidthPayload))]
    [XmlInclude(typeof(CreateDOSetPayload))]
    [XmlInclude(typeof(CreateDOClearPayload))]
    [XmlInclude(typeof(CreateDOTogglePayload))]
    [XmlInclude(typeof(CreateDOStatePayload))]
    [XmlInclude(typeof(CreateAcquisitionStartDOPayload))]
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
    [DisplayName("DI0Payload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that state of the digital input pin 0.")]
    public partial class CreateDI0Payload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that state of the digital input pin 0.
        /// </summary>
        [Description("The value that state of the digital input pin 0.")]
        public DigitalState Value { get; set; }

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
            return source.Select(_ => DI0.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sets the range and LowPass filter cutoff of the ADC.
    /// </summary>
    [DisplayName("RangeAndFilterCutoffPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the range and LowPass filter cutoff of the ADC.")]
    public partial class CreateRangeAndFilterCutoffPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sets the range and LowPass filter cutoff of the ADC.
        /// </summary>
        [Description("The value that sets the range and LowPass filter cutoff of the ADC.")]
        public RangeAndFilterConfig Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the range and LowPass filter cutoff of the ADC.
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
        /// that sets the range and LowPass filter cutoff of the ADC.
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
            return source.Select(_ => RangeAndFilterCutoff.FromPayload(MessageType, Value));
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
        public DI0ModeConfig Value { get; set; }

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
        public DO0ModeConfig Value { get; set; }

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
    [DisplayName("DO0PulseWidthPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that pulse duration (ms) for the digital output pin 0. The pulse will only be emitted when DO0Mode == Pulse.")]
    public partial class CreateDO0PulseWidthPayload : HarpCombinator
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
            return source.Select(_ => DO0PulseWidth.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that set the specified digital output lines.
    /// </summary>
    [DisplayName("DOSetPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that set the specified digital output lines.")]
    public partial class CreateDOSetPayload : HarpCombinator
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
            return source.Select(_ => DOSet.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that clear the specified digital output lines.
    /// </summary>
    [DisplayName("DOClearPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that clear the specified digital output lines.")]
    public partial class CreateDOClearPayload : HarpCombinator
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
            return source.Select(_ => DOClear.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that toggle the specified digital output lines.
    /// </summary>
    [DisplayName("DOTogglePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that toggle the specified digital output lines.")]
    public partial class CreateDOTogglePayload : HarpCombinator
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
            return source.Select(_ => DOToggle.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that write the state of all digital output lines. An event will be emitted when the value of any pin was changed by a threshold crossing event.
    /// </summary>
    [DisplayName("DOStatePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that write the state of all digital output lines. An event will be emitted when the value of any pin was changed by a threshold crossing event.")]
    public partial class CreateDOStatePayload : HarpCombinator
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
            return source.Select(_ => DOState.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that digital output that will be set when the acquisition starts.
    /// </summary>
    [DisplayName("AcquisitionStartDOPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that digital output that will be set when the acquisition starts.")]
    public partial class CreateAcquisitionStartDOPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that digital output that will be set when the acquisition starts.
        /// </summary>
        [Description("The value that digital output that will be set when the acquisition starts.")]
        public AcquisitionStartDOTargets Value { get; set; }

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
            return source.Select(_ => AcquisitionStartDO.FromPayload(MessageType, Value));
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
    public enum DigitalState : byte
    {
        Low = 0,
        High = 1
    }

    /// <summary>
    /// The available settings for set the range and LowPass filter cutoff of the ADC.
    /// </summary>
    public enum RangeAndFilterConfig : byte
    {
        Range5V_LowPass1k5Hz = 6,
        Range5V_LowPass3kHz = 5,
        Range5V_LowPass6kHz = 4,
        Range5V_LowPass10k3Hz = 3,
        Range5V_LowPass13k7Hz = 2,
        Range5V_LowPass15kHz = 1,
        Range10V_LowPass1k5Hz = 22,
        Range10V_LowPass3kHz = 21,
        Range10V_LowPass6kHz = 20,
        Range10V_LowPass11k9Hz = 19,
        Range10V_LowPass18k5Hz = 18,
        Range10V_LowPass22kHz = 17
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
    public enum DI0ModeConfig : byte
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
    public enum DO0ModeConfig : byte
    {
        Output = 0,
        ToggleEachSecond = 1,
        Pulse = 2
    }

    /// <summary>
    /// Available digital output pins that are able to be triggered on acquisition start.
    /// </summary>
    public enum AcquisitionStartDOTargets : byte
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
    public enum ThresholdOnAdc : byte
    {
        Channel0 = 0,
        Channel1 = 1,
        Channel2 = 2,
        Channel3 = 3,
        None = 8
    }
}
