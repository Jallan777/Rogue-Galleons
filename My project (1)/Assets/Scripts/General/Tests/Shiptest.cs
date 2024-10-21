using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;

public class ShipTest
{
    [Test]
    public void Test_TakeDamage_WithShield()
    {
        // Arrange
        GameObject shipObject = new GameObject();
        Ship ship = shipObject.AddComponent<Ship>();
        ship.maxHealth = 100f;
        ship.currentHealth = 100f;
        ship.maxShield = 75f;
        ship.currentShield = 75f;

        // Creating an attack with a specified damage value
        Attack attack = new Attack(50f); // Pass the required damage parameter here

        // Act
        ship.takeDamage(attack);

        // Assert
        Assert.AreEqual(25f, ship.currentShield);
        Assert.AreEqual(100f, ship.currentHealth);
    }

    [Test]
    public void Test_TakeDamage_WithoutShield()
    {
        // Arrange
        GameObject shipObject = new GameObject();
        Ship ship = shipObject.AddComponent<Ship>();

        ship.maxShield = 100f;
        ship.currentShield = 0f; // Shield is already depleted
        ship.maxHealth = 100f;
        ship.currentHealth = 100f;

        // Initialize the event
        ship.OnTakingDamage = new UnityEvent<Ship>();



        Attack attack = new Attack(30f);

        // Act
        ship.takeDamage(attack);

        // Assert
        Assert.AreEqual(0f, ship.currentShield); // Shield should be 0
        Assert.AreEqual(70f, ship.currentHealth); // Health should reduce by 30
    }

}
