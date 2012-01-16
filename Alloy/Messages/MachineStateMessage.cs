using Tempest;

namespace Alloy.Messages
{
	/// <summary>
	/// Message sent from the server to tell a client of it's current state.
	/// </summary>
	public class MachineStateMessage
		: AlloyMessage
	{
		public MachineStateMessage()
			: base (AlloyMessageType.MachineState)
		{
		}

		public bool IsActive
		{
			get;
			set;
		}

		public override void WritePayload (ISerializationContext context, IValueWriter writer)
		{
			writer.WriteBool (this.IsActive);
		}

		public override void ReadPayload (ISerializationContext context, IValueReader reader)
		{
			this.IsActive = reader.ReadBool();
		}
	}
}