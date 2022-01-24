namespace Game.Models
{
    /// <summary>
    /// The Types of Jobs a character can have
    /// Used in Character Crudi, and in Battles.
    /// </summary>
    public enum CharacterJobEnum
    {
        // Not specified
        Unknown = 0,

        // Fighters hit hard and have fight abilities
        Fighter = 10,

        // Corvettes defend well and have buff abilities
        Cleric = 12,

        Corvette = 13,

        // Frigates support well and have heal abilities
        Frigate = 14,

        // Destroyers attack well and can trade hp for increased damage
        Destroyer = 16,

        // Battlecruisers are highly mobile and can teleport anywhere
        Battlecruiser = 18,


    }

    /// <summary>
    /// Friendly strings for the Enum Class
    /// </summary>
    public static class CharacterJobEnumExtensions
    {
        /// <summary>
        /// Display a String for the Enums
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToMessage(this CharacterJobEnum value)
        {
            // Default String
            var Message = "Player";

            switch (value)
            {
                case CharacterJobEnum.Fighter:
                    Message = "Fighter";
                    break;

                case CharacterJobEnum.Cleric:
                    Message = "Corvette";
                    break;

                case CharacterJobEnum.Frigate:
                    Message = "Frigate";
                    break;

                case CharacterJobEnum.Destroyer:
                    Message = "Destroyer";
                    break;

                case CharacterJobEnum.Battlecruiser:
                    Message = "Battlecruiser";
                    break;

                case CharacterJobEnum.Unknown:
                default:
                    break;
            }

            return Message;
        }
    }
}