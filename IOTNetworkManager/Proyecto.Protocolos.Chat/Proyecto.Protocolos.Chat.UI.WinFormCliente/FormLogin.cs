﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Proyecto.Protocolos.Chat.Entidades;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Proyecto.Protocolos.Chat.UI.WinFormCliente.Properties;
using System.Threading;
using System.Net.NetworkInformation;
using Microsoft.Win32;
using System.IO;
using System.Management;
using System.Configuration;
namespace Proyecto.Protocolos.Chat.UI.WinFormCliente
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        ProtocoloCliente pcliente = new ProtocoloCliente();
        int numerointentos=0;
        
        equipo_has_mensaje unequipomensaje;
        bool exiteequipo=false;
        IPAddress ipAddress;
        IPAddress ipAddressServidor;
        equipo equipo;
        public equipo Equipo
        {
            get {
                return this.equipo;
            }
            set {
                this.equipo = value;
            }
        }
        grupo gruposequipo;
        public grupo Gruposequipo
        {
            get
            {
                return this.gruposequipo;
            }
            set
            {
                this.gruposequipo = value;
            }
        }
        Thread Escuchasala;  
        private void FormLogin_Load(object sender, EventArgs e)
        {
            IPHostEntry entry = Dns.GetHostByName(Dns.GetHostName().ToString());
            ipAddress = entry.AddressList[0];
            ConfiguracionEquipo.ConfigurarRegistro("IpCliente", ipAddress.ToString());
            ipAddressServidor = Dns.GetHostAddresses(ConfigurationManager.AppSettings.Get("IpServidor"))[0];            
            Escuchasala = new Thread(this.Escuchar);
            Escuchasala.Start();
            this.MostrarConfiguracion();
        }
        public void MostrarConfiguracion()
        {
           // ConfigurationManager.AppSettings.Get("IpServidor = "10212";
            //ConfigurationManager.AppSettings.Get("Save();
            Configuration cn = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            this.labelIp.Text =ConfigurationManager.AppSettings.Get("IPServidor");
            this.labelPuerto.Text = ConfigurationManager.AppSettings.Get("PuertoIngreso");
        }
        
        private void buttonIngresar_Click(object sender, EventArgs e)
        {
            this.EnviarMensajeValidacion();
            //Ocultamos el formulario
            this.Visible = false;
            //Hacemos visible el icono de la bandeja del sistema
            notifyIcon1.Visible = true;
            notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon1.BalloonTipTitle = "Administración SNMP";
            notifyIcon1.BalloonTipText = "Has minimizado la administración SNMP";
            notifyIcon1.ShowBalloonTip(1000);
            if (this.frmcli.Equipo != null)
            {
                this.frmcli.EnvioMensajeQuitarUsuarioOtro();
            }
            
            


            
        }
        public void EnviarMensajeValidacion()
        {
            try
            {

                equipo = this.leerventana();
                sala unsala = new sala(int.Parse(ConfigurationManager.AppSettings.Get("PuertoIngreso")));
                equipo.sala = unsala;
                mensaje mensaje = new mensaje(1, ipAddress.ToString(), ConfigurationManager.AppSettings.Get("EnviarTodos"), DateTime.Now, DateTime.Now.TimeOfDay, ConfigurationManager.AppSettings.Get("IMensajeConectar"), ConfigurationManager.AppSettings.Get("IMesajeValidacion"));
                unequipomensaje = new equipo_has_mensaje(mensaje, equipo);
                bool enviado = pcliente.EnviarMensaje(unequipomensaje, ipAddressServidor.ToString());
                if (enviado)
                {
                    this.Visible = false;

                    if (this.Equipo != null)
                    {
                       


                    }
                }
                else
                {

                    MessageBox.Show(ConfigurationManager.AppSettings.Get("MensajeNoenviado"), ConfigurationManager.AppSettings.Get("TInfo"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception error)
            {
                if (error.Message.Contains("No se puede establecer"))
                {
                    // codigo sensible

                    this.equipo = null;
                    if (this.frmcli.Equipo != null)
                    {
                        this.frmcli.Equipo = null;
                    }
                    MessageBox.Show(ConfigurationManager.AppSettings.Get("SinServidor"), ConfigurationManager.AppSettings.Get("TInfo"), MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                MessageBox.Show(error.Message);
            }  
        }
      
        public equipo leerventana()
        {
            if (this.textBoxContraseña.Text != "" && this.textBoxUsuario.Text != "")
            {
                return new equipo(this.textBoxUsuario.Text, this.textBoxContraseña.Text);
            }
            else
                throw new Exception(ConfigurationManager.AppSettings.Get("ParametroInvalido"));
        }
        FormCliente frmcli=new FormCliente();
        private void timerEscuchando_Tick(object sender, EventArgs e)
        {

            if (this.cerrar)
            {
                if (frmcli != null)
                {
                    frmcli.Close();
                    frmcli = new FormCliente();
                }
                this.equipo.pos = this.pos;
                this.equipo.IpCliente = this.ipAddress.ToString();
                this.equipo.Mac = GetMacAddressFromIPAddress.GetMacFromIP(this.ipAddress).ToString();
                this.equipo.Dominio = this.GrupoDeTrabajo();               
                this.equipo = unequipomensaje.equipo;
                this.equipo.Informacionbasica = InformacionBasica.GetInformacion(SystemInformation.PrimaryMonitorSize.ToString());
                this.Equipo.Activo = true;
                frmcli.Equipo = this.Equipo;
                frmcli.Show();
                this.cerrar = false;
                
            }
            
        }
        public string GrupoDeTrabajo()
        {
            string val="";
            SelectQuery query = new SelectQuery("Win32_ComputerSystem");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
            foreach (ManagementObject mo in searcher.Get())
            {
                if ((bool)mo["partofdomain"] != true)
                    val =mo["domain"].ToString();
                else
                    val= mo["domain"].ToString();
            }
            return val;
        }
        bool cerrar = false;
        public void Escuchar()
        {
            
            while (true)
            {
                
                if (this.pcliente.Stream != null)
                {
                    if (this.pcliente.Stream.CanRead && this.pcliente.Stream.DataAvailable)
                    {
                        IFormatter formatter = new BinaryFormatter();
                        //equipo sala desde el servidor
                        unequipomensaje = (equipo_has_mensaje)formatter.Deserialize(this.pcliente.Stream);
                        byte[] buff = new byte[unequipomensaje.mensaje.valor.Length];	//Declaro un arreglo de bytes
                        string str = "";					//Declaro una cadena
                        try
                        {
                           
                            this.pcliente.Stream.Read(buff, 0, buff.Length);	//Leo los bytes del Stream
                            str = System.Text.Encoding.ASCII.GetString(buff);//Convierto los bytes a Cadena
                            if (unequipomensaje.mensaje.metodo == ConfigurationManager.AppSettings.Get("IMesajeValidacion"))
                            {
                                if (bool.Parse(unequipomensaje.mensaje.valor))
                                {
                                    this.equipo = unequipomensaje.equipo;
                                    this.gruposequipo = unequipomensaje.equipo.sala.grupo;
                                    this.cerrar = true;
                                }
                                else
                                {
                                    if (this.numerointentos > 1)
                                    {
                                        this.equipo = null;
                                        this.ShowALgo();
                                        this.cerrar = true;
                                    }
                                    else
                                    {
                                        this.equipo = null;  
                                        this.numerointentos++;
                                        this.ShowALgo2();

                                    }
                                }
                            }
                            else
                            {
                                if (!bool.Parse(unequipomensaje.mensaje.valor))
                                {
                                    string cad = String.Format(ConfigurationManager.AppSettings.Get("UsuarioActivo"), this.equipo.NombreNick);
                                    MessageBox.Show(cad, ConfigurationManager.AppSettings.Get("TInfo"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }

                        }
                        catch (System.Exception X)
                        {
                            MessageBox.Show(ConfigurationManager.AppSettings.Get("MensajeNoenviado") + X.Message, ConfigurationManager.AppSettings.Get("TInfo"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                    }
                }
            }
        }
        public void ShowALgo()
        {
            MessageBox.Show(ConfigurationManager.AppSettings.Get("InvalidUser"));
        }
        public void ShowALgo2()
        {
            string cad = String.Format(ConfigurationManager.AppSettings.Get("InvalidUser") + " tiene {1} intentos", this.textBoxUsuario.Text.ToUpper(), 3 - this.numerointentos);
            MessageBox.Show(cad, ConfigurationManager.AppSettings.Get("TInfo"), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        





        private void FormLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            this.Escuchasala.Abort();
            this.pcliente.Cerrar();
            this.timerEscuchando.Stop();
           
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            //Ocultamos el icono de la bandeja de sistema
            notifyIcon1.Visible = false;
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void estadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!frmcli.IsDisposed)
            {
                if (frmcli.Equipo != null)
                {
                    frmcli.Visible = true;
                }
            }
            else
            {
                MessageBox.Show("Servidor desconectado", ConfigurationManager.AppSettings.Get("TInfo"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void opcionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Visible = true;
        }

        private void pictureBox26_Click(object sender, EventArgs e)
        {

        }
        Posicion pos;
        private void buttonSeleccion_Click(object sender, EventArgs e)
        {
            FormSeccionPosicionCliente frpos = new FormSeccionPosicionCliente();
            frpos.ShowDialog();
            pos=frpos.pos;
        }

        private void buttonPropiedades_Click(object sender, EventArgs e)
        {
            FormPropiedades frmpro = new FormPropiedades();
            frmpro.ShowDialog();
            if (frmpro.IpAddress != null)
            {
                this.ipAddress = frmpro.IpAddress;
                this.ipAddressServidor = frmpro.IpAddressServidor;
            }

        }
    }
}
