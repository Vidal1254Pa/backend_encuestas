using System;
namespace Encuestadora.Backend.Domain.Configuracion.Domain
{
	public class Encuesta
	{
		public int Id { get; set; }
		public string Pregunta { get; set; }
		public int Escala_Id { get; set; }
		public int Categoria_Id { get; set; }
		public Escala? Escala { get; set; }
	}
}

