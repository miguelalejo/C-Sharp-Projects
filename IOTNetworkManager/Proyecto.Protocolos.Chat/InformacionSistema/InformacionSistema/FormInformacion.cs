﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ASOCKETLib;

namespace InformacionSistema
{
    public partial class FormInformacion : Form
    {
        public FormInformacion()
        {
            InitializeComponent();
        }
        string _ipequipo;
        public string Ipequipo
        {
            get
            {
                return this._ipequipo;
            }
            set
            {
                this._ipequipo = value;
            }
        }

        ClassConexionSnmp conexion = new ClassConexionSnmp("127.0.0.1");
        private void FormInformacion_Load(object sender, EventArgs e)
        {
            this.Mostrar();

            this.reportViewer4.RefreshReport();
        }
        public void Mostrar()
        {
            try
            {
                conexion = new ClassConexionSnmp(this._ipequipo);
                conexion.Open();
                SnmpObject snmpobj = conexion.Get("system.sysDescr.0");
                string val = snmpobj.Value;
                string[] info = val.Split('-');
                info[0] = info[0].Replace("Hardware: ", "");
                info[1] = info[1].Replace(" Software: ", "");
                this.labelWindowsWindows.Text = info[1];
                this.labelXfamili.Text = info[0];
                snmpobj = conexion.Get("system.sysUpTime.0");
                val = snmpobj.Value;
                float timestart = float.Parse(val);
                this.labelWindowsInicio.Text = timestart.ToString();
                snmpobj = conexion.Get("system.sysServices.0");
                val = snmpobj.Value;
                int nroser = int.Parse(val);
                this.labelWindowsInstalado.Text = nroser.ToString();
                snmpobj = conexion.Get("system.sysName.0");
                val = snmpobj.Value;
                string nombresis = val;
                this.labelWindowsUsuario.Text = nombresis;
                TablaDisco[] discos = this.Discos(this.TablaDiscos(conexion));
                this.dataGridViewDiscos.DataSource = discos;
                //this.dataGridViewDiscos.DataSource = this.TablaProgramasInicio(conexion);  
                //this.dataGridViewDiscos.DataSource = this.TablaProgramasInstalados(conexion);  
                //this.dataGridViewDiscos.DataSource = this.TablaDeviceTable(conexion);  
                //this.dataGridViewDiscos.DataSource = this.TablaNetworkTable(conexion);  
                //this.dataGridViewDiscos.DataSource = this.TablaProcessorTable(conexion);
                this.tablaDiscoBindingSource.DataSource = this.Discos(TablaDiscos(conexion));
                DeviceT[] dispsitivos = this.Dispositivos(this.TablaDeviceTable(conexion));
                this.labelResumenProcesador.Text = DeviceT.BuscarTipo(dispsitivos, "procesador").HrDeviceDescr;
                this.labelResumenRaton.Text = DeviceT.BuscarTipo(dispsitivos, "mouse").HrDeviceDescr;
                this.labelResumenTeclado.Text = DeviceT.BuscarTipo(dispsitivos, "teclado").HrDeviceDescr;
                TablaDisco tbdisco = TablaDisco.BuscarDisco(discos, "ram");
                this.labelResumenMemoria.Text = tbdisco.HrStorageSize.ToString() + " : " + tbdisco.HrStorageDescr;
                this.labelResumenPlacaBase.Text = info[0];
                this.labelResumenNucleos.Text = DeviceT.CantidadTipos(dispsitivos, "procesador").ToString();

                this.reportViewer1.RefreshReport();
                this.reportViewer2.RefreshReport();
                this.reportViewer2.RefreshReport();
                this.reportViewer3.RefreshReport();
                this.reportViewer3.RefreshReport();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message,"Información", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
            }
        }
        public DataTable TablaDiscos(ClassConexionSnmp unaconexion)
        {
            SnmpObject snmpobj1 = unaconexion.Get("1.3.6.1.2.1.25.2.3.1.1");
            SnmpObject snmpobj2 = unaconexion.Get("1.3.6.1.2.1.25.2.3.1.2");
            SnmpObject snmpobj3 = unaconexion.Get("1.3.6.1.2.1.25.2.3.1.3");
            SnmpObject snmpobj4 = unaconexion.Get("1.3.6.1.2.1.25.2.3.1.4");
            SnmpObject snmpobj5 = unaconexion.Get("1.3.6.1.2.1.25.2.3.1.5");
            SnmpObject snmpobj6 = unaconexion.Get("1.3.6.1.2.1.25.2.3.1.6");
            SnmpObject snmpobj7 = unaconexion.Get("1.3.6.1.2.1.25.2.3.1.7");
            DataTable tabla = new DataTable("Discos");
            tabla.Columns.Add("hrStorageIndex");
            tabla.Columns.Add("hrStorageType");
            tabla.Columns.Add("hrStorageDescr");
            tabla.Columns.Add("hrStorageAllocationUnits");
            tabla.Columns.Add("hrStorageSize");
            tabla.Columns.Add("hrStorageUsed");
            tabla.Columns.Add("hrStorageAllocationFailures");

            while (!snmpobj1.OID.Contains("hrStorageType"))
            {
                snmpobj1 = unaconexion.GetNext(snmpobj1.OID);
                snmpobj2 = unaconexion.GetNext(snmpobj2.OID);
                snmpobj3 = unaconexion.GetNext(snmpobj3.OID);
                snmpobj4 = unaconexion.GetNext(snmpobj4.OID);
                snmpobj5 = unaconexion.GetNext(snmpobj5.OID);
                snmpobj6 = unaconexion.GetNext(snmpobj6.OID);
                snmpobj7 = unaconexion.GetNext(snmpobj7.OID);
                if (!snmpobj1.OID.Contains("hrStorageType"))
                {

                    Object[] objeto = new object[7];
                    objeto[0] = int.Parse(snmpobj1.Value);
                    objeto[1] = snmpobj2.Value;
                    objeto[2] = snmpobj3.Value;
                    objeto[3] = double.Parse(snmpobj4.Value);
                    objeto[4] = double.Parse(snmpobj5.Value);
                    objeto[5] = double.Parse(snmpobj6.Value);
                    objeto[6] = double.Parse(snmpobj7.Value);
                    tabla.Rows.Add(objeto);
                }
                

            }
            return tabla;
        }
        public DataTable TablaInterfaces(int nrointer,ClassConexionSnmp unaconexion)
        {

            SnmpObject snmpobj1 = unaconexion.Get("1.3.6.1.2.1.2.2.1.1");
            SnmpObject snmpobj2 = unaconexion.Get("1.3.6.1.2.1.2.2.1.2");
            SnmpObject snmpobj3 = unaconexion.Get("1.3.6.1.2.1.2.2.1.3");
            SnmpObject snmpobj4 = unaconexion.Get("1.3.6.1.2.1.2.2.1.4");
            SnmpObject snmpobj5 = unaconexion.Get("1.3.6.1.2.1.2.2.1.5");
            SnmpObject snmpobj6 = unaconexion.Get("1.3.6.1.2.1.2.2.1.6");
            SnmpObject snmpobj7 = unaconexion.Get("1.3.6.1.2.1.2.2.1.7");
            SnmpObject snmpobj8 = unaconexion.Get("1.3.6.1.2.1.2.2.1.8");
            SnmpObject snmpobj9 = unaconexion.Get("1.3.6.1.2.1.2.2.1.9");
            SnmpObject snmpobj10 = unaconexion.Get("1.3.6.1.2.1.2.2.1.10");
            SnmpObject snmpobj11 = unaconexion.Get("1.3.6.1.2.1.2.2.1.11");
            SnmpObject snmpobj12 = unaconexion.Get("1.3.6.1.2.1.2.2.1.12");
            SnmpObject snmpobj13 = unaconexion.Get("1.3.6.1.2.1.2.2.1.13");
            SnmpObject snmpobj14 = unaconexion.Get("1.3.6.1.2.1.2.2.1.14");
            SnmpObject snmpobj15 = unaconexion.Get("1.3.6.1.2.1.2.2.1.15");
            SnmpObject snmpobj16 = unaconexion.Get("1.3.6.1.2.1.2.2.1.16");
            SnmpObject snmpobj17 = unaconexion.Get("1.3.6.1.2.1.2.2.1.17");
            SnmpObject snmpobj18 = unaconexion.Get("1.3.6.1.2.1.2.2.1.18");
            SnmpObject snmpobj19 = unaconexion.Get("1.3.6.1.2.1.2.2.1.19");
            SnmpObject snmpobj20 = unaconexion.Get("1.3.6.1.2.1.2.2.1.20");
            SnmpObject snmpobj21 = unaconexion.Get("1.3.6.1.2.1.2.2.1.21");
            SnmpObject snmpobj22 = unaconexion.Get("1.3.6.1.2.1.2.2.1.22");
            DataTable tabla = new DataTable("Interfaces");
            tabla.Columns.Add("ifIndex");
            tabla.Columns.Add("ifDescr");
            tabla.Columns.Add("ifType");
             tabla.Columns.Add("ifMtu");             
            tabla.Columns.Add("ifSpeed");
            tabla.Columns.Add("ifPhysAddress");
            tabla.Columns.Add("ifAdminStatus");
            tabla.Columns.Add("ifOperStatus");
            tabla.Columns.Add("ifLastChange");
            tabla.Columns.Add("ifInOctets");
            tabla.Columns.Add("ifInUcastPkts");
            tabla.Columns.Add("ifInNUcastPkts");
            tabla.Columns.Add("ifInDiscards");
            tabla.Columns.Add("ifInErrors");
            tabla.Columns.Add("ifInUnknownProtos");
            tabla.Columns.Add("ifOutOctets");
            tabla.Columns.Add("ifOutUcastPkts");
            tabla.Columns.Add("ifOutNUcastPkts");
            tabla.Columns.Add("ifOutDiscards");
            tabla.Columns.Add("ifOutErrors");
            tabla.Columns.Add("ifOutQLen");
            tabla.Columns.Add("ifSpecific");
           
        

            for (int i = 0; i < nrointer; i++)
            {
                snmpobj1 = unaconexion.GetNext(snmpobj1.OID);
                snmpobj2 = unaconexion.GetNext(snmpobj2.OID);
                snmpobj3 = unaconexion.GetNext(snmpobj3.OID);
                snmpobj4 = unaconexion.GetNext(snmpobj4.OID);
                snmpobj5 = unaconexion.GetNext(snmpobj5.OID);
                snmpobj6 = unaconexion.GetNext(snmpobj6.OID);
                snmpobj7 = unaconexion.GetNext(snmpobj7.OID);
                snmpobj8 = unaconexion.GetNext(snmpobj8.OID);
                snmpobj9 = unaconexion.GetNext(snmpobj9.OID);
                snmpobj10 = unaconexion.GetNext(snmpobj10.OID);
                snmpobj11 = unaconexion.GetNext(snmpobj11.OID);
                snmpobj12 = unaconexion.GetNext(snmpobj12.OID);
                snmpobj13 = unaconexion.GetNext(snmpobj13.OID);
                snmpobj14 = unaconexion.GetNext(snmpobj14.OID);
                snmpobj15 = unaconexion.GetNext(snmpobj15.OID);
                snmpobj16 = unaconexion.GetNext(snmpobj16.OID);
                snmpobj17 = unaconexion.GetNext(snmpobj17.OID);
                snmpobj18 = unaconexion.GetNext(snmpobj18.OID);
                snmpobj19 = unaconexion.GetNext(snmpobj19.OID);
                snmpobj20 = unaconexion.GetNext(snmpobj20.OID);
                Object[] objeto = new object[20];
                objeto[0] = snmpobj1.Value;
                objeto[1] = snmpobj2.Value;
                objeto[2] = snmpobj3.Value;
                objeto[3] = snmpobj4.Value;
                objeto[4] = snmpobj5.Value;
                objeto[5] = snmpobj6.Value;
                objeto[6] = snmpobj7.Value;
                objeto[7] = snmpobj8.Value;
                objeto[8] = snmpobj9.Value;
                objeto[9] = snmpobj10.Value;
                objeto[10] = snmpobj11.Value;
                objeto[11] = snmpobj12.Value;
                objeto[12] = snmpobj13.Value;
                objeto[13] = snmpobj14.Value;
                objeto[14] = snmpobj15.Value;
                objeto[15] = snmpobj16.Value;
                objeto[16] = snmpobj17.Value;
                objeto[17] = snmpobj18.Value;
                objeto[18] = snmpobj19.Value;
                objeto[19] = snmpobj20.Value;
                objeto[20] = snmpobj21.Value;
                objeto[21] = snmpobj22.Value;
                tabla.Rows.Add(objeto);

            }
            return tabla;
        }
        public DataTable TablaEstadoImpresoras(ClassConexionSnmp unaconexion)
        {
            SnmpObject snmpobj1 = unaconexion.Get("1.3.6.1.2.1.25.3.5.1.1");
            SnmpObject snmpobj2 = unaconexion.Get("1.3.6.1.2.1.25.3.5.1.2");
            DataTable tabla = new DataTable("Impresoras");
            tabla.Columns.Add("hrPrinterStatus");
            tabla.Columns.Add("hrPrinterDetectedErrorState");



            while (!snmpobj1.OID.Contains("hrPrinterDetectedErrorState"))
            {
                snmpobj1 = unaconexion.GetNext(snmpobj1.OID);
                snmpobj2 = unaconexion.GetNext(snmpobj2.OID);
                if (!snmpobj1.OID.Contains("hrPrinterDetectedErrorState"))
                {
                    Object[] objeto = new object[2];
                    objeto[0] = snmpobj1.Value;
                    objeto[1] = snmpobj2.Value.Replace("\0", "");
                    tabla.Rows.Add(objeto);
                }
            }
            return tabla;
        }
        public DataTable TablaParticionDiscos(ClassConexionSnmp unaconexion)
        {
            SnmpObject snmpobj1 = unaconexion.Get("1.3.6.1.2.1.25.3.7.1.1");
            SnmpObject snmpobj2 = unaconexion.Get("1.3.6.1.2.1.25.3.7.1.2");
            SnmpObject snmpobj3 = unaconexion.Get("1.3.6.1.2.1.25.3.7.1.3");
            SnmpObject snmpobj4 = unaconexion.Get("1.3.6.1.2.1.25.3.7.1.4");
            SnmpObject snmpobj5 = unaconexion.Get("1.3.6.1.2.1.25.3.7.1.5");

            DataTable tabla = new DataTable("DiscosParticion");
            tabla.Columns.Add("hrPartitionIndex");
            tabla.Columns.Add("hrPartitionLabel");
            tabla.Columns.Add("hrPartitionID");
            tabla.Columns.Add("hrPartitionSize");
            tabla.Columns.Add("hrPartitionFSIndex");


            while (!snmpobj1.OID.Contains("hrPartitionLabel"))
            {
                snmpobj1 = unaconexion.GetNext(snmpobj1.OID);
                snmpobj2 = unaconexion.GetNext(snmpobj2.OID);
                snmpobj3 = unaconexion.GetNext(snmpobj3.OID);
                snmpobj4 = unaconexion.GetNext(snmpobj4.OID);
                snmpobj5 = unaconexion.GetNext(snmpobj5.OID);

                if (!snmpobj1.OID.Contains("hrPartitionLabel"))
                {
                    Object[] objeto = new object[5];
                    objeto[0] = int.Parse(snmpobj1.Value);
                    objeto[1] = snmpobj2.Value;
                    string rep = snmpobj3.Value.Replace("", "").Replace("\0", "");
                    objeto[2] = rep;
                    objeto[3] = double.Parse(snmpobj4.Value);
                    objeto[4] = double.Parse(snmpobj5.Value);
                    tabla.Rows.Add(objeto);
                }


            }
            return tabla;
        }

