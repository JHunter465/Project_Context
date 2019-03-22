using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProfileManager : MonoBehaviour
{
    public Image colorImage;
    public List<ProfileData> DataList = new List<ProfileData>();

    public void SetProfileData(int _dataIndex)
    {
        ProfileData _profileData = DataList[_dataIndex];

        SetProfileData(_profileData);
    }

    //Test
    public void SetProfileData(ProfileData _profileData)
    {
        _profileData.Value = _profileData.Inputfield.text;
        _profileData.OutputTextfield.text = _profileData.Name + ": " + _profileData.Value;
    }

    public void SetProfileColor(TMP_Dropdown _dropdown)
    {
        //ipv een lege image wordt dit wellicht de sprite van de speler

        switch (_dropdown.value)
        {
            default:
            case 0:
                colorImage.color = Color.red;
                break;
            case 1:
                colorImage.color = Color.green;
                break;
            case 2:
                colorImage.color = Color.blue;
                break;
        }
    }
}

[System.Serializable]
public class ProfileData
{
    public string Name;
    [HideInInspector] public string Value;
    public TMP_InputField Inputfield;
    public TMP_Text OutputTextfield;
}
