using Gardening.Enum;
using Gardening.Logic.ValidatorForPlant;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gardening.Class
{
    public class Plant
    {
        private string? _species;

        private double _height;

        private int _age;

        public static double PriceFactor { private get; set; } = 0.66;

        public Plant()
        {
            UpdateAdultHeight();
            NumberOfPlants++;
        }

        public Plant(PlantType type) : this()
        {
            Type = type;
            UpdateAdultHeight();
        }

        public Plant(PlantType type, double height, int age) : this(type)
        {
            _species = "Unknown";
            _height = height;
            _age = age;
        }

        public static int NumberOfPlants { get; set; }

        public PlantType Type { get; set; }

        public string Species
        {
            get { return _species; }
            set
            {
                if (!PlantValidator.IsSpeciesValid(value))
                    throw new ArgumentException("Only Latin letters are allowed to be used. Min length is 4, max length is 15.");
                _species = value;
            }
        }

        public double Height
        {
            get { return _height; }
            set
            {
                if (!PlantValidator.IsHeightValid(value))
                    throw new ArgumentOutOfRangeException("Plant height must be between 0 and 25 meters.");
                _height = value;
            }
        }

        public int Age
        {
            get { return _age; }
            set
            {
                if (!PlantValidator.IsAgeValid(value))
                    throw new ArgumentOutOfRangeException("Plant age must be between 0 and 40 years.");
                _age = value;
            }
        }

        public double AdultHeight { get; private set; } = 0.25;

        public double TimeToGetMaxHeight
        {
            get
            {
                if (_height >= AdultHeight)
                {
                    return 0;
                }

                double growthRate = (AdultHeight - 0.25) / 15.0;
                double remainingHeight = AdultHeight - _height;
                double timeToMature = remainingHeight / growthRate;

                return Math.Round(timeToMature, 2);
            }
        }

        private void UpdateAdultHeight()
        {
            switch (Type)
            {
                case PlantType.Shrub:
                    AdultHeight = 2;
                    break;

                case PlantType.Flower:
                    AdultHeight = 1;
                    break;

                case PlantType.Tree:
                    AdultHeight = 25;
                    break;

                case PlantType.Grass:
                    AdultHeight = 0.5;
                    break;
            }
        }

        private bool IsRequiresLight()
        {
            switch (Type)
            {
                case PlantType.Moss:
                case PlantType.Shrub:
                    return false;

                case PlantType.Flower:
                case PlantType.Tree:
                case PlantType.Grass:
                    return true;

                default:
                    throw new InvalidOperationException("Unsupported plant type.");
            }
        }

        private double CalculatePrice()
        {
            return Math.Round((int)Type * PriceFactor * _height * _age, 2);
        }

        public void GetInfo()
        {
            Console.WriteLine(@$"-
Plant type: {Type}
Plant species: {_species}
Plant current height: {_height} m.
Plant adult height: {AdultHeight} m.
Plant age: {_age} y.o.
Plant price: {CalculatePrice()}
Requires light: {IsRequiresLight()}");
        }

        public static void PrintGardeningInfo()
        {
            Console.WriteLine("Welcome to the Gardening Domain!");
            Console.WriteLine("Our gardening domain specializes in various plants including moss, shrubs, flowers, trees, and grass.");
            Console.WriteLine("We offer a wide range of gardening services and expertise.");
            Console.WriteLine("Feel free to explore our garden and learn more about the fascinating world of plants!");
        }

        public static Plant Parse(string plantInfo)
        {
            if (string.IsNullOrWhiteSpace(plantInfo))
            {
                throw new ArgumentException("Input string cannot be null or empty.");
            }

            string[] plantInfoArray = plantInfo.Split(';');

            if (plantInfoArray.Length != 4)
            {
                throw new FormatException("Invalid input format. Expected format: Type;Species;Height;Age");
            }

            if (!System.Enum.TryParse<PlantType>(plantInfoArray[0], out PlantType type))
            {
                throw new FormatException("Invalid plant type.");
            }

            string species = plantInfoArray[1].Trim();
            if (!PlantValidator.IsSpeciesValid(species))
            {
                throw new ArgumentException("Invalid species. Only Latin letters are allowed. Min length is 4, max length is 15.");
            }

            if (!double.TryParse(plantInfoArray[2], out double height))
            {
                throw new ArgumentOutOfRangeException("Invalid plant height. Height must be between 0 and 25 meters.");
            }

            if (!int.TryParse(plantInfoArray[3], out int age))
            {
                throw new ArgumentOutOfRangeException("Invalid plant age. Age must be between 0 and 40 years.");
            }

            Plant parsedPlant = new Plant(type, height, age);
            parsedPlant.Species = species;

            return parsedPlant;
        }

        public static bool TryParse(string plantInfo, out Plant parsedPlant)
        {
            parsedPlant = null;

            try
            {
                parsedPlant = Parse(plantInfo);
                return true;
            }
            catch (FormatException ex)
            {
                return false;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return false;
            }
            catch (ArgumentException ex)
            {
                return false;
            }
        }

        public static void PrintTallestPlantInfo(List<Plant> plants)
        {
            if (!plants.Any())
            {
                Console.WriteLine("List is empty.");
                return;
            }

            Plant tallestPlant = plants[0];
            foreach (Plant plant in plants)
            {
                if (plant.Height > tallestPlant.Height)
                {
                    tallestPlant = plant;
                }
            }

            Console.WriteLine("Information about the tallest plant:");
            tallestPlant.GetInfo();
        }

        public override string ToString()
        {
            return $"{Type};{Species};{Height};{Age}";
        }
    }
}