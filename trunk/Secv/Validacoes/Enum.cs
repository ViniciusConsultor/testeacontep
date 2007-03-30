using System;
using System.Collections.Generic;
using System.Text;

namespace Acontep.Validacoes
{
    /// <summary>
    /// Tipos de validacoes aplicaveis em <see cref="Validacao"/>
    /// </summary>
    [Flags]
    public enum TipoValidacao : short
    {
        /// <summary>
        /// Validar se existe um valor
        /// </summary>
        Requerido = 1,
        /// <summary>
        /// Validar se valor igual
        /// </summary>
        ValorIgual = 2,
        /// <summary>
        /// Validar se o valor � maior que o valor minimo
        /// </summary>
        ValorMinimo = 4,
        /// <summary>
        /// Validar se o valor � menor que o valor maximo
        /// </summary>
        ValorMaximo = 8,
        /// <summary>
        /// Validar se o valor � maior ou igual ao valor minimo
        /// </summary>
        ValorMinimoIgual = 16,
        /// <summary>
        /// Validar se o valor � menor ou igual ao valor maximo
        /// </summary>
        ValorMaximoIgual = 32,
        /// <summary>
        /// Validar se o valor est� contido mas n�o � igual aos valores do intervalo
        /// </summary>
        ValorContido = ValorMinimo | ValorMaximo,
        /// <summary>
        /// Validar se o valor � menor ou igual aos valores do intervalo
        /// </summary>
        ValorContidoInclusivo = ValorMinimoIgual | ValorMaximoIgual,
        /// <summary>
        /// Validar se a largura � igual
        /// </summary>
        LarguraIgual = 64,
        /// <summary>
        /// Validar se a largura � maior que a largura minima
        /// </summary>
        LarguraMinima = 128,
        /// <summary>
        /// Validar se a largura � menor que a largura maxima
        /// </summary>
        LarguraMaxima = 256,
        /// <summary>
        /// Validar se a largura � maior ou igual ao valor minima
        /// </summary>
        LarguraMinimaIgual = 512,
        /// <summary>
        /// Validar se a largura � menor ou igual ao valor maxima
        /// </summary>
        LarguraMaximaIgual = 1024,
        /// <summary>
        /// Validar se a largura est� contida mas n�o � igual aos valores do intervalo
        /// </summary>
        LarguraContida = LarguraMinima | LarguraMaxima,
        /// <summary>
        /// Validar se a largura � menor ou igual aos valores do intervalo
        /// </summary>
        LarguraContidaInclusivo = LarguraMinimaIgual | LarguraMaximaIgual,
        /// <summary>
        /// Validacao usando expressao regular
        /// </summary>
        Regex = 2048,
        /// <summary>
        /// Validacao customizada
        /// </summary>
        Customizada = 4096
    }
}
