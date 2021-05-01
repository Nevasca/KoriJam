using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActivable
{
    void ToggleActive();
    bool IsActive();
}
