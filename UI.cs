using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using UnityEngine.UI;
public class UI : MonoBehaviour
{
    public enum menu { add_atom, save_sim, exit, usual }
    public menu myMenu = menu.usual;
    readonly string prev = "AgnesTachyon\n<size=30>molecule simulator</size>\n\npress'=' to back home \n\n";
    readonly string menu_menu = "a- add atom";

    string? substring_addatom;
    Text t;
    public Simulation sim;
    readonly double radius_p = (0.04) * (0.1);
    readonly double ion_r_decresec = 0.98d;
    // Start is called before the first frame update
    void Start()
    {
        t = GetComponent<Text>();
    }

    void LoadXML(string namae, int ion)
    {
        TextAsset textAsset = (TextAsset)Resources.Load("element");
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);
        XmlNodeList nodes = xmlDoc.SelectNodes("Elements/" + namae);

        foreach (XmlNode node in nodes)
        {

            var p = node.SelectSingleNode("Proton").InnerText;
            var n = node.SelectSingleNode("Neutron").InnerText;
            var r = node.SelectSingleNode("Radius").InnerText;

            var k = new Atom
            {
                proton = int.Parse(p),
                neutron = int.Parse(n),
                radius = (double)(int.Parse(r))
            };
            var ion_ion = Mathf.Clamp(ion, -int.Parse(p), int.Parse(p));
            k.electron -= ion_ion;
            k.radius = k.radius * radius_p;
            sim.CreatAtomObject(k);
            return;
            //주기에 따라 이온반지름 크기 정하기 구현 예정
        }
    }
    int index;
    int _add_stage;
    string[] names = new string[3] { "Hydrogen", "Helium", "Berrylium" };
    string to_name;
    int to_ion;
    void AddAtom()
    {
        switch (_add_stage)
        {
            case 0: SelectAtom(); break;
            case 1: SelectIon(); break;
        }
    }
    void SelectAtom()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (index == 0) index = names.Length - 1;
            if (index > names.Length - 1) index = 0;
            index--;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (index == 0) index = names.Length - 1;
            if (index > names.Length - 1) index = 0;
            index++;
        };
        index = Mathf.Clamp(index, 0, names.Length - 1);
        substring_addatom = "ADD ATOM:\n" + names[index];
        if (Input.GetKeyDown(KeyCode.Return))
        {
            to_name = names[index];
            _add_stage = 1;
        }
    }
    void SelectIon()
    {
        if (Input.anyKeyDown) to_ion += (int)(Input.GetAxisRaw("Vertical"));
        substring_addatom = "ADD ATOM:\n" + names[index] + "\nION STATE:\n" + to_ion.ToString();

        if (Input.GetKeyDown(KeyCode.Return))
        {
            LoadXML(to_name, to_ion);
            index = 0;
            _add_stage = 0;
        }
    }
    // Update is called once per frame
    void Update()
    {
        switch (myMenu)
        {
            case menu.add_atom:
                AddAtom();
                t.text = prev + substring_addatom;
                break;
            default:
                t.text = prev + menu_menu;
                break;

        }
        if (Input.anyKeyDown)
        {
            switch (Input.inputString)
            {
                case "a": myMenu = (menu)0; _add_stage = 0; break;
                case "s": myMenu = (menu)1; break;
                case "e": myMenu = (menu)2; break;
                case "=": myMenu = (menu)3; break;
            }
        }
    }

}

[System.Serializable]
public class Atom
{
    public int proton;
    public int neutron;
    public double radius;
    public int electron;
}