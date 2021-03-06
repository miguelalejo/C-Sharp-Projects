﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace InformacionSistema
{
    public class DeviceT
    {
        int hrDeviceIndex;
        string hrDeviceType;
        string hrDeviceDescr;
        string hrDeviceID;
        int hrDeviceStatus;
        int hrDeviceErrors;
        public DeviceT()
        { }

        public DeviceT(int _hrDeviceIndex, string _hrDeviceType, string _hrDeviceDescr, string _hrDeviceID, int _hrDeviceStatus, int _hrDeviceErrors)
        {
            this.hrDeviceIndex = _hrDeviceIndex;
            this.hrDeviceType = Tipo(_hrDeviceType);
            this.hrDeviceDescr = _hrDeviceDescr;
            this.hrDeviceID = _hrDeviceID;
            this.hrDeviceStatus = _hrDeviceStatus;
            this.hrDeviceErrors = _hrDeviceErrors;
        }

        public int HrDeviceIndex
        {
            set { this.hrDeviceIndex = value; }
            get { return this.hrDeviceIndex;  }
        }
        public string HrDeviceType
        {
            set { this.hrDeviceType = Tipo(value); }
            get { return this.hrDeviceType; }
        }

        public string HrDeviceDescr
        {
            set { this.hrDeviceDescr = value; }
            get { return this.hrDeviceDescr; }
        }

        public string HrDeviceID
        {
            set { this.hrDeviceID = value; }
            get { return this.hrDeviceID; }
        }
        
        public int HrDeviceStatus
        {
            set { this.hrDeviceStatus = value; }
            get { return this.hrDeviceStatus; }
        }

        public int HrDeviceErrors
        {
            set { this.hrDeviceErrors = value; }
            get { return this.hrDeviceErrors;  }
        }
        public static DeviceT BuscarTipo(DeviceT[]lista,string unDeviceType)
        {
            for (int i = 0; i < lista.Length; i++)
            {
                if (lista[i].hrDeviceType.Contains(unDeviceType))
                    return lista[i];
            }
            return null;
        }
        public static int CantidadTipos(DeviceT[] lista, string unDeviceType)
        {
            int suma = 0;
            for (int i = 0; i < lista.Length; i++)
            {
                if (lista[i].hrDeviceType.Contains(unDeviceType))
                    suma++;
            }
            return suma;
        }
        public static string DevuelveTipo(string tipo)
        {
            switch (tipo)
            {
                case "otro":
                    return "1.3.6.1.2.1.25.3.1.1";
                case "desconocido":
                    return "1.3.6.1.2.1.25.3.1.2";
                case "procesador":
                    return "1.3.6.1.2.1.25.3.1.3";
                case "red":
                    return "1.3.6.1.2.1.25.3.1.4";
                case "impresora":
                    return "1.3.6.1.2.1.25.3.1.5";
                case "disco":
                    return "1.3.6.1.2.1.25.3.1.6";
                case "video":
                    return "1.3.6.1.2.1.25.3.1.10";
                case "audio":
                    return "1.3.6.1.2.1.25.3.1.11";
                case "coprocesador":
                    return "1.3.6.1.2.1.25.3.1.12";
                case "teclado":
                    return "1.3.6.1.2.1.25.3.1.13";
                case "modem":
                    return "1.3.6.1.2.1.25.3.1.14";
                case "paralelo":
                    return "1.3.6.1.2.1.25.3.1.15";
                case "mouse":
                    return "1.3.6.1.2.1.25.3.1.16";
                case "serial":
                    return "1.3.6.1.2.1.25.3.1.17";
                case "tape":
                    return "1.3.6.1.2.1.25.3.1.18";
                case "reloj":
                    return "1.3.6.1.2.1.25.3.1.19";
                case "ram":
                    return "1.3.6.1.2.1.25.3.1.20";
                case "rom":
                    return "1.3.6.1.2.1.25.3.1.21";

            }
            return "";
        }
        public static string Tipo(string DeviceType)
        {
            DeviceType = DeviceType.Remove(0, 1);
            if (DeviceType.Equals("1.3.6.1.2.1.25.3.1.1"))
                return "otro";
            if (DeviceType.Equals("1.3.6.1.2.1.25.3.1.2"))
                return "desconocido";
            if (DeviceType.Equals("1.3.6.1.2.1.25.3.1.3"))
                return "procesador";
            if (DeviceType.Equals("1.3.6.1.2.1.25.3.1.4"))
                return "red";
            if (DeviceType.Equals("1.3.6.1.2.1.25.3.1.5"))
                return "impresora";
            if (DeviceType.Equals("1.3.6.1.2.1.25.3.1.6"))
                return "disco";
            if (DeviceType.Equals("1.3.6.1.2.1.25.3.1.10"))
                return "video";
            if (DeviceType.Equals("1.3.6.1.2.1.25.3.1.11"))
                return "audio";
            if (DeviceType.Equals("1.3.6.1.2.1.25.3.1.12"))
                return "coprocesador";
            if (DeviceType.Equals("1.3.6.1.2.1.25.3.1.13"))
                return "teclado";
            if (DeviceType.Equals("1.3.6.1.2.1.25.3.1.14"))
                return "modem";
            if (DeviceType.Equals("1.3.6.1.2.1.25.3.1.15"))
                return "paralelo";
            if (DeviceType.Equals("1.3.6.1.2.1.25.3.1.16"))
                return "mouse";
            if (DeviceType.Equals("1.3.6.1.2.1.25.3.1.17"))
                return "serial";
            if (DeviceType.Equals("1.3.6.1.2.1.25.3.1.18"))
                return "tape";
            if (DeviceType.Equals("1.3.6.1.2.1.25.3.1.19"))
                return "reloj";
            if (DeviceType.Equals("1.3.6.1.2.1.25.3.1.20"))
                return "ram";
            if (DeviceType.Equals("1.3.6.1.2.1.25.3.1.21"))
                return "rom";
            else
                return "";
        }
        public static DeviceT[] Dispositivos(DataTable lista)
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

    }
}
