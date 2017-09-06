using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class SaveDataManager : MonoBehaviour {
    static protected SaveDataManager m_Instance;
    static public SaveDataManager instance { get { return m_Instance; } }

    static int s_Version = 10;

    public int currency, rank;
    public float masterVolume = float.MinValue, musicVolume = float.MinValue, masterSFXVolume = float.MinValue;
    public bool licenceAccepted;

    [Header("Player Stat Data")]
    public int playerMaxHealth;
    public int playerDefense;
    public int playerAttackPower;

    [Header("Item Data")]
    public List<string> itemNames;
    public List<int> itemPotencies, itemMaxReserves;
    public List<float> itemCoolDowns;

    [Header("Ability Data")]
    public List<string> abilityNames;
    public List<int> abilityPotencies;
    public List<float> abilityCoolDowns;

    protected string saveFile = "";

    // File management

    static public void Create() {
        if (m_Instance == null) {
            m_Instance = new SaveDataManager();
        }

        m_Instance.saveFile = Application.persistentDataPath + "/save.bin";

        if (File.Exists(m_Instance.saveFile)) {
            // If we have a save, we read it.
            m_Instance.Read();
        } else {
            // If not we create one with default data.
            NewSave();
        }
    }

    static public void NewSave() {
        m_Instance.currency = 0;
        m_Instance.rank = 0;

        m_Instance.playerMaxHealth = 10;
        m_Instance.playerDefense = 1;
        m_Instance.playerAttackPower = 1;

        m_Instance.itemNames.Clear();
        m_Instance.itemNames.Add("HEAL");
        m_Instance.itemNames.Add("BUFF");
        m_Instance.itemNames.Add("DAMAGE");

        m_Instance.itemPotencies.Clear();
        for(int u = 0; u < 3; u++) { m_Instance.itemPotencies.Add(1); }

        m_Instance.itemMaxReserves.Clear();
        for(int v = 0; v < 3; v++) { m_Instance.itemMaxReserves.Add(3); }

        m_Instance.itemCoolDowns.Clear();
        for(int w = 0; w < 3; w++) {
            switch (m_Instance.itemNames[w]) {
                case "HEAL": { m_Instance.itemCoolDowns[w] = 0; } break;
                case "BUFF": { m_Instance.itemCoolDowns[w] = 6f; } break;
                case "DAMAGE": { m_Instance.itemCoolDowns[w] = 0; } break;
            }
        }

        m_Instance.abilityNames.Clear();
        m_Instance.abilityNames.Add("DEFENSE");
        m_Instance.abilityNames.Add("DODGE");
        m_Instance.abilityNames.Add("STUN");

        m_Instance.abilityPotencies.Clear();
        for(int x = 0; x < 3; x++) { m_Instance.abilityPotencies.Add(1); }

        m_Instance.abilityCoolDowns.Clear();
        for(int y = 0; y < 3; y++) {
            switch (m_Instance.abilityNames[y]) {
                case "DEFENSE" : { } break;
                case "DODGE" : { } break;
                case "STUN" : { } break;
            }
        }

        m_Instance.currency = 0;

        m_Instance.Save();
    }

    public void Read() {
        BinaryReader r = new BinaryReader(new FileStream(saveFile, FileMode.Open));

        int ver = r.ReadInt32();

        if (ver < 6) {
            r.Close();

            NewSave();
            r = new BinaryReader(new FileStream(saveFile, FileMode.Open));
            ver = r.ReadInt32();
        }

        currency = r.ReadInt32();
        int consumableCount = r.ReadInt32();

        if (ver >= 8) {
            licenceAccepted = r.ReadBoolean();
        }

        if (ver >= 9) {
            masterVolume = r.ReadSingle();
            musicVolume = r.ReadSingle();
            masterSFXVolume = r.ReadSingle();
        }

        if (ver >= 10) {
            rank = r.ReadInt32();
        }

        r.Close();
    }

    public void Save() {
        BinaryWriter w = new BinaryWriter(new FileStream(saveFile, FileMode.OpenOrCreate));
        // write dev version
        w.Write(s_Version);

        // write currency
        w.Write(currency);

        // write licenseAccepted
        w.Write(licenceAccepted);

        // write audio volumes
        w.Write(masterVolume);
        w.Write(musicVolume);
        w.Write(masterSFXVolume);

        // write rank
        w.Write(rank);

        w.Close();
    }
}