        public TablaDisco[] Discos(DataTable lista)
        {
            TablaDisco[] temp = new TablaDisco[lista.Rows.Count];
            for(int i=0;i<temp.Length;i++)
            {

                object[] objeto = lista.Rows[i].ItemArray;
                temp[i] = new TablaDisco();
                temp[i].HrStorageIndex = int.Parse(objeto[0].ToString());
                temp[i].HrStorageType = objeto[1].ToString();
                temp[i].HrStorageDescr = objeto[2].ToString();
                temp[i].HrStorageAllocationUnits = double.Parse(objeto[3].ToString());
                temp[i].HrStorageSize = double.Parse(objeto[4].ToString());
                temp[i].HrStorageUsed = double.Parse(objeto[5].ToString());
                temp[i].HrStorageAllocationFailures = double.Parse(objeto[6].ToString());
            }
            return temp;
        }

        public DataTable TablaProgramasInicio(ClassConexionSnmp unaconexion)
        {
            SnmpObject snmpobj1 = unaconexion.Get("1.3.6.1.2.1.25.4.2.1.1");
            SnmpObject snmpobj2 = unaconexion.Get("1.3.6.1.2.1.25.4.2.1.2");
            SnmpObject snmpobj3 = unaconexion.Get("1.3.6.1.2.1.25.4.2.1.3");
            SnmpObject snmpobj4 = unaconexion.Get("1.3.6.1.2.1.25.4.2.1.4");
            SnmpObject snmpobj5 = unaconexion.Get("1.3.6.1.2.1.25.4.2.1.5");
            SnmpObject snmpobj6 = unaconexion.Get("1.3.6.1.2.1.25.4.2.1.6");
            SnmpObject snmpobj7 = unaconexion.Get("1.3.6.1.2.1.25.4.2.1.7");
            DataTable tabla = new DataTable("ProgramasInicio");
            tabla.Columns.Add("hrSWRunIndex");
            tabla.Columns.Add("hrSWRunName");
            tabla.Columns.Add("hrSWRunID");
            tabla.Columns.Add("hrSWRunPath");
            tabla.Columns.Add("hrSWRunParameters");
            tabla.Columns.Add("hrSWRunType");
            tabla.Columns.Add("hrSWRunStatus");

            while (!snmpobj1.OID.Contains("hrSWRunName"))
            {
                snmpobj1 = unaconexion.GetNext(snmpobj1.OID);
                snmpobj2 = unaconexion.GetNext(snmpobj2.OID);
                snmpobj3 = unaconexion.GetNext(snmpobj3.OID);
                snmpobj4 = unaconexion.GetNext(snmpobj4.OID);
                snmpobj5 = unaconexion.GetNext(snmpobj5.OID);
                snmpobj6 = unaconexion.GetNext(snmpobj6.OID);
                snmpobj7 = unaconexion.GetNext(snmpobj7.OID);
                if (!snmpobj1.OID.Contains("hrSWRunName"))
                {
                    Object[] objeto = new object[7];
                    objeto[0] = int.Parse(snmpobj1.Value);
                    objeto[1] = snmpobj2.Value;
                    objeto[2] = snmpobj3.Value;
                    objeto[3] = snmpobj4.Value;
                    objeto[4] = (snmpobj5.Value);
                    objeto[5] = int.Parse(snmpobj6.Value);
                    objeto[6] = int.Parse(snmpobj7.Value);
                    tabla.Rows.Add(objeto);
                }


            }
            return tabla;
        }
        


