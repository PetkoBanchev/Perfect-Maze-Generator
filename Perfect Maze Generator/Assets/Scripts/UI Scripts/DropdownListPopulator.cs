using System;
using System.Collections.Generic;
using TMPro;
/// <summary>
/// A helper class used to populate a dropdown list with enums
/// </summary>
public static class DropdownListPopulator
{   
    public static void PopulateDropdown<T>(TMP_Dropdown dropdown)
    {
        string[] enumNames = Enum.GetNames(typeof(T));
        List<string> optionNames = new List<string>(enumNames);
        dropdown.AddOptions(optionNames);
    }
}
