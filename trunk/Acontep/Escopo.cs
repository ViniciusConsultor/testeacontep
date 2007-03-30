using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Configuration;
using Acontep.Dado;
using Acontep.Seguranca;
using System.Runtime.CompilerServices;
using System.Data;
using System.Security.Principal;

namespace Acontep
{
    /// <summary>
    /// Informações do escopo de execução da aplicação
    /// </summary>
    /// <remarks>Esta classe não é thread-safe</remarks>
    public class Escopo : Componente, IDisposable
    {
        #region Campos da classe

        [ThreadStatic]
        private static Escopo _Atual;

        /// <summary>
        /// Escopo de execucao ativo
        /// </summary>
        public static Escopo Atual
        {
            get { return Escopo._Atual; }
        }

        /// <summary>
        /// Identidado do usuario
        /// </summary>
        public static IIdentity Identity
        {
            get { return Escopo.Atual.ContextoAutenticacao.Principal.Identity; }
        }

        /// <summary>
        /// 
        /// </summary>
        public static DbConnection Connection
        {
            get { return Escopo.Atual.ContextoAcessoDado.Connection; }
        }

        /// <summary>
        /// 
        /// </summary>
        public static DbTransaction Transaction
        {
            get { return Escopo.Atual.ContextoAcessoDado.Transaction; }
        }

        #endregion

        #region Operadores

        /// <summary>
        /// Compara igualdade usando <see cref="Equals"/> fazendo com que o resultado sempre seja
        /// baseado no escopo atual
        /// </summary>
        /// <param name="esquerda"></param>
        /// <param name="direita"></param>
        /// <returns></returns>
        public static bool operator ==(Escopo esquerda, Escopo direita)
        {
            bool esquerdaNull, direitaNull;
            esquerdaNull = Object.ReferenceEquals(esquerda, null);
            direitaNull = Object.ReferenceEquals(direita, null);
            // Ambos sao valores nulos
            if (esquerdaNull && direitaNull)
                return true;
            // Somente um dos lados possui valor null
            else if (esquerdaNull || direitaNull)
                return false;
            // Caso contrario verifica usando Equals
            else
                // Compara da esquerda para a direita e da direita para esquerda
                // para que (Atual == escopo) retorne o mesmo resultado de (escopo == atual)
                return esquerda.Equals(direita) && direita.Equals(esquerda);
        }

        /// <summary>
        /// Compara diferenca usando <see cref="Equals"/> fazendo com que o resultado sempre seja
        /// baseado no escopo atual
        /// </summary>
        /// <param name="esquerda"></param>
        /// <param name="direita"></param>
        /// <returns></returns>
        public static bool operator !=(Escopo esquerda, Escopo direita)
        {
            // Reaproveita codigo de == e nega o seu retorno
            return !(esquerda == direita);
        }

        #endregion

        #region Construtores

        /// <summary>
        /// Copia o escopo informado para <see cref="Escopo.Atual"/>
        /// </summary>
        /// <remarks>
        /// Geralmente usado em casos onde o escopo atual é usado em outras Threads para aproveitar 
        /// a mesma transacao por exemplo.
        /// </remarks>
        /// <param name="escopo"></param>
        public Escopo(Escopo escopo)
        {
            if (escopo.EstaFinalizado)
                throw new ObjectDisposedException("Não posso inicializar um escopo a partir de outro escopo finalizado");
            if (escopo == null)
                throw new NullReferenceException();
            this.AtribuirEscopoAtual();
        }

        /// <summary>
        /// Inicia um novo escopo de execução ou participa de um existente
        /// </summary>
        public Escopo()
            : this(OpcaoEscopo.Requerido, ContextoAcessoDado.NOME_CONEXAO_GERAL, OpcaoTransacao.Requerido)
        {
        }

        /// <summary>
        /// Inicia um novo escopo de execução ou participa de um existente
        /// </summary>
        /// <param name="opcaoTransacao">Opcao da transacao. Se for <see cref="OpcaoTransacao.Requerido"/> 
        /// e ja existir um escopo, um novo escopo é criado de forma transparente</param>
        public Escopo(OpcaoTransacao opcaoTransacao)
            : this(ContextoAcessoDado.NOME_CONEXAO_GERAL, opcaoTransacao)
        {
        }

        /// <summary>
        /// Inicia um novo escopo de execução ou participa de um existente
        /// </summary>
        /// <param name="nomeConfigConexao">Nome da conexao em connectionSettings</param>
        /// <param name="opcaoTransacao">Opcao da transacao. Se for <see cref="OpcaoTransacao.Requerido"/> 
        /// e ja existir um escopo, um novo escopo é criado de forma transparente</param>
        public Escopo(string nomeConfigConexao, OpcaoTransacao opcaoTransacao)
        {
            if (!OpcaoTransacaoRequerNovoEscopo(opcaoTransacao))
                InicializarEscopo(OpcaoEscopo.Requerido, nomeConfigConexao, opcaoTransacao);
            else
                // Se precisar mudo o comportamento padrão de inicializacao
                InicializarEscopo(OpcaoEscopo.RequerNovo, nomeConfigConexao, opcaoTransacao);
        }