        public DataTable TablaProgramasInstalados(ClassConexionSnmp unaconexion)
        {
            SnmpObject snmpobj1 = unaconexion.Get("1.3.6.1.2.1.25.6.3.1.1");
            SnmpObject snmpobj2 = unaconexion.Get("1.3.6.1.2.1.25.6.3.1.2");
            SnmpObject snmpobj3 = unaconexion.Get("1.3.6.1.2.1.25.6.3.1.3");
            SnmpObject snmpobj4 = unaconexion.Get("1.3.6.1.2.1.25.6.3.1.4");
            SnmpObject snmpobj5 = unaconexion.Get("1.3.6.1.2.1.25.6.3.1.5");
            
            DataTable tabla = new DataTable("ProgramasInstalados");
            tabla.Columns.Add("hrSWInstalledIndex");
            tabla.Columns.Add("hrSWInstalledName");
            tabla.Columns.Add("hrSWInstalledID");
            tabla.Columns.Add("hrSWInstalledType");
            tabla.Columns.Add("hrSWInstalledDate");


            while (!snmpobj1.OID.Contains("hrSWInstalledName"))
            {
                snmpobj1 = unaconexion.GetNext(snmpobj1.OID);
                snmpobj2 = unaconexion.GetNext(snmpobj2.OID);
                snmpobj3 = unaconexion.GetNext(snmpobj3.OID);
                snmpobj4 = unaconexion.GetNext(snmpobj4.OID);
                snmpobj5 = unaconexion.GetNext(snmpobj5.OID);

                if (!snmpobj1.OID.Contains("hrSWInstalledName"))
                {
                    Object[] objeto = new object[5];
                    objeto[0] = int.Parse(snmpobj1.Value);
                    objeto[1] = snmpobj2.Value;
                    objeto[2] = snmpobj3.Value;
                    objeto[3] = int.Parse(snmpobj4.Value);
                    objeto[4] = snmpobj5.Value;
                    
                    tabla.Rows.Add(objeto);
                }


            }
            return tabla;
        }
        public ProgramasIns[] Programas(DataTable lista)
        {
            ProgramasIns[] temp = new ProgramasIns[lista.Rows.Count];
            for (int i = 0; i < temp.Length; i++)
            {

                object[] objeto = lista.Rows[i].ItemArray;
                temp[i] = new ProgramasIns();
                temp[i].HrSWInstalledIndex = int.Parse(objeto[0].ToString());
                temp[i].HrSWInstalledName = objeto[1].ToString();
                temp[i].HrSWInstalledID = objeto[2].ToString();
                temp[i].HrSWInstalledType = int.Parse(objeto[3].ToString());
                temp[i].HrSWInstalledDate =objeto[4].ToString();      





            }
            return temp;
        }

