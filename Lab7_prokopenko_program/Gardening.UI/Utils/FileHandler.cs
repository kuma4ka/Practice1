using Gardening.Class;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Gardening.UI.Utils
{
    public class FileHandler
    {
        public static readonly string txtFilePath
            = "E:\\ХАІ\\2КУРС\\1 семестр\\ООП\\Lab_7\\Lab7_prokopenko_program\\TestFile.txt";

        public static readonly string jsonFilePath
            = "E:\\ХАІ\\2КУРС\\1 семестр\\ООП\\Lab_7\\Lab7_prokopenko_program\\TestFile.json";

        public static void SaveToFileTXT(List<Plant> plants)
        {
            List<string> lines = new();

            foreach (var plant in plants)
            {
                lines.Add(plant.ToString());
            }
            try
            {
                File.WriteAllLines(txtFilePath, lines);
                Console.WriteLine($"Check out the TXT file at: {Path.GetFullPath(txtFilePath)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void SaveToFileJson(List<Plant> plants)
        {
            try
            {
                string jsonstring = "";
                foreach (var plant in plants)
                {
                    jsonstring += JsonSerializer.Serialize(plant);
                    jsonstring += "\n";
                }
                File.WriteAllText(jsonFilePath, jsonstring);
                Console.WriteLine($"Check out the JSON file at: {Path.GetFullPath(jsonFilePath)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static List<Plant> ReadFromFileTXT(List<Plant> plants)
        {
            try
            {
                List<string> lines = new();

                lines = File.ReadAllLines(txtFilePath).ToList();
                if (lines.Count == 0)
                {
                    throw new Exception("There are no lines in file.");
                }
                Console.WriteLine("Contents of TXT Plant file:\n");

                foreach (var item in lines)
                {
                    Console.WriteLine(item);
                    bool result = Plant.TryParse(item, out Plant? plant);
                    if (result) plants.Add(plant);
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Reading TXT file error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return plants;
        }

        public static List<Plant> ReadFromFileJson(List<Plant> plants)
        {
            try
            {
                List<string> lines = new();
                lines = File.ReadAllLines(jsonFilePath).ToList();
                if (lines.Count == 0)
                {
                    throw new Exception("There are no lines in file.");
                }
                Console.WriteLine("\nContents of JSON Plant file:\n");

                foreach (var line in lines)
                {
                    Console.WriteLine(line);
                    Plant? plant = JsonSerializer.Deserialize<Plant>(line);
                    if (plant != null) plants.Add(plant);
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Reading JSON file error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return plants;
        }
    }
}