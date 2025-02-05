using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject UIContentPrefab;
    public Transform SpawnPoint;

    public void SetupUI()
    {

    }

    public UICarInfo GetUICardInfo()
    {
        GameObject uiContent = Instantiate(UIContentPrefab, SpawnPoint);
        return uiContent.GetComponent<UICarInfo>();
    }
}
