using System;
using BepInEx;
using Photon.Pun;
using UnityEngine;
using Utilla;
using Utilla.Attributes;
using Utilla.Models;

namespace MonkeGus
{
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    [ModdedGamemode("monkegus_Infection", "MONKEGUS", BaseGamemode.Infection)]
    public class Plugin : BaseUnityPlugin
    {
        public bool inRoom;
        [ModdedGamemodeJoin]
        public void OnJoin()
        {
            inRoom = true;
        }
        [ModdedGamemodeLeave]
        public void OnLeave()
        {
            inRoom = false;
        }

        public void Update()
        {
            if (inRoom)
            {
                foreach (VRRig rig in GorillaParent.instance.vrrigs)
                {
                    if (rig != GorillaTagger.Instance.offlineVRRig)
                    {
                        Material[] sharedMaterials = rig.mainSkin.sharedMaterials;
                        sharedMaterials[0] = rig.materialsToChangeTo[0];
                        sharedMaterials[1] = rig.defaultSkin.chestMaterial;
                        rig.mainSkin.sharedMaterials = sharedMaterials;
                        rig.mainSkin.material.color = Color.grey;
                        rig.concatStringOfCosmeticsAllowed = null;
                        rig.playerText2.gameObject.SetActive(false);
                        rig.playerText1.gameObject.SetActive(false);
                        rig.lavaParticleSystem.Stop();
                    }
                }
            }
            else
            {
                // just in case
                foreach (VRRig rig in GorillaParent.instance.vrrigs)
                {
                    rig.showName = true;
                }
            }
        }
    }
}