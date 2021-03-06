﻿using System;
using System.Collections.Generic;

using System.Text;
using System.Data;

namespace InformacionSistema
{
    public class TablaDisco
    {
        int hrStorageIndex;
        string hrStorageType;
        string hrStorageDescr;
        double hrStorageAllocationUnits;
        double hrStorageSize;
        double hrStorageUsed;
        double hrStorageAllocationFailures;
        double hrStorageFree;
        public TablaDisco()
        { }

        public TablaDisco(int _hrStorageIndex, string _hrStorageType, string _hrStorageDescr, double _hrStorageAllocationUnits, double _hrStorageSize, double _hrStorageUsed, double _hrStorageAllocationFailures)
        {
            this.hrStorageIndex = _hrStorageIndex;
            this.hrStorageType = this.TipoAlmacenamiento(_hrStorageType);
            this.hrStorageDescr = _hrStorageDescr;
            this.hrStorageAllocationUnits = _hrStorageAllocationUnits;
            this.hrStorageSize = _hrStorageSize;
            this.hrStorageUsed = _hrStorageUsed;
            this.hrStorageAllocationFailures = _hrStorageAllocationFailures;
        }

        public int HrStorageIndex
        {
            set { this.hrStorageIndex = value; }
            get { return this.hrStorageIndex; }
        }

        public string HrStorageType
        {
            set { this.hrStorageType = this.TipoAlmacenamiento(value);  }
            get { return this.hrStorageType; }
        }

        public string HrStorageDescr
        {
            set { this.hrStorageDescr = value; }
            get { return this.hrStorageDescr; }
        }
        
        public double HrStorageAllocationUnits
        {
            set { this.hrStorageAllocationUnits = value; }
            get { return this.hrStorageAllocationUnits; }
        }

        public double HrStorageSize
        {
            set { this.hrStorageSize = value; }
            get { return this.hrStorageSize; }
        }

        public double HrStorageUsed
        {
            set { this.hrStorageUsed = value; }
            get { return this.hrStorageUsed; }
        }
        public double HrStorageFree
        {
            get {
                this.hrStorageFree=this.hrStorageSize - this.hrStorageUsed;
                return this.hrStorageFree;
            }
            set {
                this.hrStorageFree = value;
            }
        }
        public double HrStorageAllocationFailures
        {
            set { this.hrStorageAllocationFailures = value; }
            get { return this.hrStorageAllocationFailures; }
        }
        public string TipoAlmacenamiento(string StorageType)
        {
       
                if(StorageType.Contains("1.3.6.1.2.1.25.2.1.1"))
                    return "otro";
                if(StorageType.Contains("1.3.6.1.2.1.25.2.1.2"))
                    return "ram";
               if(StorageType.Contains("1.3.6.1.2.1.25.2.1.3"))
                    return "memoria virtual";
                if(StorageType.Contains("1.3.6.1.2.1.25.2.1.4"))
                    return "fixed disk";
                if(StorageType.Contains("1.3.6.1.2.1.25.2.1.5"))
                    return "disco remobible";
                if(StorageType.Contains("1.3.6.1.2.1.25.2.1.6"))
                    return "floppy disk";
                if(StorageType.Contains("1.3.6.1.2.1.25.2.1.7"))
                    return "disco compacto";
                if(StorageType.Contains("1.3.6.1.2.1.25.2.1.8"))
                    return "disco ram";
                if(StorageType.Contains("1.3.6.1.2.1.25.2.1.9"))
                    return "memoru flash";
                if(StorageType.Contains("1.3.6.1.2.1.25.2.1.10"))
                    return "disco de red";
                else        
                    return "";
        }
        public static TablaDisco BuscarDisco(TablaDisco[]lista,string tipo)
        {
            for (int i = 0; i < lista.Length; i++)
            {
                if (lista[i].hrStorageType==tipo)
                    return lista[i];
            }
            return null; 
        }
        public static TablaDisco[] Discos(DataTable lista)
        {
            TablaDisco[] temp = new TablaDisco[lista.Rows.Count];
            for (int i = 0; i < temp.Length; i++)
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

    }
}
