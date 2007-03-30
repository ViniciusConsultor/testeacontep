using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace Acontep.Validacoes
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class Validacao<T> : Validacao
    {
        #region Construtores

        #region Construtores com identificacao de campo requerido automatica

        /// <summary>
        /// Construtor de Validacao indicando se requerido se <typeparamref name="T"/> não aceitar valor <c>null</c>
        /// </summary>
        /// <param name="rotuloMensagemErro">Rotulo usado para indentificar os campos usando as mensagens de erro.</param>
        public Validacao(string rotuloMensagemErro)
            : this(rotuloMensagemErro, null, null, null, null, null, null, null, null)
        {
        }

        /// <summary>
        /// Construtor de Validacao indicando se requerido se <typeparamref name="T"/> não aceitar valor <c>null</c>
        /// </summary>
        /// <param name="rotuloMensagemErro">Rotulo usado para indentificar os campos usando as mensagens de erro.</param>
        /// <param name="larguraIgual">Quantidade de carcteres/dígito que o valor sendo validado deve ter</param>
        public Validacao(string rotuloMensagemErro, int? larguraIgual)
            : this(rotuloMensagemErro, null, null, larguraIgual, null, null, null, null, null)
        {
        }

        /// <summary>
        /// Construtor de Validacao indicando se requerido se <typeparamref name="T"/> não aceitar valor <c>null</c>
        /// </summary>
        /// <param name="rotuloMensagemErro">Rotulo usado para indentificar os campos usando as mensagens de erro.</param>
        /// <param name="larguraMinima">Quantidade de carcteres/dígito mínimo do valor sendo validado</param>
        /// <param name="larguraMaxima">Quantidade de carcteres/dígito máximo do valor sendo validado</param>
        public Validacao(string rotuloMensagemErro, int? larguraMinima, int? larguraMaxima)
            : this(rotuloMensagemErro, larguraMinima, larguraMaxima, null, null, null, null, null, null)
        {
        }

        /// <summary>
        /// Construtor de Validacao indicando se requerido se <typeparamref name="T"/> não aceitar valor <c>null</c>
        /// </summary>
        /// <exception cref="ArgumentException">Os parametros <paramref name="larguraMinima"/> ou <paramref name="larguraMaxima"/> possuem valor e <paramref name="larguraIgual"/> também possui valor</exception>
        /// <param name="rotuloMensagemErro">Rotulo usado para indentificar os campos usando as mensagens de erro.</param>
        /// <param name="larguraMinima">Quantidade de carcteres/dígito mínimo do valor sendo validado</param>
        /// <param name="larguraMaxima">Quantidade de carcteres/dígito máximo do valor sendo validado</param>
        /// <param name="larguraIgual">Quantidade de carcteres/dígito que o valor sendo validado deve ter</param>
        /// <param name="valorIgual">Valor que o valor sendo validado deve ter</param>
        public Validacao(string rotuloMensagemErro, int? larguraMinima, int? larguraMaxima, int? larguraIgual, object valorIgual)
            : this(rotuloMensagemErro, larguraMinima, larguraMaxima, larguraIgual, null, null, valorIgual, null, null)
        {
        }

        /// <summary>
        /// Construtor de Validacao indicando se requerido se <typeparamref name="T"/> não aceitar valor <c>null</c>
        /// </summary>
        /// <exception cref="ArgumentException">Os parametros <paramref name="larguraMinima"/> ou <paramref name="larguraMaxima"/> possuem valor e <paramref name="larguraIgual"/> também possui valor</exception>
        /// <param name="rotuloMensagemErro">Rotulo usado para indentificar os campos usando as mensagens de erro.</param>
        /// <param name="larguraMinima">Quantidade de carcteres/dígito mínimo do valor sendo validado</param>
        /// <param name="larguraMaxima">Quantidade de carcteres/dígito máximo do valor sendo validado</param>
        /// <param name="larguraIgual">Quantidade de carcteres/dígito que o valor sendo validado deve ter</param>
        /// <param name="valorMinimo">Valor mínimo do valor sendo validado</param>
        /// <param name="valorMaximo">Valor máximo do valor sendo validado</param>
        public Validacao(string rotuloMensagemErro, int? larguraMinima, int? larguraMaxima, int? larguraIgual, object valorMinimo, object valorMaximo)
            : this(rotuloMensagemErro, larguraMinima, larguraMaxima, larguraIgual, valorMinimo, valorMaximo, null, null, null)
        {
        }

        /// <summary>
        /// Construtor de Validacao indicando se requerido se <typeparamref name="T"/> não aceitar valor <c>null</c>
        /// </summary>
        /// <exception cref="ArgumentException">Os parametros <paramref name="larguraMinima"/> ou <paramref name="larguraMaxima"/> possuem valor e <paramref name="larguraIgual"/> também possui valor</exception>
        /// <exception cref="ArgumentException">Os parametros <paramref name="valorMinimo"/> ou <paramref name="valorMaximo"/> possuem valor e <paramref name="valorIgual"/> também possui valor</exception>
        /// <param name="rotuloMensagemErro">Rotulo usado para indentificar os campos usando as mensagens de erro.</param>
        /// <param name="larguraMinima">Quantidade de carcteres/dígito mínimo do valor sendo validado</param>
        /// <param name="larguraMaxima">Quantidade de carcteres/dígito máximo do valor sendo validado</param>
        /// <param name="larguraIgual">Quantidade de carcteres/dígito que o valor sendo validado deve ter</param>
        /// <param name="valorMinimo">Valor mínimo do valor sendo validado</param>
        /// <param name="valorMaximo">Valor máximo do valor sendo validado</param>
        /// <param name="valorIgual">Valor que o valor sendo validado deve ter</param>
        /// <param name="formatProvider">Provider de formatacao usado quando convertendo o valor ou quando exibindo os valores minimo e maximo</param>
        /// <param name="regexPattern">Padrao de validação usando o regex</param>
        public Validacao(string rotuloMensagemErro, int? larguraMinima, int? larguraMaxima, int? larguraIgual, object valorMinimo, object valorMaximo, object valorIgual, IFormatProvider formatProvider, string regexPattern)
            : this(rotuloMensagemErro, default(T) != null, larguraMinima, larguraMaxima, larguraIgual, valorMinimo, valorMaximo, null, null, null)
        {
        }

        #endregion Construtores com identificacao de campo requerido automatica

        #region Construtores com identificacao de campo requerido explicitada

        /// <summary>
        /// Construtor de Validacao
        /// </summary>
        /// <param name="rotuloMensagemErro">Rotulo usado para indentificar os campos usando as mensagens de erro.</param>
        /// <param name="requerido">Indica se o valor precisa ser diferente de <c>null</c></param>
        public Validacao(string rotuloMensagemErro, bool requerido)
            : this(rotuloMensagemErro, requerido, null, null, null, null, null, null, null, null)
        {
        }

        /// <summary>
        /// Construtor de Validacao
        /// </summary>
        /// <param name="rotuloMensagemErro">Rotulo usado para indentificar os campos usando as mensagens de erro.</param>
        /// <param name="requerido">Indica se o valor precisa ser diferente de <c>null</c></param>
        /// <param name="larguraIgual">Quantidade de carcteres/dígito que o valor sendo validado deve ter</param>
        public Validacao(string rotuloMensagemErro, bool requerido, int? larguraIgual)
            : this(rotuloMensagemErro, requerido, null, null, larguraIgual, null, null, null, null, null)
        {
        }

        /// <summary>
        /// Construtor de Validacao
        /// </summary>
        /// <param name="rotuloMensagemErro">Rotulo usado para indentificar os campos usando as mensagens de erro.</param>
        /// <param name="requerido">Indica se o valor precisa ser diferente de <c>null</c></param>
        /// <param name="larguraMinima">Quantidade de carcteres/dígito mínimo do valor sendo validado</param>
        /// <param name="larguraMaxima">Quantidade de carcteres/dígito máximo do valor sendo validado</param>
        public Validacao(string rotuloMensagemErro, bool requerido, int? larguraMinima, int? larguraMaxima)
            : this(rotuloMensagemErro, requerido, larguraMinima, larguraMaxima, null, null, null, null, null, null)
        {
        }

        /// <summary>
        /// Construtor de Validacao
        /// </summary>
        /// <exception cref="ArgumentException">Os parametros <paramref name="larguraMinima"/> ou <paramref name="larguraMaxima"/> possuem valor e <paramref name="larguraIgual"/> também possui valor</exception>
        /// <param name="rotuloMensagemErro">Rotulo usado para indentificar os campos usando as mensagens de erro.</param>
        /// <param name="requerido">Indica se o valor precisa ser diferente de <c>null</c></param>
        /// <param name="larguraMinima">Quantidade de carcteres/dígito mínimo do valor sendo validado</param>
        /// <param name="larguraMaxima">Quantidade de carcteres/dígito máximo do valor sendo validado</param>
        /// <param name="larguraIgual">Quantidade de carcteres/dígito que o valor sendo validado deve ter</param>
        /// <param name="valorIgual">Valor que o valor sendo validado deve ter</param>
        public Validacao(string rotuloMensagemErro, bool requerido, int? larguraMinima, int? larguraMaxima, int? larguraIgual, object valorIgual)
            : this(rotuloMensagemErro, requerido, larguraMinima, larguraMaxima, larguraIgual, null, null, valorIgual, null, null)
        {
        }

        /// <summary>
        /// Construtor de Validacao
        /// </summary>
        /// <exception cref="ArgumentException">Os parametros <paramref name="larguraMinima"/> ou <paramref name="larguraMaxima"/> possuem valor e <paramref name="larguraIgual"/> também possui valor</exception>
        /// <param name="rotuloMensagemErro">Rotulo usado para indentificar os campos usando as mensagens de erro.</param>
        /// <param name="requerido">Indica se o valor precisa ser diferente de <c>null</c></param>
        /// <param name="larguraMinima">Quantidade de carcteres/dígito mínimo do valor sendo validado</param>
        /// <param name="larguraMaxima">Quantidade de carcteres/dígito máximo do valor sendo validado</param>
        /// <param name="larguraIgual">Quantidade de carcteres/dígito que o valor sendo validado deve ter</param>
        /// <param name="valorMinimo">Valor mínimo do valor sendo validado</param>
        /// <param name="valorMaximo">Valor máximo do valor sendo validado</param>
        public Validacao(string rotuloMensagemErro, bool requerido, int? larguraMinima, int? larguraMaxima, int? larguraIgual, object valorMinimo, object valorMaximo)
            : this(rotuloMensagemErro, requerido, larguraMinima, larguraMaxima, larguraIgual, valorMinimo, valorMaximo, null, null, null)
        {
        }

        /// <summary>
        /// Construtor de Validacao
        /// </summary>
        /// <exception cref="ArgumentException">Os parametros <paramref name="larguraMinima"/> ou <paramref name="larguraMaxima"/> possuem valor e <paramref name="larguraIgual"/> também possui valor</exception>
        /// <exception cref="ArgumentException">Os parametros <paramref name="valorMinimo"/> ou <paramref name="valorMaximo"/> possuem valor e <paramref name="valorIgual"/> também possui valor</exception>
        /// <param name="rotuloMensagemErro">Rotulo usado para indentificar os campos usando as mensagens de erro.</param>
        /// <param name="requerido">Indica se o valor precisa ser diferente de <c>null</c></param>
        /// <param name="larguraMinima">Quantidade de carcteres/dígito mínimo do valor sendo validado</param>
        /// <param name="larguraMaxima">Quantidade de carcteres/dígito máximo do valor sendo validado</param>
        /// <param name="larguraIgual">Quantidade de carcteres/dígito que o valor sendo validado deve ter</param>
        /// <param name="valorMinimo">Valor mínimo do valor sendo validado</param>
        /// <param name="valorMaximo">Valor máximo do valor sendo validado</param>
        /// <param name="valorIgual">Valor que o valor sendo validado deve ter</param>
        /// <param name="formatProvider">Provider de formatacao usado quando convertendo o valor ou quando exibindo os valores minimo e maximo</param>
        /// <param name="regexPattern">Padrao de validação usando o regex</param>
        public Validacao(string rotuloMensagemErro, bool requerido, int? larguraMinima, int? larguraMaxima, int? larguraIgual, object valorMinimo, object valorMaximo, object valorIgual, IFormatProvider formatProvider, string regexPattern)
            : base(rotuloMensagemErro, typeof(T), requerido, larguraMinima, larguraMaxima, larguraIgual, valorMinimo, valorMaximo, null, null, null)
        {
        }

        #endregion Construtores com identificacao de campo requerido explicitada

        #endregion Construtores

        /// <summary>
        /// Retorna um boolean indicando se o valor for valido dentro das configuracoes da classe
        /// </summary>
        /// <param name="valor">Valor a ser validado</param>
        /// <param name="resultado">Valor convertido se <paramref name="valor"/> é válido, ou o valor default
        /// caso contrario</param>
        /// <param name="mensagemErro">Mensagem indicando o erro ocorrido</param>
        /// <returns>True se o valor foi valido, false caso contrario</returns>
        public bool TentarConverter(object valor, out T resultado, out string mensagemErro)
        {
            mensagemErro = ObterMensagemValidacao(valor);
            if (mensagemErro.Length == 0)
            {
                resultado = Converter(valor);
                return true;
            }
            else
            {
                resultado = default(T);
                return false;
            }
        }

        /// <summary>
        /// Usando as regras definidas valida e converte o valor passado como parametro
        /// </summary>
        /// <param name="valor">Valor a ser convertido</param>
        /// <returns>Valor convertido</returns>
        public new T Converter(object valor)
        {
            return (T)base.Converter(valor);
        }
    }

    /// <summary>
    /// Classe para validacoes rotineiras de dados
    /// </summary>    
    [Serializable]
    public class Validacao
    {
        #region Campos e operacoes privadas

        private string _RotuloMensagemErro;
        private string _MensagemCustomizada;
        private IFormatProvider _FormatProvider;
        private Type _DataType;
        private bool _Requerido;
        private int? _LarguraIgual;
        private int? _LarguraMinima;
        private int? _LarguraMaxima;
        private object _ValorIgual;
        private object _ValorMinimo;
        private object _ValorMaximo;
        private Regex _Regex;
        private event ValidacaoEventHandler _ValidandoValor;
        private TipoValidacao _TipoValidacao;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="valor"></param>
        /// <param name="adicionarSeNotNull"></param>
        /// <param name="removerSeNull"></param>
        private TipoValidacao ConfigValorValidacao(int? valor, TipoValidacao adicionarSeNotNull, TipoValidacao removerSeNull)
        {
            TipoValidacao tipoValidacao = this.TipoValidacao;
            if (valor == null)
            {
                RemoverTipoValidacao(ref tipoValidacao, removerSeNull);
            }
            else
            {
                AtribuirTipoValidacao(ref tipoValidacao, adicionarSeNotNull);
            }
            return tipoValidacao;
        }

        private TipoValidacao ObterValorTipoValidacao(object valor, TipoValidacao adicionarSeNotNull, TipoValidacao removerSeNull)
        {
            TipoValidacao tipoValidacao = this.TipoValidacao;
            if (valor == null)
            {
                RemoverTipoValidacao(ref tipoValidacao, removerSeNull);
            }
            else
            {
                AtribuirTipoValidacao(ref tipoValidacao, adicionarSeNotNull);
            }
            return tipoValidacao;
        }

        /// <summary>
        /// Obtem uma string correspondente ao valor informado, usando <see cref="FormatProvider"/> se existir
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        private string ObterValorString(object valor)
        {
            if (EhValorNull(valor))
            {
                return String.Empty;
            }
            else
            {
                if (FormatProvider != null)
                    return Convert.ToString(valor, FormatProvider).Trim();
                else
                    return Convert.ToString(valor).Trim();
            }
        }

        private static bool EhValorNull(object valor)
        {
            return valor == null ||
                Convert.IsDBNull(valor) ||
                (valor is DateTime && DateTime.MinValue.Equals(valor) ||
                (valor is String && valor.ToString().Trim().Length == 0)
                );
        }

        #region Metodos para validacoes

        private bool FazerValidacaoTipoDado(ref object valor, out string mensagem)
        {
            mensagem = String.Empty;
            // Um valor nulo foi informado
            if (EhValorNull(valor))
            {
                valor = null;
                if (Requerido)
                    mensagem = String.Format(ValidacaoResource.ErroValorRequerido, RotuloMensagemErro);
                else
                    mensagem = String.Empty;
                // Se for um valor nulo entao a validação vai estar concluída nesse ponto
                // com retorno indicando um falso erro
                return false;
            }
            else
            {
                try
                {
                    valor = ObterValorConvertido(valor);
                }
                catch (FormatException)
                {
                    mensagem = String.Format(ValidacaoResource.ErroFormatoDadoInvalido, RotuloMensagemErro);
                }
                catch (OverflowException)
                {
                    mensagem = String.Format(ValidacaoResource.ErroFormatoDadoInvalido, RotuloMensagemErro);
                }
                catch (InvalidCastException)
                {
                    mensagem = String.Format(ValidacaoResource.ErroTipoConversaoInvalida, RotuloMensagemErro);
                }
            }
            return String.IsNullOrEmpty(mensagem);
        }

        /// <summary>
        /// Converte quando necessario o valor sendo validado para o tipo correto definido em <see cref="DataType"/>, usando <see cref="FormatProvider"/>
        /// </summary>
        /// <param name="valor">Valor a ser convertido</param>
        /// <returns>Valor convertido</returns>
        private object ObterValorConvertido(object valor)
        {
            // Para valores null retorna null
            if (valor == null)
            {
                return null;
            }
            // Se o valor for do tipo char, converte ele para uma string
            if (valor.GetType() == typeof(char))
            {
                valor = valor.ToString();
            }
            // Se o valor for compativel com o DataType esperado retorna ele direto
            if (DataType.IsInstanceOfType(valor))
            {
                return valor;
            }
            valor = FazConversaoTipo(valor);
            return valor;
        }

        /// <summary>
        /// Faz o conversao para o tipo de dado esperado, formatando ele se necessario, usando <see cref="FormatProvider"/>
        /// </summary>
        /// <param name="valor">Valor a ser convertido</param>
        /// <returns>Valor convertido</returns>
        private object FazConversaoTipo(object valor)
        {
            // Obter o DataType, considerando o uso de Nullable
            Type dataType = ObterDataTypeReal(DataType);
            // Converte o dado usando formatacao customizada se existir
            if (FormatProvider == null)
                valor = Convert.ChangeType(valor, dataType);
            else
                valor = Convert.ChangeType(valor, dataType, FormatProvider);
            return valor;
        }

        /// <summary>
        /// Obtem o tipo de dado a partir de <see cref="DataType"/>. Se estivermos trabalhando com 
        /// <see cref="Nullable"/> entao obtem o tipo real
        /// trabalhando nullable
        /// </summary>
        /// <returns></returns>
        private Type ObterDataTypeReal(Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                return Nullable.GetUnderlyingType(type);
            }
            else
            {
                return type;
            }
        }

        /// <summary>
        /// Faz a validacao para intervalos de valores especificado
        /// </summary>
        /// <param name="value">Valor sendo validado</param>
        /// <param name="mensagem">Mensagem de erro se o <paramref name="value"/> estiver fora do intervalo válido</param>
        /// <returns>True se <paramref name="value"/> possui um valor em um intervalo válido, false caso contrário</returns>
        private bool FazerValidacaoIntervaloValores(object value, out string mensagem)
        {
            mensagem = String.Empty;

            if (!EhParaValidarIntervaloValores(this.TipoValidacao))
                return true;

            IComparable comparer = (IComparable)value;

            if (ContemTipoValidacao(_TipoValidacao, TipoValidacao.ValorContido))
            {
                if (comparer.CompareTo(_ValorMinimo) <= 0 || comparer.CompareTo(this._ValorMaximo) >= 0)
                {
                    mensagem = String.Format(ValidacaoResource.ErroForaIntervaloExclusivo, _RotuloMensagemErro, ObterValorString(ValorMinimo), ObterValorString(ValorMaximo));
                }
            }
            else if (ContemTipoValidacao(_TipoValidacao, TipoValidacao.ValorContidoInclusivo))
            {
                if (comparer.CompareTo(_ValorMinimo) < 0 || comparer.CompareTo(this._ValorMaximo) > 0)
                {
                    mensagem = String.Format(ValidacaoResource.ErroForaIntervaloInclusivo, _RotuloMensagemErro, ObterValorString(ValorMinimo), ObterValorString(ValorMaximo));
                }
            }
            else if (ContemTipoValidacao(_TipoValidacao, TipoValidacao.ValorIgual))
            {
                if (comparer.CompareTo(this.ValorIgual) != 0)
                {
                    mensagem = String.Format("O valor informado para {0} deve ser igual a {1}!", _RotuloMensagemErro, ObterValorString(this.ValorIgual));
                }
            }
            else
            {
                if (ContemTipoValidacao(_TipoValidacao, TipoValidacao.ValorMinimo))
                {
                    if (comparer.CompareTo(_ValorMinimo) <= 0)
                    {
                        mensagem = String.Format("O valor informado para {0} deve ser maior que {1}!", _RotuloMensagemErro, ObterValorString(ValorMinimo));
                    }
                }
                else if (ContemTipoValidacao(_TipoValidacao, TipoValidacao.ValorMinimoIgual))
                {
                    if (comparer.CompareTo(_ValorMinimo) < 0)
                    {
                        mensagem = String.Format("O valor informado para {0} deve ser maior ou igual a {1}!", _RotuloMensagemErro, ObterValorString(ValorMinimo));
                    }
                }
                if (ContemTipoValidacao(_TipoValidacao, TipoValidacao.ValorMaximo))
                {
                    if (comparer.CompareTo(_ValorMaximo) >= 0)
                    {
                        mensagem = String.Format("O valor informado para {0} deve ser menor que {1}!", _RotuloMensagemErro, ObterValorString(ValorMaximo));
                    }
                }
                else if (ContemTipoValidacao(_TipoValidacao, TipoValidacao.ValorMaximoIgual))
                {
                    if (comparer.CompareTo(_ValorMaximo) > 0)
                    {
                        mensagem = String.Format("O valor informado para {0} deve ser menor ou igual a {1}!", _RotuloMensagemErro, ObterValorString(ValorMaximo));
                    }
                }
            }
            return String.IsNullOrEmpty(mensagem);
        }

        /// <summary>
        /// Faz a validacao para intervalos de quantidade de caracteres/digitos especificado
        /// </summary>
        /// <param name="valor">Valor sendo validado</param>
        /// <param name="mensagem">Mensagem de erro se o <paramref name="valor"/> estiver fora do intervalo válido</param>
        /// <returns>True se <paramref name="valor"/> possui uma largura valida, false caso contrário</returns>
        private bool FazerValidacaoIntervaloLargura(object valor, out string mensagem)
        {
            mensagem = String.Empty;
            // Ignora validacao e retorna um "positivo" se a validacao por intervalo nao estiver configurada
            if (!EhParaValidarIntervaloLargura(this.TipoValidacao))
                return true;
            // Faz validacao
            string strValue = ObterValorString(valor);
            int length = strValue.Length;
            if (ContemTipoValidacao(_TipoValidacao, TipoValidacao.LarguraContida))
            {
                if (length <= this.LarguraMinima || length >= this.LarguraMaxima)
                {
                    mensagem = String.Format("A quantidade de caracteres informado para {0} deve estar entre {1} e {2}!", _RotuloMensagemErro, this.LarguraMinima, this.LarguraMaxima);
                }
            }
            else if (ContemTipoValidacao(_TipoValidacao, TipoValidacao.LarguraContidaInclusivo))
            {
                if (length < this.LarguraMinima || length > this.LarguraMaxima)
                {
                    mensagem = String.Format("A quantidade de caracteres informado para {0} deve ser maior ou igual a {1} e menor ou igual a {2}!", _RotuloMensagemErro, this.LarguraMinima, this.LarguraMaxima);
                }
            }
            else if (ContemTipoValidacao(_TipoValidacao, TipoValidacao.LarguraIgual))
            {
                if (length == this.LarguraIgual)
                {
                    mensagem = String.Format("A quantidade de caracteres informado para {0} deve ser igual a {1}!", _RotuloMensagemErro, this.LarguraIgual);
                }
            }
            else // Faz validacoes independentes de limites de valores
            {
                if (ContemTipoValidacao(_TipoValidacao, TipoValidacao.LarguraMinima))
                {
                    if (length <= this.LarguraMinima)
                    {
                        mensagem = String.Format("A quantidade de caracteres informado para {0} deve ser maior que {1}!", _RotuloMensagemErro, this.LarguraMinima);
                    }
                }
                else if (ContemTipoValidacao(_TipoValidacao, TipoValidacao.LarguraMinimaIgual))
                {
                    if (length < this.LarguraMinima)
                    {
                        mensagem = String.Format("A quantidade de caracteres informado para {0} deve ser maior ou igual a {1}!", _RotuloMensagemErro, this.LarguraMinima);
                    }
                }
                if (ContemTipoValidacao(_TipoValidacao, TipoValidacao.LarguraMaxima))
                {
                    if (length >= this.LarguraMaxima)
                    {
                        mensagem = String.Format("A quantidade de caracteres informado para {0} deve ser menor que {1}!", _RotuloMensagemErro, this.LarguraMaxima);
                    }
                }
                else if (ContemTipoValidacao(_TipoValidacao, TipoValidacao.LarguraMaximaIgual))
                {
                    if (length > this.LarguraMaxima)
                    {
                        mensagem = String.Format("A quantidade de caracteres informado para {0} deve ser menor ou igual a {1}!", _RotuloMensagemErro, this.LarguraMaxima);
                    }
                }
            }
            return String.IsNullOrEmpty(mensagem);
        }

        private bool FazerValidacaoRegex(object valor, out string mensagem)
        {
            mensagem = String.Empty;
            if (!EhParaValidarRegex(this.TipoValidacao))
                return true;
            string strValue = ObterValorString(valor);
            if (!Regex.IsMatch(strValue))
            {
                mensagem = String.Format("O valor informado para {0} possui um formato incorreto!", _RotuloMensagemErro);
            }
            return String.IsNullOrEmpty(mensagem);
        }

        private bool FazerValidacaoCustomizada(object valor, out string mensagem)
        {
            mensagem = String.Empty;
            if (!EhParaValidarCustomizado(this.TipoValidacao))
                return true;
            ValidacaoEventArgs e = new ValidacaoEventArgs(RotuloMensagemErro, valor);
            _ValidandoValor(this, e);
            mensagem = e.MensagemErro;
            return String.IsNullOrEmpty(mensagem);
        }

        /// <summary>
        /// Retorna texto descritivo com informacoes da validacao do valor
        /// </summary>
        /// <param name="valor">Valor a ser validado</param>
        /// <returns>Erro ocorrido ou <c>String.Empty</c> se o valor esta correto</returns>
        private string FazerValidacaoGeral(ref object valor)
        {
            string mensagem;
            if (!FazerValidacaoTipoDado(ref valor, out mensagem))
            {
                return ObterMensagemErro(mensagem);
            }
            else if (!FazerValidacaoIntervaloLargura(valor, out mensagem))
            {
                return ObterMensagemErro(mensagem);
            }
            else if (!FazerValidacaoIntervaloValores(valor, out mensagem))
            {
                return ObterMensagemErro(mensagem);
            }
            else if (!FazerValidacaoRegex(valor, out mensagem))
            {
                return ObterMensagemErro(mensagem);
            }
            else if (!FazerValidacaoCustomizada(valor, out mensagem))
            {
                return ObterMensagemErro(mensagem);
            }
            return String.Empty;
        }

        /// <summary>
        /// Obtem mensagem de erro identificada ou uma mensagem customizada se existir uma definida
        /// </summary>
        /// <param name="mensagem"></param>
        /// <returns></returns>
        private string ObterMensagemErro(string mensagem)
        {
            if (String.IsNullOrEmpty(mensagem))
            {
                return mensagem;
            }
            else
            {
                return String.IsNullOrEmpty(MensagemCustomizada) ? mensagem : MensagemCustomizada;
            }
        }

        #endregion Metodos para validacoes


        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="ValorInvalidoException">O valor informado não é válido</exception>
        /// <param name="valor"></param>
        private void Validar(ref object valor)
        {
            string mensagem = FazerValidacaoGeral(ref valor);
            if (!String.IsNullOrEmpty(mensagem))
                throw new ValorInvalidoException(mensagem);
        }

        #endregion

        #region Metodos para TipoValidacao

        /// <summary>
        /// Verifica se <paramref name="origem"/> contem o <paramref name="contido"/>
        /// </summary>
        /// <param name="origem">Tipos de validacoes</param>
        /// <param name="contido">Tipo de validacao a ser verificado se existem em <paramref name="origem"/></param>
        /// <returns>True se <paramref name="origem"/> contem <paramref name="contido"/></returns>
        internal bool ContemTipoValidacao(TipoValidacao origem, TipoValidacao contido)
        {
            return (origem & contido) == contido;
        }

        /// <summary>
        /// Verifica se <paramref name="origem"/> contem qualquer um dos valores informados em <paramref name="contido"/>
        /// </summary>
        /// <param name="origem">Tipos de validacoes</param>
        /// <param name="contido">Tipos de validacoes a ser verificado se existem em <paramref name="origem"/></param>
        /// <returns>True se <paramref name="origem"/> contem <paramref name="contido"/></returns>
        internal bool ContemQualquerTipoValidacao(TipoValidacao origem, TipoValidacao contido)
        {
            TipoValidacao[] tiposOrigem = ObterTiposValidacao(origem);

            Predicate<TipoValidacao> origemExisteEmContido = delegate(TipoValidacao tipo) { return ContemTipoValidacao(contido, tipo); };

            // Obtem somente os tipos 
            return Array.Exists<TipoValidacao>(tiposOrigem, origemExisteEmContido);
        }

        private void AtribuirTipoValidacao(ref TipoValidacao atual, TipoValidacao novo)
        {
            // Verifica se existe em novo dois tipos incompativeis entre si
            TipoValidacao tiposIncomp = ObterTiposIncompativeis(novo);
            if (ContemQualquerTipoValidacao(novo, tiposIncomp))
            {
                throw new InvalidOperationException(
                    String.Format("Os novos tipos de validação possuem dois ou mais tipos incompativeis definidos. Tipos em novo: {0}; Tipos incompativeis: {1}.",
                    novo, tiposIncomp
                    )
                );
            }
            // Remove os tipos incompativeis com as novas validacoes
            tiposIncomp = ObterTiposIncompativeis(novo);
            // Remove todas as configuracoes incompativeis da lista de validacoes configuradas
            RemoverTipoValidacao(ref atual, tiposIncomp);
            // Atribui as novas validacoes
            atual |= novo;
        }

        /// <summary>
        /// Provoca um exception se, para qualquer tipo de validacao, nao existir valor
        /// na propriedade correspondente.
        /// </summary>
        /// <param name="novo"></param>
        private void ValidarSePossuiValorParaTipoValidacao(TipoValidacao novo)
        {
            TipoValidacao[] tipos = ObterTiposValidacao(novo);
            foreach (TipoValidacao tipo in tipos)
            {
                switch (tipo)
                {
                    case TipoValidacao.LarguraIgual:
                        Conversao.Para<int>(LarguraIgual, "largura igual");
                        break;
                    case TipoValidacao.LarguraMaxima:
                    case TipoValidacao.LarguraMaximaIgual:
                        Conversao.Para<int>(LarguraMaxima, "largura máxima");
                        break;
                    case TipoValidacao.LarguraMinima:
                    case TipoValidacao.LarguraMinimaIgual:
                        Conversao.Para<int>(LarguraMinima, "largura mínima");
                        break;
                    case TipoValidacao.ValorIgual:
                        Conversao.Para<object>(ValorIgual, "valor igual", true);
                        break;
                    case TipoValidacao.ValorMaximo:
                    case TipoValidacao.ValorMaximoIgual:
                        Conversao.Para<object>(ValorMaximo, "valor máximo", true);
                        break;
                    case TipoValidacao.ValorMinimo:
                    case TipoValidacao.ValorMinimoIgual:
                        Conversao.Para<object>(ValorMinimo, "valor mínimo", true);
                        break;
                    case TipoValidacao.Regex:
                        Conversao.Para<Regex>(Regex, "regex");
                        break;
                    case TipoValidacao.Customizada:
                        Conversao.Para<ValidacaoEventHandler>(this._ValidandoValor, "validação customizada", true);
                        break;
                }
            }
        }

        private TipoValidacao[] ObterTiposValidacao(TipoValidacao tipos)
        {
            Predicate<TipoValidacao> existeTipo = delegate(TipoValidacao tipo) { return ContemTipoValidacao(tipos, tipo); };
            TipoValidacao[] todosTipos = (TipoValidacao[])Enum.GetValues(typeof(TipoValidacao));
            return Array.FindAll<TipoValidacao>(todosTipos, existeTipo);
        }

        private TipoValidacao ObterTiposIncompativeis(TipoValidacao tipos)
        {
            TipoValidacao tiposIncomp = 0;
            foreach (TipoValidacao tipo in ObterTiposValidacao(tipos))
            {
                tiposIncomp |= ObterTiposIncompativeisLarg(tipo) | ObterTiposIncompativeisInterv(tipo);
            }
            return tiposIncomp;
        }

        /// <summary>
        /// Retornar todos os tipos de validacao icompativeis com <see cref="TipoValidacao"/>
        /// </summary>
        /// <param name="tipo">Tipo a se obter incompatibilidade</param>
        /// <returns>Tipos de validacao</returns>
        private TipoValidacao ObterTiposIncompativeisLarg(TipoValidacao tipo)
        {
            switch (tipo)
            {
                case TipoValidacao.LarguraIgual:
                    return TipoValidacao.LarguraContida | TipoValidacao.LarguraContidaInclusivo;
                case TipoValidacao.LarguraMinima:
                    return TipoValidacao.LarguraMinimaIgual | TipoValidacao.LarguraIgual;
                case TipoValidacao.LarguraMinimaIgual:
                    return TipoValidacao.LarguraMinima | TipoValidacao.LarguraIgual;
                case TipoValidacao.LarguraMaxima:
                    return TipoValidacao.LarguraMaximaIgual | TipoValidacao.LarguraIgual;
                case TipoValidacao.LarguraMaximaIgual:
                    return TipoValidacao.LarguraMaxima | TipoValidacao.LarguraIgual;
                default:
                    // Retorna zero se nenhum tipo incompativel foi encontrado para intervalo de valores
                    return 0;
            }
        }

        /// <summary>
        /// Retornar todos os tipos de validacao icompativeis com <see cref="TipoValidacao"/>
        /// </summary>
        /// <param name="tipo">Tipo a se obter incompatibilidade</param>
        /// <returns>Tipos de validacao</returns>
        private TipoValidacao ObterTiposIncompativeisInterv(TipoValidacao tipo)
        {
            switch (tipo)
            {
                case TipoValidacao.ValorIgual:
                    return TipoValidacao.ValorContido | TipoValidacao.ValorContidoInclusivo;
                case TipoValidacao.ValorMinimo:
                    return TipoValidacao.ValorMinimoIgual | TipoValidacao.ValorIgual;
                case TipoValidacao.ValorMinimoIgual:
                    return TipoValidacao.ValorMinimo | TipoValidacao.ValorIgual;
                case TipoValidacao.ValorMaximo:
                    return TipoValidacao.ValorMaximoIgual | TipoValidacao.ValorIgual;
                case TipoValidacao.ValorMaximoIgual:
                    return TipoValidacao.ValorMaximo | TipoValidacao.ValorIgual;
                default:
                    // Retorna zero se nenhum tipo incompativel foi encontrado para intervalo de valores
                    return 0;
            }
        }

        private void RemoverTipoValidacao(ref TipoValidacao atual, TipoValidacao remover)
        {
            atual = (atual ^ remover) & atual;
        }

        /// <summary>
        /// Validacao casos onde a validacao por intervalo é solicitada mas o tipo de dado nao da suporte a comparacoes
        /// </summary>
        private void ValidacaoPorIntervaloValoresEhPossivel(TipoValidacao validateType, Type type)
        {
            type = ObterDataTypeReal(type);
            if (EhParaValidarIntervaloValores(validateType) && type.GetInterface("System.IComparable") == null)
            {
                throw new ArgumentException("O tipo de dado nao permite validações de intervalo! O uso de uma validação customizada pode ser feito para atender a alguns casos mais especificos!");
            }
        }

        private bool EhParaValidarIntervaloValores(TipoValidacao tipoValidacao)
        {
            return ContemTipoValidacao(tipoValidacao, TipoValidacao.ValorMinimo) ||
                ContemTipoValidacao(tipoValidacao, TipoValidacao.ValorMinimoIgual) ||
                ContemTipoValidacao(tipoValidacao, TipoValidacao.ValorMaximo) ||
                ContemTipoValidacao(tipoValidacao, TipoValidacao.ValorMaximoIgual) ||
                ContemTipoValidacao(tipoValidacao, TipoValidacao.ValorIgual);
        }

        private bool EhParaValidarIntervaloLargura(TipoValidacao tipoValidacao)
        {
            return ContemTipoValidacao(tipoValidacao, TipoValidacao.LarguraMinima) ||
                ContemTipoValidacao(tipoValidacao, TipoValidacao.LarguraMinimaIgual) ||
                ContemTipoValidacao(tipoValidacao, TipoValidacao.LarguraMaxima) ||
                ContemTipoValidacao(tipoValidacao, TipoValidacao.LarguraMaximaIgual) ||
                ContemTipoValidacao(tipoValidacao, TipoValidacao.LarguraIgual);
        }

        private bool EhParaValidarRegex(TipoValidacao tipoValidacao)
        {
            return ContemTipoValidacao(tipoValidacao, TipoValidacao.Regex);
        }

        private bool EhParaValidarCustomizado(TipoValidacao tipoValidacao)
        {
            return ContemTipoValidacao(tipoValidacao, TipoValidacao.Customizada);
        }

        /// <summary>
        /// Configura o valor da instância para um tipo compatível com o tipo de dado sendo validado
        /// </summary>
        /// <exception cref="ApplicationException">O tipo de dado não é compatível com o <see cref="DataType"/></exception>
        /// <param name="value">Valor a ser verificado se possui tipo contivel</param>
        private void ConfigParaIntervaloValorValido(ref object value)
        {
            if (value != null)
            {
                try
                {
                    value = ObterValorConvertido(value);
                }
                catch (FormatException)
                {
                    string msgErro = String.Format(ValidacaoResource.ErroTipoParaIntervaloValorInvalido, value, value.GetType(), ObterDataTypeReal(DataType));
                    throw new ApplicationException(msgErro);
                }
                catch (OverflowException)
                {
                    string msgErro = String.Format(ValidacaoResource.ErroTipoParaIntervaloValorInvalido, value, value.GetType(), ObterDataTypeReal(DataType));
                    throw new ApplicationException(msgErro);
                }
                catch (InvalidCastException)
                {
                    string msgErro = String.Format(ValidacaoResource.ErroTipoParaIntervaloValorInvalido, value, value.GetType(), ObterDataTypeReal(DataType));
                    throw new ApplicationException(msgErro);
                }
            }
        }

        /// <summary>
        /// Configuracao e validacao para valores maximo e minimo
        /// </summary>
        /// <param name="value"></param>
        /// <param name="adicionarSeNotNull">Valor a ser adicionado a <see cref="TipoValidacao"/> se <paramref name="valorLargura"/> for diferente de <c>null</c></param>
        /// <param name="removerSeNull">Valor a ser removido de <see cref="TipoValidacao"/> se <paramref name="valorLargura"/> for <c>null</c></param>
        private void ConfigValoresMinimoMaximo(ref object value, TipoValidacao adicionarSeNotNull, TipoValidacao removerSeNull)
        {
            TipoValidacao tipoValidacao = ObterValorTipoValidacao(value,
                adicionarSeNotNull, removerSeNull
            );
            if (value != null)
            {
                this.ConfigParaIntervaloValorValido(ref value);
                this.ValidacaoPorIntervaloValoresEhPossivel(tipoValidacao, DataType);
                if (this.ValorIgual != null)
                    this.ValorIgual = null;
            }
            this.TipoValidacao = tipoValidacao;
        }

         /// <summary>
        /// Configuracao e validacao para largura maxima e minima
        /// </summary>
        /// <param name="value">Valor</param>
        /// <param name="adicionarSeNotNull">Valor a ser adicionado a <see cref="TipoValidacao"/> se <paramref name="valorLargura"/> for diferente de <c>null</c></param>
        /// <param name="removerSeNull">Valor a ser removido de <see cref="TipoValidacao"/> se <paramref name="valorLargura"/> for <c>null</c></param>
        private void ConfigLarguraMinimaMaxima(int? value, TipoValidacao adicionarSeNotNull, TipoValidacao removerSeNull)
        {
            this.TipoValidacao = ObterValorTipoValidacao(value,
                adicionarSeNotNull,
                removerSeNull
                );
            if (value != null)
            {
                if (this.LarguraIgual != null)
                    this.LarguraIgual = null;
            }
        }

        #endregion

        #region Construtores

        /// <summary>
        /// Construtor de Validacao
        /// </summary>
        /// <param name="rotuloMensagemErro">Rotulo usado para indentificar os campos usando as mensagens de erro.</param>
        /// <param name="dataType">Tipo do dado a ser validado</param>
        /// <param name="requerido">Indica se o valor precisa ser diferente de <c>null</c></param>
        public Validacao(string rotuloMensagemErro , Type dataType, bool requerido )
            : this( rotuloMensagemErro, dataType, requerido, null, null, null, null, null, null, null, null)
        {
        }

        /// <summary>
        /// Construtor de Validacao
        /// </summary>
        /// <param name="rotuloMensagemErro">Rotulo usado para indentificar os campos usando as mensagens de erro.</param>
        /// <param name="dataType">Tipo do dado a ser validado</param>
        /// <param name="requerido">Indica se o valor precisa ser diferente de <c>null</c></param>
        /// <param name="larguraIgual">Quantidade de carcteres/dígito que o valor sendo validado deve ter</param>
        public Validacao(string rotuloMensagemErro, Type dataType, bool requerido , int ? larguraIgual )
            : this( rotuloMensagemErro, dataType, requerido, null, null, larguraIgual, null, null, null, null, null)
        {
        }

        /// <summary>
        /// Construtor de Validacao
        /// </summary>
        /// <param name="rotuloMensagemErro">Rotulo usado para indentificar os campos usando as mensagens de erro.</param>
        /// <param name="dataType">Tipo do dado a ser validado</param>
        /// <param name="requerido">Indica se o valor precisa ser diferente de <c>null</c></param>
        /// <param name="larguraMinima">Quantidade de carcteres/dígito mínimo do valor sendo validado</param>
        /// <param name="larguraMaxima">Quantidade de carcteres/dígito máximo do valor sendo validado</param>
        public Validacao(string rotuloMensagemErro, Type dataType, bool requerido, int ? larguraMinima , int ? larguraMaxima )
            : this( rotuloMensagemErro, dataType, requerido, larguraMinima, larguraMaxima, null, null, null, null, null, null)
        {
        }

        /// <summary>
        /// Construtor de Validacao
        /// </summary>
        /// <exception cref="ArgumentException">Os parametros <paramref name="larguraMinima"/> ou <paramref name="larguraMaxima"/> possuem valor e <paramref name="larguraIgual"/> também possui valor</exception>
        /// <param name="rotuloMensagemErro">Rotulo usado para indentificar os campos usando as mensagens de erro.</param>
        /// <param name="dataType">Tipo do dado a ser validado</param>
        /// <param name="requerido">Indica se o valor precisa ser diferente de <c>null</c></param>
        /// <param name="larguraMinima">Quantidade de carcteres/dígito mínimo do valor sendo validado</param>
        /// <param name="larguraMaxima">Quantidade de carcteres/dígito máximo do valor sendo validado</param>
        /// <param name="larguraIgual">Quantidade de carcteres/dígito que o valor sendo validado deve ter</param>
        /// <param name="valorIgual">Valor que o valor sendo validado deve ter</param>
        public Validacao(string rotuloMensagemErro, Type dataType, bool requerido, int? larguraMinima, int? larguraMaxima, int? larguraIgual, object valorIgual )
            : this( rotuloMensagemErro, dataType, requerido, larguraMinima, larguraMaxima, larguraIgual, null, null, valorIgual, null, null)
        {
        }

        /// <summary>
        /// Construtor de Validacao
        /// </summary>
        /// <exception cref="ArgumentException">Os parametros <paramref name="larguraMinima"/> ou <paramref name="larguraMaxima"/> possuem valor e <paramref name="larguraIgual"/> também possui valor</exception>
        /// <param name="rotuloMensagemErro">Rotulo usado para indentificar os campos usando as mensagens de erro.</param>
        /// <param name="dataType">Tipo do dado a ser validado</param>
        /// <param name="requerido">Indica se o valor precisa ser diferente de <c>null</c></param>
        /// <param name="larguraMinima">Quantidade de carcteres/dígito mínimo do valor sendo validado</param>
        /// <param name="larguraMaxima">Quantidade de carcteres/dígito máximo do valor sendo validado</param>
        /// <param name="larguraIgual">Quantidade de carcteres/dígito que o valor sendo validado deve ter</param>
        /// <param name="valorMinimo">Valor mínimo do valor sendo validado</param>
        /// <param name="valorMaximo">Valor máximo do valor sendo validado</param>
        public Validacao(string rotuloMensagemErro, Type dataType, bool requerido, int? larguraMinima, int? larguraMaxima, int? larguraIgual , object valorMinimo, object valorMaximo )
            : this( rotuloMensagemErro, dataType, requerido, larguraMinima, larguraMaxima, larguraIgual, valorMinimo, valorMaximo, null, null, null)
        {
        }

        /// <summary>
        /// Construtor de Validacao
        /// </summary>
        /// <exception cref="ArgumentException">Os parametros <paramref name="larguraMinima"/> ou <paramref name="larguraMaxima"/> possuem valor e <paramref name="larguraIgual"/> também possui valor</exception>
        /// <exception cref="ArgumentException">Os parametros <paramref name="valorMinimo"/> ou <paramref name="valorMaximo"/> possuem valor e <paramref name="valorIgual"/> também possui valor</exception>
        /// <param name="rotuloMensagemErro">Rotulo usado para indentificar os campos usando as mensagens de erro.</param>
        /// <param name="dataType">Tipo do dado a ser validado</param>
        /// <param name="requerido">Indica se o valor precisa ser diferente de <c>null</c></param>
        /// <param name="larguraMinima">Quantidade de carcteres/dígito mínimo do valor sendo validado</param>
        /// <param name="larguraMaxima">Quantidade de carcteres/dígito máximo do valor sendo validado</param>
        /// <param name="larguraIgual">Quantidade de carcteres/dígito que o valor sendo validado deve ter</param>
        /// <param name="valorMinimo">Valor mínimo do valor sendo validado</param>
        /// <param name="valorMaximo">Valor máximo do valor sendo validado</param>
        /// <param name="valorIgual">Valor que o valor sendo validado deve ter</param>
        /// <param name="formatProvider">Provider de formatacao usado quando convertendo o valor ou quando exibindo os valores minimo e maximo</param>
        /// <param name="regexPattern">Padrao de validação usando o regex</param>
        public Validacao(string rotuloMensagemErro, Type dataType, bool requerido, int? larguraMinima, int? larguraMaxima, int? larguraIgual, object valorMinimo, object valorMaximo, object valorIgual, IFormatProvider formatProvider, string regexPattern)
        {
            if (larguraIgual != null && (larguraMinima != null || larguraMaxima != null))
                throw new ArgumentException("Não se pode informar largura mínima ou máxima e uma largura igual simultanamente.");
            if (valorIgual != null && (valorMinimo != null || valorMaximo != null))
                throw new ArgumentException("Não se pode informar valor mínimo ou máximo e um valor igual simultanamente.");
            // Coloca o set de format provider no inicio para que as propriedades de intervalo de valores usem ele
            this.FormatProvider = formatProvider;
            this.RotuloMensagemErro = rotuloMensagemErro;
            this.DataType = dataType;
            this.Requerido = requerido;
            this.LarguraMinima = larguraMinima;
            this.LarguraMaxima = larguraMaxima;
            this.LarguraIgual = larguraIgual;
            this.ValorMinimo = valorMinimo;
            this.ValorMaximo = valorMaximo;
            this.ValorIgual = valorIgual;
            this.Regex = !String.IsNullOrEmpty(regexPattern) ? new Regex(regexPattern) : null;
        }

        #endregion

        #region Propriedades

        /// <summary>
        /// Rotulo usado para compor as mensagens de erros se <see cref="MensagemCustomizada"/> não for definida
        /// </summary>
        public string RotuloMensagemErro
        {
            get { return _RotuloMensagemErro; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("RotuloMensagemErro");
                else if (value.Trim().Length == 0)
                    throw new ArgumentException("O valor informado não pode ser em branco", "CaptionErrorMessage");
                _RotuloMensagemErro = value;
            }
        }

        /// <summary>
        /// Define uma mensagem customizada para qualquer erro encontrado no processo de validação. Atribuia null ou <see cref="String.Empty"/>
        /// para desabilitar.
        /// </summary>
        public string MensagemCustomizada
        {
            get { return _MensagemCustomizada; }
            set { _MensagemCustomizada = value; }
        }

        /// <summary>
        /// Tipo de dado valido
        /// </summary>
        public Type DataType
        {
            get { return _DataType; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("DataType");
                _DataType = value;
            }
        }

        /// <summary>
        /// Indica se um valor diferente de null é necessário para a validação da classe
        /// </summary>
        public bool Requerido
        {
            get { return _Requerido; }
            set
            {
                _Requerido = value;
                if (_Requerido)
                {
                    AtribuirTipoValidacao(TipoValidacao.Requerido);
                }
            }
        }

        /// <summary>
        /// Prove um mecanismo de controlar a formatacao
        /// </summary>
        public IFormatProvider FormatProvider
        {
            get { return _FormatProvider; }
            set
            {
                _FormatProvider = value;
            }
        }

        /// <summary>
        /// O valor precisa ser igual ao valor indicado
        /// </summary>
        public object ValorIgual
        {
            get { return this._ValorIgual; }
            set
            {
                TipoValidacao tipoValidacao = ObterValorTipoValidacao(value,
                    TipoValidacao.ValorIgual, TipoValidacao.ValorIgual);
                if (value != null)
                {
                    this.ConfigParaIntervaloValorValido(ref value);
                    this.ValidacaoPorIntervaloValoresEhPossivel(tipoValidacao, DataType);

                    if (this.ValorMinimo != null)
                        this.ValorMinimo = null;
                    if (this.ValorMaximo != null)
                        this.ValorMaximo = null;
                }
                this.TipoValidacao = tipoValidacao;
                this._ValorIgual = value;
            }
        }

        /// <summary>
        /// O valor mínimo o valor sendo validado deve ter
        /// </summary>
        public object ValorMinimo
        {
            get { return this._ValorMinimo; }
            set
            {
                ConfigValoresMinimoMaximo(ref value,
                    TipoValidacao.ValorMinimoIgual,
                    TipoValidacao.ValorMinimo | TipoValidacao.ValorMinimoIgual
                );
                this._ValorMinimo = value;
            }
        }

        /// <summary>
        /// O valor máximo o valor sendo validado deve ter
        /// </summary>
        public object ValorMaximo
        {
            get { return this._ValorMaximo; }
            set
            {
                ConfigValoresMinimoMaximo(ref value,
                    TipoValidacao.ValorMaximoIgual,
                    TipoValidacao.ValorMaximo | TipoValidacao.ValorMaximoIgual
                );
                this._ValorMaximo = value;
            }
        }

        /// <summary>
        /// Quantidade de caracteres/digito que o valor sendo validado deve conter
        /// </summary>
        public int? LarguraIgual
        {
            get { return this._LarguraIgual; }
            set
            {
                this.TipoValidacao = ConfigValorValidacao(value,
                    TipoValidacao.LarguraIgual, TipoValidacao.LarguraIgual);
                if (value != null)
                {
                    if (this.LarguraMinima != null)
                        this.LarguraMinima = null;
                    if (this.LarguraMaxima != null)
                        this.LarguraMaxima = null;
                }
                this._LarguraIgual = value;
            }
        }

        /// <summary>
        /// Quantidade de caracteres/digito minima do valor sendo validado
        /// </summary>
        public int? LarguraMinima
        {
            get { return this._LarguraMinima; }
            set
            {
                ConfigLarguraMinimaMaxima(value,
                    TipoValidacao.LarguraMinimaIgual,
                    TipoValidacao.LarguraMinima | TipoValidacao.LarguraMinimaIgual
                    );
                this._LarguraMinima = value;
            }
        }

        /// <summary>
        /// Quantidade de caracteres/digito maxima do valor sendo validado
        /// </summary>
        public int? LarguraMaxima
        {
            get { return this._LarguraMaxima; }
            set
            {
                ConfigLarguraMinimaMaxima(value,
                    TipoValidacao.LarguraMaximaIgual,
                    TipoValidacao.LarguraMaxima | TipoValidacao.LarguraMaximaIgual
                    );
                this._LarguraMaxima = value;
            }
        }

        /// <summary>
        /// Expressao regular usada para validar o valor
        /// </summary>
        public Regex Regex
        {
            get { return _Regex; }
            set
            {
                this.TipoValidacao = ObterValorTipoValidacao(value,
                    TipoValidacao.Regex,
                    TipoValidacao.Regex
                    );
                this._Regex = value;
            }
        }

        /// <summary>
        /// Essa é o evento executado quando fazendo uma validacao <see cref="Acontep.Validacoes.TipoValidacao.Customizada"/>
        /// </summary>
        public event ValidacaoEventHandler ValidandoValor
        {
            add
            {
                _ValidandoValor += value;
                AtribuirTipoValidacao(TipoValidacao.Customizada);
            }
            remove
            {
                _ValidandoValor -= value;
                if (_ValidandoValor.GetInvocationList().Length == 0)
                    RemoverTipoValidacao(TipoValidacao.Customizada);
            }
        }

        /// <summary>
        /// Contem o tipo de validacao que deve ser feita em um valor informado
        /// </summary>
        public TipoValidacao TipoValidacao
        {
            get { return this._TipoValidacao; }
            set { this._TipoValidacao = value; }
        }

        #endregion

        /// <summary>
        /// Adiciona um tipo de validacao a essa instancia
        /// </summary>
        public void AtribuirTipoValidacao(TipoValidacao novo)
        {
            TipoValidacao tipo = _TipoValidacao;
            this.AtribuirTipoValidacao(ref tipo, novo);
            this.ValidarSePossuiValorParaTipoValidacao(tipo);
            _TipoValidacao = tipo;
        }

        /// <summary>
        /// Remove um tipo de validacao dessa instancia
        /// </summary>
        public void RemoverTipoValidacao(TipoValidacao remover)
        {
            this.RemoverTipoValidacao(ref _TipoValidacao, remover);
        }

        /// <summary>
        /// Retorna um boolean se o valor for valido dentro das configuracoes do objeto
        /// </summary>
        /// <param name="valor">Valor a ser validado</param>
        /// <returns>True se o valor foi valido, fale caso contrario</returns>
        public bool EhValido(object valor)
        {
            return ObterMensagemValidacao(valor).Length == 0;
        }

        /// <summary>
        /// Retorna um boolean se o valor for valido dentro das configuracoes da classe
        /// </summary>
        /// <param name="valor">valor a ser validado</param>
        /// <param name="mensagemErro">mensagem indicando o erro ocorrido</param>
        /// <returns>True se o valor for valido, false caso contrario</returns>
        public bool EhValido(object valor, out string mensagemErro)
        {
            mensagemErro = ObterMensagemValidacao(valor);
            return mensagemErro.Length == 0;
        }

        /// <summary>
        /// Valida o valor passado como parametro
        /// </summary>
        /// <exception cref="ValorInvalidoException">O valor informado não é válido de acordo com as regras configuradas</exception>
        /// <param name="valor">Valor a ser validado</param>
        public void Validar(object valor)
        {
            Validar(ref valor);
        }

        /// <summary>
        /// Retorna texto descritivo com informacoes da validacao do valor
        /// </summary>
        /// <param name="valor">Valor a ser validado</param>
        /// <returns>Erro ocorrido ou <c>String.Empty</c> se o valor esta correto</returns>
        public string ObterMensagemValidacao(object valor)
        {
            return FazerValidacaoGeral(ref valor);
        }

        /// <summary>
        /// Usando as regras definidas valida e converte para o tipo configurado o valor passado como parametro
        /// </summary>
        /// <param name="valor">Valor a ser convertido</param>
        /// <returns>Valor convertido</returns>
        public object Converter(object valor)
        {
            this.Validar(ref valor);
            return valor;
        }
    }
}