using CipherApp.Models;
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

        public HomeController()
        { }

        public IActionResult Index()
        {
            return View(vigenere);
        }

        [HttpPost]
        public IActionResult VigenereEncoder(VigenereModel vigenereData)
        {
            vigenere.EncoderPlainText = vigenereData.EncoderPlainText;
            vigenere.EncoderKey = vigenereData.EncoderKey;
            vigenere.EncoderCipherText = Encrypt(vigenereData.EncoderPlainText, vigenereData.EncoderKey);
            vigenere.DecoderCipherText = "";
            vigenere.DecoderKey = "";
            vigenere.DecoderPlainText = "";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult VigenereDecoder(VigenereModel vigenereData)
        {
            vigenere.DecoderCipherText = vigenereData.DecoderCipherText;
            vigenere.DecoderKey = vigenereData.DecoderKey;
            vigenere.DecoderPlainText = Decrypt(vigenereData.DecoderCipherText, vigenereData.DecoderKey);
            vigenere.EncoderPlainText = "";
            vigenere.EncoderKey = "";
            vigenere.EncoderCipherText = "";
            return RedirectToAction(nameof(Index));
        }

        private string Encrypt(string plainText, string key)
        {
            string alphabet = "AĄBCĆDEĘFGHIJKLŁMNŃOÓPQRSŚTUVWXYZŹŻ";
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
            string alphabet = "AĄBCĆDEĘFGHIJKLŁMNŃOÓPQRSŚTUVWXYZŹŻ";
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
