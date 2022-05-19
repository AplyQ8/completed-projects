using System;


public interface IUnit
{
    int UnitId { get; set; }
    string UnitName { get; set; }
    int UnitHitPoints { get; set; }
    int UnitAttack { get; set; }
    int UnitDefence { get; set; }
    int UnitCost { get; set; }
}

