using BCustomCosmetics.Scripts.Net;
using BepInEx;
using GorillaNetworking;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace BCustomCosmetics
{
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        string ListOfInstalledHats;
        List<string> Direcotries;
        List<string> filehatList = new List<string>();
        void Start()
        {Utilla.Events.GameInitialized += OnGameInitialized;}

        void OnEnable()
        {HarmonyPatches.ApplyHarmonyPatches();}

        void OnDisable()
        {HarmonyPatches.RemoveHarmonyPatches();}

        void OnGameInitialized(object sender, EventArgs e)
        {
            
        }

        void GetCosFolders()
        {
            string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string hatsFolder = assemblyFolder + "/Hats";
            string badgeFolder = assemblyFolder + "/Badges";
            string matsFolder = assemblyFolder + "/Materials";
            Direcotries = new List<string> { hatsFolder, badgeFolder, matsFolder };
            GetItems(Direcotries);
        }
        

        void GetItems(List<string> dirc)
        {
            for (int i = 0; i < dirc.Count; i++)
            {
                ItemGetStep1(dirc[i]);
            }
        }
        void ItemGetStep1(string path)
        {
            DirectoryInfo d = new DirectoryInfo(@path);

            FileInfo[] Files = d.GetFiles();
            string str = "";

            foreach (FileInfo file in Files)
            {
                if (file.FullName.Contains(".hat"))
                {
                    filehatList.Add(file.Name);
                }
            }
        }
        void Update()
        {
            if (PhotonNetwork.IsConnectedAndReady && NetStuff.instance == null)
            {
                NetStuff.instance = FindObjectOfType<CosmeticsController>().gameObject.AddComponent<NetStuff>();
                NetStuff.instance.AddHatsToProp();
            } 
        }
    }
}
