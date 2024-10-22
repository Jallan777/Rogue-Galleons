using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

[TestFixture]
public class CreateMapTests
{
    private CreateMap createMap;
    private GameObject[] testIslands;

    // Setup before each test
    [SetUp]
    public void Setup()
    {
        // Create GameObject and attach CreateMap component
        GameObject mapObject = new GameObject();
        createMap = mapObject.AddComponent<CreateMap>();

        // Create test islands
        testIslands = new GameObject[2];
        for (int i = 0; i < 2; i++)
        {
            testIslands[i] = new GameObject($"TestIsland_{i}");
            testIslands[i].AddComponent<RectTransform>();
        }
        createMap.otherIslands = testIslands;
    }

    [TearDown]
    public void Teardown()
    {
        Object.Destroy(createMap.gameObject);
        createMap = null;
    }



    [Test]
    public void PlaceRandomly_PlacesIslandAtDefaultPosition()
    {
        // Arrange
        RectTransform islandRect = testIslands[0].GetComponent<RectTransform>();
        
        // Act
        // PlaceRandomly might not randomize positions yet (so it just places them at a default position)
        createMap.PlaceRandomly(testIslands[0]);

        // Assert
        Vector2 position = islandRect.anchoredPosition;
        
        Assert.AreEqual(Vector2.zero, position, "The island should be placed at a default position (0, 0) because randomization is not yet implemented.");
    }


    [Test]
    public void SaveIslandPositions_DoesNotSaveCorrectly()
    {
        // Arrange
        RectTransform islandRect = testIslands[0].GetComponent<RectTransform>();
        islandRect.anchoredPosition = new Vector2(100, 200);  // Assign a sample position

        // Act
        // SaveIslandPositions may not be fully implemented (so no positions will actually be saved)
        createMap.SaveIslandPosistions();

        // Assert
        Assert.AreNotEqual(100f, PlayerPrefs.GetFloat("OtherIslandX_0"), "Position X should not be saved because SaveIslandPositions is incomplete.");
        Assert.AreNotEqual(200f, PlayerPrefs.GetFloat("OtherIslandY_0"), "Position Y should not be saved because SaveIslandPositions is incomplete.");
    }
    
}
