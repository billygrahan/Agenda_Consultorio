# Agenda_Consult�rio  

Este � um sistema de gerenciamento para um consult�rio odontol�gico, desenvolvido em **.NET**. Ele permite agendar e gerenciar consultas de pacientes, al�m de realizar opera��es como cadastro, exclus�o e listagem.  

## Nota Importante  
**O programa ainda est� em desenvolvimento e precisa de ajustes para melhor desempenho.**  
As valida��es e consultas, que atualmente s�o feitas diretamente nas listas em mem�ria, dever�o ser reimplementadas para serem realizadas no banco de dados. No in�cio da execu��o, todas as tabelas s�o importadas para listas em mem�ria para realizar essas opera��es.  

## Funcionalidades  

### 1. **Cadastro de Pacientes**  
- **Cadastrar paciente**: Cadastro de pacientes informando nome, CPF e data de nascimento.  
- **Excluir paciente**: Exclui um paciente, desde que n�o possua agendamentos futuros.  
- **Listar pacientes**: Permite visualizar os pacientes cadastrados com as seguintes op��es de ordena��o:  
  - Por **CPF**.  
  - Por **nome**.  

### 2. **Agenda de Consultas**  
- **Agendar consulta**: Permite agendar consultas com valida��o de hor�rios e conflitos.  
- **Cancelar consulta**: Remove consultas futuras da agenda.  
- **Listar agendamentos**: Exibe os agendamentos com detalhes, com filtro opcional por per�odo.  

## Estrutura do Projeto  

1. **Controllers**: Intera��o com o banco de dados.  
2. **Models**: Representam os dados do sistema:  
    - `Paciente`: Gerencia informa��es dos pacientes.  
    - `Agendamento`: Gerencia dados de consultas agendadas.  
3. **Views**: Respons�veis pela intera��o visual (menus e respostas).  
4. **Sistema**: Classe principal que orquestra o funcionamento do sistema.  
5. **Program.cs**: Cont�m o ponto de entrada do sistema.  

## Como Executar  

### 1. Clonar o Reposit�rio  

Clone o reposit�rio para o seu ambiente local:  
```bash  
git clone https://github.com/billygrahan/Agenda_Consultorio  
cd Agenda_Consultorio  
```  

### 2. Restaurar Depend�ncias  

Use o comando abaixo para restaurar as depend�ncias do projeto:  
```bash  
dotnet restore  
```  

### 3. Configurar a String de Conex�o  

1. Abra o arquivo `AppDbContext.cs` na pasta `Context`.  
2. Localize o m�todo `OnConfiguring` e ajuste os valores de conex�o:  
   ```csharp  
   string host = "SEU_HOST"; // Exemplo: "localhost"  
   string username = "SEU_USUARIO"; // Exemplo: "postgres"  
   string password = "SUA_SENHA"; // Exemplo: "@marelO50"  
   string database = "SEU_BANCO_DE_DADOS"; // Exemplo: "ApiConsult�rioBD"  

   string connectionString = $"Host={host};Username={username};Password={password};Database={database}";  

   optionsBuilder.UseNpgsql(connectionString);  
   ```  
3. Salve as altera��es.  

### 4. Migrar o Banco de Dados  

Crie as tabelas no banco de dados utilizando as migrations:  
```bash  
dotnet ef database update  
```  

### 5. Compilar e Executar  

Compile o projeto:  
```bash  
dotnet build  
```  

Execute o projeto:  
```bash  
dotnet run  
```  

## Valida��es  

### 1. **Valida��es de Pacientes**  
- **Nome**: Deve conter ao menos 5 caracteres.  
- **CPF**: Deve ser v�lido e �nico no sistema.  
- **Data de Nascimento**: Apenas maiores de 13 anos s�o permitidos.  

### 2. **Valida��es de Consultas**  
- **Hor�rio**: Dentro do per�odo das 8h �s 19h e m�ltiplos de 15 minutos.  
- **Conflito**: N�o � permitido agendar consultas em hor�rios j� ocupados.  
- **Consulta Pendente**: Um paciente com consulta pendente n�o pode agendar outra.  

### 3. **Valida��es de Exclus�es**  
- **Pacientes**: S� podem ser exclu�dos se n�o possu�rem consultas futuras.  
- **Agendamentos**: Apenas consultas futuras podem ser removidas.  

## Tecnologias Utilizadas  

- **.NET Core**: Framework robusto para aplica��es.  
- **C#**: Linguagem de programa��o principal.  
- **Entity Framework Core**: ORM para intera��es com banco de dados.  

## Contribui��es  

Sinta-se � vontade para fazer um fork do projeto, sugerir melhorias ou abrir issues. Para contribuir:  
1. Fa�a um fork do reposit�rio.  
2. Crie uma branch para sua feature:  
   ```bash  
   git checkout -b minha-feature  
   ```  
3. Ap�s as mudan�as, envie um pull request.  

