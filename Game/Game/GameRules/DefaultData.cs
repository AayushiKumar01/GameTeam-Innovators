using System.Collections.Generic;

using Game.Models;
using Game.ViewModels;

namespace Game.GameRules
{
    public static class DefaultData
    {
        /// <summary>
        /// Load the Default data
        /// </summary>
        /// <returns></returns>
        public static List<ItemModel> LoadData(ItemModel temp)
        {
            var datalist = new List<ItemModel>()
            {
                new ItemModel {
                    Name = "Ion Cannon",
                    Description = "Vaporize Your Enemies",
                    ImageURI = "item3.png",
                    Range = 0,
                    Damage = 10,
                    Value = 9,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Attack},

                new ItemModel {
                    Name = "Mordhau Mk II Missile",
                    Description = "Hull cracker",
                    ImageURI = "item5.png",
                    Range = 0,
                    Damage = 8,
                    Value = 9,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Attack},

                new ItemModel {
                    Name = "Viking Warhead",
                    Description = "Devastating explosions",
                    ImageURI = "item3.png",
                    Range = 0,
                    Damage = 6,
                    Value = 9,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Attack},

                new ItemModel {
                    Name = "Blue Plasma Cannon",
                    Description = "Molten fire",
                    ImageURI = "reactor1.png",
                    Range = 10,
                    Damage = 10,
                    Value = 9,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Attack},

                new ItemModel {
                    Name = "PDC",
                    Description = "Automatic tracking gatling guns",
                    ImageURI = "item4.png",
                    Range = 0,
                    Damage = 6,
                    Value = 9,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Defense},

                new ItemModel {
                    Name = "Red Laser Turret",
                    Description = "Standard laser weapon",
                    ImageURI = "item5.png",
                    Range = 10,
                    Damage = 10,
                    Value = 9,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Attack},

                new ItemModel {
                    Name = "Ion Shield",
                    Description = "Enough to hide behind",
                    ImageURI = "item2.png",
                    Range = 0,
                    Damage = 0,
                    Value = 9,
                    Location = ItemLocationEnum.OffHand,
                    Attribute = AttributeEnum.Defense},

                new ItemModel {
                    Name = "Blue Plasma Cannon",
                    Description = "Molten fire",
                    ImageURI = "item3.png",
                    Range = 10,
                    Damage = 10,
                    Value = 9,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Attack},

                new ItemModel {
                    Name = "Point Defense Shield",
                    Description = "Shoots down missiles",
                    ImageURI = "item2.png",
                    Range = 0,
                    Damage = 0,
                    Value = 9,
                    Location = ItemLocationEnum.OffHand,
                    Attribute = AttributeEnum.Defense},

                new ItemModel {
                    Name = "Red Laser Turret",
                    Description = "Standard laser weapon",
                    ImageURI = "item5.png",
                    Range = 10,
                    Damage = 10,
                    Value = 9,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Attack},

                new ItemModel {
                    Name = "Blast Shield",
                    Description = "Solid iron",
                    ImageURI = "item2.png",
                    Range = 0,
                    Damage = 0,
                    Value = 9,
                    Location = ItemLocationEnum.OffHand,
                    Attribute = AttributeEnum.MaxHealth},

                new ItemModel {
                    Name = "Plasma Shield",
                    Description = "Incinerate incoming projectiles",
                    ImageURI = "item2.png",
                    Range = 0,
                    Damage = 0,
                    Value = 9,
                    Location = ItemLocationEnum.OffHand,
                    Attribute = AttributeEnum.Attack},

                new ItemModel {
                    Name = "Captain Kirk",
                    Description = "Venturing where no man has gone before",
                    ImageURI = "item1.png",
                    Range = 0,
                    Damage = 0,
                    Value = 9,
                    Location = ItemLocationEnum.Head,
                    Attribute = AttributeEnum.Speed},

                new ItemModel {
                    Name = "Captain Holden",
                    Description = "Better lucky than good",
                    ImageURI = "item1.png",
                    Range = 0,
                    Damage = 0,
                    Value = 9,
                    Location = ItemLocationEnum.Head,
                    Attribute = AttributeEnum.Defense},

                new ItemModel {
                    Name = "Sonar Radar",
                    Description = "Sound travels through space?",
                    ImageURI = "radar1.png",
                    Range = 0,
                    Damage = 0,
                    Value = 9,
                    Location = ItemLocationEnum.Necklass,
                    Attribute = AttributeEnum.Speed},

                new ItemModel {
                    Name = "Infrared Radar",
                    Description = "Tracks heat signatures",
                    ImageURI = "radar1.png",
                    Range = 0,
                    Damage = 0,
                    Value = 9,
                    Location = ItemLocationEnum.Necklass,
                    Attribute = AttributeEnum.Attack},

                new ItemModel {
                    Name = "Captain Nemo",
                    Description = "1000 Leagues under the nebula",
                    ImageURI = "item1.png",
                    Range = 0,
                    Damage = 0,
                    Value = 9,
                    Location = ItemLocationEnum.Head,
                    Attribute = AttributeEnum.Defense},

                new ItemModel {
                    Name = "Rocket Flares",
                    Description = "Distract Missiles",
                    ImageURI = "reactor2.png",
                    Range = 0,
                    Damage = 0,
                    Value = 9,
                    Location = ItemLocationEnum.Finger,
                    Attribute = AttributeEnum.Defense},

                new ItemModel {
                    Name = "Auxillary Thruster",
                    Description = "Extra mobility",
                    ImageURI = "thruster1.png",
                    Range = 0,
                    Damage = 0,
                    Value = 9,
                    Location = ItemLocationEnum.Finger,
                    Attribute = AttributeEnum.Speed},

                new ItemModel {
                    Name = "Nuclear Reactor Engine",
                    Description = "Don't let it get damaged",
                    ImageURI = "engine2.png",
                    Range = 0,
                    Damage = 0,
                    Value = 9,
                    Location = ItemLocationEnum.Feet,
                    Attribute = AttributeEnum.Attack},

                new ItemModel {
                    Name = "Johnson-Tanaka Engine",
                    Description = "Thrust up to 10G",
                    ImageURI = "engine1.png",
                    Range = 0,
                    Damage = 0,
                    Value = 9,
                    Location = ItemLocationEnum.Feet,
                    Attribute = AttributeEnum.Speed},
            };

            return datalist;
        }