        public ProgInicio[] ProgramasIni(DataTable lista)
        {
            ProgInicio[] temp = new ProgInicio[lista.Rows.Count];
            for (int i = 0; i < temp.Length; i++)
            {

                object[] objeto = lista.Rows[i].ItemArray;
                temp[i] = new ProgInicio();
                temp[i].HrSWRunIndex = int.Parse(objeto[0].ToString());
                temp[i].HrSWRunName = objeto[1].ToString();
                temp[i].HrSWRunID = objeto[2].ToString();
                temp[i].HrSWRunPath = (objeto[3].ToString());
                temp[i].HrSWRunParameters = objeto[4].ToString();
                temp[i].HrSWRunType=int.Parse(objeto[5].ToString());
                temp[i].HrSWRunStatus=int.Parse(objeto[6].ToString());


            }
            return temp;
        }

        public DataTable TablaDeviceTable(ClassConexionSnmp unaconexion)
        {
            SnmpObject snmpobj1 = unaconexion.Get("1.3.6.1.2.1.25.3.2.1.1");
            SnmpObject snmpobj2 = unaconexion.Get("1.3.6.1.2.1.25.3.2.1.2");
            SnmpObject snmpobj3 = unaconexion.Get("1.3.6.1.2.1.25.3.2.1.3");
            SnmpObject snmpobj4 = unaconexion.Get("1.3.6.1.2.1.25.3.2.1.4");
            SnmpObject snmpobj5 = unaconexion.Get("1.3.6.1.2.1.25.3.2.1.5");
            SnmpObject snmpobj6 = unaconexion.Get("1.3.6.1.2.1.25.3.2.1.6");
           
            DataTable tabla = new DataTable("DeviceTable");
            tabla.Columns.Add("hrDeviceIndex");
            tabla.Columns.Add("hrDeviceType");
            tabla.Columns.Add("hrDeviceDescr");
            tabla.Columns.Add("hrDeviceID");
            tabla.Columns.Add("hrDeviceStatus");
            tabla.Columns.Add("hrDeviceErrors");


            while (!snmpobj1.OID.Contains("hrDeviceType"))
            {
                snmpobj1 = unaconexion.GetNext(snmpobj1.OID);
                snmpobj2 = unaconexion.GetNext(snmpobj2.OID);
                snmpobj3 = unaconexion.GetNext(snmpobj3.OID);
                snmpobj4 = unaconexion.GetNext(snmpobj4.OID);
                snmpobj5 = unaconexion.GetNext(snmpobj5.OID);
                snmpobj6 = unaconexion.GetNext(snmpobj6.OID);

                if (!snmpobj1.OID.Contains("hrDeviceType"))
                {
                    Object[] objeto = new object[6];
                    if (snmpobj1.GetType() != Type.GetType("Int"))
                    {

                        objeto[0] = int.Parse(snmpobj1.Value);
                        objeto[1] = snmpobj2.Value;
                        objeto[2] = snmpobj3.Value;
                        objeto[3] = snmpobj4.Value;
                        objeto[4] = int.Parse(snmpobj5.Value);
                        objeto[5] = int.Parse(snmpobj6.Value);

                        tabla.Rows.Add(objeto);
                    }
                }
            }
            return tabla;
        }
        public DeviceT[] Dispositivos(DataTable lista)
        {
            DeviceT[] temp = new DeviceT[lista.Rows.Count];
            for (int i = 0; i < temp.Length; i++)
            {

                object[] objeto = lista.Rows[i].ItemArray;
                temp[i] = new DeviceT();
              
                temp[i].HrDeviceIndex = int.Parse(objeto[0].ToString());
                temp[i].HrDeviceType = objeto[1].ToString();
                temp[i].HrDeviceDescr = objeto[2].ToString();
                temp[i].HrDeviceID = objeto[3].ToString();
                temp[i].HrDeviceStatus = int.Parse(objeto[4].ToString());
                temp[i].HrDeviceErrors = int.Parse(objeto[5].ToString());


            }
            return temp;
        }

