using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Models
{
    /// <summary>
    /// Special Abilities that Characters can have
    /// </summary>
    public enum SpecialAbility
    {
        // Invalid State
        Unknown = 0,

        // Increase Damage
        Damage = 1,

        // Increase Shield
        Shield = 2,

        // Healing over tie
        Healing = 3,

        // Teleport
        Teleportation = 4,

        // Increase Speed
        Speed = 5
    }
}
