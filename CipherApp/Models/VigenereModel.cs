using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CipherApp.Models
{
    public class VigenereModel
    {
        [Required(ErrorMessage = "Wprowadź tekst do zaszyfrowania")]
        [MinLength(1)]
        [MaxLength(1500)]
        public string EncoderPlainText { get; set; }

        [Required(ErrorMessage = "Wprowadź słowo kluczowe")]
        [MinLength(1)]
        [StringLength(50)]
        [RegularExpression("^[A-Za-z]*$", ErrorMessage = "Słowo kluczowe może zawierać tylko litery podstawowego alfabetu łacińskiego")]
        public string EncoderKey { get; set; }

        public string EncoderCipherText { get; set; }

        [Required(ErrorMessage = "Wprowadź tekst do odszyfrowania")]
        [MinLength(1)]
        [StringLength(1500)]
        public string DecoderCipherText { get; set; }

        [Required(ErrorMessage = "Wprowadź słowo kluczowe")]
        [MinLength(1)]
        [StringLength(50)]
        [RegularExpression("^[A-Za-z]*$", ErrorMessage = "Słowo kluczowe może zawierać tylko litery podstawowego alfabetu łacińskiego")]
        public string DecoderKey { get; set; }

        public string DecoderPlainText { get; set; }
    }
}
