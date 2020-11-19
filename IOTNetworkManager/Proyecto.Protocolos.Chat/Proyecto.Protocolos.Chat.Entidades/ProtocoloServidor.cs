﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;


namespace Proyecto.Protocolos.Chat.Entidades
{
    public class ProtocoloServidor
    {
        TcpListener Listener;	//Para habilitaru puerto
        TcpClient cliente;		//Para obtener el cliente
        NetworkStream stream; //Para el fichero de intercambio
        TcpClient[] listaCliente;
        NetworkStream[] listaStream;
        Thread Iniciosala;
        ArrayList listaeerrores=new ArrayList();
        IPAddress ipAddressservidor;
        private equipo_has_mensaje[] _listaequipossala;
        private sala _sala;
        public sala sala
        {
            get {
                return this._sala;
            }
            set {
                this._sala = value;
            }

        }
        public equipo_has_mensaje[] listaequipossala
        {
            get {
                return this._listaequipossala;
            }
            set {
                this._listaequipossala = value;
            }
        }
        Int32 _puerto;
        Int32 Puerto
        {
            get {
                return this._puerto;
            }
            set
            {
                this._puerto = value;
            }
        }


        public TcpClient Cliente
        {
            get {
                return this.cliente;
            }
            set {
                this.cliente = value;
            }
        }
        public NetworkStream Stream
        {
            get
            {
                return this.stream;
            }
            set
            {
                this.stream= value;
            }
        }
        public TcpClient[] ListaCliente
        {
            get
            {
                return this.listaCliente;
            }
            set
            {
                this.listaCliente = value;
            }
        }
        public NetworkStream[] ListaStream
        {
            get
            {
                return this.listaStream;
            }
            set
            {
                this.listaStream= value;
            }
        }
        public ProtocoloServidor(IPAddress ipAddress, Int32 port)
        {
            this.ipAddressservidor = ipAddress;
            this._puerto = port;
            this.Listener = new System.Net.Sockets.TcpListener(ipAddress, port);	//Inicializo el TCPListener en el puerto 2020
            this.Listener.Start();										//Pongo el puerto en Escucha
            Iniciosala = new Thread(this.Leer);
            Iniciosala.Start();
        }
        public ProtocoloServidor()
        {
           
        }
        public void Leer()
        {
            while (true)
            {
                this.cliente = this.Listener.AcceptTcpClient();
                this.stream = this.cliente.GetStream();

                //byte[] buff = new byte[256]; //Declaro arreglo de bytes para almacenar lo recibido
                //if (this.listaCliente == null)
                //{
                //    buff = System.Text.Encoding.ASCII.GetBytes("Hola ya te conectaste");//Convierto la cadena de Saludo a Bytes	
                //    if (stream.CanWrite)									//Si puedo escribir Escribo
                //        stream.Write(buff, 0, buff.Length);
                //}
                //else
                //{
                //    if (!this.Existe(this.cliente))
                //    {
                //        buff = System.Text.Encoding.ASCII.GetBytes("Hola ya te conectaste");//Convierto la cadena de Saludo a Bytes	
                //        if (stream.CanWrite)									//Si puedo escribir Escribo
                //            stream.Write(buff, 0, buff.Length);
                //    }
                //}
                this.LlenarClientesStream(this.cliente, this.stream);

            }


        }
        public void LlenarListaequipos(equipo_has_mensaje unequipo)
        {
            
            if (this._listaequipossala!=null&&this._listaequipossala.Length > 0)
            {
                bool llenar = true;
                for (int i = 0; i < this._listaequipossala.Length; i++)
                {
                    if (this._listaequipossala[i].equipo.idEquipo == unequipo.equipo.idEquipo)
                    {
                        llenar = false;
                                            
                    }
                }
                if (llenar)
                {
                    equipo_has_mensaje[] tempus = new equipo_has_mensaje[this._listaequipossala.Length + 1];
                    this._listaequipossala.CopyTo(tempus, 0);
                    tempus[this._listaequipossala.Length] = unequipo;
                    this._listaequipossala = tempus;
                }
               
            }
            else
            {
                this._listaequipossala = new equipo_has_mensaje[1];
                this._listaequipossala[0] = unequipo;
            }
        }
        public void QuitarListaequipos(equipo_has_mensaje unequipo)
        {
            if (this._listaequipossala != null)
            {
                if(this._listaequipossala.Length > 1)
                {
                    if(this.existeEquipo(unequipo.equipo))
                    {
                    equipo_has_mensaje[] tempus = new equipo_has_mensaje[this._listaequipossala.Length - 1];
                    for (int i = 0, k = 0; i < this._listaequipossala.Length; i++)
                    {
                        if (this._listaequipossala[i].equipo.idEquipo != unequipo.equipo.idEquipo)
                        {

                            tempus[k] = this._listaequipossala[i];
                            k++;
                        }
                    }
                    this._listaequipossala = tempus;
                    }
                }
                else{
                    this._listaequipossala = null;
                }

            }       

            
        }
        public bool existeEquipo(equipo unequipo)
        {
            for (int i = 0, k = 0; i < this._listaequipossala.Length; i++)
            {
                if (this._listaequipossala[i].equipo.idEquipo == unequipo.idEquipo)
                {

                    return true;
                }
            }
            return false;
        }
        public void LlenarClientesStream(TcpClient un_Cliente, NetworkStream un_Stream)
        {
            if (this.listaCliente != null)
            {
                if (!this.Existe(un_Cliente))
                {
                    TcpClient[] tempC = new TcpClient[this.listaCliente.Length + 1];
                    NetworkStream[] tempS = new NetworkStream[this.listaStream.Length + 1];
                    this.listaCliente.CopyTo(tempC, 0);
                    this.listaStream.CopyTo(tempS, 0);
                    tempC[this.listaCliente.Length] = un_Cliente;
                    tempS[this.listaStream.Length] = un_Stream;
                    this.listaCliente = tempC;
                    this.listaStream = tempS;
                }
                else
                {
                    this.Llenar(un_Cliente, un_Stream);
                }
            }
            else
            {
                this.listaCliente = new TcpClient[1];
                this.listaStream = new NetworkStream[1];
                this.listaCliente[0] = un_Cliente;
                this.listaStream[0] = un_Stream;
            }

        }
        public bool Existe(TcpClient temp)
        {

            for (int i = 0; i < this.listaCliente.Length; i++)
            {
                IPEndPoint ipend = (IPEndPoint)temp.Client.RemoteEndPoint;
                IPEndPoint iplista = (IPEndPoint)this.listaCliente[i].Client.RemoteEndPoint;
                if (ipend.Address.Equals(iplista.Address))
                    return true;
            }
            return false;
        }
        public void Llenar(TcpClient un_Cliente, NetworkStream un_Stream)
        {
            TcpClient[] tempC = this.listaCliente;
            NetworkStream[] tempS = this.listaStream;
            for (int i = 0; i < this.listaCliente.Length; i++)
            {
                IPEndPoint ipend = (IPEndPoint)un_Cliente.Client.RemoteEndPoint;
                IPEndPoint iplista = (IPEndPoint)tempC[i].Client.RemoteEndPoint;
                if (ipend.Address.Equals(iplista.Address))
                {

                    tempC[i] = un_Cliente;
                    tempS[i] = un_Stream;
                }
                else
                    tempC[i] = this.listaCliente[i];
                tempS[i] = this.listaStream[i];
            }
            this.listaCliente = tempC;
            this.listaStream = tempS;

        }