        /// <summary>
        /// Inicia um novo escopo de execução ou participa de um existente
        /// </summary>
        /// <exception cref="InvalidOperationException"><see cref="OpcaoTransacao.Requerido"/> exige <see cref="OpcaoEscopo.Requerido"/></exception>
        /// se um escopo ja existir sem uma transação ativa
        /// <param name="opcaoEscopo">Configura a forma como esse escopo vai participar de um ja existente</param>
        /// <param name="opcaoTransacao">Configura a forma como esse escopo vai participar de uma transação</param>
        public Escopo(OpcaoEscopo opcaoEscopo, OpcaoTransacao opcaoTransacao)
            : this(opcaoEscopo, ContextoAcessoDado.NOME_CONEXAO_GERAL, opcaoTransacao)
        {
        }

        /// <summary>
        /// Inicia um novo escopo de execução com <paramref name="acessoDado"/>
        /// </summary>
        /// <param name="acessoDado">Inicia um escopo com este contexto de acesso a dado</param>
        public Escopo(ContextoAcessoDado acessoDado)
        {
            InicializarEscopo(acessoDado);
        }

        /// <summary>
        /// Inicia um novo escopo de execução ou participa de um existente
        /// </summary>
        /// <exception cref="InvalidOperationException"><see cref="OpcaoTransacao.Requerido"/> exige <see cref="OpcaoEscopo.Requerido"/></exception>
        /// se um escopo ja existir sem uma transação ativa
        /// <param name="opcaoEscopo">Configura a forma como esse escopo vai participar de um ja existente</param>
        /// <param name="nomeConfigConexao">Nome da configuração de conexao em connectionStrings do arquivo de configuracao da aplicacao</param>
        /// <param name="opcaoTransacao">Configura a forma como esse escopo vai participar de uma transação</param>
        public Escopo(OpcaoEscopo opcaoEscopo, string nomeConfigConexao, OpcaoTransacao opcaoTransacao)
        {
            // Se o usuario informou explicitamente opcoes de escopo e de transacao provoca erro
            if (OpcaoTransacaoRequerNovoEscopo(opcaoTransacao) && opcaoEscopo == OpcaoEscopo.Requerido)
                throw new InvalidOperationException("Erro Transação Requer Novo Escopo");
            InicializarEscopo(opcaoEscopo, nomeConfigConexao, opcaoTransacao);
        }

        #endregion Construtores

        #region Campos da instancia
        /// <summary>
        /// Escopo anterior na fila de escops dessa Thread
        /// </summary>
        private Escopo _EscopoAnterior;
        /// <summary>
        /// <see cref="ContextoAcessoDado"/>
        /// </summary>
        private ContextoAcessoDado _ContextoAcessoDado;
        /// <summary>
        /// <see cref="ContextoAutenticacao"/>
        /// </summary>
        private ContextoAutenticacao _ContextoAutenticacao;
        /// <summary>
        /// Faz contagem de quantos objetos <see cref="Escopo"/> foram instanciados dentro do escopo atual
        /// </summary>
        private int _ContadorEscopo;
        /// <summary>
        /// Indicado se o trabalho do escopo foi terminado com sucesso
        /// </summary>
        private bool _MarcadoComoCompleto;

        /// <summary>
        /// Obtem o contador de escopo de instancias referente a <see cref="Escopo.Atual"/>
        /// </summary>
        private int ContadorEscopo
        {
            get { return Atual._ContadorEscopo; }
            set { Atual._ContadorEscopo = value; }
        }

        /// <summary>
        /// Indica se <see cref="Escopo.Atual"/> foi marcado como finalizado com sucesso. So surte
        /// efeito se estivermos lidando com o Escopo raiz de uma hierarquia de escopos
        /// </summary>
        /// <remarks>
        /// Apenas se a ultima linha do primeiro escopo iniciado a atribuicao de finalizado com sucesso
        /// ser valida
        /// </remarks>
        private bool MarcadoComoCompleto
        {
            get { return Atual._MarcadoComoCompleto; }
            set
            {
                if (ContadorEscopo == 1)
                    Atual._MarcadoComoCompleto = value;
            }
        }

        #endregion

        #region Propriedades

