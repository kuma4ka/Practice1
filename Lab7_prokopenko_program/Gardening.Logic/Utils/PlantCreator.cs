using Gardening.Class;
using Gardening.Enum;

namespace Gardening.Logic.Utils
{
    public class PlantCreator
    {
        private static LineSeparator _lineSeparator = new();

        public static Plant DefaultPlant()
        {
            return new Plant() { Age = 5, Height = 0.25, Species = "Sphagnum" };
        }

        public static Plant ManualPlant()
        {
            Plant manualPlant;
            int input;
            do
            {
                Console.WriteLine(
                    @"Please, choose type of plant from this list:
1 - Moss,
2 - Flower,
3 - Shrub,
4 - Tree,
5 - Grass.
                ");

                if (!int.TryParse(Console.ReadLine(), out input))
                {
                    Console.WriteLine("Invalid input. Please enter only numbers.");
                    _lineSeparator.Separate('-');
                }
            } while (input < 1 || input > 5);
            _lineSeparator.Separate('-');

            switch (input)
            {
                case 1:
                    manualPlant = new Plant(PlantType.Moss);
                    break;

                case 2:
                    manualPlant = new Plant(PlantType.Flower);
                    break;

                case 3:
                    manualPlant = new Plant(PlantType.Shrub);
                    break;

                case 4:
                    manualPlant = new Plant(PlantType.Tree);
                    break;

                default:
                    manualPlant = new Plant(PlantType.Grass);
                    break;
            }
            PlantDetails(manualPlant);
            if (manualPlant.Age == -1 || manualPlant.Height == -1) manualPlant = null;
            return manualPlant;
        }

        public static Plant RandomPlant()
        {
            Random random = new Random();
            PlantType type;
            switch (random.Next(1, 5))
            {
                case 1:
                    type = PlantType.Moss;
                    Plant.PriceFactor = 0.95;
                    break;

                case 2:
                    type = PlantType.Flower;
                    Plant.PriceFactor = 1.5;
                    break;

                case 3:
                    type = PlantType.Shrub;
                    Plant.PriceFactor = 3.5;
                    break;

                case 4:
                    type = PlantType.Tree;
                    Plant.PriceFactor = 8;
                    break;

                default:
                    type = PlantType.Grass;
                    Plant.PriceFactor = 0.75;
                    break;
            }

            double height = Math.Round(random.NextDouble() * 25.0, 2);
            int age = random.Next(1, 41);

            Plant randomPlant = new(type, height, age);
            return randomPlant;
        }

        public static Plant ParsingString(string plantInfoString, out Plant plant)
        {
            plant = null;

            if (Plant.TryParse(plantInfoString, out plant))
            {
                return plant;
            }

            return plant;
        }

        private static void PlantDetails(Plant plant)
        {
            try
            {
                Console.Write("Plant species: ");
                plant.Species = Convert.ToString(Console.ReadLine());

                Console.Write("Plant height: ");
                if (!double.TryParse(Console.ReadLine(), out double height))
                {
                    plant.Height = -1;
                    throw new ArgumentException("Only numbers can be used.");
                }
                else plant.Height = height;

                Console.Write("Plant age: ");
                if (!int.TryParse(Console.ReadLine(), out int age))
                {
                    plant.Age = -1;
                    throw new ArgumentException("Only numbers can be used.");
                }
                else plant.Age = age;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
        }
    }
}