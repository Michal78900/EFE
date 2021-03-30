using Exiled.Events.EventArgs;
using Grenades;
using UnityEngine;
using Mirror;
using System.Collections.Generic;
using MEC;
using UnityEngine.Playables;

namespace EFE
{
    public class Handler
    {
        private readonly EFE plugin;
        public Handler(EFE plugin) => this.plugin = plugin;

        private List<GameObject> EfeGrenades = new List<GameObject>();

        internal void OnWaitingForPlayers()
        {
            EfeGrenades.Clear();
        }

        internal void OnPlayerDying(DyingEventArgs ev)
        {
            if (!plugin.Config.Roles.ContainsKey(ev.Target.Role))
                return;

            RoleType role = ev.Target.Role;
            if (role == RoleType.Spectator)
            {
                return;
            }

            for (uint i = 0; i < plugin.Config.Roles[role]; i++)
            {
                Timing.CallDelayed(0.1f * i, () =>
                {
                    Grenade grenade = Object.Instantiate(ev.Target.GrenadeManager.availableGrenades[0].grenadeInstance).GetComponent<Grenade>();
                    grenade.fuseDuration = plugin.Config.FuseTime;
                    grenade.InitData(ev.Target.ReferenceHub.GetComponent<GrenadeManager>(), Vector3.zero, Vector3.zero, 0);

                    EfeGrenades.Add(grenade.gameObject);
                    NetworkServer.Spawn(grenade.gameObject);
                });
            }
        }

        internal void OnExploding(ExplodingGrenadeEventArgs ev)
        {
            if (!plugin.Config.AllowDamage && EfeGrenades.Contains(ev.Grenade))
            {
                ev.TargetToDamages.Clear();
                EfeGrenades.Remove(ev.Grenade);
            }
        }
    }
}
