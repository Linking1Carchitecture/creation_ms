using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Creacion.Models
{
    public class ListCreationModel
    {
        public int total_rows { get; set; }
        public int offset { get; set; }
        public List<ViewCreationModel> rows { get; set; }
    }
}