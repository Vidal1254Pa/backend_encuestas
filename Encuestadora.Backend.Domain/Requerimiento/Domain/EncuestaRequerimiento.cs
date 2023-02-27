using System;
namespace Encuestadora.Backend.Domain.Requerimiento.Domain
{
	public class EncuestaRequerimiento
	{
		public int Id { get; set; }
		public int Encuestado_Id { get; set; }
        public string? Encuestado_Nombre { get; set; }
        public int Sucursal_Id { get; set; }
        public string? Sucursal_Nombre { get; set; }
        public DateTime? Fecha_Realizada { get; set; }
		public List<DetalleEncuesta>? DetalleEncuestas { get; set; }
	}
}

