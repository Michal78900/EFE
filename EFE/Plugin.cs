using Exiled.API.Features;
using System;

using ServerEvent = Exiled.Events.Handlers.Server;
using PlayerEvent = Exiled.Events.Handlers.Player;
using MapEvent = Exiled.Events.Handlers.Map;

namespace EFE
{
	public class EFE : Plugin<Config>
	{
		public static EFE Singleton;

		public override string Author { get; } = "Michal78900";
		public override string Name { get; } = "Everyone Fucking Explodes";
		public override string Prefix { get; } = "EFE";
		public override Version Version { get; } = new Version(1, 0, 0);
		public override Version RequiredExiledVersion { get; } = new Version(2, 8, 0);

		private Handler handler;

		public override void OnEnabled()
		{
			Singleton = this;
			handler = new Handler(this);

			ServerEvent.WaitingForPlayers += handler.OnWaitingForPlayers;

			PlayerEvent.Dying += handler.OnPlayerDying;

			MapEvent.ExplodingGrenade += handler.OnExploding;
		}

		public override void OnDisabled()
		{
			ServerEvent.WaitingForPlayers -= handler.OnWaitingForPlayers;

			PlayerEvent.Dying -= handler.OnPlayerDying;

			MapEvent.ExplodingGrenade -= handler.OnExploding;

			Singleton = null;
			handler = null;
		}
	}
}
