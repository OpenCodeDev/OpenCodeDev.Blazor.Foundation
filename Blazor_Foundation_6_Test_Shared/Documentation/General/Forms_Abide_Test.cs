using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCodeDev.Blazor.Foundation.Extensions.ValidatorAttributes;
namespace OpenCodeDev.Blazor.Foundation.Doc.Core.Documentation.General
{
    public class Forms_Abide_Test
    {
        [Required]
        [AbideDomain(false, ErrorMessage = " no no ")]
        public string Alpha { get; set; }
    }
}
