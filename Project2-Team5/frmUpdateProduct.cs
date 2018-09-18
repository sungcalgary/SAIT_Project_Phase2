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

namespace Project2_Team5
{
     /*
     * Author: Chris Earle
     * Created: 2018-07
     */
    public partial class frmUpdateProduct : Form
    {
        //initialize
        public frmUpdateProduct()
        {
            InitializeComponent();
        }

        public Product product; // current product
        public Product oldProduct; // original product data

        List<Product> productList;
        List<Supplier> supplierList;
        List<Product> products;
        List<String> originalList;    //to deal with casting trouble create an original List<String> to compare
                                      //with list of selected Suppliers for the product

        //When the form first loads
        private void frmUpdateProduct_Load(object sender, EventArgs e)
        {
            productList = ProductDB.GetProducts();
            int prodID = product.ProductId;
            string prodName = product.ProdName;
            txtProdID.Text = prodID.ToString();     //product ID to display (0 for add new)
            txtProdName.Text = prodName;            //product name to display ("" for add new)

            //list of all suppliers for specified product
            products = ProductDB.GetProductsBySupplier(prodID);  
            //new list for all suppliers not belonging to specified product
            List<Product> supplierNotAdded = ProductDB.GetSuppliersNotForProduct(prodID);
            
            lbSupNameAdded.Items.Clear();
            foreach (Product prod in products)
            {
                if (prod != null)
                    //populate left listbox with suppliers for specified product
                    lbSupNameAdded.Items.Add(prod.SupName);
            }

            lbSupName.Items.Clear();
            foreach (Product prod in supplierNotAdded)
            {
                if (prod != null)
                    //populate right listbox with suppliers not offering specified product
                    lbSupName.Items.Add(prod.SupName);
            }

            //original List<String> to compare with list of selected Suppliers for the product
            originalList = lbSupNameAdded.Items.Cast<String>().ToList();

            if (Convert.ToInt32(txtProdID.Text) != 0)
                txtProdName.ReadOnly = true;
        }


        //when add button clicked, after user has selected suppliers from right column
        private void btnAddProd_Click(object sender, EventArgs e)
        {
            MoveListBoxItems(lbSupName, lbSupNameAdded);
        }

        //when remove button clicked, after user has selected suppliers from left column
        private void btnRemoveProd_Click(object sender, EventArgs e)
        {
            MoveListBoxItems(lbSupNameAdded, lbSupName);
        }

        //Move items between listboxes based on if add or remove button selected
        //source and destination determined by what is defined under btnRemoveProd_Click and 
        //btnAddProd_Click
        private void MoveListBoxItems(ListBox source, ListBox destination)
        {
            //move items to destination, while removing from source
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

        //When user selects save button, either adds product or edits product
        private void btnSave_Click(object sender, EventArgs e)
        {
            {
                if (txtProdID.Text == "0")  //this condition is when addng a new product
                {
                    if (txtProdName.Text != "")  //check is user entered a name for the product 
                    {
                        List<String> ToAdd = lbSupNameAdded.Items.Cast<String>().ToList();  //add lbSupName to list
                        ProductDB.AddProducts(txtProdName.Text);    //selected suppliers to add to product
                        foreach (String sup in ToAdd)
                            ProductDB.AddSuppliersToProduct(txtProdName.Text, sup);  //adding selected suppliers
                        this.DialogResult = DialogResult.OK;    //when ok the main form product list refreshes
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Please enter a name for the product");  //tell user to enter name
                    }
                }
                else  //for editing existing Products (which Suppliers offer the product)
                {
                    supplierList = SupplierDB.GetSupplier();
                    productList = ProductDB.GetProducts();
                    //list of suppliers to compare to original list when page first loads
                    List<String> ToCompare = lbSupNameAdded.Items.Cast<String>().ToList();  
                    List<String> MatchingOriginalList = new List<String>();

                    foreach (String s in originalList)
                    {
                        foreach (String sup in ToCompare)
                        {
                            MatchingOriginalList.Add(sup); //listbox suppliers matching to original list
                        }
                    }

                    List<String> removed;
                    //compare lists to find suppliers removed from original suppliers
                    removed = originalList.Except(MatchingOriginalList).ToList();
                    List<String> added;
                    //compare lists to find suppliers added to original suppliers
                    added = MatchingOriginalList.Except(originalList).ToList();

                    try
                    {
                        //for each item in list of suppliers removed from original suppliers
                        //query to remove Suppliers from Product, reflected in Product_Suppliers Table
                        foreach (String removedSup in removed)
                            if (removedSup != null)
                                Products_SupplierDB.RemoveSuppliersFromProduct(Convert.ToInt32(txtProdID.Text), removedSup);
                        //for each item in list of suppliers added to original suppliers
                        //query to remove Suppliers from Product, reflected in Product_Suppliers Table
                        foreach (String addedSup in added)
                            if (addedSup != null)
                                ProductDB.AddSuppliersToProduct(txtProdName.Text, addedSup);
                        this.Close();
                    }
                    catch
                    {
                        MessageBox.Show("Can't remove this supplier, someone has already ordered this product from this supplier");
                    }
                    
                }
            }
        }

        //When user selects cancel button close this form
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}