        public int NumeroClientes()
        {
            int numero = 0;
            for (int i = 0; i < this.listaCliente.Length; i++)
            {

                if (this.listaCliente[i] != null && this.listaCliente[i].Client.Connected)
                {

                    numero++;
                }

            }
            return numero;
        }
        public void Quitarclientes()
        {

            if (this.listaCliente != null)
            {

                int n = this.NumeroClientes();
                TcpClient[] tempC = new TcpClient[n];
                NetworkStream[] tempS = new NetworkStream[n];
                for (int i = 0, k = 0; i < this.listaCliente.Length; i++)
                {
                    if (this.listaCliente[i] != null && this.listaCliente[i].Client.Connected)
                    {
                        tempC[k] = this.listaCliente[i];
                        tempS[k] = this.listaStream[i];
                        k++;
                    }
                }
                this.listaCliente = null;
                this.listaStream = null;
                this.listaCliente = tempC;
                this.listaStream = tempS;

            }


        }

        public void Cerrar()
        {
            Iniciosala.Abort();
            if(this.cliente!=null)
                this.cliente.Close();
            if(this.stream!=null)
                this.stream.Close();
            this.Listener.Stop();
        }
        public equipo_has_mensaje Escuchar()
        {
            equipo_has_mensaje equiposalatemp=null;
            if(this.cliente!=null)
                this.stream = this.cliente.GetStream();
            if (stream != null) //Si hay conexion (existe el cliente)
            {
                if (stream.CanRead && stream.DataAvailable) //Si  puedo leer y hay informacion disponible
                {


                   
                        IFormatter formatter = new BinaryFormatter();                        
                        //Usuario sala desde el servidor                         
                        equiposalatemp = (equipo_has_mensaje)formatter.Deserialize(stream);
                        
                                                
                    
                }
            }
            return equiposalatemp;
        }
        public void EscucharEnviarOtrosClientes(equipo_has_mensaje un_equiposala)
        {
            
            if (this.listaStream != null)
            {
                //
                for (int i = 0; i < this.listaStream.Length; i++)
                {
                    TcpClient temp = this.listaCliente[i];
                    if (this.listaCliente[i].Client.Connected)
                    {
                        
                        NetworkStream strm = this.listaCliente[i].GetStream();
                        
                        if (!this.stream.Equals(strm))
                        {
                            try
                            {
                                if (strm.CanWrite)
                                {
                                    IFormatter formatter = new BinaryFormatter();
                                    formatter.Serialize(strm, un_equiposala);
                                    byte[] buff = new byte[un_equiposala.mensaje.valor.Length];
                                    buff = System.Text.Encoding.ASCII.GetBytes(un_equiposala.mensaje.valor);
                                    strm.Write(buff, 0, (int)buff.Length);
                                }
                            }
                            catch (Exception error)
                            {
                                
                                IPEndPoint iplista = (IPEndPoint)temp.Client.RemoteEndPoint;
                                this.Quitarclientes();
                                this.listaeerrores.Add(error.Message + "," + iplista.Address.ToString() + " estado: " + cliente.Connected.ToString());

                            }

                        }
                        
                    }

                }
            }
        }
        public void EnviarMensajeequiposConectados(equipo_has_mensaje un_equiposala)
        {
            if (this.listaStream != null)
            {
                for (int i = 0; i < this.listaStream.Length; i++)
                {

                    TcpClient clientetemp = this.listaCliente[i];


                    if (this.listaCliente[i].Connected)
                    {

                        try
                        {

                            //cliente.Connect(this.ipAddressservidor, un_equiposala.sala.puerto);
                            NetworkStream str = clientetemp.GetStream();
                            if (str.CanWrite && str != null)
                            {
                                IFormatter formatter = new BinaryFormatter();
                                formatter.Serialize(str, un_equiposala);
                                byte[] buff = new byte[un_equiposala.mensaje.valor.Length];
                                buff = System.Text.Encoding.ASCII.GetBytes(un_equiposala.mensaje.valor);
                                str.Write(buff, 0, (int)buff.Length);


                            }


                        }
                        catch (Exception error)
                        {

                            this.Quitarclientes();
                            IPEndPoint iplista = (IPEndPoint)cliente.Client.RemoteEndPoint;
                            this.listaeerrores.Add(error.Message + "," + iplista.Address.ToString() + " estado: " + cliente.Connected.ToString());

                        }


                    }
                }
            }
         
        }
       
