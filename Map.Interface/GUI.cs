/* Copyright 2018 */
/* Cody Rose */

using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace USC_Map
{
    public partial class GUI : Form
    {
        // remember to remove before github push
        private string apiKey = "REDACTED";
        private string mapType = "roadmap";
        private string unusedType = "hybrid";
        public GUI()
        {
            InitializeComponent();
            AutoComplete();
            PopulateListBox();
        }

        private void GUI_Load(object sender, EventArgs e)
        {
            webBrowser1.AllowNavigation = true;
            Uri coordURL = new Uri(returnURL("The+Horseshoe,Columbia+SC"));
            webBrowser1.Navigate(coordURL);
        }

        private void AutoComplete()
        {
            NameSearch.AutoCompleteMode = AutoCompleteMode.Suggest;
            NameSearch.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection coll = new AutoCompleteStringCollection();

            foreach (var kvp in Database.buildingDB)
                coll.Add(kvp.Value.Name + "-" + kvp.Value.Code);

            NameSearch.AutoCompleteCustomSource = coll;
        }

        private void PopulateListBox()
        {
            List<string> items = new List<string>();
            foreach (var kvp in Database.buildingDB)
                items.Add(kvp.Value.Name);
            items.Sort();

            foreach (var entry in items)
                listBox1.Items.Add(entry);
        }

        private void input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                var building = Database.FindBuildings(listBox1.Text);
                if (building.Count == 1)
                    UpdateMap(building[0]);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UpdateListboxSelection();
            var building = Database.FindBuildings(listBox1.Text);
            if (building.Count == 1)
                UpdateMap(building[0]);
        }

        private void UpdateMap(Building building)
        {
            webBrowser1.Navigate(new Uri(returnURL(building.WebName)));
        }

        private string returnURL(string webname)
        {
            return $"https://maps.googleapis.com/maps/api/staticmap?center={webname}&zoom=17&size={webBrowser1.Size.Width}x{webBrowser1.Size.Height-50}&maptype={mapType}&style=feature:poi|visibility:off&markers=color:red|{webname}&key={apiKey}";
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox listbox = sender as ListBox;
            var buildingName = listbox.SelectedItem;

            var building = Database.FindBuildings((string)buildingName);
            if (building.Count == 1)
                UpdateMap(building[0]);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            updateView();
            var building = Database.FindBuildings(listBox1.Text);
            if (building.Count == 1)
                UpdateMap(building[0]);
        }

        private void updateView()
        {
            string temp = mapType;
            mapType = unusedType;
            unusedType = temp;
        }

        private void NameSearch_TextChanged(object sender, EventArgs e)
        {
            UpdateListboxSelection();
        }

        private void UpdateListboxSelection()
        {
            var building = Database.FindBuildingMatch(NameSearch.Text);
            if (building != null)
            {
                int index = -1;
                for (int i = 0; i < listBox1.Items.Count; i++)
                {
                    string name = listBox1.Items[i] as string;
                    if (building.Name.ToLower().Equals(name.ToLower()))
                    {
                        index = i;
                        break;
                    }
                }
                listBox1.SelectedItem = listBox1.Items[index];
            }
        }
    }
}
