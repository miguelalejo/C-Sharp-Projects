﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ASOCKETLib;

namespace InformacionSistema
{
    public class DataTableInfoSistema
    {
        public static DataTable TablaDeviceTable(ClassConexionSnmp unaconexion)
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
        public static DataTable TablaProcessorTable(ClassConexionSnmp unaconexion)
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
        public static DataTable TablaNetworkTable(ClassConexionSnmp unaconexion)
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
        public static DataTable TablaProgramasInicio(ClassConexionSnmp unaconexion)
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



        public static DataTable TablaProgramasInstalados(ClassConexionSnmp unaconexion)
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
        public static DataTable TablaDiscos(ClassConexionSnmp unaconexion)
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
        public static DataTable TablaInterfaces(int nrointer, ClassConexionSnmp unaconexion)
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
        public static DataTable TablaEstadoImpresoras(ClassConexionSnmp unaconexion)
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
        public static DataTable TablaParticionDiscos(ClassConexionSnmp unaconexion)
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
        

    }
}
