using Gardening.Class;
using Gardening.Interface;

namespace Test
{
    [TestClass]
    public class MenuTest
    {
        [TestMethod]
        public void AddPlantDefault_ReturnsNonNullPlant()
        {
            Menu menu = new Menu();

            Plant plant = menu.AddPlantDefault();

            Assert.IsNotNull(plant);
        }

        [TestMethod]
        public void AddRandomPlant_ReturnsNonNullPlant()
        {
            Menu menu = new Menu();

            Plant plant = menu.AddRandomPlant();

            Assert.IsNotNull(plant);
        }

        [TestMethod]
        public void AddPlantFromString_WithValidInput_ShouldReturnNonNullPlant()
        {
            Menu menu = new Menu();
            string input = "Moss;Sphagnum;0,125;10";

            Plant plant = menu.AddPlantFromString(input);

            Assert.IsNotNull(plant);
        }

        [TestMethod]
        public void AddPlantFromString_WithInvalidInput_ShouldReturnNullPlant()
        {
            Menu menu = new Menu();
            string input = "InvalidInput";

            Plant plant = menu.AddPlantFromString(input);

            Assert.IsNull(plant);
        }
    }
}