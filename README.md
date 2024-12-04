# Agenda_Consultório  

Este é um sistema de gerenciamento para um consultório odontológico, desenvolvido em **.NET**. Ele permite agendar e gerenciar consultas de pacientes, além de realizar operações como cadastro, exclusão e listagem.  

## Nota Importante  
**O programa ainda está em desenvolvimento e precisa de ajustes para melhor desempenho.**  
As validações e consultas, que atualmente são feitas diretamente nas listas em memória, deverão ser reimplementadas para serem realizadas no banco de dados. No início da execução, todas as tabelas são importadas para listas em memória para realizar essas operações.  

## Funcionalidades  

### 1. **Cadastro de Pacientes**  
- **Cadastrar paciente**: Cadastro de pacientes informando nome, CPF e data de nascimento.  
- **Excluir paciente**: Exclui um paciente, desde que não possua agendamentos futuros.  
- **Listar pacientes**: Permite visualizar os pacientes cadastrados com as seguintes opções de ordenação:  
  - Por **CPF**.  
  - Por **nome**.  

### 2. **Agenda de Consultas**  
- **Agendar consulta**: Permite agendar consultas com validação de horários e conflitos.  
- **Cancelar consulta**: Remove consultas futuras da agenda.  
- **Listar agendamentos**: Exibe os agendamentos com detalhes, com filtro opcional por período.  

## Estrutura do Projeto  

1. **Controllers**: Interação com o banco de dados.  
2. **Models**: Representam os dados do sistema:  
    - `Paciente`: Gerencia informações dos pacientes.  
    - `Agendamento`: Gerencia dados de consultas agendadas.  
3. **Views**: Responsáveis pela interação visual (menus e respostas).  
4. **Sistema**: Classe principal que orquestra o funcionamento do sistema.  
5. **Program.cs**: Contém o ponto de entrada do sistema.  

## Como Executar  

### 1. Clonar o Repositório  

Clone o repositório para o seu ambiente local:  
```bash  
git clone https://github.com/billygrahan/Agenda_Consultorio  
cd Agenda_Consultorio  
```  

### 2. Restaurar Dependências  

Use o comando abaixo para restaurar as dependências do projeto:  
```bash  
dotnet restore  
```  

### 3. Configurar a String de Conexão  

1. Abra o arquivo `AppDbContext.cs` na pasta `Context`.  
2. Localize o método `OnConfiguring` e ajuste os valores de conexão:  
   ```csharp  
   string host = "SEU_HOST"; // Exemplo: "localhost"  
   string username = "SEU_USUARIO"; // Exemplo: "postgres"  
   string password = "SUA_SENHA"; // Exemplo: "@marelO50"  
   string database = "SEU_BANCO_DE_DADOS"; // Exemplo: "ApiConsultórioBD"  

   string connectionString = $"Host={host};Username={username};Password={password};Database={database}";  

   optionsBuilder.UseNpgsql(connectionString);  
   ```  
3. Salve as alterações.  

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

## Validações  

### 1. **Validações de Pacientes**  
- **Nome**: Deve conter ao menos 5 caracteres.  
- **CPF**: Deve ser válido e único no sistema.  
- **Data de Nascimento**: Apenas maiores de 13 anos são permitidos.  

### 2. **Validações de Consultas**  
- **Horário**: Dentro do período das 8h às 19h e múltiplos de 15 minutos.  
- **Conflito**: Não é permitido agendar consultas em horários já ocupados.  
- **Consulta Pendente**: Um paciente com consulta pendente não pode agendar outra.  

### 3. **Validações de Exclusões**  
- **Pacientes**: Só podem ser excluídos se não possuírem consultas futuras.  
- **Agendamentos**: Apenas consultas futuras podem ser removidas.  

## Tecnologias Utilizadas  

- **.NET Core**: Framework robusto para aplicações.  
- **C#**: Linguagem de programação principal.  
- **Entity Framework Core**: ORM para interações com banco de dados.  

## Contribuições  

Sinta-se à vontade para fazer um fork do projeto, sugerir melhorias ou abrir issues. Para contribuir:  
1. Faça um fork do repositório.  
2. Crie uma branch para sua feature:  
   ```bash  
   git checkout -b minha-feature  
   ```  
3. Após as mudanças, envie um pull request.  

