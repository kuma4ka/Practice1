using Gardening.Class;
using Gardening.Logic.Utils;
using Gardening.UI.Utils;

namespace Gardening.Interface
{
    public class Menu
    {
        private List<Plant> plants = new();
        private LineSeparator lineSeparator = new();

        private void AddPlant()
        {
            int input;
            do
            {
                Console.WriteLine(
                @"Please, choose method of creating plant:
1 - Default,
2 - Manual,
3 - Random,
4 - By parsing string.
                ");

                if (!int.TryParse(Console.ReadLine(), out input))
                {
                    Console.WriteLine("Invalid input. Please enter only numbers.");
                    lineSeparator.Separate('-');
                }
            } while (input < 1 || input > 4);
            lineSeparator.Separate('-');

            switch (input)
            {
                case 2:
                    try
                    {
                        Plant plant = AddPlantManually();
                        if (plant != null)
                        {
                            plants.Add(plant);
                            Console.WriteLine("\nPlant added successfully.");
                        }
                        else throw new ArgumentNullException("Plant info didn't pass validation.");
                    }
                    catch (ArgumentNullException ex)
                    {
                        Console.WriteLine($"{ex.Message}");
                        return;
                    }
                    break;

                case 3:
                    plants.Add(AddRandomPlant());
                    Console.WriteLine("\nPlant added successfully.");
                    break;

                case 4:
                    Console.WriteLine("Please, enter a string using the template: Moss;Sphagnum;0,25;10");
                    string? stringToParse = Console.ReadLine();
                    try
                    {
                        Plant plant = AddPlantFromString(stringToParse);
                        if (plant != null)
                        {
                            plants.Add(plant);
                            Console.WriteLine("Added plant:");
                            plant.GetInfo();
                            Console.WriteLine("\nPlant added successfully.");
                        }
                        else throw new ArgumentNullException("String hasn't been parsed.");
                    }
                    catch (ArgumentNullException ex)
                    {
                        Console.WriteLine($"{ex.Message}");
                        return;
                    }
                    break;

                default:
                    plants.Add(AddPlantDefault());
                    Console.WriteLine("\nPlant added successfully.");
                    break;
            }

            lineSeparator.Separate('-');
        }

        public Plant AddPlantDefault()
        {
            Plant plant = PlantCreator.DefaultPlant();
            return plant;
        }

        public Plant AddPlantManually()
        {
            Plant plant = PlantCreator.ManualPlant();
            return plant;
        }

        public Plant AddRandomPlant()
        {
            Plant plant = PlantCreator.RandomPlant();
            return plant;
        }

        public Plant AddPlantFromString(string input)
        {
            Plant plant = PlantCreator.ParsingString(input, out _);
            return plant;
        }

        private void OutputOptions()
        {
            Console.WriteLine(
                @"Choose an option:
1 - Output info in console as block
2 - Output in console as a string
0 - Exit"
            );

            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("Invalid input. Please enter a valid option.");
            }
            lineSeparator.Separate('*');

            Console.WriteLine($"Total amount of plants: {Plant.NumberOfPlants}");

            switch (choice)
            {
                case 1:
                    OutputPlantsInConsole();
                    break;

                case 2:
                    OutputPlantsAsString();
                    break;

                case 0:
                    Console.WriteLine("Exiting the program.");
                    return;

                default:
                    Console.WriteLine("Invalid option. Please enter a valid option.");
                    break;
            }
        }

        private void OutputPlantsInConsole()
        {
            if (!plants.Any())
            {
                Console.WriteLine("List is empty.");
                return;
            }

            foreach (Plant plant in plants)
            {
                try
                {
                    plant.GetInfo();
                    Console.WriteLine($"Estimated time for the plant to become an adult: {plant.TimeToGetMaxHeight} months.");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"{ex.Message}");
                    return;
                }
            }

            lineSeparator.Separate('-');
        }

        private void OutputPlantsAsString()
        {
            if (!plants.Any())
            {
                Console.WriteLine("List is empty.");
                return;
            }

            foreach (Plant plant in plants)
            {
                try
                {
                    Console.WriteLine(plant.ToString());
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"{ex.Message}");
                    return;
                }
            }

