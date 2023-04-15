using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureEditorDisplay : MonoBehaviour
{
    public CreatureDisplay display;
    public Transform creatureButtonList;
    public GameObject loadCreatureButtonPrefab;

    public void SetupFromController()
    {
        Setup(new List<CreatureEntry>(PortController.single.creatures.Values));
    }

    public void Setup(List<CreatureEntry> creatures)
    {
        GameObject spawnedObject;
        LoadCreatureButton spawnedButton;
        for (int i = 0; i < creatureButtonList.childCount; i++)
        {
            Destroy(creatureButtonList.GetChild(i).gameObject, 0.1f);
        }

        bool loadedDefault = false;
        foreach (CreatureEntry creature in creatures)
        {
            spawnedObject = Instantiate(loadCreatureButtonPrefab, creatureButtonList);
            if (spawnedObject.TryGetComponent(out spawnedButton))
            {
                spawnedButton.Setup(creature, display);
                if (!loadedDefault) { loadedDefault = true; spawnedButton.LoadCreature(); }
            }
        }
    }

}
