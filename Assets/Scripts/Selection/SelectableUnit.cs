using UnityEngine;

public class SelectableUnit : MonoBehaviour, ISelectable
{
    public int team = -1;
    public int Team
    {
        get { return team; }
    }

    public void Select()
    {
        GetComponent<MeshRenderer>().material.color = Color.red;
        GetComponent<ArmyGuyUnit>().isSelected = true;
        GetComponent<ArmyGuyUnit>().selectedMarker.SetActive(true);
    }

    public void Deselect()
    {   
        GetComponent<MeshRenderer>().material.color = Color.white;
        GetComponent<ArmyGuyUnit>().isSelected = false;
        GetComponent<ArmyGuyUnit>().selectedMarker.SetActive(false);
    }

    // This is extra
    private void Start()
    {
        GetComponent<MeshRenderer>().material.color = Color.white;
    }
}
