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

/*
 * this file has one author
 * Author: Sunghyun Lee
 * Created: 2018-07
 */
namespace Project2_Team5
{
    public partial class frmUpdatePackage : Form
    {
        /*
        exception handling example:
        try
            {
                order = OrderDB.GetOrder(OrderID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        */
        public frmUpdatePackage()
        {
            InitializeComponent();
        }

        public Package package; // current package
        public Package oldPackage; // original package
        public SortedList<int, int> addedIds; // Key: id of a product added to the package, Value: id of a supplier who owns the product
        public List<int> removedIds; // ids of products being removed from the package
        public bool modifying; // returns true if this form is opened to modify an existing package

        List<Product> productList; //list of all products from DB
        List<Supplier> supplierList; // list of all suppliers from DB
       

        private void PackageEditing_Load(object sender, EventArgs e)
        {
            addedIds = new SortedList<int, int>();
            removedIds = new List<int>();
            productList = ProductDB.GetProducts();
            supplierList = SupplierDB.GetSupplier();

            displayPackage();
        }

        private void displayPackage()
        {

            txtId.Text = package.PackageId.ToString();
            txtName.Text = package.PkgName.ToString();
            txtDesc.Text = package.PkgDesc;
            dateTimePicker1.Value = package.PkgStartDate;
            dateTimePicker2.Value = package.PkgEndDate;
            txtBase.Text = package.PkgBasePrice.ToString();
            txtCommission.Text = package.PkgAgencyCommission.ToString();

            // finds name of products of the package and display in lstPackageProducts
            lstPackageProducts.Items.Clear();
            foreach (int id in package.ProductIds)
            {
                foreach (Product p in productList)
                {
                    if (p.ProductId == id)
                        lstPackageProducts.Items.Add(p.ProdName);
                }

            }
            //finds name of all products display them on lstAllProducts
            lstAllProducts.Items.Clear();
            foreach (Product p in productList)
            {
                lstAllProducts.Items.Add(p.ProdName);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // if user did not select either a product or a supplier
            if (lstAllProducts.SelectedItem == null || lstSuppliers.SelectedItem == null)
            {
                MessageBox.Show("Please select a product and a supplier");
            }
            else // user selected both
            {
                if (lstPackageProducts.Items.Contains(lstAllProducts.SelectedItem.ToString())) // if the user already added this product
                    MessageBox.Show("You have already added this product into the package");
                else
                {
                    int prodid = 0;
                    foreach (Product p in productList)
                    {
                        if (p.ProdName == lstAllProducts.SelectedItem.ToString())
                            prodid = p.ProductId;
                    }
                    // finds supplierId of the selected supplier
                    int supid = 0; // supplierId
                    foreach (Supplier s in supplierList)
                    {
                        if (s.SupName == lstSuppliers.SelectedItem.ToString())
                            supid = s.SupplierId;
                    }
                    addedIds.Add(prodid, supid);
                    lstPackageProducts.Items.Add(lstAllProducts.SelectedItem.ToString());
                    //lstPackageProducts.Items.Add(productList[prodid - 1].ProdName);
                }
            }
        }
        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lstPackageProducts.Items.Count > 0 && lstPackageProducts.SelectedItem != null)
            {
                string selectedName = lstPackageProducts.SelectedItem.ToString();
                int selectedId = 0;
                // finds id of the selected product
                foreach (Product p in productList)
                {
                    if (p.ProdName == selectedName)
                    {
                        selectedId = p.ProductId;
                        break;
                    }
                }
                // checks whether the selected product has been just added or already saved into the database
                if (addedIds.ContainsKey(selectedId)) // if the product has been just added
                {
                    addedIds.Remove(selectedId); // simply remove from the addedIds. No need to touch DB
                }
                else // if the product was saved into the database
                {
                    package.ProductIds.Remove(selectedId);
                    removedIds.Add(selectedId);
                }
                lstPackageProducts.Items.RemoveAt(lstPackageProducts.SelectedIndex);
            }
            else
            {
                MessageBox.Show("You have not selected a product to remove");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                try
                {
                    // get data from input boxes
                    package.PkgName = txtName.Text;
                    package.PkgStartDate = dateTimePicker1.Value;
                    package.PkgEndDate = dateTimePicker2.Value;
                    package.PkgDesc = txtDesc.Text;
                    package.PkgBasePrice = Convert.ToDouble(txtBase.Text);
                    package.PkgAgencyCommission = Convert.ToDouble(txtCommission.Text);


                    if (modifying)
                    {
                        if (addedIds.Count > 0) // if any product has been added to the package
                            foreach (KeyValuePair<int, int> pair in addedIds)
                            {
                                PackageDB.AddProductToPackage(package.PackageId, pair.Key, pair.Value);
                                package.ProductIds.Add(pair.Key);
                            }
                        if (removedIds.Count > 0)
                        {
                            try
                            {
                                foreach (int id in removedIds)
                                {
                                    PackageDB.RemoveProductFromPackage(package.PackageId, id);
                                }
                            }
                            catch
                            {
                                MessageBox.Show("Customers already purchased this package with the products you are trying to remove");
                            }
                        }
                        bool success = PackageDB.UpdatePackage(oldPackage, package);
                        if (success)
                            this.DialogResult = DialogResult.OK;
                        else
                            this.DialogResult = DialogResult.Retry;
                    }
                    else // creating a new package
                    {
                        PackageDB.AddPackage(package);
                        if (addedIds.Count > 0) // if any product has been added to the package

                            foreach (KeyValuePair<int, int> pair in addedIds)
                            {
                                PackageDB.AddProductToPackage(package.PackageId, pair.Key, pair.Value);
                                package.ProductIds.Add(pair.Key);
                            }
                        this.DialogResult = DialogResult.OK;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.GetType().ToString());
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        // display names of suppliers to whom the selected product belongs
        private void lstAllProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstSuppliers.Items.Clear();
            int productid = 0;
            // finds product id of the selected product
            foreach (Product p in productList)
            {
                if (p.ProdName == lstAllProducts.SelectedItem.ToString())
                    productid = p.ProductId;
            }
            foreach (Supplier s in SupplierDB.GetProductSupplier(productid))
            {
                lstSuppliers.Items.Add(s.SupName);

            }
        }



        // returns true if the form is validated 
        private bool ValidateForm()
        {

            bool result = true;
            double dummyVariable; // dummy variable that will store an output value from TryParse function
            // package date validation
            if (dateTimePicker1.Value > dateTimePicker2.Value)
            {
                MessageBox.Show("End date must be later than Start date");
                result = false;
            }
            // empty textbox validation
            else if (txtName.Text == "" || txtDesc.Text == "" || txtCommission.Text == "" || txtBase.Text == "")
            {
                MessageBox.Show("There is an empty input");
                result = false;
            }
            // format validation

            else if (!Double.TryParse(txtBase.Text, out dummyVariable))
            {
                MessageBox.Show("Base Price should be a number");
                result = false;
            }
            else if (!Double.TryParse(txtCommission.Text, out dummyVariable))
            {
                MessageBox.Show("Agency Commission should be a number");
                result = false;
            }
            // agency commission and base price
            else if (Convert.ToDouble(txtCommission.Text) > Convert.ToDouble(txtBase.Text))
            {
                MessageBox.Show("Agency Commission cannot be greater than Base Price");
                result = false;
            }
            else if (Convert.ToDouble(txtCommission.Text)<0 || Convert.ToDouble(txtBase.Text)<0)
            {
                MessageBox.Show("Agency Commission and Base Price must not be a negative number");
                result = false;
            }
            return result;
        }
    }

        
    }
