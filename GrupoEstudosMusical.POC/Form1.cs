﻿
using FastReport;
using GrupoEstudosMusical.Models.Entities;
using System;
using System.Collections.Generic;

using System.Windows.Forms;

namespace GrupoEstudosMusical.POC
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var report = new Report();
            var dados = new List<Ocorrencia>();
            report.RegisterData(dados, "Dados", 5);
            report.GetDataSource("Dados").Enabled = true;
            report.Design();
            report.Dispose();
        }
    }
}
