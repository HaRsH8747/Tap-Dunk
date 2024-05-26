using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFX : MonoBehaviour
{
    [SerializeField] Transform[] _vfx;

    public void SetActive(int vfxIndex, bool enable)
    {
        _vfx[vfxIndex].gameObject.SetActive(enable);
    }
}
