using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Models
{
    /// <summary>
    /// The Types of s a Ability can have
    /// Used in Ability Crudi, and in Battles.
    /// </summary>
    public enum AbilityEnum
    {
        // Not specified
        Unknown = 0,

        // Not specified
        None = 1,

        // General Abilities 10 Range
        // Heal Self
        Bandage = 10,


        // Fighter Abilities > 20 Range
        // Buff Speed
        Nimble = 21,

        // Buff Defense
        Toughness = 22,

        // Buff Attack
        Focus = 23,


        // Cleric Abilities > 50 Range
        // Buff Speed
        Quick = 51,

        // Buff Defense
        Barrier = 52,

        // Buff Attack
        Curse = 53,

        // Heal Self
        Heal = 54,

        //Repair Self
        Repair = 55,
    }

    /// <summary>
    /// Friendly strings for the Enum Class
    /// </summary>
    public static class AbilityEnumExtensions
    {
        /// <summary>
        /// Display a string for the Enums
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToMessage(this AbilityEnum value)
        {
            // Default string
            var Message = "None";

            switch (value)
            {
                case AbilityEnum.Bandage:
                    Message = "Emergency Repair";
                    break;

                case AbilityEnum.Nimble:
                    Message = "Quick Thrusters";
                    break;

                case AbilityEnum.Toughness:
                    Message = "Increase Shields";
                    break;

                case AbilityEnum.Focus:
                    Message = "Power Weapons Systems";
                    break;

                case AbilityEnum.Quick:
                    Message = "Anticipate";
                    break;

                case AbilityEnum.Barrier:
                    Message = "Barrier Defense";
                    break;

                case AbilityEnum.Curse:
                    Message = "Charge Lasers";
                    break;

                case AbilityEnum.Repair:
                case AbilityEnum.Heal:
                    Message = "Repair Self";
                    break;

                case AbilityEnum.None:
                case AbilityEnum.Unknown:
                default:
                    break;
            }

            return Message;
        }
    }

    /// <summary>
    /// Helper for the Ability Enum Class
    /// </summary>
    public static class AbilityEnumHelper
    {
        /// <summary>
        /// Returns a list of strings of the enum for Ability
        /// Removes the Abilitys that are not changable by Items such as Unknown, MaxHealth
        /// </summary>
        public static List<string> GetFullList
        {
            get
            {
                var myList = Enum.GetNames(typeof(AbilityEnum)).ToList();
                return myList;
            }
        }

        /// <summary>
        /// Returns a list of strings of the enum for Fighter
        /// </summary>
        public static List<string> GetListFighter
        {
            get
            {
                List<string> AbilityList = new List<string>{
                AbilityEnum.Curse.ToString()
                };

                AbilityList.AddRange(GetListOthers);
                return AbilityList;
            }
        }

        /// <summary>
        /// Returns a list of strings of the enum for Frigate
        /// </summary>
        public static List<string> GetListCorvette
        {
            get
            {

                List<string> AbilityList = new List<string>{
                AbilityEnum.Nimble.ToString()
                };

                AbilityList.AddRange(GetListOthers);
                return AbilityList;
            }
        }

        /// <summary>
        /// Returns a list of strings of the enum for Cleric
        /// </summary>
        public static List<string> GetListCleric
        {
            get
            {

                List<string> AbilityList = new List<string>{
                AbilityEnum.Quick.ToString(),
                AbilityEnum.Barrier.ToString(),
                AbilityEnum.Curse.ToString(),
                AbilityEnum.Heal.ToString()
                };

                AbilityList.AddRange(GetListOthers);
                return AbilityList;
            }
        }

        /// <summary>
        /// Returns a list of strings of the enum for Frigate
        /// </summary>
        public static List<string> GetListFrigate
        {
            get
            {

                List<string> AbilityList = new List<string>{
                AbilityEnum.Repair.ToString()
                };

                AbilityList.AddRange(GetListOthers);
                return AbilityList;
            }
        }

        /// <summary>
        /// Returns a list of strings of the enum for Frigate
        /// </summary>
        public static List<string> GetListDestroyer
        {
            get
            {

                List<string> AbilityList = new List<string>{
                AbilityEnum.Focus.ToString()
                };

                AbilityList.AddRange(GetListOthers);
                return AbilityList;
            }
        }

        /// <summary>
        /// Returns a list of strings of the enum for Frigate
        /// </summary>
        public static List<string> GetListBattlecruiser
        {
            get
            {

                List<string> AbilityList = new List<string>{
                AbilityEnum.Barrier.ToString()
                };

                AbilityList.AddRange(GetListOthers);
                return AbilityList;
            }
        }

        /// <summary>
        /// Returns a list of strings of the enum of not Cleric or Fighter
        /// </summary>
        public static List<string> GetListOthers
        {
            get
            {

                List<string> AbilityList = new List<string>{
                AbilityEnum.Bandage.ToString(),
                };

                return AbilityList;
            }
        }

        /// <summary>
        /// Given the string for an enum, return its value.  That allows for the enums to be numbered 2,4,6 rather than 1,2,3
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static AbilityEnum ConvertStringToEnum(string value)
        {
            return (AbilityEnum)Enum.Parse(typeof(AbilityEnum), value);
        }
    }
}