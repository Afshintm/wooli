using System;
using System.Collections.Generic;
using System.Text;

namespace Glossary.BusinessServices.Models
{
    public class GlossaryItem
    {
        public Guid Id { get; set; }
        public string Term { get; set; }
        public string Definition { get; set; }

        public GlossaryItem()
        {
            Id = Guid.NewGuid();
        }
    }
}
