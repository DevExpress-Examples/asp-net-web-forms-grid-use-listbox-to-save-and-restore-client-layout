using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxClasses;

public partial class _Default : System.Web.UI.Page {
    bool isSaveLayout = true;

    protected void Page_Load(object sender, EventArgs e) {
        Grid.JSProperties["cpCallback"] = 0;
        if (Session["layout"] == null) {
            Session["layout"] = new Dictionary<string, string>();
        }
        else {
            Dictionary<string, string> dictionary = Session["layout"] as Dictionary<string, string>;
            ListBox.Items.Clear();
            foreach (KeyValuePair<string, string> item in dictionary) {
                ListEditItem editItem = new ListEditItem(item.Key, item.Value);
                ListBox.Items.Add(editItem);
            }
        }
    }
    protected void ListBox_Callback(object sender, CallbackEventArgsBase e) {
        if (Session["selectedLayout"] != null)
            ListBox.JSProperties["cpSelected"] = ListBox.Items.IndexOfValue(Session["selectedLayout"]);
    }
    protected void Grid_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e) {
        if (ListBox.SelectedItem != null) {
            Grid.LoadClientLayout(ListBox.SelectedItem.Value.ToString());
            isSaveLayout = false;
            Grid.JSProperties["cpCallback"] = 1;
        }
    }
    protected void Grid_ClientLayout(object sender, ASPxClientLayoutArgs e) {
        if (e.LayoutMode == ClientLayoutMode.Saving && isSaveLayout) {
            string key = DateTime.Now.ToString();
            string layout = e.LayoutData;
            Dictionary<string, string> dictionary = Session["layout"] as Dictionary<string, string>;
            if (!dictionary.ContainsValue(layout)) {
                ListBox.Items.Add(key, layout);
                dictionary[key] = layout;
            }
            else {
                Session["selectedLayout"] = layout;
            }
            Session["layout"] = dictionary;
        }
    }
}
