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
        /// Asynchronously reads the contents of the DI0 register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DigitalState> ReadDI0Async()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DI0.Address));
            return DI0.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DI0 register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DigitalState>> ReadTimestampedDI0Async()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DI0.Address));
            return DI0.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the contents of the RangeAndFilterCutoff register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<RangeAndFilterConfig> ReadRangeAndFilterCutoffAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(RangeAndFilterCutoff.Address));
            return RangeAndFilterCutoff.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the RangeAndFilterCutoff register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<RangeAndFilterConfig>> ReadTimestampedRangeAndFilterCutoffAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(RangeAndFilterCutoff.Address));
            return RangeAndFilterCutoff.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the RangeAndFilterCutoff register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteRangeAndFilterCutoffAsync(RangeAndFilterConfig value)
        {
            var request = RangeAndFilterCutoff.FromPayload(MessageType.Write, value);
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
        public async Task<DI0ModeConfig> ReadDI0ModeAsync()
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
        public async Task<Timestamped<DI0ModeConfig>> ReadTimestampedDI0ModeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DI0Mode.Address));
            return DI0Mode.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DI0Mode register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDI0ModeAsync(DI0ModeConfig value)
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
        public async Task<DO0ModeConfig> ReadDO0ModeAsync()
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
        public async Task<Timestamped<DO0ModeConfig>> ReadTimestampedDO0ModeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DO0Mode.Address));
            return DO0Mode.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO0Mode register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO0ModeAsync(DO0ModeConfig value)
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
        /// Asynchronously reads the contents of the DOSet register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DigitalOutputs> ReadDOSetAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DOSet.Address));
            return DOSet.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DOSet register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DigitalOutputs>> ReadTimestampedDOSetAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DOSet.Address));
            return DOSet.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DOSet register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDOSetAsync(DigitalOutputs value)
        {
            var request = DOSet.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DOClear register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DigitalOutputs> ReadDOClearAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DOClear.Address));
            return DOClear.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DOClear register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DigitalOutputs>> ReadTimestampedDOClearAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DOClear.Address));
            return DOClear.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DOClear register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDOClearAsync(DigitalOutputs value)
        {
            var request = DOClear.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DOToggle register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DigitalOutputs> ReadDOToggleAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DOToggle.Address));
            return DOToggle.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DOToggle register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DigitalOutputs>> ReadTimestampedDOToggleAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DOToggle.Address));
            return DOToggle.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DOToggle register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDOToggleAsync(DigitalOutputs value)
        {
            var request = DOToggle.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DOState register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DigitalOutputs> ReadDOStateAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DOState.Address));
            return DOState.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DOState register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DigitalOutputs>> ReadTimestampedDOStateAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DOState.Address));
            return DOState.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DOState register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDOStateAsync(DigitalOutputs value)
        {
            var request = DOState.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the AcquisitionStartDO register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<AcquisitionStartDOTargets> ReadAcquisitionStartDOAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(AcquisitionStartDO.Address));
            return AcquisitionStartDO.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the AcquisitionStartDO register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<AcquisitionStartDOTargets>> ReadTimestampedAcquisitionStartDOAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(AcquisitionStartDO.Address));
            return AcquisitionStartDO.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the AcquisitionStartDO register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteAcquisitionStartDOAsync(AcquisitionStartDOTargets value)
        {
            var request = AcquisitionStartDO.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }
    }
}
