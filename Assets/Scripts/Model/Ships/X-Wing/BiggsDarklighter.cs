﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ship
{
    namespace XWing
    {
        public class BiggsDarklighter : XWing
        {
            public BiggsDarklighter(Players.PlayerNo playerNo, int shipId, Vector3 position) : base(playerNo, shipId, position)
            {
                PilotName = "Biggs Darklighter";
                ImageUrl = "https://vignette3.wikia.nocookie.net/xwing-miniatures/images/9/90/Biggs-darklighter.png";
                isUnique = true;
                PilotSkill = 5;

                Actions.OnCheckTargetIsLegal += CanPerformAttack;

                InitializePilot();
            }

            public void CanPerformAttack(ref bool result, Ship.GenericShip attacker, Ship.GenericShip defender)
            {
                bool abilityIsActive = false;
                if (defender.ShipId != this.ShipId)
                {
                    if (defender.Owner.PlayerNo == this.Owner.PlayerNo)
                    {
                        if (Actions.GetRange(defender, this) <= 1)
                        {
                            if (Combat.SecondaryWeapon == null)
                            {
                                if (attacker.CanShootWithPrimaryWeaponAt(this)) abilityIsActive = true;
                            }
                            else
                            {
                                if (Combat.SecondaryWeapon.IsShotAvailable(this)) abilityIsActive = true;
                            }
                        }
                    }
                }

                if (abilityIsActive)
                {
                    Game.UI.ShowError("Biggs DarkLighter: You cannot attack target ship");
                    //Todo: Adapt Highlights
                    result = false;
                }
            }
        }
    }
}