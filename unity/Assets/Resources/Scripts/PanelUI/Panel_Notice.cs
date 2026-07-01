using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Panel_Notice : MonoBehaviour
{
    public TextMeshProUGUI textNotice;

    public void Close()
    {
        this.gameObject.SetActive(false);
    }
}
