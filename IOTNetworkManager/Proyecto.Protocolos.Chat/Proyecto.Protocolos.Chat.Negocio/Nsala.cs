﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Proyecto.Protocolos.Chat.Entidades;
using Proyecto.Protocolos.Chat.AccesoDatos;
using System.Linq;

namespace Proyecto.Protocolos.Chat.Negocio
{
    public class Nsala
    {
        public static void Guardarsala(sala un_sala )
        {
            Basesala basesal = new Basesala();
            basesal.Guardarsala(un_sala);

           // AdministradorCorreo.EnviarCorreoCreacionCuenta(usuario.Nombres, usuario.Clave, usuario.Correo);
        }
        public static void Modificarsala(sala un_sala)
        {
            Basesala basesal = new Basesala();
            basesal.Modificarsala(un_sala);

            // AdministradorCorreo.EnviarCorreoCreacionCuenta(usuario.Nombres, usuario.Clave, usuario.Correo);
        }
        public static void Eliminarsala(sala un_sala)
        {
            Basesala basesal = new Basesala();
            basesal.Eliminarsala(un_sala.idSala);

            // AdministradorCorreo.EnviarCorreoCreacionCuenta(usuario.Nombres, usuario.Clave, usuario.Correo);
        }

        public static void Registrossala()
        {
            Basesala basesal = new Basesala();
            basesal.Registrossala();

            // AdministradorCorreo.EnviarCorreoCreacionCuenta(usuario.Nombres, usuario.Clave, usuario.Correo);
        }
        public static sala[] BuscarRegistros_idGrupo(int idGrupo)
        {
            Basesala basesal = new Basesala();
            return basesal.BuscarRegistros_idGrupo(idGrupo);
        }
        public static sala[] BuscarRegistrossala()
        {
            Basesala basesal = new Basesala();
            return basesal.BuscarRegistrossala();
        }
        public static void Modificarsala_Activo(int idsala, bool activo)
        {
            Basesala basesal = new Basesala();
            basesal.Modificarsala_Activo(idsala, activo);
        }
        public static sala[] BuscarRegistros_idsala(int idsala)
        {
            Basesala basesal = new Basesala();
            return basesal.BuscarRegistros_idSala(idsala);
        }
        public static sala[] activaTextoLista(bool activo, sala[] lista)
        {
            sala[] temp = new sala[lista.Length];
            int k = 0;
            foreach (sala sala in lista)
            {
                sala.activatexto = activo;
                temp[k] = sala;
                k++;
            }
            return temp;
        }
        public static sala[] activaLista_Listaseleccionada(sala[]sublista, sala[] listasala)
        {
            sala[] listatem = listasala;
            for (int i = 0,k=0; i < listasala.Length&&k<sublista.Length; i++)
            {
                if(listatem[i].idSala==sublista[k].idSala)
                {
                    if (listasala[i].activa == sublista[k].activatexto)
                    {
                        listatem[i] = sublista[k];
                        k++;
                    }
                    else
                    {
                        sublista[k].activatexto = false;
                        listatem[i] = sublista[k];
                        k++;
                    }
                }
            }
            return listatem;
        }

        public static sala[] listasala_idGrupo(int idGrupo, sala[] lista)
        {
            sala[] listatem = new sala[NumeroSalasGrupo(idGrupo,lista)];
            for (int i = 0, k = 0; i < lista.Length; i++)
            {
                if (lista[i].grupo.idGrupo == idGrupo)
                {
                    listatem[k] = lista[i];
                    k++;
                }
            }
            return listatem;

        }
        public static int NumeroSalasGrupo(int idGrupo, sala[] lista)
        {
            int nro = 0;
            for (int i = 0, k = 0; i < lista.Length; i++)
            {
                if (lista[i].grupo.idGrupo == idGrupo)
                {
                    nro++;
                }
            }
            return nro;
        }
        public static bool ExisteGrupo(int idGrupo, sala[] lista)
        {
            
            if (NumeroSalasGrupo(idGrupo,lista) > 0)
                return SalasActivas_Grupo(idGrupo,lista);
            else
                return false;
            
        }
        public static bool SalasActivas_Grupo(int idGrupo, sala[] lista)
        {
            bool exitensalas=false;
            for (int i = 0, k = 0; i < lista.Length; i++)
            {
                if (lista[i].grupo.idGrupo == idGrupo)
                {
                    if(lista[i].activa)
                        exitensalas=true;
                }
            }
            return exitensalas;
        }
        public static int NumeroSalasActivas_Grupo(int idGrupo, sala[] lista)
        {
            int nro = 0;
            for (int i = 0, k = 0; i < lista.Length; i++)
            {
                if (lista[i].grupo.idGrupo == idGrupo)
                {
                    if (lista[i].activa)
                        nro++;
                }
            }
            return nro;
        }
        public static sala[] SalasActivas_sala(int idGrupo, sala[] lista)
        {
            sala[]listatemp=new sala[NumeroSalasActivas_Grupo(idGrupo,lista)];
              for (int i = 0, k = 0; i < lista.Length&& k<listatemp.Length; i++)
            {
                if (lista[i].grupo.idGrupo == idGrupo)
                {
                    if (lista[i].activa)
                    {
                        listatemp[k]=lista[i];
                        k++;
                    }
                }
            }
            return listatemp;

        }
        public static sala[] activaListaseleccionada(sala[] lista,bool activa)
        {
            for (int i = 0; i < lista.Length; i++)
            {
                if (lista[i].activa)
                {
                    lista[i].activatexto = activa;
                }

            }
            return lista;
        }
        public static void Modificarsala_Nroequipos(int nroequipos,int idsala )
        {
            Basesala basesal = new Basesala();
            basesal.Modificarsala_Nroequipos(nroequipos, idsala);
        }
        public static void ModificarListaequipoReinicioCuantas(sala[] lista)
        {
            Basesala basesal = new Basesala();
            for (int i = 0; i < lista.Length; i++)
            {
                if (lista[i] != null)
                {
                   
                    basesal.Modificarsala_Nroequipos(0, lista[i].idSala);
                }
            }
        }

    
    }
}
