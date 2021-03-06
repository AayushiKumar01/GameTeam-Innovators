using System;
using System.Collections.Generic;
using System.Linq;

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

        Cleric = 11,

        // Corvettes defend well and have buff abilities
        Corvette = 12,

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
        /// Display a string for the Enums
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToMessage(this CharacterJobEnum value)
        {
            // Default string
            var Message = "Player";

            switch (value)
            {
                case CharacterJobEnum.Fighter:
                    Message = "Fighter";
                    break;

                case CharacterJobEnum.Cleric:
                    Message = "Cleric";
                    break;

                case CharacterJobEnum.Corvette:
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

    public static class CharacterJobEnumHelper
    {
        // <summary>
        ///  Gets the list of Jobs (Spaceship types) for a character
        ///  Removes Unknown
        /// </summary>
        public static List<string> GetListMessageCharacterJob
        {
            get
            {
                var myList = new List<string>();
                foreach (CharacterJobEnum job in Enum.GetValues(typeof(CharacterJobEnum)))
                {
                    if (job != CharacterJobEnum.Unknown
                        && job != CharacterJobEnum.Cleric)
                        myList.Add(job.ToMessage());
                }

                return myList;
            }
        }
            /// <summary>
            /// Given the Message for an enum
            /// Look it up and return the enum
            /// 
            /// Right Finger => RightFinger
            /// 
            /// </summary>
            /// <param name="value"></param>
            /// <returns></returns>
        public static CharacterJobEnum ConvertMessageToEnum(string value)
        {
            // Get the Message, Determine Which enum has that message, and return that enum.
            foreach (CharacterJobEnum job in Enum.GetValues(typeof(CharacterJobEnum)))
            {
                if (job.ToMessage().Equals(value))
                {
                    return job;
                }
            }
            return CharacterJobEnum.Unknown;
        }
    

    }
}

