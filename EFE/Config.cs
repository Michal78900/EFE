using Exiled.API.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;

namespace EFE
{
    public class Config : IConfig
    {
        [Description("Is the plugin enabled.")]
        public bool IsEnabled { get; set; } = true;

        [Description("The fuse time (in seconds) of the spawned grenades.")]
        public float FuseTime { get; set; } = 1f;

        [Description("Should spawned grenades deal damage to players.")]
        public bool AllowDamage { get; set; } = true;

        [Description("Roles that will explode and their magnitude:")]
        public Dictionary<RoleType, uint> Roles { get; set; } = new Dictionary<RoleType, uint>
        {
            {RoleType.Scp173, 2 },
        };
    }
}
