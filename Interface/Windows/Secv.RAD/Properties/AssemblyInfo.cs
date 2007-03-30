using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Utilitário de desenvolvimento da CIAL [.NET 2.0]")]
[assembly: AssemblyDescription(
@"Versão [1.2.2.1] - 2007-03-09
Autor: Francisco Rodrigues
Adequação:
    - Removido o suporte de ""autocomplete"" em consultas que utilizam LIKE.

Versão [1.2.2.0] - 2007-03-09
Autor: Francisco Rodrigues
Adição:
    - Inteligência para decidir qual chava primária utilizar quando não existe nenhuma chave primária com o nome PK_<Nome da tabela>.

Versão [1.2.1.1] - 2007-03-09
Autor: Francisco Rodrigues
Correção:
    - Geração de SQL de consulta quando modificado o parametro que ativa ou não o caracter coringa.

Versão [1.2.1.0] - 2007-03-09
Autor: Francisco Rodrigues
Adição:
    - Adição no arquivo de configurações a possibilidade de retirar e % das consultas;
    - Adição no arquivo de configurações um campo para representar o símbolo coringa em LIKE;
    - Adição no arquivo de configurações um campo para representar o símbolo de concatenação de string;    
    - Adicionada a data da última modificação no título.
Correção:
    - Correção da exibição do nome da tabela quando não existe nenhuma primary key definida;
    - Correção na geração de consultas quando existem dois índices com o mesmo prefixo e com campos diferentes (quantidade ou nome);
    - Correção na recuperação da chave primária (a tabela possuia o nome correto e mesmo assim ele ignorava a chave primária por motivo desconhecido)

Versão [1.2.0.0] - 2007-02-09
Autor: Francisco Rodrigues
Adição:
    - Mensagem avisando que a tabela não possui primary key (necessário devido a forma que os métodos são gerados pelo crud possibilitando exclusão/atualização de todas as linhas);
    - Mensagem avisando que não existem todos os campos de controle transacional (Avisar quando os campos de controle interno não existem – Solicitado devido existir algumas tabelas novas sem esses campos, detectado quando atualizei os scripts);
    - Geração de consulta com o ‘%’ no final do texto para campos texto (DbType.AnsiString, DbType.AnsiStringFixedLength, DbType.String e DbType.StringFixedLength);
    - Modificado o tipo de dado a ser adicionado como parametro quando o campo for System.String (antes era informado DbType.String agora está sendo informado DbType.AnsiString).

Versão [1.1.4.4] - 2007-01-08
Autor: Francisco Rodrigues
Correção:
    - Correção de fix.

Versão [1.1.4.3] - 2007-01-08
Autor: Francisco Rodrigues
Correção:
    - Não recuperava corretamente as chaves para alteração/exclusão.

Versão [1.1.4.2] - 2007-01-05
Autor: Francisco Rodrigues
Correção:
    - Não informava a nova versão no arquivo.

Versão [1.1.4.1] - 2007-01-03
Autor: Francisco Rodrigues
Adição:
    - Suporte a migração do arquivo CRUD salvo.

Versão [1.1.4.0] - 2007-01-03
Autor: Francisco Rodrigues
Correção:
    - Correção do CRUD para usar as primary keys ao invés do Identity da tabela.

Versão [1.1.3.3]/[1.1.3.2]/[1.1.3.1] - 2007-01-02
Autor: Francisco Rodrigues
Correção:
    - Correção do CRUD para obter os métodos de consulta.

Versão [1.1.3.0] - 2007-01-02
Autor: Francisco Rodrigues
Melhoramento:
    - Adicionado suporte a schema no banco.
  
Versão [1.1.2.0] - 2006-09-29
Autor: Francisco Rodrigues
Melhoramento:
    - Adicionado TimeOut personalizado por listas de arquivos.
  
Versão [1.1.1.1] - 2006-09-29
Autor: Francisco Rodrigues
Melhoramento:
    - Extraido o core da execução dos arquivos para um novo projeto, visando a possibilidade da execução via DOS;
    
Versão [1.1.1.0] - 2006-09-28
Autor: Francisco Rodrigues
Melhoramento:
    - Os caminhos da lista de arquivos que serão executados são guardados em ""caminho virtual"", tendo como base o local onde o arquivo do projeto foi salvo.
    - Separado os botões de ""adicionar"" e ""executar"" afim de evitar uma execução não planejada ou incompleta.
Corrigido:
    - Mensagem do término do processo.

Versão [1.1.0.0] - 2006-09-26
Autor: Francisco Rodrigues
Adicionado:
    Tela para rodar uma lista de scripts sql.

Versão [1.0.1.0] - 2006-09-21
Autor: Francisco Rodrigues
Corrigido:
    - Adição da sugestão para o nome do arquivo CRUD ou Fachada
    - Bug no nome da classe de fachada de negócio
Melhoramento:
    - Editor de texto com a possibilidade de filtrar e comandos comuns de edição (desfazer e refazer)

Versão [1.0.0.2] - 2006-09-15
Autor: Francisco Rodrigues	
Corrigido:
    - Problema com o tipo de dados BIT e smallint
    - Problema com a geração do método de excluir do CRUD
Adicionado
    - Opção para gerar classe ""Fachada de negócio""

Versão [1.0.0.1] - 2006-08-30
Autor: Francisco Rodrigues	
Adicionado:
	- Atalhos para as chamadas de tela;
	- Caminho padrão para abrir e salvar arquivos;
	- Chaves para personalizar as configurações;
	- Exibição da versão utilizada;
Corrigido:
	- Criação/abertura de um arquivo já existente.
	- Corrigido a escolha do nome (usar o default ou o informado pelo usuário) para os métodos de insert, update, select, delete.
	- Bug que deixava os metodos padrões (I/U/S/D) quando dava um erro no momento da geração do código;	")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Cia Alagoana de Refrigerantes")]
[assembly: AssemblyProduct("Gerador CRUD")]
[assembly: AssemblyCopyright("Copyright © Cia Alagoana de Refrigerantes 2006")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]


// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("cc6a56ff-07b0-4f07-ac40-eace9d391d7a")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
[assembly: AssemblyVersion("1.2.2.1")]
[assembly: AssemblyFileVersion("1.2.2.1")]