        public void EnviarMensajeequiposConectadosLista(equipo equipo,sala sala,equipo admin,string cad)
        {
            if (this.listaStream != null)
            {
                for (int i = 0; i < this.listaStream.Length; i++)
                {

                    TcpClient clientetemp = this.listaCliente[i];


                    if (this.listaCliente[i].Connected)
                    {

                        try
                        {
                               equipo_has_mensaje un_equiposala = new equipo_has_mensaje(0, null, admin, DateTime.Now, DateTime.Now);
                            //this.textBoxMensaje.Text = unequipomensaje.equipo.NombreNick.ToUpper() + " dice:\n\t" + this.textBoxMensaje.Text;
                           // ******en esta linea cambiar equipo segun la sala******
                               un_equiposala.mensaje = new mensaje(0, admin.IpCliente, equipo.IpCliente, DateTime.Now, DateTime.Now.TimeOfDay, cad, "mensaje");
                               un_equiposala.mensaje.cabecera = un_equiposala.equipo.NombreNick.ToUpper() + " dice:\n\t";
                            

                            //cliente.Connect(this.ipAddressservidor, un_equiposala.sala.puerto);
                            NetworkStream str = clientetemp.GetStream();
                            if (str.CanWrite && str != null)
                            {
                                IFormatter formatter = new BinaryFormatter();
                                formatter.Serialize(str, un_equiposala);

                                 byte[] buff = new byte[un_equiposala.mensaje.valor.Length];
                                buff = System.Text.Encoding.ASCII.GetBytes(un_equiposala.mensaje.valor);
                                str.Write(buff, 0, (int)buff.Length);


                            }


                        }
                        catch (Exception error)
                        {

                            this.Quitarclientes();
                            IPEndPoint iplista = (IPEndPoint)cliente.Client.RemoteEndPoint;
                            this.listaeerrores.Add(error.Message + "," + iplista.Address.ToString() + " estado: " + cliente.Connected.ToString());

                        }


                    }
                }
            }

        }
        
        public bool EnviarMensaje(equipo_has_mensaje un_equiposala)
        {
     
            stream = cliente.GetStream();
            if (stream.CanWrite && stream != null)
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, un_equiposala);

                byte[] buff = new byte[un_equiposala.mensaje.valor.Length];
                buff = System.Text.Encoding.ASCII.GetBytes(un_equiposala.mensaje.valor);
                stream.Write(buff, 0, (int)buff.Length);

                return true;
            }
            return false;
        }
        




    }
}
