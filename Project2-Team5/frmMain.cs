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
     * This file has many Authors
     * Author: Chris, Sunghyun, Luke, Marc
     * Created: 2018-07
     */
    public partial class frmMain : Form
    {
        const int PACKAGE_EDIT_INDEX = 5; // column in the grid that contains Edit buttons
        const int PRODUCT_EDIT_INDEX = 1; // column in the Product grid that contains Edit buttons
        const int SUPPLIER_EDIT_INDEX = 2; // column in the Supplier grid that contains Edit buttons --Luke
        public frmMain()
        {
            InitializeComponent();
        }
        List<Package> packageList; // all packages that exist in the database
        List<Product> productList; // all products that exist in the database
        List<Supplier> supplierList; // all supplier that exist in the database --Luke
        Package oldPackage;
        Product oldProduct;
        Supplier oldSupplier; 

        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            packageList = PackageDB.GetPackages();
            productList = ProductDB.GetProducts();
            supplierList = SupplierDB.GetSupplier();

            DisplayPackages();
            DisplayProducts();
            DisplaySuppliers();


            PackagesPanelButtonOn();
        }

        // display packages on data grid view --Sunghyun lee
        private void DisplayPackages()
        {// Sunghyun Lee
            dataGridView1.Rows.Clear();
            foreach (Package pkg in packageList)
            {

                string productNames = "";
                
                foreach (int id in pkg.ProductIds)
                {
                    string productName = "";
                    foreach (Product p in productList)
                    {
                        if (p.ProductId == id)
                            productName = p.ProdName;
                    }
                    productNames += productName + ".  ";
                }
                dataGridView1.Rows.Add(pkg.PackageId, pkg.PkgName, pkg.PkgStartDate.ToString("yyyy-MM-dd"), pkg.PkgEndDate.ToString("yyyy-MM-dd"), productNames);

            }
        }

        //display products --Chris
        private void DisplayProducts()
        {
            dgvProducts.Rows.Clear();   // clear product gatagrid view before populating
            foreach (Product prod in productList)
            {
                if (prod != null)
                    dgvProducts.Rows.Add(prod.ProdName);  //add products to datagrid view  
            }

            //make all columns not sortable as this will cause problems with what the user thinks
            //they are clicking and what is really being clicked after sorting
            foreach (DataGridViewColumn column in dgvProducts.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }


        // display suppliers on data grid view --Luke
        private void DisplaySuppliers()
        {
            supplierList = SupplierDB.GetSupplier();
            dgvSuppliers.Rows.Clear();
            foreach(Supplier supp in supplierList)
            {
                //string supplierNames = "";
                //foreach (int id in supp.SupplierIds)
                //    supplierNames += supplierList[id - 1].SupName + ".  ";
                if (supp != null)
                    dgvSuppliers.Rows.Add(supp.SupplierId, supp.SupName);
            }
        }
        // when user clicks an edit button, opens a frmUpdatePackage with 'modifying=true' -- Sunghyun
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {// Sunghyun Lee
            if (e.ColumnIndex == PACKAGE_EDIT_INDEX && e.RowIndex != -1)
            {
                oldPackage = packageList[e.RowIndex].CopyPackage(); // make a  separate copy before update
                frmUpdatePackage updateForm = new frmUpdatePackage();
                //updateForm.package = packageList[e.RowIndex]; // "pass" current customer to the form
                int pkgId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                foreach (Package pkg in packageList)
                {
                    if (pkg.PackageId == pkgId)
                        updateForm.package = pkg;
                }
                updateForm.oldPackage = oldPackage;        // same for the original customer data
                updateForm.modifying = true; // the program is modifying an existing package
                DialogResult result = updateForm.ShowDialog(); // display modal second form
                if (result == DialogResult.OK) // update accepted
                {
                    // refresh the grid view
                    DisplayPackages();

                }
                else // update cancelled
                {
                    packageList[e.RowIndex] = oldPackage; // revert to the old values
                }
            }
        }
        // create a new package -- Sunghyun Lee
        private void btnAddPackage_Click(object sender, EventArgs e)
        {//Sunghyun
            frmUpdatePackage updateForm = new frmUpdatePackage();
            updateForm.modifying = false;
            // initialize a new package
            updateForm.package = new Package();
            updateForm.package.PackageId = 0;
            updateForm.package.PkgName = "";    
            updateForm.package.PkgStartDate = DateTime.Today;
            updateForm.package.PkgEndDate = DateTime.Today;
            updateForm.package.PkgBasePrice = 0;
            updateForm.package.PkgAgencyCommission = 0;
            updateForm.package.ProductIds = new List<int>();




            DialogResult result = updateForm.ShowDialog(); // display second form

            if (result == DialogResult.OK) // update accepted
            {
                packageList.Add(updateForm.package);
            }
            // refresh the grid view
            DisplayPackages();
        }

        //when user clicks on the edit button in the products data grid view -- Chris
        private void dgvProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //make sure when edit column header is clicked, do nothing as this would crash program
            //When an edit button is clicked
            if (e.ColumnIndex == PRODUCT_EDIT_INDEX && e.RowIndex != -1)
            {
                oldProduct = productList[e.RowIndex].CopyProduct(); // make a  separate copy before update
                frmUpdateProduct updateForm = new frmUpdateProduct();
                updateForm.product = productList[e.RowIndex]; // "pass" current customer to the form
                DialogResult result = updateForm.ShowDialog(); // display modal second form
                if (result == DialogResult.OK) // update accepted
                {
                    // refresh the grid view
                    DisplayProducts();
                }
            }
        }

        //When Add Product button is clicked -- Chris
        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            frmUpdateProduct updateForm = new frmUpdateProduct();
            // initialize a new product
            updateForm.product = new Product();
            updateForm.product.ProductId = 0;   //set as zero, this does not matter (autoincriment on SQL insert), but will show as noneditable
            updateForm.product.ProdName = "";  //blank package name, user will enter for this

            DialogResult result = updateForm.ShowDialog(); // display second form

            if (result == DialogResult.OK) // update accepted
            {
                //refresh profucts displayed manin form
                productList = ProductDB.GetProducts();
                DisplayProducts();
            }
        }

        /////////////////////////   start Luke          /////////////////////////////

        private void dgvSuppliers_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == SUPPLIER_EDIT_INDEX)
            {
                oldSupplier = supplierList[e.RowIndex].CopySupplier(); // make a  separate copy before update
                frmUpdateSupplier updateForm = new frmUpdateSupplier();
                updateForm.supplier = supplierList[e.RowIndex]; 
                updateForm.oldSupplier = oldSupplier;       
                updateForm.modifying = true; // the program is modifying an existing supplier
                DialogResult result = updateForm.ShowDialog(); // display modal second form
                if (result == DialogResult.OK) // update accepted
                {
                    // refresh the grid view
                    DisplaySuppliers();

                }
                else // update cancelled
                {
                    supplierList[e.RowIndex] = oldSupplier; // revert to the old values
                }
            }
        }

        private void btnAddSupplier_Click_1(object sender, EventArgs e)
        {
            frmUpdateSupplier updateForm = new frmUpdateSupplier();
            // initialize a new Supplier
            updateForm.supplier = new Supplier();
            updateForm.supplier.SupplierId = 0;           
            updateForm.supplier.SupName = "";           
            
            DialogResult result = updateForm.ShowDialog(); // display second form

            if (result == DialogResult.OK) // update accepted
            {
                DisplaySuppliers();
            }
           
        }



        // Marc Javier - UI Modifications

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void PackagesPanelButtonOn()
        {
            panelPackages.BackColor = System.Drawing.Color.White;
            lblPanelPackages.ForeColor = System.Drawing.Color.SlateBlue;
            panelBoardPackages.Visible = true;
            panelBoardPackages.Location = new Point(450, 80);
            panelBoardPackages.Size = new Size(850, 600);
            btnAddPackage.Visible = true;
            btnAddPackage.Location = new Point(300, 80);
        }

        private void PackagesPanelButtonOff()
        {
            panelPackages.BackColor = System.Drawing.Color.SlateBlue;
            lblPanelPackages.ForeColor = System.Drawing.Color.White;
            panelBoardPackages.Visible = false;
            btnAddPackage.Visible = false;
        }

        private void ProductsPanelButtonOn()
        {
            panelProducts.BackColor = System.Drawing.Color.White;
            lblPanelProducts.ForeColor = System.Drawing.Color.SlateBlue;
            panelBoardProducts.Visible = true;
            panelBoardProducts.Location = new Point(450, 80);
            panelBoardProducts.Size = new Size(850, 600);
            btnAddProduct.Visible = true;
            btnAddProduct.Location = new Point(300, 80);
        }

        private void ProductsPanelButtonOff()
        {
            panelProducts.BackColor = System.Drawing.Color.SlateBlue;
            lblPanelProducts.ForeColor = System.Drawing.Color.White;
            panelBoardProducts.Visible = false;
            btnAddProduct.Visible = false;
        }

        private void SuppliersPanelButtonOn()
        {
            panelSuppliers.BackColor = System.Drawing.Color.White;
            lblPanelSuppliers.ForeColor = System.Drawing.Color.SlateBlue;
            panelBoardSupplier.Visible = true;
            panelBoardSupplier.Location = new Point(450, 80);
            panelBoardSupplier.Size = new Size(850, 600);
            btnAddSupplier.Visible = true;
            btnAddSupplier.Location = new Point(300, 80);
        }

        private void SuppliersPanelButtonOff()
        {
            panelSuppliers.BackColor = System.Drawing.Color.SlateBlue;
            lblPanelSuppliers.ForeColor = System.Drawing.Color.White;
            panelBoardSupplier.Visible = false;
            btnAddSupplier.Visible = false;
        }

        private void panelPackages_Click(object sender, EventArgs e)
        {
            PackagesPanelButtonOn();
            ProductsPanelButtonOff();
            SuppliersPanelButtonOff();
        }

        private void panelProducts_Click(object sender, EventArgs e)
        {
            ProductsPanelButtonOn();
            PackagesPanelButtonOff();
            SuppliersPanelButtonOff();
        }

        private void panelSuppliers_Click(object sender, EventArgs e)
        {
            SuppliersPanelButtonOn();
            ProductsPanelButtonOff();
            PackagesPanelButtonOff();
        }

        private void lblPanelPackages_Click(object sender, EventArgs e)
        {
            PackagesPanelButtonOn();
            ProductsPanelButtonOff();
            SuppliersPanelButtonOff();
        }

        private void lblPanelProducts_Click(object sender, EventArgs e)
        {
            ProductsPanelButtonOn();
            PackagesPanelButtonOff();
            SuppliersPanelButtonOff();
        }

        private void lblPanelSuppliers_Click(object sender, EventArgs e)
        {
            SuppliersPanelButtonOn();
            ProductsPanelButtonOff();
            PackagesPanelButtonOff();
        }
    }
}
