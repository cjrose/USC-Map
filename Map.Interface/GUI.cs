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
        public GUI()
        {
            InitializeComponent();
            AutoComplete();
            PopulateListBox();
        }

        private void GUI_Load(object sender, EventArgs e)
        {
            webBrowser1.AllowNavigation = true;
            Uri coordURL = new Uri($"https://maps.googleapis.com/maps/api/staticmap?center=The+Horseshoe,Columbia+SC&zoom=17&size={webBrowser1.Size.Width}x{webBrowser1.Size.Height-50}&maptype=roadmap&key=REDACTED");
            webBrowser1.Navigate(coordURL);
        }

        private void AutoComplete()
        {
            NameSearch.AutoCompleteMode = AutoCompleteMode.Suggest;
            NameSearch.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection coll = new AutoCompleteStringCollection();

            foreach (var kvp in Database.buildingDB)
            {
                coll.Add(kvp.Value.Name + "-" + kvp.Value.Code);
            }

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

        private void SearchBox_TextChanged(object sender, EventArgs e)
        {

            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                var searchResults = Database.FindBuildings(textBox.Text);
            }
        }

        private void input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                var building = Database.FindBuildings(NameSearch.Text);
                if (building.Count == 1)
                {
                    UpdateMap(building[0]);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var building = Database.FindBuildings(NameSearch.Text);
            if (building.Count == 1)
            {

                UpdateMap(building[0]);
            }
        }

        private void UpdateMap(Building building)
        {
            webBrowser1.Navigate(new Uri(returnURL(building.WebName)));
        }

        private string returnURL(string webname)
        {
            return $"https://maps.googleapis.com/maps/api/staticmap?center={webname}&zoom=17&size={webBrowser1.Size.Width}x{webBrowser1.Size.Height-50}&maptype=roadmap&key=REDACTED";
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox listbox = sender as ListBox;
            var buildingName = listbox.SelectedItem;

            var building = Database.FindBuildings((string)buildingName);
            if (building.Count == 1)
            {
                UpdateMap(building[0]);
            }
        }
    }
}