        /// <summary>
        /// Load Example Scores
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        public static List<ScoreModel> LoadData(ScoreModel temp)
        {
            var datalist = new List<ScoreModel>()
            {
                new ScoreModel {
                    Name = "First Score",
                    Description = "Test Data",
                },

                new ScoreModel {
                    Name = "Second Score",
                    Description = "Test Data",
                }
            };

            return datalist;
        }

        /// <summary>
        /// Load Characters
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        public static List<CharacterModel> LoadData(CharacterModel temp)
        {
            var HeadString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.Head);
            var NecklassString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.Necklass);
            var PrimaryHandString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.PrimaryHand);
            var OffHandString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.OffHand);
            var FeetString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.Feet);
            var RightFingerString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.Finger);
            var LeftFingerString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.Finger);


            var datalist = new List<CharacterModel>()
            {
                new CharacterModel {
                    Name = "Pillar of Autumn",
                    Description = "UNSC Flagship",
                    Level = 3,
                    MaxHealth = 18,
                    Speed = 2,
                    ImageURI = "char1.gif",
                    Head = HeadString,
                    Necklass = NecklassString,
                    PrimaryHand = PrimaryHandString,
                    OffHand = OffHandString,
                    Feet = FeetString,
                    RightFinger = RightFingerString,
                    LeftFinger = LeftFingerString,
                    Job = CharacterJobEnum.Fighter,
                },

                new CharacterModel {
                    Name = "Hyperion",
                    Description = "Veteran of the Third Solar War",
                    Level = 3,
                    MaxHealth = 5,
                    Speed = 1,
                    ImageURI = "char2.gif",
                    Head = HeadString,
                    Necklass = NecklassString,
                    PrimaryHand = PrimaryHandString,
                    OffHand = OffHandString,
                    Feet = FeetString,
                    RightFinger = RightFingerString,
                    LeftFinger = LeftFingerString,
                    Job = CharacterJobEnum.Destroyer,
                },

                new CharacterModel {
                    Name = "The Rocinante",
                    Description = "A Legitimate Salvage",
                    Level = 3,
                    MaxHealth = 18,
                    Speed = 3,
                    ImageURI = "char3.gif",
                    Head = HeadString,
                    Necklass = NecklassString,
                    PrimaryHand = PrimaryHandString,
                    OffHand = OffHandString,
                    Feet = FeetString,
                    RightFinger = RightFingerString,
                    LeftFinger = LeftFingerString,
                    Job = CharacterJobEnum.Frigate,
                },

                new CharacterModel {
                    Name = "The Behemoth",
                    Description = "A salvager converted into a warship",
                    Level = 4,
                    MaxHealth = 42,
                    Speed = 2,
                    ImageURI = "char4.gif",
                    Job = CharacterJobEnum.Corvette,
                },

                new CharacterModel {
                    Name = "Firefly",
                    Description = "Light and nimble",
                    Level = 2,
                    MaxHealth = 12,
                    Speed = 4,
                    ImageURI = "char5.gif",
                    Job = CharacterJobEnum.Fighter,
                },

                new CharacterModel {
                    Name = "Conqueror",
                    Description = "All shall fall before me",
                    Level = 5,
                    MaxHealth = 32,
                    Speed = 3,
                    ImageURI = "char6.gif",
                    Job = CharacterJobEnum.Battlecruiser,
                },

                new CharacterModel
                {
                    Name = "The Pioneer",
                    Description = "A classic spaceship from the Early Era",
                    Level = 1,
                    MaxHealth = 15,
                    ImageURI = "ship8.png",
                    Job = CharacterJobEnum.Corvette,
                }
            };
            return datalist;
        }

        /// <summary>
        /// Load Characters
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        public static List<MonsterModel> LoadData(MonsterModel temp)
        {
            var datalist = new List<MonsterModel>()
            {
                new MonsterModel {
                    Name = "Purple Phage",
                    Description = "Spreads like a virus",
                    ImageURI = "monster1.gif",
                    Job = CharacterJobEnum.Frigate,
                },

                new MonsterModel {
                    Name = "Medusozoan",
                    Description = "Watch out for the tentacles",
                    ImageURI = "monster2.gif",
                    Job = CharacterJobEnum.Destroyer,
                },

                new MonsterModel {
                    Name = "Protomolecule",
                    Description = "It assimilates any lifeform",
                    ImageURI = "monster3.gif",
                    Job = CharacterJobEnum.Corvette,
                },

                new MonsterModel {
                    Name = "Armillary",
                    Description = "It sees incomprehensible truths",
                    ImageURI = "monster4.gif",
                    Job = CharacterJobEnum.Battlecruiser,
                },

                new MonsterModel {
                    Name = "Hydrozoan",
                    Description = "An organic spaceship",
                    ImageURI = "monster5.gif",
                    Job = CharacterJobEnum.Corvette,
                },

                new MonsterModel {
                    Name = "Jackal",
                    Description = "Able to shoot in any direction",
                    ImageURI = "monster6.gif",
                    Job = CharacterJobEnum.Fighter,
                },

                new MonsterModel {
                    Name = "Predator",
                    Description = "Prepare to be abducted",
                    ImageURI = "monsterdrafts7.png",
                    Job = CharacterJobEnum.Fighter,
                },

                new MonsterModel {
                    Name = "Baria",
                    Description = "Terrible to behold",
                    ImageURI = "monsterdrafts8.png",
                    Job = CharacterJobEnum.Corvette,
                }
            };

            return datalist;
        }
    }
}