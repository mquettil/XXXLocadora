using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicalLayer.Security
{
    class HashUtils
    {
        /// <summary>
        /// Aplicará o algoritmo de HASH MD5, utilizando
        /// um "salt".
        /// </summary>
        /// <param name="senha">Senha bruta</param>
        /// <returns>Senha "hasheada"</returns>
        public static string HashPassword(string senha)
        {
            //Adicionar um valor de Salt
            string saltValue = "1necoLuzDeVelas5";
            //SHA1.Create().ComputeHash()

            //Adiciona o salt na senha e transforma para uma 
            //representação númerica em vetor
            byte[] codificacaoUTF8 =
                Encoding.UTF8.GetBytes(senha + saltValue);

            //Invoca o método 
            byte[] senhaHasheada =
                SHA1.Create().ComputeHash(codificacaoUTF8);

            //Retorna a senha hasheada utilizando codificação
            //utilizada por aplicações WEB.
            return Convert.ToBase64String(senhaHasheada);
        }
    }
}
