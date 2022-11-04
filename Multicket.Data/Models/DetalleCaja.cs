﻿using System;

namespace Multicket.Data.Models
{
    public partial class DetalleCaja
    {
        partial void OnCreated();
        public virtual Guid Id { get; set; }
        public virtual decimal? MontoInicial { get; set; }
        public virtual decimal? MontoCierre { get; set; }
        public virtual string Estado { get; set; }
        public virtual DateTime? Apertura_At { get; set; }
        public virtual DateTime? Cierre_At { get; set; }
        public virtual Caja Caja { get; set; }

        public DetalleCaja()
        {
            OnCreated();
        }
    }
}
