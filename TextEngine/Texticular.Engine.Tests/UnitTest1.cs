using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Texticular.Engine.Environment;

namespace Texticular.Engine.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GameObjectTest()
        {
            //Arrange
            var testObject = new GameObject()
            {
                Name = "Headphones",
                Description="White Steel Series Headphones."
            };

            var testObject2 = new GameObject(name: "Excalibur", description: "The sword of legend", keyValue: "excalibur");

            //Act
            foreach(KeyValuePair<string, GameObject> obj in GameObject.Objects)
            {
                Console.Write($"{obj.Key}: {obj.Value.ToString()}");
            }

            //Assert
        }
    }
}