        /// <summary>
        /// Informacoes relevantes a acesso a dados para o escopo
        /// </summary>
        /// <remarks>
        /// Sempre fornece uma conexao aberta
        /// </remarks>
        public ContextoAcessoDado ContextoAcessoDado
        {
            get
            {
                Atual.ChecarDisposed();
                if (Atual._ContextoAcessoDado.Connection.State == ConnectionState.Closed)
                    Atual._ContextoAcessoDado.Connection.Open();
                return Atual._ContextoAcessoDado;
            }
        }

        /// <summary>
        /// Informacoes relevantes a autenticacao para o escopo
        /// </summary>
        public ContextoAutenticacao ContextoAutenticacao
        {
            get
            {
                Atual.ChecarDisposed();
                return Atual._ContextoAutenticacao;
            }
        }

        #endregion Propriedades

        /// <summary>
        /// Informa que o escopo atual chegou ao seu fim com sucesso.
        /// </summary>
        /// <remarks>
        /// Sempre deve ser o ultimo comando de uma clausula <c>using</c>. Se existe uma transacao no escopo
        /// atual a transação sofrerá um Commit se <c>Terminar</c> for chamado na raiz da hierarquia de escopos
        /// criados.
        /// A chamada a esse metodo deve ser a ultima linha antes da chamada do <see cref="Dispose"/>. Dessa
        /// forma, se ele for executado na raiz da árvore de escopos isso significa que nenhuma excessão ocorreu.
        /// </remarks>
        public void Terminar()
        {
            ChecarDisposed();
            MarcadoComoCompleto = true;
        }

        /// <summary>
        /// HashCode do escopo atual
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Atual.GetHashCode();
        }

