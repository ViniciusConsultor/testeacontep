using System;
using System.Collections.Generic;
using System.Text;

namespace Acontep.Seguranca
{
    /// <summary>
    /// 
    /// </summary>
    public class CriptSenha
    {
        //Define uma chave para a criptografia                                 				                                
        private const string TEXTO_NORMAL = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-+*/;.?!@$%&()_={}#%";
        private const string TEXTO_CHAVE = "¡²³¤€¼½¾‘’ÄÅÉ®ÞÜÚÍÓÖ«»¬ÁßÐØ¶´Æ©Ñ?Ç¿¹£D§7ªBV#'*412$%^09(8";

        /// <summary>
        /// Nome: TesteCript
        /// Desenvolvido: Breno Marques Data:24/01/2002
        /// Atualizado: Data:
        /// Descrição: Método para testar se o valor pode ou não ser criptografado.
        /// </summary>
        /// <param name="pValor">Informa o valor que deve ser criptografado.</param>
        /// <returns>Retorna Verdadeiro ou Falso.</returns>		
        public static bool TesteCript(string pValor)
        {
            //transforma o valor para mauscula
            pValor = pValor.ToUpper();
            //Set a variavel de retorno para Verdadeiro
            bool lResult = true;
            //Varre toda a string para ver se tem algun caracter que não esteja previsto para ser criptogrfado 
            for (int lI = 0; lI < pValor.Length; lI++)
            {
                //Caso não encontre
                if (TEXTO_NORMAL.IndexOf(pValor.Substring(lI, 1)) == -1)
                {
                    lResult = false;
                }
            }
            return lResult;
        }

        /// <summary>
        /// Nome: EmCript
        /// Desenvolvido: Breno Marques Data:24/01/2002
        /// Atualizado: Data:
        /// Descrição: Método para criptografar uma senha.
        /// </summary>
        /// <param name="pValor">Informa o valor que deve ser retornado por extenso.</param>
        /// <returns>Retorna com o valor criptografado.</returns>		
        public static string EmCript(string pValor)
        {
            //transforma o valor para mauscula
            pValor = pValor.ToUpper();
            //Set a variavel de retorno para Verdadeiro
            string lResult = "";
            //Verifica se o valor a ser cript. e valido
            if (TesteCript(pValor))
            {
                //Varre toda a string para criptografar
                for (int lI = 0; lI < pValor.Length; lI++)
                {
                    lResult += TEXTO_CHAVE.Substring(TEXTO_NORMAL.IndexOf(pValor.Substring(lI, 1)), 1);
                }
            }
            return lResult;
        }

        /// <summary>
        /// Nome: DesCript
        /// Desenvolvido: Breno Marques Data:24/01/2002
        /// Atualizado: Data:
        /// Descrição: Método para descriptografar uma senha.
        /// </summary>
        /// <param name="pValor">Informa o valor que deve ser retornado por extenso.</param>
        /// <returns>Retorna com o valor pro extenso.</returns>		
        public static string DesCript(string pValor)
        {
            //transforma o valor para mauscula
            pValor = pValor.ToUpper();
            //Set a variavel de retorno para Verdadeiro
            string lResult = "";
            //Varre toda a string para descriptografar
            for (int lI = 0; lI < pValor.Length; lI++)
            {
                lResult += TEXTO_NORMAL.Substring(TEXTO_CHAVE.IndexOf(pValor.Substring(lI, 1)), 1);
            }

            return lResult;
        }
    }
}
