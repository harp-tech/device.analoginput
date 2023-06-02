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
        /// Asynchronously reads the contents of the DI0State register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DigitalState> ReadDI0StateAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DI0State.Address));
            return DI0State.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DI0State register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DigitalState>> ReadTimestampedDI0StateAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DI0State.Address));
            return DI0State.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the contents of the RangeAndFilter register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<RangeAndFilterConfig> ReadRangeAndFilterAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(RangeAndFilter.Address));
            return RangeAndFilter.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the RangeAndFilter register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<RangeAndFilterConfig>> ReadTimestampedRangeAndFilterAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(RangeAndFilter.Address));
            return RangeAndFilter.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the RangeAndFilter register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteRangeAndFilterAsync(RangeAndFilterConfig value)
        {
            var request = RangeAndFilter.FromPayload(MessageType.Write, value);
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
        /// Asynchronously reads the contents of the DI0Trigger register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DI0TriggerConfig> ReadDI0TriggerAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DI0Trigger.Address));
            return DI0Trigger.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DI0Trigger register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DI0TriggerConfig>> ReadTimestampedDI0TriggerAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DI0Trigger.Address));
            return DI0Trigger.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DI0Trigger register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDI0TriggerAsync(DI0TriggerConfig value)
        {
            var request = DI0Trigger.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DO0Sync register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DO0SyncConfig> ReadDO0SyncAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DO0Sync.Address));
            return DO0Sync.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO0Sync register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DO0SyncConfig>> ReadTimestampedDO0SyncAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DO0Sync.Address));
            return DO0Sync.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO0Sync register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO0SyncAsync(DO0SyncConfig value)
        {
            var request = DO0Sync.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DO0PulseWidth register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<byte> ReadDO0PulseWidthAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DO0PulseWidth.Address));
            return DO0PulseWidth.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO0PulseWidth register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<byte>> ReadTimestampedDO0PulseWidthAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DO0PulseWidth.Address));
            return DO0PulseWidth.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO0PulseWidth register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO0PulseWidthAsync(byte value)
        {
            var request = DO0PulseWidth.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DigitalOutputSet register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DigitalOutputs> ReadDigitalOutputSetAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DigitalOutputSet.Address));
            return DigitalOutputSet.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DigitalOutputSet register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DigitalOutputs>> ReadTimestampedDigitalOutputSetAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DigitalOutputSet.Address));
            return DigitalOutputSet.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DigitalOutputSet register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDigitalOutputSetAsync(DigitalOutputs value)
        {
            var request = DigitalOutputSet.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DigitalOutputClear register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DigitalOutputs> ReadDigitalOutputClearAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DigitalOutputClear.Address));
            return DigitalOutputClear.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DigitalOutputClear register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DigitalOutputs>> ReadTimestampedDigitalOutputClearAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DigitalOutputClear.Address));
            return DigitalOutputClear.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DigitalOutputClear register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDigitalOutputClearAsync(DigitalOutputs value)
        {
            var request = DigitalOutputClear.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DigitalOutputToggle register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DigitalOutputs> ReadDigitalOutputToggleAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DigitalOutputToggle.Address));
            return DigitalOutputToggle.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DigitalOutputToggle register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DigitalOutputs>> ReadTimestampedDigitalOutputToggleAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DigitalOutputToggle.Address));
            return DigitalOutputToggle.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DigitalOutputToggle register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDigitalOutputToggleAsync(DigitalOutputs value)
        {
            var request = DigitalOutputToggle.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DigitalOutputState register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DigitalOutputs> ReadDigitalOutputStateAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DigitalOutputState.Address));
            return DigitalOutputState.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DigitalOutputState register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DigitalOutputs>> ReadTimestampedDigitalOutputStateAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DigitalOutputState.Address));
            return DigitalOutputState.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DigitalOutputState register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDigitalOutputStateAsync(DigitalOutputs value)
        {
            var request = DigitalOutputState.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the StartSyncOutput register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<StartSyncOutputTargets> ReadStartSyncOutputAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(StartSyncOutput.Address));
            return StartSyncOutput.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the StartSyncOutput register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<StartSyncOutputTargets>> ReadTimestampedStartSyncOutputAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(StartSyncOutput.Address));
            return StartSyncOutput.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the StartSyncOutput register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteStartSyncOutputAsync(StartSyncOutputTargets value)
        {
            var request = StartSyncOutput.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }
    }
}
