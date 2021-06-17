using CipherApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CipherApp.Controllers
{
    public class HomeController : Controller
    {
        private static VigenereModel vigenere = new VigenereModel() { };
        private static VigenereModel vigenereOutput;
        private static bool reset = true;

        public HomeController()
        {
            vigenereOutput = reset ? new VigenereModel() { } : vigenere;
        }

        public IActionResult Index()
        {
            reset = true;
            return View(vigenereOutput);
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)), 
                new CookieOptions { Expires = DateTimeOffset.Now.AddDays(30) });

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult VigenereEncoder(VigenereModel vigenereInput)
        {
            vigenere.EncoderPlainText = vigenereInput.EncoderPlainText;
            vigenere.EncoderKey = vigenereInput.EncoderKey;
            vigenere.EncoderCipherText = Encrypt(vigenereInput.EncoderPlainText, vigenereInput.EncoderKey);
            vigenere.DecoderCipherText = "";
            vigenere.DecoderKey = "";
            vigenere.DecoderPlainText = "";
            reset = false;
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult VigenereDecoder(VigenereModel vigenereInput)
        {
            vigenere.DecoderCipherText = vigenereInput.DecoderCipherText;
            vigenere.DecoderKey = vigenereInput.DecoderKey;
            vigenere.DecoderPlainText = Decrypt(vigenereInput.DecoderCipherText, vigenereInput.DecoderKey);
            vigenere.EncoderPlainText = "";
            vigenere.EncoderKey = "";
            vigenere.EncoderCipherText = "";
            reset = false;
            return RedirectToAction(nameof(Index));
        }

        private string Encrypt(string plainText, string key)
        {
            var culture = Request.HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture;
            string alphabet = culture.Name == "en" ? "ABCDEFGHIJKLMNOPQRSTUVWXYZ" : "AĄBCĆDEĘFGHIJKLŁMNŃOÓPQRSŚTUVWXYZŹŻ";
            string cipherAlphabet; 
            string cipherText = "";
            int j = 0;

            for (int i = 0; i < plainText.Length; i++)
            {
                char letter = plainText[i];
                int letterIndex = alphabet.ToList().FindIndex(x => x == Char.ToUpper(letter));
                bool isLower = Char.IsLower(letter);

                char keyLetter = key[j % key.Length];
                int keyLetterIndex = alphabet.ToList().FindIndex(x => x == Char.ToUpper(keyLetter));

                if (keyLetterIndex >= 0 && letterIndex >= 0)
                {
                    cipherAlphabet = alphabet.Substring(keyLetterIndex) + alphabet.Substring(0, keyLetterIndex);
                    cipherText += isLower ? Char.ToLower(cipherAlphabet[letterIndex]) : cipherAlphabet[letterIndex];
                    j++;
                }

                else
                {
                    cipherText += letter;
                }
            }

            return cipherText;
        }

        private string Decrypt(string cipherText, string key)
        {
            var culture = Request.HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture;
            string alphabet = culture.Name == "en" ? "ABCDEFGHIJKLMNOPQRSTUVWXYZ" : "AĄBCĆDEĘFGHIJKLŁMNŃOÓPQRSŚTUVWXYZŹŻ";
            string cipherAlphabet; 
            string plainText = "";
            int j = 0;

            for (int i = 0; i < cipherText.Length; i++)
            {
                char letter = cipherText[i];
                bool isLower = Char.IsLower(letter);

                char keyLetter = key[j % key.Length];
                int keyLetterIndex = alphabet.ToList().FindIndex(x => x == Char.ToUpper(keyLetter));

                if (keyLetterIndex >= 0)
                {
                    cipherAlphabet = alphabet.Substring(keyLetterIndex) + alphabet.Substring(0, keyLetterIndex);
                    int letterIndex = cipherAlphabet.ToList().FindIndex(x => x == Char.ToUpper(letter));

                    if (letterIndex >= 0)
                    {
                        plainText += isLower ? Char.ToLower(alphabet[letterIndex]) : alphabet[letterIndex];
                        j++;
                    }

                    else
                    {
                        plainText += letter;
                    }
                }
            }

            return plainText;
        }
    }
}