            lineSeparator.Separate('-');
        }

        private void SearchPlant()
        {
            if (!plants.Any())
            {
                Console.WriteLine("List is empty.");
                return;
            }

            Console.WriteLine(@"Search by:
    1 - Age
    2 - Height
    3 - Species
    4 - Adult height");

            if (!int.TryParse(Console.ReadLine(), out int searchOption)
                || searchOption < 1
                || searchOption > 4)
            {
                Console.WriteLine("Invalid option.");
                return;
            }

            Console.Write("Enter the value to search for: ");
            string? searchValue = Console.ReadLine();

            List<Plant> searchResults = new List<Plant>();

            switch (searchOption)
            {
                case 1:
                    searchResults = plants.Where(plant => plant.Age.ToString() == searchValue).ToList();
                    break;

                case 2:
                    searchResults = plants.Where(plant => plant.Height.ToString() == searchValue).ToList();
                    break;

                case 3:
                    searchResults = plants.Where(plant => plant.Species.Equals(searchValue, StringComparison.OrdinalIgnoreCase)).ToList();
                    break;

                case 4:
                    searchResults = plants.Where(plant => plant.AdultHeight.ToString() == searchValue).ToList();
                    break;
            }

            if (searchResults.Count == 0)
            {
                Console.WriteLine("No matching plants found.");
            }
            else
            {
                lineSeparator.Separate('-');
                Console.WriteLine("Matching plants:");
                foreach (Plant plant in searchResults)
                {
                    try
                    {
                        plant.GetInfo();
                        Console.WriteLine($"Estimated time for the plant to become an adult: {plant.TimeToGetMaxHeight} months.");
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"{ex.Message}");
                        return;
                    }
                }
                lineSeparator.Separate('-');
            }
        }

        private void DeletePlant()
        {
            if (!plants.Any())
            {
                Console.WriteLine("List is empty.");
                return;
            }

            Console.WriteLine(@"Choose the field for deleting objects:
    1 - Age
    2 - Height
    3 - Species
    4 - Adult height");

            if (!int.TryParse(Console.ReadLine(), out int deleteOption)
                || deleteOption < 1
                || deleteOption > 4)
            {
                Console.WriteLine("Invalid option.");
                return;
            }

            Console.Write("Enter the value to delete objects by: ");
            string? deleteValue = Console.ReadLine();

            plants.RemoveAll(plant =>
            {
                switch (deleteOption)
                {
                    case 1:
                        return plant.Age.ToString() == deleteValue;

                    case 2:
                        return plant.Height.ToString() == deleteValue;

                    case 3:
                        return plant.Species.Equals(deleteValue, StringComparison.OrdinalIgnoreCase);

                    case 4:
                        return plant.AdultHeight.ToString() == deleteValue;

                    default:
                        return false;
                }
            });
            Console.WriteLine("Plants were removed.");
        }

        private void ParsingShowCase()
        {
            List<string> plantsAsString = new();

            foreach (Plant plant in plants)
            {
                try
                {
                    plantsAsString.Add(plant.ToString());
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"{ex.Message}");
                }
            }

            foreach (string plantAsString in plantsAsString)
            {
                Console.WriteLine($"Plant as string: {plantAsString}");
                Plant parsedPlant = Plant.Parse(plantAsString);
                parsedPlant.GetInfo();
            }

            lineSeparator.Separate('-');
        }

        private void DemonstrateBehavior()
        {
            AddPlant();
            OutputOptions();
            SearchPlant();
            DeletePlant();
        }

        private void DemonstrateStaticMethods()
        {
            Plant.PrintGardeningInfo();
            ParsingShowCase();
            Plant.PrintTallestPlantInfo(plants);
        }

        private void SavePlantsToFile()
        {
            Console.WriteLine(@"Save to:
1 - .txt file
2 - .json file
    ");

            if (!int.TryParse(Console.ReadLine(), out int searchOption)
                || searchOption < 1
                || searchOption > 2)
            {
                Console.WriteLine("Invalid option.");
                return;
            }

            switch (searchOption)
            {
                case 1:
                    FileHandler.SaveToFileTXT(plants);
                    break;

                case 2:
                    FileHandler.SaveToFileJson(plants);
                    break;
            }
        }

        private void ReadPlantsFromFile()
        {
            Console.WriteLine(@"Read from:
1 - .txt file
2 - .json file
    ");

            if (!int.TryParse(Console.ReadLine(), out int searchOption)
                || searchOption < 1
                || searchOption > 2)
            {
                Console.WriteLine("Invalid option.");
                return;
            }

            switch (searchOption)
            {
                case 1:
                    FileHandler.ReadFromFileTXT(plants);
                    break;

                case 2:
                    FileHandler.ReadFromFileJson(plants);
                    break;
            }
        }

        public void MenuOptions()
        {
            while (true)
            {
                Console.WriteLine(
    @"***********************
Choose an option:
1 - Add an object
2 - Display objects
3 - Search for an object
4 - Delete an object
5 - Demonstrate behavior
6 - Demonstrate static methods
7 - Save plants list in file
8 - Read plants list from file
9 - Clear plants list
0 - Exit"
        );

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid input. Please enter a valid option.");
                    continue;
                }
                lineSeparator.Separate('*');

                switch (choice)
                {
                    case 1:
                        AddPlant();
                        break;

                    case 2:
                        OutputOptions();
                        break;

                    case 3:
                        SearchPlant();
                        break;

                    case 4:
                        DeletePlant();
                        break;

                    case 5:
                        DemonstrateBehavior();
                        break;

                    case 6:
                        DemonstrateStaticMethods();
                        break;

                    case 7:
                        SavePlantsToFile();
                        break;

                    case 8:
                        ReadPlantsFromFile();
                        break;

                    case 9:
                        Plant.NumberOfPlants = 0;
                        plants.Clear();
                        Console.WriteLine("Plants list was cleared.");
                        break;

                    case 0:
                        Console.WriteLine("Exiting the program.");
                        return;

                    default:
                        Console.WriteLine("Invalid option. Please enter a valid option.");
                        break;
                }
            }
        }
    }
}