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
                    ImageURI = "item3.png",
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
                    ImageURI = "item6.png",
                    Range = 0,
                    Damage = 0,
                    Value = 9,
                    Location = ItemLocationEnum.Necklass,
                    Attribute = AttributeEnum.Speed},

                new ItemModel {
                    Name = "Infrared Radar",
                    Description = "Tracks heat signatures",
                    ImageURI = "item2.png",
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
                    Name = "Horned Hat",
                    Description = "Where's the Rabbit?",
                    ImageURI = "item2.png",
                    Range = 0,
                    Damage = 0,
                    Value = 9,
                    Location = ItemLocationEnum.Head,
                    Attribute = AttributeEnum.Defense},

                new ItemModel {
                    Name = "Ring of Power",
                    Description = "The wearer has all the power",
                    ImageURI = "item3.png",
                    Range = 0,
                    Damage = 0,
                    Value = 9,
                    Location = ItemLocationEnum.Finger,
                    Attribute = AttributeEnum.Speed},

                new ItemModel {
                    Name = "Strong Ring",
                    Description = "Watch out",
                    ImageURI = "item4.png",
                    Range = 0,
                    Damage = 0,
                    Value = 9,
                    Location = ItemLocationEnum.Finger,
                    Attribute = AttributeEnum.Attack},

                new ItemModel {
                    Name = "Warm Shoes",
                    Description = "Strong Shoes",
                    ImageURI = "item5.png",
                    Range = 0,
                    Damage = 0,
                    Value = 9,
                    Location = ItemLocationEnum.Feet,
                    Attribute = AttributeEnum.Attack},

                new ItemModel {
                    Name = "Johnson-Tanaka Engine",
                    Description = "Thrust up to 10G",
                    ImageURI = "item6.png",
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
                    Level = 1,
                    MaxHealth = 5,
                    ImageURI = "ship1.png",
                    Head = HeadString,
                    Necklass = NecklassString,
                    PrimaryHand = PrimaryHandString,
                    OffHand = OffHandString,
                    Feet = FeetString,
                    RightFinger = RightFingerString,
                    LeftFinger = LeftFingerString,
                },

                new CharacterModel {
                    Name = "Hyperion",
                    Description = "Veteran of the Third Solar War",
                    Level = 1,
                    MaxHealth = 5,
                    ImageURI = "ship2.png",
                    Head = HeadString,
                    Necklass = NecklassString,
                    PrimaryHand = PrimaryHandString,
                    OffHand = OffHandString,
                    Feet = FeetString,
                    RightFinger = RightFingerString,
                    LeftFinger = LeftFingerString,
                },

                new CharacterModel {
                    Name = "The Rocinante",
                    Description = "A Legitimate Salvage",
                    Level = 1,
                    MaxHealth = 8,
                    ImageURI = "ship3.png",
                    Head = HeadString,
                    Necklass = NecklassString,
                    PrimaryHand = PrimaryHandString,
                    OffHand = OffHandString,
                    Feet = FeetString,
                    RightFinger = RightFingerString,
                    LeftFinger = LeftFingerString,
                },

                new CharacterModel {
                    Name = "The Behemoth",
                    Description = "A salvager converted into a warship",
                    Level = 4,
                    MaxHealth = 38,
                    ImageURI = "ship4.png"
                },

                new CharacterModel {
                    Name = "Firefly",
                    Description = "Light and nimble",
                    Level = 5,
                    MaxHealth = 43,
                    ImageURI = "ship5.png"
                },

                new CharacterModel {
                    Name = "Conqueror",
                    Description = "All shall fall before me",
                    Level = 5,
                    MaxHealth = 43,
                    ImageURI = "ship6.png"
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
                    Name = "Purple Phage Frigate",
                    Description = "Spreads like a virus",
                    ImageURI = "monsterdrafts1.png",
                    Job = CharacterJobEnum.Frigate,
                },

                new MonsterModel {
                    Name = "Medusozoan Destroyer",
                    Description = "Watch out for the tentacles",
                    ImageURI = "monsterdrafts2.png",
                    Job = CharacterJobEnum.Destroyer,
                },

                new MonsterModel {
                    Name = "Protomolecule Corvette",
                    Description = "It assimilates any lifeform",
                    ImageURI = "monsterdrafts3.png",
                    Job = CharacterJobEnum.Corvette,
                },

                new MonsterModel {
                    Name = "Armillary Battlecruiser",
                    Description = "It sees incomprehensible truths",
                    ImageURI = "monsterdrafts4.png",
                    Job = CharacterJobEnum.Battlecruiser,
                },

                new MonsterModel {
                    Name = "Hydrozoan Corvette",
                    Description = "An organic spaceship",
                    ImageURI = "monsterdrafts5.png",
                    Job = CharacterJobEnum.Corvette,
                },

                new MonsterModel {
                    Name = "Jackal Fighter",
                    Description = "Able to shoot in any direction",
                    ImageURI = "monsterdrafts6.png",
                    Job = CharacterJobEnum.Fighter,
                },
            };

            return datalist;
        }
    }
}