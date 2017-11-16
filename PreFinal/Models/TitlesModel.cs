using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PreFinal.Models
{
    public class TitlesModel
    {
        [Key]
        public int titleID { get; set; }

        [Display (Name = "Publisher")]
        [Required]
        public int pubID { get; set; }

        public List<SelectListItem> pubNames { get; set; }

        public string pubName { get; set; }

        [Display (Name = "Author")]
        [Required]
        public int authorID { get; set; }

        public List<SelectListItem> authorNames { get; set; }

        public string authorName { get; set; }

        [Display (Name = "Title Name")]
        [MaxLength(80)]
        [Required(ErrorMessage = "Field required.")]
        public string titleName { get; set; }

        [Display (Name = "Price")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Field required.")]
        public string titlePrice { get; set; }

        [Display (Name = "Publication Date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Field required.")]
        public DateTime titlePubDate { get; set; }

        [Display (Name = "Notes")]
        [MaxLength(200)]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Field required.")]
        public string titleNotes { get; set; }
    }
}