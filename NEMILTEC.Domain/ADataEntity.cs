using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEMILTEC.Interfaces.Service.Domain;

namespace NEMILTEC.Domain
{
    public abstract class ADataEntity : IDataEntity
    {
        [Required()]
        [Key()]
        public long Id { get; set; }

        [Required()]
        [StringLength(100)]
        public string Name { get; set; }
    }
}
