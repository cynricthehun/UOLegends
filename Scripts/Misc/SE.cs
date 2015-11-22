using System;
using Server.Network;

namespace Server
{
	public class SE
	{
		public const bool Enabled = false;

		public static void Configure()
		{
			Core.SE = Enabled;
		}
	}
}