        /// <summary>
        /// Verifica se <paramref name="obj"/> é igual ao escopo atual ja que a instancia corrente sempre é o
        /// escopo atual
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return Object.ReferenceEquals(Atual, obj);
        }

        #region IDisposable Members

        /// <summary>
        /// Valor da mensagem de erro de <see cref="ObjectDisposedException"/>
        /// </summary>
        protected override string MensagemErroObjectDisposedException
        {
            get { return "Escopo"; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            bool possoFinalizar = !EstaFinalizado && disposing && --(Atual.ContadorEscopo) < 1;
            if (possoFinalizar)
            {
                bool marcadoComoCompleto = MarcadoComoCompleto;
                // Primeiro, remover esta instancia da fila (mas, apenas se nos somos o atual da fila e se nosso 
                // contador de escopos indicar que somos o primeiro criado)
                //  Nota: Thread-local _Atual, e requerimento de que o escopo nao sofra dispose em outras threads
                //      significam que podemos dispensar o lock.
                if (_Atual == this)
                {
                    // Caso o usuario tenha chamado dispose fora de ordem, retorna na fila
                    // ate encontrar objetos que nao tenham sido finalizados (chamada a dispose).
                    Escopo anterior = _EscopoAnterior;
                    while (anterior != null && anterior.EstaFinalizado)
                    {
                        anterior = anterior._EscopoAnterior;
                    }
                    _Atual = anterior;
                }
                // segundo, marcar nosso estado interno como finalizado
                ContextoAcessoDado contextoAcessoDado = _ContextoAcessoDado;
                _ContextoAcessoDado = null;
                // Por ultimo, limpar os escopos de dados mantidos por mim
                FinalizarContextoAcessoDado(contextoAcessoDado, marcadoComoCompleto);
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Faz Commit na transacao se for necessario e chama <see cref="Acontep.Dado.ContextoAcessoDado.Dispose"/>
        /// </summary>
        /// <param name="contextoAcessoDado">Valor a sofrer o processo de finalizacao</param>
        /// <param name="marcadoComoCompleto"></param>
        private void FinalizarContextoAcessoDado(ContextoAcessoDado contextoAcessoDado, bool marcadoComoCompleto)
        {
            if (contextoAcessoDado != null)
            {
                try
                {
                    // Se o escopo foi marcado como terminado entao faz um commit na transacao
                    if (marcadoComoCompleto && contextoAcessoDado.Transaction != null)
                        contextoAcessoDado.Transaction.Commit();
                }
                finally
                {
                    contextoAcessoDado.Dispose();
                }
            }
        }

        #endregion

        #region Operacoes privadas

        private void InicializarEscopo(ContextoAcessoDado acessoDado)
        {
            bool precisaMudarEscopo;
            VerificarMudancaEscopo(OpcaoEscopo.RequerNovo, String.Empty, out precisaMudarEscopo);

            if (precisaMudarEscopo)
            {
                // Inicializando apenas se um novo escopo vai ser criado
                _ContextoAcessoDado = acessoDado;
                AtribuirEscopoAtual();
            }
        }

        private void InicializarEscopo(OpcaoEscopo opcaoEscopo, string nomeConfigConexao, OpcaoTransacao opcaoTransacao)
        {
            // nos precisamos mudar _Atual?
            bool precisaMudarEscopo;
            VerificarMudancaEscopo(opcaoEscopo, nomeConfigConexao, out precisaMudarEscopo);

            //this.InicializarEscopo(opcaoEscopo, );
            if (precisaMudarEscopo)
            {
                // Inicializando apenas se um novo escopo vai ser criado
                _ContextoAcessoDado = new ContextoAcessoDado(nomeConfigConexao);
                _ContextoAutenticacao = new ContextoAutenticacao();
                AtribuirEscopoAtual();
                // Uma transacao nunca é iniciada em um escopo que nao inciou transacional
                ConfigurarTransacao(opcaoTransacao);
            }
        }

        /// <summary>
        /// Identifica se um escopo precisa ser mudado e também se ele pode ser mudado
        /// </summary>
        /// <param name="opcaoEscopo"></param>
        /// <param name="nomeConfigConexao"></param>
        /// <param name="precisaMudarEscopo"></param>
        private void VerificarMudancaEscopo(OpcaoEscopo opcaoEscopo, string nomeConfigConexao, out bool precisaMudarEscopo)
        {
            precisaMudarEscopo = PrecisaMudarEscopo(opcaoEscopo);

            if (!precisaMudarEscopo)
            {
                // Se nao instanciou novo Escopo e usou o existente entao suprime finalizador
                GC.SuppressFinalize(this);
                if (_Atual != null)
                {
                    if (ContextoAcessoDado.ConnectionStringSettingName != nomeConfigConexao)
                        throw new InvalidOperationException("Para conexoes diferentes um novo escopo é necessario");
                    Atual.ContadorEscopo += 1;
                }
            }
        }

        /// <summary>
        /// Indica se <see cref="Atual"/> precisa ser mudado para um novo escopo
        /// </summary>
        /// <param name="opcaoEscopo"></param>
        /// <returns>True se um novo escopo precisa ser criado para <see cref="Atual"/></returns>
        private static bool PrecisaMudarEscopo(OpcaoEscopo opcaoEscopo)
        {
            bool precisaMudarEscopo;
            switch (opcaoEscopo)
            {
                case OpcaoEscopo.Requerido:
                    precisaMudarEscopo = (_Atual == null);
                    break;
                case OpcaoEscopo.RequerNovo:
                    precisaMudarEscopo = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("opção");
            }
            return precisaMudarEscopo;
        }

        /// <summary>
        /// Faz atribuicao do escopo atual com o valor desta instancia
        /// </summary>
        private void AtribuirEscopoAtual()
        {
            // Ordem de atribuicao inicial é importante em caso de erro!
            //  _EscopoAnterior primeiro assegura que nos saberemos que nós precisamos restaurar
            //  _EstaFinalizado segundo, para termos certeza de que nos nao faremos dispose ate nós estarmos tão próximos 
            //      quanto possível para fazermos uma configuração correta (leia todos os outros campos sao setados antes de
            //      _EstaFinalizado possuir valor null
            //  __Atual finalmente, para termos certeza de que thread static possuem apenas objetos validos
            _EscopoAnterior = _Atual;
            _MarcadoComoCompleto = false;
            _Atual = this;
            this.ContadorEscopo = 1;
        }

        /// <summary>
        /// Inicializa propriedade <see cref="Acontep.Dado.ContextoAcessoDado.Transaction"/>
        /// </summary>
        /// <param name="opcaoTransacao"></param>
        private void ConfigurarTransacao(OpcaoTransacao opcaoTransacao)
        {
            if (opcaoTransacao == OpcaoTransacao.Requerido)
            {
                // Assume que se chegou aqui nao existe transacao
                ContextoAcessoDado.Transaction = ContextoAcessoDado.Connection.BeginTransaction();
            }
        }

        /// <summary>
        /// Identifica se com a <paramref name="opcaoTransacao"/> informado um novo escopo deve ser criado
        /// para delimitar a vida util da transacao.
        /// </summary>
        /// <remarks>
        /// Se <see cref="OpcaoTransacao.Requerido"/> for informado e <see cref="Escopo.Atual"/> nao possuir
        /// uma transacao então um novo escopo deve ser criado para que ao fim dele o commit ou rollback ocorra.
        /// </remarks>
        /// <param name="opcaoTransacao"></param>
        /// <returns></returns>
        private bool OpcaoTransacaoRequerNovoEscopo(OpcaoTransacao opcaoTransacao)
        {
            // Se nao existir nenhum atualmente entao nao preciso fazer nada de diferente
            // na inicializacao
            if (Escopo.Atual == null)
                return false;
            else
                return opcaoTransacao == OpcaoTransacao.Requerido &&
                    Escopo.Atual.ContextoAcessoDado.Transaction == null;
        }

        #endregion Operacoes privadas
    }
}
