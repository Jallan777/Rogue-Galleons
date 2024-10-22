using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TestTools;

public class ShipTests
{
    private GameObject shipObject;
    private Ship ship;

    [SetUp]
    public void SetUp()
    {
        // Create a new GameObject for the ship and add the Ship component
        shipObject = new GameObject();
        ship = shipObject.AddComponent<Ship>();

        // Set up mock GameObjects for success and failure UI
        ship.success = new GameObject();
        ship.failure = new GameObject();

        // Set these objects to inactive initially
        ship.success.SetActive(false);
        ship.failure.SetActive(false);

        // Set default health and type
        ship.currentHealth = 100;
        ship.shipType = Ship.ShipType.PLAYER;
    }

    [Test]
    public void ApplyDamage_ReducesHealth()
    {
        int initialHealth = ship.currentHealth;
        ship.ApplyDamage(10);
        Assert.AreEqual(initialHealth - 10, ship.currentHealth);
    }

    [Test]
    public void ApplyDamage_DoesNotGoBelowZero()
    {
        ship.ApplyDamage(150); // Exceed health
        Assert.AreEqual(0, ship.currentHealth);
    }

    [Test]
    public void ApplyDamage_ActivatesSuccessOnEnemyDestruction()
    {
        ship.shipType = Ship.ShipType.ENEMY;
        ship.ApplyDamage(100);
        Assert.IsTrue(ship.success.activeSelf);
    }

    [Test]
    public void ApplyDamage_ActivatesFailureOnPlayerDestruction()
    {
        ship.shipType = Ship.ShipType.PLAYER;
        ship.ApplyDamage(100);
        Assert.IsTrue(ship.failure.activeSelf);
    }

    [Test]
    public void OnTakingDamage_IsInvoked()
    {
        bool eventInvoked = false;
        ship.OnTakingDamage = new UnityEvent<Ship>();
        ship.OnTakingDamage.AddListener((s) => { eventInvoked = true; });

        ship.ApplyDamage(10);

        Assert.IsTrue(eventInvoked);
    }

    [TearDown]
    public void TearDown()
    {
        // Destroy the test objects to avoid memory leaks
        Object.Destroy(shipObject);
    }
}
