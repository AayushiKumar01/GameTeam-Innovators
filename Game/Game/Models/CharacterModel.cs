using System.Collections.Generic;
using Game.GameRules;

namespace Game.Models
{
    /// <summary>
    /// Characters
    /// 
    /// Derive from BasePlayerModel
    /// </summary>
    public class CharacterModel : BasePlayerModel<CharacterModel>
    {
        
        // Dictionary for tracking Special Abilities per job type
        static Dictionary < CharacterJobEnum, SpecialAbilityEnum > JobToSpecialAbility = new Dictionary < CharacterJobEnum, SpecialAbilityEnum > ();

        /// <summary>
        /// Static constructor that executes the first time this class is used
        /// </summary>
        static CharacterModel()
        {
            // Initialize job to special ability dictionary the first time we use this class
            JobToSpecialAbility.Add(CharacterJobEnum.Frigate, SpecialAbilityEnum.Healing);
            JobToSpecialAbility.Add(CharacterJobEnum.Destroyer, SpecialAbilityEnum.Damage);
            JobToSpecialAbility.Add(CharacterJobEnum.Battlecruiser, SpecialAbilityEnum.Shield);
            JobToSpecialAbility.Add(CharacterJobEnum.Corvette, SpecialAbilityEnum.Speed);
            JobToSpecialAbility.Add(CharacterJobEnum.Fighter, SpecialAbilityEnum.Damage);
        }
        
        /// <summary>
        /// Default character
        /// 
        /// Gets a type, guid, name and description
        /// </summary>
        public CharacterModel()
        {
            PlayerType = PlayerTypeEnum.Character;
            Guid = Id;
            Name = "Hyperion";
            Description = "Veteran of the Third Solar War";
            Level = 1;
            ImageURI = "ship2.gif";
            ExperienceTotal = 0;
            ExperienceRemaining = LevelTableHelper.LevelDetailsList[Level + 1].Experience - 1;
            SpecialAbility = SpecialAbilityEnum.Unknown;

            // Default to Fighter in our game
            Job = CharacterJobEnum.Fighter;
        }

        // Special Ability of the Character, default if unknown
        public SpecialAbilityEnum SpecialAbility { get; set; } = SpecialAbilityEnum.Unknown;

        /// <summary>
        /// Create a copy
        /// </summary>
        /// <param name="data"></param>
        public CharacterModel(CharacterModel data)
        {
            _ = Update(data);
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="newData"></param>
        /// <returns></returns>
        public override bool Update(CharacterModel newData)
        {
            if (newData == null)
            {
                return false;
            }

            PlayerType = newData.PlayerType;
            Guid = newData.Guid;
            Name = newData.Name;
            Description = newData.Description;
            Level = newData.Level;
            ImageURI = newData.ImageURI;

            // Difficulty = newData.Difficulty;

            Speed = newData.Speed;
            Defense = newData.Defense;
            Attack = newData.Attack;

            ExperienceTotal = newData.ExperienceTotal;
            ExperienceRemaining = newData.ExperienceRemaining;
            CurrentHealth = newData.CurrentHealth;
            MaxHealth = newData.MaxHealth;

            Head = newData.Head;
            Necklass = newData.Necklass;
            PrimaryHand = newData.PrimaryHand;
            OffHand = newData.OffHand;
            RightFinger = newData.RightFinger;
            LeftFinger = newData.LeftFinger;
            Feet = newData.Feet;
            UniqueItem = newData.UniqueItem;

            // Update the Job
            Job = newData.Job;

            return true;
        }

        /// <summary>
        /// Helper to combine the attributes into a single line, to make it easier to display the item as a string
        /// </summary>
        /// <returns></returns>
        public override string FormatOutput()
        {
            var myReturn = string.Empty;
            myReturn += Name;
            myReturn += " , " + Description;
            myReturn += " , a " + Job.ToMessage();
            myReturn += " , Level : " + Level.ToString();
            myReturn += " , Total Experience : " + ExperienceTotal;
            myReturn += " , Attack :" + GetAttackTotal;
            myReturn += " , Defense :" + GetDefenseTotal;
            myReturn += " , Speed :" + GetSpeedTotal;
            myReturn += " , Items : " + ItemSlotsFormatOutput();
            myReturn += " , Damage : " + GetDamageTotalString;

            return myReturn;
        }
    }
}