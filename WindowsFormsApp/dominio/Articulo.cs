﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Articulo
    {
        public int Id { get; set; }
        [DisplayName("Código")]
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        [DisplayName("Descripción")]
        public string Descripcion { get; set; }
        [DisplayName("Marca")]
        public Marca Marca { get; set; }
        [DisplayName("Categoria")]
        public Categoria Categoria { get; set; }
        public Decimal Precio { get; set; }
        //public Imagen Imagenes { get; set; }


    }
}
