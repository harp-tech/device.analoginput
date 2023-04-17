using Bonsai.Harp;
using System.Threading.Tasks;

namespace Harp.AnalogInput
{
    /// <inheritdoc/>
    public partial class Device
    {
        /// <summary>
        /// Initializes a new instance of the asynchronous API to configure and interface
        /// with AnalogInput devices on the specified serial port.
        /// </summary>
        /// <param name="portName">
        /// The name of the serial port used to communicate with the Harp device.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous initialization operation. The value of
        /// the <see cref="Task{TResult}.Result"/> parameter contains a new instance of
        /// the <see cref="AsyncDevice"/> class.
        /// </returns>
        public static async Task<AsyncDevice> CreateAsync(string portName)
        {
            var device = new AsyncDevice(portName);
            var whoAmI = await device.ReadWhoAmIAsync();
            if (whoAmI != Device.WhoAmI)
            {
                var errorMessage = string.Format(
                    "The device ID {1} on {0} was unexpected. Check whether a AnalogInput device is connected to the specified serial port.",
                    portName, whoAmI);
                throw new HarpException(errorMessage);
            }

            return device;
        }
    }

    /// <summary>
    /// Represents an asynchronous API to configure and interface with AnalogInput devices.
    /// </summary>
    public partial class AsyncDevice : Bonsai.Harp.AsyncDevice
    {
        internal AsyncDevice(string portName)
            : base(portName)
        {
        }

