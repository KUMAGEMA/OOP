using System;
using Sirenix.OdinInspector;
using UnityEngine;

/* Explanation
Inheritance:
- The CarType determines which subclass (Car or SolarCar) to instantiate.
- Both Car and SolarCar share common properties (e.g., Brand, TopSpeed) inherited from the Car class.
- The RechargeRate is specific to SolarCar and does not exist in Car, showcasing extension through inheritance.

Polymorphism:
- The Car reference is polymorphic and can hold objects of either the Car class or any of its derived classes (SolarCar).
- The Move() method is called polymorphically, and at runtime, the correct implementation (base or overridden) is executed depending on the object type.
- The ai.SetCar(Car) method demonstrates polymorphism in action, as the CarAI system interacts with the Car object without needing to know its exact type.
*/
public class CarInstance : MonoBehaviour
{
    #region Car Setup Variables
    [Header("Car Setup")]
    [Tooltip("Select the type of car (Gasoline or Solar)")]
    public CarType CarType; // Determines the type of car (affects which class is instantiated)

    [Tooltip("Brand of the car")]
    public string Brand; // Shared property across all car types

    public Sprite CarSprite; // Visual representation of the car

    [Tooltip("Maximum speed the car can reach")]
    public float TopSpeed; // Shared property across car types

    [Tooltip("Rate at which the car accelerates")]
    public float Acceleration; // Shared property across car types

    [Tooltip("Maximum fuel capacity of the car")]
    public float MaxFuel; // Shared property across car types

    [Tooltip("Fuel consumption per second")]
    public float FuelConsumption; // Shared property across car types

    [ShowIf("CarType", CarType.Solar)]
    [Tooltip("Rate at which the solar car recharges per second")]
    public float RechargeRate; // Specific to SolarCar (handled using Odin Inspector for conditional visibility)
    #endregion

    #region Polymorphic Reference
    [ReadOnly] public Car Car; // Polymorphic reference to a base Car object (can hold any derived type)
    #endregion

    #region Dependency Injection
    // Dependency: Handles AI behavior
    private CarAI ai;
    #endregion

    #region Initialization
    void Start()
    {
        ai = GetComponent<CarAI>(); // Gets the CarAI component for navigation
        ai.Initialize(); // Initializes the AI system
        InitializeCar(); // Dynamically creates the appropriate Car instance
        InitializeUI(); // Sets up the UI elements for the car
    }

    private void InitializeCar()
    {
        // Use a switch statement to instantiate the correct Car type based on CarType
        switch (CarType)
        {
            case CarType.Gasoline:
                // Creates a base Car instance for gasoline-powered cars
                Car = new Car(Brand, TopSpeed, Acceleration, MaxFuel, FuelConsumption);
                break;

            case CarType.Solar:
                // Creates a SolarCar instance, utilizing polymorphism
                Car = new SolarCar(Brand, TopSpeed, Acceleration, MaxFuel, FuelConsumption, RechargeRate);
                break;
        }

        // Pass the car instance to the AI system (polymorphic behavior)
        if (ai != null)
        {
            ai.SetCar(Car);
        }
    }
    #endregion

    #region Update Loop
    private void Update()
    {
        // Call the Move method polymorphically (runtime behavior depends on the actual Car type)
        Car.Move();
        UpdateUI(); // Update the car's UI
    }
    #endregion

    #region View
    [FoldoutGroup("View", false)] public UIManager UIManager; // Reference to the UIManager for managing UI
    [ReadOnly, FoldoutGroup("View", false)] public UICarInfo UI; // Specific UI for displaying car info
    private SpriteRenderer spriteRenderer;

    private void InitializeUI()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // Gets the SpriteRenderer for car visuals
        spriteRenderer.sprite = CarSprite; // Sets the car's sprite image
        UI = UIManager.GetUICardInfo(); // Retrieves the UI card info from the UIManager
        UI.InitializeCardInfoUI(this); // Initializes the UI with car-specific details
    }

    private void UpdateUI()
    {
        if (UI != null)
        {
            // Updates the UI with the current state of the car (fuel, speed, etc.)
            UI.UpdateUI(Car);
        }
    }
    #endregion
}
