using TravelExperts_Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// Luke

namespace Project2_Team5
{
    public partial class frmUpdateSupplier : Form
    {
        public frmUpdateSupplier()
        {
            InitializeComponent();
        }

        public Supplier supplier; // current Supplier
        public Supplier oldSupplier; // original Supplier data       
        public bool modifying;

        List<Supplier> supplierList;              
        List<Product> products;
        List<Supplier> suppliers;
        List<String> originalList;    //to deal with casting trouble


        private void frmUpdateSupplier_Load_1(object sender, EventArgs e)
        {
            supplierList = SupplierDB.GetSupplier();
           
            int supID = supplier.SupplierId;
            string supName = supplier.SupName;
            
            txtSupplierId.Text = supID.ToString();
            txtSupName.Text = supName;

            suppliers = SupplierDB.GetSupplier();

            products = SupplierDB.GetProductsFromSupplier(supID);
            List<Product> productNotAdded = SupplierDB.GetProductsNotFromSupplier(supID); //list product not added

            lbProdExist.Items.Clear();
            foreach (Product prod in products)
            {
                if (prod != null)
                    lbProdExist.Items.Add(prod.ProdName);
            }

            lbProdNotAdd.Items.Clear();
            foreach (Product prod in productNotAdded)
            {
                if (prod != null)
                    lbProdNotAdd.Items.Add(prod.ProdName);
            }

            originalList = lbProdExist.Items.Cast<String>().ToList();

            if (Convert.ToInt32(txtSupplierId.Text) != 0)
                txtSupName.ReadOnly = true;
        }

        private void displaySupplier()
        {

            txtSupplierId.Text = supplier.SupplierId.ToString();
            txtSupName.Text = supplier.SupName.ToString();       
   
        }       

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                
                if (txtSupplierId.Text == "0")
                {
                    //query for adding a new products to supplier
                    if (txtSupName.Text != "")
                    {
                        List<String> ToAdd = lbProdExist.Items.Cast<String>().ToList();
                        supplier.SupplierId = SupplierDB.GetNewSupplierId();

                        supplier.SupName = txtSupName.Text;
                        
                        SupplierDB.AddSupplier(supplier);
                        
                               foreach (String prod in ToAdd)
                                   SupplierDB.AddProductsToSupplier(txtSupName.Text, prod);  //adding selected products
                               this.DialogResult = DialogResult.OK;                        
                    }

                    else
                    {
                        MessageBox.Show("Please enter a name for the supplier");
                    }
                }
                else  //for editing exist products from Supplier
                {
                    supplierList = SupplierDB.GetSupplier();
                    
                    List<String> ToCompare = lbProdExist.Items.Cast<String>().ToList();  //add lbProdNotAdd to list
                    List<String> ConvertedOriginalList = new List<String>();

                    foreach (String s in originalList)
                    {
                        foreach (String sup in ToCompare)
                        {
                            ConvertedOriginalList.Add(sup);
                        }
                    }

                    List<String> removed;
                    removed = originalList.Except(ConvertedOriginalList).ToList();
                    List<String> added;
                    added = ConvertedOriginalList.Except(originalList).ToList();

                    try
                    {
                        foreach (String removedSup in removed)
                            if (removedSup != null)
                                Products_SupplierDB.RemoveProductsFromSupplier(Convert.ToInt32(txtSupplierId.Text), removedSup);
                        foreach (String addedSup in added)
                            if (addedSup != null)
                                SupplierDB.AddProductsToSupplier(txtSupName.Text, addedSup);
                        this.Close();
                    }
                    catch
                    {
                        MessageBox.Show("Can't remove this product from supplier someone already purchased this");
                    }
                    
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool ValidateForm()
        {

            bool result = true;
            
            // empty textbox validation
            if (txtSupName.Text == "")
            {
                MessageBox.Show("There is an empty input");
                result = false;
            }
            else if (txtSupplierId.Text == "")
            {
                MessageBox.Show("SupplierID has to be input");
                result = false;
            }        

            return result;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            MoveListBoxItems(lbProdNotAdd, lbProdExist);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            MoveListBoxItems(lbProdExist, lbProdNotAdd);
        }

        private void MoveListBoxItems(ListBox source, ListBox destination)
        {
            ListBox.SelectedObjectCollection sourceItems = source.SelectedItems;
            foreach (var item in sourceItems)
            {
                destination.Items.Add(item);
            }
            while (source.SelectedItems.Count > 0)
            {
                source.Items.Remove(source.SelectedItems[0]);
            }
        }
    }
}
