using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PreFinal.Models
{
    public class NamesModel
    {
        [Key]
        public int pubID { get; set; }

        public string pubName { get; set; }

        public int authorID { get; set; }

        public string authorName { get; set; }
    }
}