        public DataTable TablaProcessorTable(ClassConexionSnmp unaconexion)
        {
            SnmpObject snmpobj1 = unaconexion.Get("1.3.6.1.2.1.25.3.3.1.1");
            SnmpObject snmpobj2 = unaconexion.Get("1.3.6.1.2.1.25.3.3.1.2");
            

            DataTable tabla = new DataTable("ProcessorTable");
            tabla.Columns.Add("hrProcessorFrwID");
            tabla.Columns.Add("hrProcessorLoad");


            while (!snmpobj1.OID.Contains("hrProcessorLoad"))
            {
                snmpobj1 = unaconexion.GetNext(snmpobj1.OID);
                snmpobj2 = unaconexion.GetNext(snmpobj2.OID);

                if (!snmpobj1.OID.Contains("hrProcessorLoad"))
                {
                    Object[] objeto = new object[2];
                    if (snmpobj1.GetType() != Type.GetType("Int"))
                    {

                        objeto[0] = snmpobj1.Value;
                        objeto[1] = int.Parse(snmpobj2.Value);
                        
                        tabla.Rows.Add(objeto);
                    }
                }


            }
            return tabla;
        }

        public DataTable TablaNetworkTable(ClassConexionSnmp unaconexion)
        {
            SnmpObject snmpobj1 = unaconexion.Get("1.3.6.1.2.1.25.3.4.1.1");
           
            DataTable tabla = new DataTable("NetworkTable");
            tabla.Columns.Add("hrNetworklfIndex");
            


            while (!snmpobj1.OID.Contains("hrPrinterTable"))
            {
                snmpobj1 = unaconexion.GetNext(snmpobj1.OID);
                
                if (!snmpobj1.OID.Contains("hrPrinterTable"))
                {
                    Object[] objeto = new object[1];
                    if (snmpobj1.GetType() != Type.GetType("Int"))
                    {

                        objeto[0] = int.Parse(snmpobj1.Value);
                        
                        tabla.Rows.Add(objeto);
                    }
                }
            }
            return tabla;
        }
        
