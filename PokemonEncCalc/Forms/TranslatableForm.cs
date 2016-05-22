using System;
using System.Windows.Forms;

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

                // rename controls of frmMainPage
                Control[] c = this.Controls.Find(control, true);
                if (c.Length > 0)
                    c[0].Text = text;


            }

        }


    }
}
