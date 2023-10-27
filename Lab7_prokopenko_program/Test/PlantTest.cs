using Gardening.Enum;
using Gardening.Class;
using Gardening.Logic.Utils;
using System;
using System.Collections.Generic;
using System.IO;

namespace Test
{
    [TestClass]
    public class PlantTest
    {
        [TestMethod]
        public void Constructor_DefaultConstructor_CreatesPlantWithDefaultValues()
        {
            Plant plant = new Plant() { Age = 5, Height = 0.25, Species = "Sphagnum" };
            Plant expectedPlant = PlantCreator.DefaultPlant();

            Assert.AreEqual(plant.Type, expectedPlant.Type);
            Assert.AreEqual(plant.Height, expectedPlant.Height);
            Assert.AreEqual(plant.Age, expectedPlant.Age);
            Assert.AreEqual(plant.Species, expectedPlant.Species);
        }

        [TestMethod]
        public void SetInvalidSpecies_ThrowsException()
        {
            Plant plant = new Plant();

            Assert.ThrowsException<ArgumentException>(() => plant.Species = "Inv@lid");
        }

        [TestMethod]
        public void TimeToGetMaxHeight_WithValidHeight_ReturnsCorrectTime()
        {
            Plant plant = new Plant(PlantType.Flower, 0.2, 1);

            double timeToMature = plant.TimeToGetMaxHeight;

            Assert.AreEqual(16, timeToMature, 0.01);
        }

        [TestMethod]
        public void PrintGardeningInfo_Always_PrintsInfoToConsole()
        {
            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            Plant.PrintGardeningInfo();

            string expectedOutput = "Welcome to the Gardening Domain!\r\n" +
                                    "Our gardening domain specializes in various plants including moss, shrubs, flowers, trees, and grass.\r\n" +
                                    "We offer a wide range of gardening services and expertise.\r\n" +
                                    "Feel free to explore our garden and learn more about the fascinating world of plants!\r\n";
            Assert.AreEqual(expectedOutput, consoleOutput.ToString());
        }

        [TestMethod]
        public void Parse_ValidInputString_ReturnsCorrectPlantObject()
        {
            string plantInfo = "Flower;Sample;10,5;5";

            Plant parsedPlant = Plant.Parse(plantInfo);

            Assert.AreEqual(PlantType.Flower, parsedPlant.Type);
            Assert.AreEqual("Sample", parsedPlant.Species);
            Assert.AreEqual(10.5, parsedPlant.Height);
            Assert.AreEqual(5, parsedPlant.Age);
        }

        [TestMethod]
        public void Parse_NullOrEmptyInputString_ThrowsArgumentException()
        {
            string plantInfo = null;

            Assert.ThrowsException<ArgumentException>(() => Plant.Parse(plantInfo));
        }

        [TestMethod]
        public void Parse_InvalidInputFormat_ThrowsFormatException()
        {
            string plantInfo = "InvalidFormat";

            Assert.ThrowsException<FormatException>(() => Plant.Parse(plantInfo));
        }

        [TestMethod]
        public void Parse_InvalidPlantType_ThrowsFormatException()
        {
            string plantInfo = "Moss,Sphagnum,10,20";

            Assert.ThrowsException<FormatException>(() => Plant.Parse(plantInfo));
        }

        [TestMethod]
        public void TryParse_ValidInput_ReturnsTrueAndCorrectPlantObject()
        {
            string plantInfo = "Flower;SampleSpecies;10,5;5";

            bool result = Plant.TryParse(plantInfo, out Plant parsedPlant);

            Assert.IsTrue(result);
            Assert.IsNotNull(parsedPlant);
            Assert.AreEqual(PlantType.Flower, parsedPlant.Type);
            Assert.AreEqual("SampleSpecies", parsedPlant.Species);
            Assert.AreEqual(10.5, parsedPlant.Height);
            Assert.AreEqual(5, parsedPlant.Age);
        }

        [TestMethod]
        public void TryParse_InvalidInput_ReturnsFalseAndNullPlantObject()
        {
            string invalidPlantInfo = "InvalidPlantInfo";

            bool result = Plant.TryParse(invalidPlantInfo, out Plant parsedPlant);

            Assert.IsFalse(result);
            Assert.IsNull(parsedPlant);
        }

        [TestMethod]
        public void ToString_ReturnsPlantObjectAsString()
        {
            Plant plant = PlantCreator.DefaultPlant();

            Assert.AreEqual("0;Sphagnum;0,25;5", plant.ToString());
        }

        [TestMethod]
        public void PrintTallestPlantInfo_WithValidPlants_PrintsCorrectInformation()
        {
            List<Plant> plants = new List<Plant>
            {
                new Plant(PlantType.Moss, 0.1, 2),
                new Plant(PlantType.Tree, 5.2, 8),
                new Plant(PlantType.Flower, 1.5, 5),
                new Plant(PlantType.Shrub, 0.8, 3)
            };

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                Plant.PrintTallestPlantInfo(plants);
                string expectedOutput = "Information about the tallest plant:\r\n" +
                                        "-\r\n" +
                                        "Plant type: Tree" +
                                        "\r\nPlant species: Unknown" +
                                        "\r\nPlant current height: 5,2 m." +
                                        "\r\nPlant adult height: 25 m." +
                                        "\r\nPlant age: 8 y.o." +
                                        "\r\nPlant price: 249,6" +
                                        "\r\nRequires light: True" +
                                        "\r\n";

                Assert.AreEqual(expectedOutput, sw.ToString());
            }
        }

        [TestMethod]
        public void PrintTallestPlantInfo_WithEmptyList_PrintsEmptyListMessage()
        {
            List<Plant> emptyList = new List<Plant>();

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                Plant.PrintTallestPlantInfo(emptyList);
                string expectedOutput = "List is empty.\r\n";

                Assert.AreEqual(expectedOutput, sw.ToString());
            }
        }
    }
}