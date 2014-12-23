﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ReportsApplication6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var cds = new CustomerDataSources();
            var customers = cds.GetAllCustomers();
            this.BindingSource.DataSource = customers;
            this.reportViewer1.RefreshReport();
        }
    }
}