using System;
using System.Data;
using System.Data.Odbc;

namespace Proyecto.Protocolos.Chat.Entidades
{
    [Serializable]
	public class mensaje
	{
		private int _idMensaje;
		private String _ipOrigen;
		private String _ipDestino;
		private DateTime _fechaEnvio;
		private TimeSpan _horaEnvio;
		private String _valor;
		private String _metodo;
		private String _cabecera="";
		private String _rtf="";
		private int _privado;
        private bool _esprivado;

		public mensaje()
		{


		}

		public int idMensaje
		{
			get {return  _idMensaje;}
			set { _idMensaje=value; }
		}
		public String ipOrigen
		{
			get {return  _ipOrigen;}
			set { _ipOrigen=value; }
		}
		public String ipDestino
		{
			get {return  _ipDestino;}
			set { _ipDestino=value; }
		}
		public DateTime fechaEnvio
		{
			get {return  _fechaEnvio;}
			set { _fechaEnvio=value; }
		}
		public TimeSpan horaEnvio
		{
			get {return  _horaEnvio;}
			set { _horaEnvio=value; }
		}
		public String valor
		{
			get {return  _valor;}
			set { _valor=value; }
		}
		public String metodo
		{
			get {return  _metodo;}
			set { _metodo=value; }
		}
		public String cabecera
		{
			get {return  _cabecera;}
			set { _cabecera=value; }
		}
		public String rtf
		{
			get {return  _rtf;}
			set { _rtf=value; }
		}
		public int privado
		{
			get {return  _privado;}
			set { _privado=value; }
		}
        public mensaje(int unidMensaje, String unipOrigen, String unipDestino, DateTime unfechaEnvio, TimeSpan unhoraEnvio, String unvalor, String unmetodo)
        {
            this._idMensaje = unidMensaje;
            this._ipOrigen = unipOrigen;
            this._ipDestino = unipDestino;
            this._fechaEnvio = unfechaEnvio;
            this._horaEnvio = new TimeSpan(unhoraEnvio.Hours,unhoraEnvio.Minutes,unhoraEnvio.Seconds);
            this._valor = unvalor;
            this._metodo = unmetodo;
        }
        public mensaje(int unidMensaje, String unipOrigen, String unipDestino, DateTime unfechaEnvio, TimeSpan unhoraEnvio, String unvalor, String unmetodo, String uncabecera, String unrtf, bool unesprivado)
        {
            this._idMensaje = unidMensaje;
            this._ipOrigen = unipOrigen;
            this._ipDestino = unipDestino;
            this._fechaEnvio = unfechaEnvio;
            this._horaEnvio = unhoraEnvio;
            this._valor = unvalor;
            this._metodo = unmetodo;
            this._cabecera = uncabecera;
            this._rtf = unrtf;
            this._esprivado = unesprivado;
        }

		public static mensaje ParseOjeto(object[]ItemArray )
		{
            return new mensaje(int.Parse(ItemArray[0].ToString()), ItemArray[1].ToString(), ItemArray[2].ToString(), (DateTime)ItemArray[3], (TimeSpan)ItemArray[4], ItemArray[5].ToString(), ItemArray[6].ToString(), ItemArray[7].ToString(), ItemArray[8].ToString(), bool.Parse(ItemArray[9].ToString()));
		}

	}
}
