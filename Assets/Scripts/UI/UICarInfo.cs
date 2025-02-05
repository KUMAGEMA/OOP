using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UICarInfo : MonoBehaviour
{
    public Image CarImage;
    public TextMeshProUGUI SpeedText;
    public TextMeshProUGUI FuelText;
    public Button RefuelButton;

    public void InitializeCardInfoUI(CarInstance carInstance)
    {
        CarImage.sprite = carInstance.CarSprite;
        RefuelButton.onClick.AddListener(carInstance.Car.Refuel);
        UpdateUI(carInstance.Car);
    }

    public void UpdateUI(Car car)
    {
        SpeedText.text = car.GetCurrentSpeed().ToString("F2"); // Limits to 2 decimal places
        FuelText.text = car.GetCurrentFuel().ToString("F2"); // Limits to 2 decimal places
    }
}
