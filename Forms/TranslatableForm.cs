using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

namespace PokemonEncCalc
{
    public class TranslatableForm : Form
    {

        protected void renameControls()
        {
            // 
            int formNum = Utils.formList.IndexOf(GetType().Name);
            if (formNum < 0)
                return;


            if (Utils.controlText[formNum] == null) return;  // if nothing to translate, abort function and don't translate user interface

            if(Utils.controlText[formNum].Count > 0)
                if(Utils.controlText[formNum][0].Contains(" = "))
                {
                    Text = Utils.controlText[formNum][0].Split(new[] { " = " }, StringSplitOptions.None)[1];
                }

            foreach (string line in Utils.controlText[formNum])
            {


                string[] stringSplit = line.Split(new[] { " = " }, StringSplitOptions.None);
                if (stringSplit.Length < 2)
                    continue; // Error : invalid format (expected "control = text")

                string control = stringSplit[0];
                string text = stringSplit[1];

                // rename controls of form
                Control[] c = Controls.Find(control, true);
                if (c.Length > 0)
                    c[0].Text = text;
                else
                    // MenuStrips are not scanned
                    // We scan them here
                    foreach (Object m in Controls)
                    {
                        if(m is MenuStrip)
                        {
                            MenuStrip n = (MenuStrip)m;
                        
                            // Get all the top menu items
                            List<ToolStripMenuItem> allItems = new List<ToolStripMenuItem>();
                            

                            foreach (ToolStripMenuItem item in n.Items)
                            {
                                // Add top menu items
                                allItems.Add(item);
                                // For each of the top menu items, get all sub items recursively
                                allItems.AddRange(GetItems(item));
                            }
                            int i = (allItems.FindIndex(s=> s.Name.Equals(control)));
                            if (i >= 0)
                                allItems[i].Text = text;
                        }
                    }
            }

        }

        private IEnumerable<ToolStripMenuItem> GetItems(ToolStripMenuItem item)
        {
            foreach (ToolStripMenuItem dropDownItem in item.DropDownItems)
            {
                if (dropDownItem.HasDropDownItems)
                {
                    foreach (ToolStripMenuItem subItem in GetItems(dropDownItem))
                        yield return subItem;
                }
                yield return dropDownItem;
            }
        }

    }
}
