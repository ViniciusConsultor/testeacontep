using System;
using System.Collections.Generic;
using System.Text;
using Acontep.Validacoes;

namespace Acontep
{
    /// <summary>
    /// Metodos de conversao
    /// </summary>
    public static class Conversao
    {
        /// <summary>
        /// Conversao para o tipo <typeparamref name="T"/> aceitando ou nao nulo se nulo for permitido
        /// </summary>
        /// <typeparam name="T">Tipo a ser convertido</typeparam>
        /// <param name="valor">Valor a ser convertido</param>
        /// <param name="rotuloMensagemErro">Rotulo usado na mensagem de erro se a conversao nao for possivel</param>
        /// <returns><paramref name="valor"/> convertido para o tipo <typeparamref name="T"/></returns>
        public static T Para<T>(object valor, string rotuloMensagemErro)
        {
            Validacao<T> validacao = new Validacao<T>(rotuloMensagemErro);
            return validacao.Converter(valor);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="valor"></param>
        /// <param name="rotuloMensagemErro"></param>
        /// <param name="larguraIgual"></param>
        /// <returns></returns>
        public static T Para<T>(object valor, string rotuloMensagemErro, int? larguraIgual)
        {
            Validacao<T> validacao = new Validacao<T>(rotuloMensagemErro, larguraIgual);
            return validacao.Converter(valor);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="valor"></param>
        /// <param name="rotuloMensagemErro"></param>
        /// <param name="larguraMinima"></param>
        /// <param name="larguraMaxima"></param>
        /// <returns></returns>
        public static T Para<T>(object valor, string rotuloMensagemErro, int? larguraMinima, int? larguraMaxima)
        {
            Validacao<T> validacao = new Validacao<T>(rotuloMensagemErro, larguraMinima, larguraMaxima);
            return validacao.Converter(valor);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="valor"></param>
        /// <param name="rotuloMensagemErro"></param>
        /// <param name="larguraMinima"></param>
        /// <param name="larguraMaxima"></param>
        /// <param name="larguraIgual"></param>
        /// <param name="valorIgual"></param>
        /// <returns></returns>
        public static T Para<T>(object valor, string rotuloMensagemErro, int? larguraMinima, int? larguraMaxima, int? larguraIgual, object valorIgual)
        {
            Validacao<T> validacao = new Validacao<T>(rotuloMensagemErro, larguraMinima, larguraMaxima, larguraIgual, valorIgual);
            return validacao.Converter(valor);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="valor"></param>
        /// <param name="rotuloMensagemErro"></param>
        /// <param name="larguraMinima"></param>
        /// <param name="larguraMaxima"></param>
        /// <param name="larguraIgual"></param>
        /// <param name="valorMinimo"></param>
        /// <param name="valorMaximo"></param>
        /// <returns></returns>
        public static T Para<T>(object valor, string rotuloMensagemErro, int? larguraMinima, int? larguraMaxima, int? larguraIgual, object valorMinimo, object valorMaximo)
        {
            Validacao<T> validacao = new Validacao<T>(rotuloMensagemErro, larguraMinima, larguraMaxima, larguraIgual, valorMinimo, valorMaximo);
            return validacao.Converter(valor);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="valor"></param>
        /// <param name="rotuloMensagemErro"></param>
        /// <param name="requerido"></param>
        /// <returns></returns>
        public static T Para<T>(object valor, string rotuloMensagemErro, bool requerido)
        {
            Validacao<T> validacao = new Validacao<T>(rotuloMensagemErro, requerido);
            return validacao.Converter(valor);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="valor"></param>
        /// <param name="rotuloMensagemErro"></param>
        /// <param name="requerido"></param>
        /// <param name="larguraIgual"></param>
        /// <returns></returns>
        public static T Para<T>(object valor, string rotuloMensagemErro, bool requerido, int? larguraIgual)
        {
            Validacao<T> validacao = new Validacao<T>(rotuloMensagemErro, requerido, larguraIgual);
            return validacao.Converter(valor);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="valor"></param>
        /// <param name="rotuloMensagemErro"></param>
        /// <param name="requerido"></param>
        /// <param name="larguraMinima"></param>
        /// <param name="larguraMaxima"></param>
        /// <returns></returns>
        public static T Para<T>(object valor, string rotuloMensagemErro, bool requerido, int? larguraMinima, int? larguraMaxima)
        {
            Validacao<T> validacao = new Validacao<T>(rotuloMensagemErro, requerido, larguraMinima, larguraMaxima);
            return validacao.Converter(valor);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="valor"></param>
        /// <param name="rotuloMensagemErro"></param>
        /// <param name="requerido"></param>
        /// <param name="larguraMinima"></param>
        /// <param name="larguraMaxima"></param>
        /// <param name="larguraIgual"></param>
        /// <param name="valorIgual"></param>
        /// <returns></returns>
        public static T Para<T>(object valor, string rotuloMensagemErro, bool requerido, int? larguraMinima, int? larguraMaxima, int? larguraIgual, object valorIgual)
        {
            Validacao<T> validacao = new Validacao<T>(rotuloMensagemErro, requerido, larguraMinima, larguraMaxima, larguraIgual, valorIgual);
            return validacao.Converter(valor);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="valor"></param>
        /// <param name="rotuloMensagemErro"></param>
        /// <param name="requerido"></param>
        /// <param name="larguraMinima"></param>
        /// <param name="larguraMaxima"></param>
        /// <param name="larguraIgual"></param>
        /// <param name="valorMinimo"></param>
        /// <param name="valorMaximo"></param>
        /// <returns></returns>
        public static T Para<T>(object valor, string rotuloMensagemErro, bool requerido, int? larguraMinima, int? larguraMaxima, int? larguraIgual, object valorMinimo, object valorMaximo)
        {
            Validacao<T> validacao = new Validacao<T>(rotuloMensagemErro, requerido, larguraMinima, larguraMaxima, larguraIgual, valorMinimo, valorMaximo);
            return validacao.Converter(valor);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="valor"></param>
        /// <param name="rotuloMensagemErro"></param>
        /// <param name="valorConvertido"></param>
        /// <param name="mensagemErro"></param>
        /// <returns></returns>
        public static bool TentarPara<T>(object valor, string rotuloMensagemErro, out T valorConvertido, out string mensagemErro)
        {
            Validacao<T> validacao = new Validacao<T>(rotuloMensagemErro);
            return validacao.TentarConverter(valor, out valorConvertido, out mensagemErro);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="valor"></param>
        /// <param name="rotuloMensagemErro"></param>
        /// <param name="valorConvertido"></param>
        /// <param name="mensagemErro"></param>
        /// <param name="larguraIgual"></param>
        /// <returns></returns>
        public static bool TentarPara<T>(object valor, string rotuloMensagemErro, out T valorConvertido, out string mensagemErro, int? larguraIgual)
        {
            Validacao<T> validacao = new Validacao<T>(rotuloMensagemErro, larguraIgual);
            return validacao.TentarConverter(valor, out valorConvertido, out mensagemErro);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="valor"></param>
        /// <param name="rotuloMensagemErro"></param>
        /// <param name="valorConvertido"></param>
        /// <param name="mensagemErro"></param>
        /// <param name="larguraMinima"></param>
        /// <param name="larguraMaxima"></param>
        /// <returns></returns>
        public static bool TentarPara<T>(object valor, string rotuloMensagemErro, out T valorConvertido, out string mensagemErro, int? larguraMinima, int? larguraMaxima)
        {
            Validacao<T> validacao = new Validacao<T>(rotuloMensagemErro, larguraMinima, larguraMaxima);
            return validacao.TentarConverter(valor, out valorConvertido, out mensagemErro);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="valor"></param>
        /// <param name="rotuloMensagemErro"></param>
        /// <param name="valorConvertido"></param>
        /// <param name="mensagemErro"></param>
        /// <param name="larguraMinima"></param>
        /// <param name="larguraMaxima"></param>
        /// <param name="larguraIgual"></param>
        /// <param name="valorIgual"></param>
        /// <returns></returns>
        public static bool TentarPara<T>(object valor, string rotuloMensagemErro, out T valorConvertido, out string mensagemErro, int? larguraMinima, int? larguraMaxima, int? larguraIgual, object valorIgual)
        {
            Validacao<T> validacao = new Validacao<T>(rotuloMensagemErro, larguraMinima, larguraMaxima, larguraIgual, valorIgual);
            return validacao.TentarConverter(valor, out valorConvertido, out mensagemErro);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="valor"></param>
        /// <param name="rotuloMensagemErro"></param>
        /// <param name="valorConvertido"></param>
        /// <param name="mensagemErro"></param>
        /// <param name="larguraMinima"></param>
        /// <param name="larguraMaxima"></param>
        /// <param name="larguraIgual"></param>
        /// <param name="valorMinimo"></param>
        /// <param name="valorMaximo"></param>
        /// <returns></returns>
        public static bool TentarPara<T>(object valor, string rotuloMensagemErro, out T valorConvertido, out string mensagemErro, int? larguraMinima, int? larguraMaxima, int? larguraIgual, object valorMinimo, object valorMaximo)
        {
            Validacao<T> validacao = new Validacao<T>(rotuloMensagemErro, larguraMinima, larguraMaxima, larguraIgual, valorMinimo, valorMaximo);
            return validacao.TentarConverter(valor, out valorConvertido, out mensagemErro);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="valor"></param>
        /// <param name="rotuloMensagemErro"></param>
        /// <param name="valorConvertido"></param>
        /// <param name="mensagemErro"></param>
        /// <param name="requerido"></param>
        /// <returns></returns>
        public static bool TentarPara<T>(object valor, string rotuloMensagemErro, out T valorConvertido, out string mensagemErro, bool requerido)
        {
            Validacao<T> validacao = new Validacao<T>(rotuloMensagemErro, requerido);
            return validacao.TentarConverter(valor, out valorConvertido, out mensagemErro);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="valor"></param>
        /// <param name="rotuloMensagemErro"></param>
        /// <param name="valorConvertido"></param>
        /// <param name="mensagemErro"></param>
        /// <param name="requerido"></param>
        /// <param name="larguraIgual"></param>
        /// <returns></returns>
        public static bool TentarPara<T>(object valor, string rotuloMensagemErro, out T valorConvertido, out string mensagemErro, bool requerido, int? larguraIgual)
        {
            Validacao<T> validacao = new Validacao<T>(rotuloMensagemErro, requerido, larguraIgual);
            return validacao.TentarConverter(valor, out valorConvertido, out mensagemErro);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="valor"></param>
        /// <param name="rotuloMensagemErro"></param>
        /// <param name="valorConvertido"></param>
        /// <param name="mensagemErro"></param>
        /// <param name="requerido"></param>
        /// <param name="larguraMinima"></param>
        /// <param name="larguraMaxima"></param>
        /// <returns></returns>
        public static bool TentarPara<T>(object valor, string rotuloMensagemErro, out T valorConvertido, out string mensagemErro, bool requerido, int? larguraMinima, int? larguraMaxima)
        {
            Validacao<T> validacao = new Validacao<T>(rotuloMensagemErro, requerido, larguraMinima, larguraMaxima);
            return validacao.TentarConverter(valor, out valorConvertido, out mensagemErro);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="valor"></param>
        /// <param name="rotuloMensagemErro"></param>
        /// <param name="valorConvertido"></param>
        /// <param name="mensagemErro"></param>
        /// <param name="requerido"></param>
        /// <param name="larguraMinima"></param>
        /// <param name="larguraMaxima"></param>
        /// <param name="larguraIgual"></param>
        /// <param name="valorIgual"></param>
        /// <returns></returns>
        public static bool TentarPara<T>(object valor, string rotuloMensagemErro, out T valorConvertido, out string mensagemErro, bool requerido, int? larguraMinima, int? larguraMaxima, int? larguraIgual, object valorIgual)
        {
            Validacao<T> validacao = new Validacao<T>(rotuloMensagemErro, requerido, larguraMinima, larguraMaxima, larguraIgual, valorIgual);
            return validacao.TentarConverter(valor, out valorConvertido, out mensagemErro);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="valor"></param>
        /// <param name="rotuloMensagemErro"></param>
        /// <param name="valorConvertido"></param>
        /// <param name="mensagemErro"></param>
        /// <param name="requerido"></param>
        /// <param name="larguraMinima"></param>
        /// <param name="larguraMaxima"></param>
        /// <param name="larguraIgual"></param>
        /// <param name="valorMinimo"></param>
        /// <param name="valorMaximo"></param>
        /// <returns></returns>
        public static bool TentarPara<T>(object valor, string rotuloMensagemErro, out T valorConvertido, out string mensagemErro, bool requerido, int? larguraMinima, int? larguraMaxima, int? larguraIgual, object valorMinimo, object valorMaximo)
        {
            Validacao<T> validacao = new Validacao<T>(rotuloMensagemErro, requerido, larguraMinima, larguraMaxima, larguraIgual, valorMinimo, valorMaximo);
            return validacao.TentarConverter(valor, out valorConvertido, out mensagemErro);
        }
    }
}
