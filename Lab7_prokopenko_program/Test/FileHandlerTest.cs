using Gardening.Class;
using Gardening.Logic.Utils;
using Gardening.UI.Utils;
using System.Collections.Generic;
using System.IO;

namespace Test
{
    [TestClass]
    public class FileHandlerTest
    {
        [TestMethod]
        public void SaveToFileTXTTest()
        {
            List<Plant> plants = new List<Plant>
            {
                PlantCreator.RandomPlant(),
                PlantCreator.RandomPlant(),
                PlantCreator.RandomPlant(),
                PlantCreator.RandomPlant(),
                PlantCreator.RandomPlant(),
                PlantCreator.RandomPlant(),
                PlantCreator.RandomPlant(),
            };

            FileHandler.SaveToFileTXT(plants);

            Assert.IsTrue(File.Exists(FileHandler.txtFilePath));
        }

        [TestMethod]
        public void SaveToFileJsonTest()
        {
            List<Plant> plants = new List<Plant>
            {
                PlantCreator.RandomPlant(),
                PlantCreator.RandomPlant(),
                PlantCreator.RandomPlant(),
                PlantCreator.RandomPlant(),
                PlantCreator.RandomPlant(),
                PlantCreator.RandomPlant(),
                PlantCreator.RandomPlant(),
            };

            FileHandler.SaveToFileJson(plants);

            Assert.IsTrue(File.Exists(FileHandler.jsonFilePath));
        }

        [TestMethod]
        public void ReadFromFileTXTTest()
        {
            List<Plant> plants = new List<Plant>();

            plants = FileHandler.ReadFromFileTXT(plants);

            Assert.IsTrue(plants.Count > 0);
        }

        [TestMethod]
        public void ReadFromFileJsonTest()
        {
            List<Plant> plants = new List<Plant>();

            plants = FileHandler.ReadFromFileJson(plants);

            Assert.IsTrue(plants.Count > 0);
        }
    }
}