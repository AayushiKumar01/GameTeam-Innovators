namespace Game.Models
{
    /// <summary>
    /// The Types of s a Action can have
    /// Used in Action Crudi, and in Battles.
    /// </summary>
    public enum ActionEnum
    {
        // Not specified
        Unknown = 0,

        // Attack
        Attack = 1,

        // Move
        Move = 10,

        // Ability
        Ability = 20,

        Rest = 25,
        
        // Random skipping
        SkipTurn = 30,
    }

    /// <summary>
    /// Friendly strings for the Enum Class
    /// </summary>
    public static class ActionEnumExtensions
    {
        /// <summary>
        /// Display a string for the Enums
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToMessage(this ActionEnum value)
        {
            // Default string
            var Message = "None";

            switch (value)
            {
                case ActionEnum.Attack:
                    Message = " Attacks ";
                    break;

                case ActionEnum.Move:
                    Message = " Moves ";
                    break;

                case ActionEnum.Ability:
                    Message = " Uses Ability ";
                    break;

                case ActionEnum.Rest:
                    Message = " Takes a rest ";
                    break;

                case ActionEnum.Unknown:
                default:
                    break;
            }

            return Message;
        }

        /// <summary>
        /// Display a string for the Enums
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToImageURI(this ActionEnum value)
        {
            // Default string
            var Message = "action_default.png";

            switch (value)
            {
                case ActionEnum.Attack:
                    Message = "action_attack.png";
                    break;

                case ActionEnum.Move:
                    Message = "action_move.png";
                    break;

                case ActionEnum.Ability:
                    Message = "action_ability.png";
                    break;

                case ActionEnum.Rest:
                    Message = "icon_add.png";
                    break;

                case ActionEnum.Unknown:
                default:
                    break;
            }

            return Message;
        }
    }
}