        /// <summary>
        /// Asynchronously reads the contents of the StartAcquisition register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<EnableFlag> ReadStartAcquisitionAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(StartAcquisition.Address));
            return StartAcquisition.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the StartAcquisition register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<EnableFlag>> ReadTimestampedStartAcquisitionAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(StartAcquisition.Address));
            return StartAcquisition.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the StartAcquisition register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteStartAcquisitionAsync(EnableFlag value)
        {
            var request = StartAcquisition.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the AnalogData register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<AnalogDataPayload> ReadAnalogDataAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadInt16(AnalogData.Address));
            return AnalogData.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the AnalogData register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<AnalogDataPayload>> ReadTimestampedAnalogDataAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadInt16(AnalogData.Address));
            return AnalogData.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the contents of the InputEvent register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DigitalInputState> ReadInputEventAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(InputEvent.Address));
            return InputEvent.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the InputEvent register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DigitalInputState>> ReadTimestampedInputEventAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(InputEvent.Address));
            return InputEvent.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the contents of the RangeAndBandwidth register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<RangeAndBandwidthConfig> ReadRangeAndBandwidthAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(RangeAndBandwidth.Address));
            return RangeAndBandwidth.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the RangeAndBandwidth register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<RangeAndBandwidthConfig>> ReadTimestampedRangeAndBandwidthAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(RangeAndBandwidth.Address));
            return RangeAndBandwidth.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the RangeAndBandwidth register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteRangeAndBandwidthAsync(RangeAndBandwidthConfig value)
        {
            var request = RangeAndBandwidth.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the SamplingFrequency register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<SamplingFrequencyConfig> ReadSamplingFrequencyAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(SamplingFrequency.Address));
            return SamplingFrequency.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the SamplingFrequency register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<SamplingFrequencyConfig>> ReadTimestampedSamplingFrequencyAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(SamplingFrequency.Address));
            return SamplingFrequency.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the SamplingFrequency register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteSamplingFrequencyAsync(SamplingFrequencyConfig value)
        {
            var request = SamplingFrequency.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DI0Mode register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DI0Configuration> ReadDI0ModeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DI0Mode.Address));
            return DI0Mode.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DI0Mode register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DI0Configuration>> ReadTimestampedDI0ModeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DI0Mode.Address));
            return DI0Mode.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DI0Mode register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDI0ModeAsync(DI0Configuration value)
        {
            var request = DI0Mode.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DO0Mode register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DO0Configuration> ReadDO0ModeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DO0Mode.Address));
            return DO0Mode.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO0Mode register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DO0Configuration>> ReadTimestampedDO0ModeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DO0Mode.Address));
            return DO0Mode.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO0Mode register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO0ModeAsync(DO0Configuration value)
        {
            var request = DO0Mode.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DO0PulseDuration register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<byte> ReadDO0PulseDurationAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DO0PulseDuration.Address));
            return DO0PulseDuration.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO0PulseDuration register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<byte>> ReadTimestampedDO0PulseDurationAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DO0PulseDuration.Address));
            return DO0PulseDuration.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO0PulseDuration register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO0PulseDurationAsync(byte value)
        {
            var request = DO0PulseDuration.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the OutputSet register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DigitalOutputs> ReadOutputSetAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(OutputSet.Address));
            return OutputSet.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the OutputSet register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DigitalOutputs>> ReadTimestampedOutputSetAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(OutputSet.Address));
            return OutputSet.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the OutputSet register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteOutputSetAsync(DigitalOutputs value)
        {
            var request = OutputSet.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the OutputClear register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DigitalOutputs> ReadOutputClearAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(OutputClear.Address));
            return OutputClear.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the OutputClear register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DigitalOutputs>> ReadTimestampedOutputClearAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(OutputClear.Address));
            return OutputClear.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the OutputClear register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteOutputClearAsync(DigitalOutputs value)
        {
            var request = OutputClear.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the OutputToggle register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DigitalOutputs> ReadOutputToggleAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(OutputToggle.Address));
            return OutputToggle.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the OutputToggle register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DigitalOutputs>> ReadTimestampedOutputToggleAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(OutputToggle.Address));
            return OutputToggle.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the OutputToggle register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteOutputToggleAsync(DigitalOutputs value)
        {
            var request = OutputToggle.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the OutputState register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DigitalOutputs> ReadOutputStateAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(OutputState.Address));
            return OutputState.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the OutputState register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DigitalOutputs>> ReadTimestampedOutputStateAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(OutputState.Address));
            return OutputState.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the OutputState register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteOutputStateAsync(DigitalOutputs value)
        {
            var request = OutputState.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the AcquisitionStartOutput register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<AcquisitionStartTargets> ReadAcquisitionStartOutputAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(AcquisitionStartOutput.Address));
            return AcquisitionStartOutput.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the AcquisitionStartOutput register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<AcquisitionStartTargets>> ReadTimestampedAcquisitionStartOutputAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(AcquisitionStartOutput.Address));
            return AcquisitionStartOutput.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the AcquisitionStartOutput register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteAcquisitionStartOutputAsync(AcquisitionStartTargets value)
        {
            var request = AcquisitionStartOutput.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DO0TargetChannel register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ThresholdOnChannel> ReadDO0TargetChannelAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DO0TargetChannel.Address));
            return DO0TargetChannel.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO0TargetChannel register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ThresholdOnChannel>> ReadTimestampedDO0TargetChannelAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DO0TargetChannel.Address));
            return DO0TargetChannel.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO0TargetChannel register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO0TargetChannelAsync(ThresholdOnChannel value)
        {
            var request = DO0TargetChannel.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DO1TargetChannel register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ThresholdOnChannel> ReadDO1TargetChannelAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DO1TargetChannel.Address));
            return DO1TargetChannel.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO1TargetChannel register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ThresholdOnChannel>> ReadTimestampedDO1TargetChannelAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DO1TargetChannel.Address));
            return DO1TargetChannel.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO1TargetChannel register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO1TargetChannelAsync(ThresholdOnChannel value)
        {
            var request = DO1TargetChannel.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DO2TargetChannel register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ThresholdOnChannel> ReadDO2TargetChannelAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DO2TargetChannel.Address));
            return DO2TargetChannel.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO2TargetChannel register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ThresholdOnChannel>> ReadTimestampedDO2TargetChannelAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DO2TargetChannel.Address));
            return DO2TargetChannel.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO2TargetChannel register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO2TargetChannelAsync(ThresholdOnChannel value)
        {
            var request = DO2TargetChannel.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DO3TargetChannel register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ThresholdOnChannel> ReadDO3TargetChannelAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DO3TargetChannel.Address));
            return DO3TargetChannel.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO3TargetChannel register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ThresholdOnChannel>> ReadTimestampedDO3TargetChannelAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DO3TargetChannel.Address));
            return DO3TargetChannel.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO3TargetChannel register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO3TargetChannelAsync(ThresholdOnChannel value)
        {
            var request = DO3TargetChannel.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DO0Threshold register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<short> ReadDO0ThresholdAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadInt16(DO0Threshold.Address));
            return DO0Threshold.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO0Threshold register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<short>> ReadTimestampedDO0ThresholdAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadInt16(DO0Threshold.Address));
            return DO0Threshold.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO0Threshold register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO0ThresholdAsync(short value)
        {
            var request = DO0Threshold.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DO1Threshold register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<short> ReadDO1ThresholdAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadInt16(DO1Threshold.Address));
            return DO1Threshold.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO1Threshold register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<short>> ReadTimestampedDO1ThresholdAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadInt16(DO1Threshold.Address));
            return DO1Threshold.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO1Threshold register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO1ThresholdAsync(short value)
        {
            var request = DO1Threshold.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DO2Threshold register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<short> ReadDO2ThresholdAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadInt16(DO2Threshold.Address));
            return DO2Threshold.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO2Threshold register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<short>> ReadTimestampedDO2ThresholdAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadInt16(DO2Threshold.Address));
            return DO2Threshold.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO2Threshold register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO2ThresholdAsync(short value)
        {
            var request = DO2Threshold.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DO3Threshold register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<short> ReadDO3ThresholdAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadInt16(DO3Threshold.Address));
            return DO3Threshold.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO3Threshold register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<short>> ReadTimestampedDO3ThresholdAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadInt16(DO3Threshold.Address));
            return DO3Threshold.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO3Threshold register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO3ThresholdAsync(short value)
        {
            var request = DO3Threshold.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DO0BufferRisingEdge register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadDO0BufferRisingEdgeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO0BufferRisingEdge.Address));
            return DO0BufferRisingEdge.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO0BufferRisingEdge register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedDO0BufferRisingEdgeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO0BufferRisingEdge.Address));
            return DO0BufferRisingEdge.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO0BufferRisingEdge register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO0BufferRisingEdgeAsync(ushort value)
        {
            var request = DO0BufferRisingEdge.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DO1BufferRisingEdge register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadDO1BufferRisingEdgeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO1BufferRisingEdge.Address));
            return DO1BufferRisingEdge.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO1BufferRisingEdge register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedDO1BufferRisingEdgeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO1BufferRisingEdge.Address));
            return DO1BufferRisingEdge.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO1BufferRisingEdge register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO1BufferRisingEdgeAsync(ushort value)
        {
            var request = DO1BufferRisingEdge.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DO2BufferRisingEdge register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadDO2BufferRisingEdgeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO2BufferRisingEdge.Address));
            return DO2BufferRisingEdge.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO2BufferRisingEdge register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedDO2BufferRisingEdgeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO2BufferRisingEdge.Address));
            return DO2BufferRisingEdge.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO2BufferRisingEdge register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO2BufferRisingEdgeAsync(ushort value)
        {
            var request = DO2BufferRisingEdge.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DO3BufferRisingEdge register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadDO3BufferRisingEdgeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO3BufferRisingEdge.Address));
            return DO3BufferRisingEdge.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO3BufferRisingEdge register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedDO3BufferRisingEdgeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO3BufferRisingEdge.Address));
            return DO3BufferRisingEdge.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO3BufferRisingEdge register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO3BufferRisingEdgeAsync(ushort value)
        {
            var request = DO3BufferRisingEdge.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DO0BufferFallingEdge register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadDO0BufferFallingEdgeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO0BufferFallingEdge.Address));
            return DO0BufferFallingEdge.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO0BufferFallingEdge register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedDO0BufferFallingEdgeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO0BufferFallingEdge.Address));
            return DO0BufferFallingEdge.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO0BufferFallingEdge register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO0BufferFallingEdgeAsync(ushort value)
        {
            var request = DO0BufferFallingEdge.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DO1BufferFallingEdge register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadDO1BufferFallingEdgeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO1BufferFallingEdge.Address));
            return DO1BufferFallingEdge.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO1BufferFallingEdge register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedDO1BufferFallingEdgeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO1BufferFallingEdge.Address));
            return DO1BufferFallingEdge.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO1BufferFallingEdge register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO1BufferFallingEdgeAsync(ushort value)
        {
            var request = DO1BufferFallingEdge.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DO2BufferFallingEdge register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadDO2BufferFallingEdgeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO2BufferFallingEdge.Address));
            return DO2BufferFallingEdge.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO2BufferFallingEdge register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedDO2BufferFallingEdgeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO2BufferFallingEdge.Address));
            return DO2BufferFallingEdge.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO2BufferFallingEdge register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO2BufferFallingEdgeAsync(ushort value)
        {
            var request = DO2BufferFallingEdge.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DO3BufferFallingEdge register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadDO3BufferFallingEdgeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO3BufferFallingEdge.Address));
            return DO3BufferFallingEdge.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO3BufferFallingEdge register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedDO3BufferFallingEdgeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO3BufferFallingEdge.Address));
            return DO3BufferFallingEdge.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO3BufferFallingEdge register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO3BufferFallingEdgeAsync(ushort value)
        {
            var request = DO3BufferFallingEdge.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }
    }
}