        private void dataGridViewDiscos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void comboBoxUnidadesDeDisco_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                TablaDisco disco = this.comboBoxUnidadesDeDisco.SelectedItem as TablaDisco;
                this.labelUnidadesdeDiscoTotal.Text = disco.HrStorageSize.ToString();
                this.labelUnidadesDeDiscoLibre.Text = disco.HrStorageFree.ToString();
                this.labelUnidadesDeDiscoOcupado.Text = disco.HrStorageUsed.ToString();
                this.BindingSource.DataSource = disco;
                this.reportViewer1.RefreshReport();
            }
            catch
            {
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {
            
            
        }

        private void tabPage1_Enter(object sender, EventArgs e)
        {
            ProgramasIns[] prog = this.Programas(this.TablaProgramasInstalados(conexion));
            this.bindingSourceInstal.DataSource = prog;
            this.reportViewer2.RefreshReport();
        }

        private void ProgramasInicio_Click(object sender, EventArgs e)
        {

        }

        private void ProgramasInicio_Enter(object sender, EventArgs e)
        {
            ProgInicio[] prog = this.ProgramasIni(this.TablaProgramasInicio(conexion));
            this.bindingSourceProgrInicio.DataSource=prog;
            this.reportViewer3.RefreshReport();
        }

        private void buttonConec_Click(object sender, EventArgs e)
        {
            string ip = this.textBoxIP.Text;
            conexion = new ClassConexionSnmp(ip);
            this.Mostrar();
            MessageBox.Show("Bienvenido esta conectado a : "+ip, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
        }

        private void FormInformacion_Enter(object sender, EventArgs e)
        {

        }

        private void HardwareDeSistema_Enter(object sender, EventArgs e)
        {
            DeviceT[] dispsitivos = this.Dispositivos(this.TablaDeviceTable(conexion));
            this.bindingSourceDispositi.DataSource = dispsitivos;
            this.reportViewer4.RefreshReport();
        }


    }
}
