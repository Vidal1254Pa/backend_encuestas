using System;
namespace Encuestadora.Backend.Domain.Requerimiento.Domain
{
	public class DetalleEncuesta
	{
		public int EncuestaRealizada_Id { get; set; }
		public int Encuesta_Id { get; set; }
		public string Respuesta { get; set; }
	}
}

