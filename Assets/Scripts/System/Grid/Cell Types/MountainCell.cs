using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountainCell : LandCell
{
    public override Color Highlighted => _sprite.color + Color.white / 4.0f;

    public override string GetCellType() => "Mountain";
}
