# Agenda_Consultório

Este é um sistema de gerenciamento de um consultório Odontológico desenvolvido em **.NET** para agendar e gerenciar consultas de pacientes. O sistema permite o cadastro de pacientes, o agendamento de consultas e a listagem de agendamentos.

## Funcionalidades

### 1. **Cadastro de Pacientes**
- **Cadastrar paciente**: Permite o cadastro de um novo paciente informando seu nome, CPF e data de nascimento.
- **Excluir paciente**: Exclui um paciente da lista de pacientes cadastrados.
- **Listar pacientes**: Permite listar os pacientes, com opções de ordenação:
  - Por **CPF**.
  - Por **nome**.

### 2. **Agenda de Consultas**
- **Agendar consulta**: Permite agendar uma consulta para um paciente, definindo data, hora de início e de término.
- **Cancelar consulta**: Permite cancelar um agendamento de consulta existente.
- **Listar agendamentos**: Exibe os agendamentos de consulta com detalhes, como data, hora de início, hora de término e nome do paciente. A lista pode ser filtrada por data.

## Estrutura do Projeto

1. **Models**: Contém as classes que representam os dados do sistema.
    - `Paciente.cs`: Representa os pacientes com dados como CPF, nome e data de nascimento.
    - `Agendamento.cs`: Representa os agendamentos de consultas, incluindo data, hora de início, hora de término e CPF do paciente.
    - `Sistema.cs`: Gerencia a execução das funcionalidades principais do sistema (cadastro, agendamento, listagem).

2. **Views**: Contém as classes responsáveis por exibir as opções para o usuário no console.
    - `Menus.cs`: Exibe os menus interativos para navegação entre as funcionalidades.
    - `Respostas.cs`: Exibe as respostas ou resultados das operações, como a listagem de pacientes e agendamentos.

3. **Program.cs**: Contém o ponto de entrada principal do sistema, onde o método `run()` é chamado para iniciar a execução do programa.

## Como Executar

### 1. Clonar o Repositório

Clone o repositório para o seu ambiente local usando o comando:
```bash
git clone https://github.com/billygrahan/Agenda_Consultorio
cd Agenda_Consultorio
```

### 2. Restaurar Dependências

Para restaurar as dependências do projeto, use:
```bash
dotnet restore
```

### 3. Compilar o Projeto

Compile o projeto para verificar se tudo está correto:
```bash
dotnet build
```

### 4. Executar o Projeto

Para rodar o projeto e iniciar a aplicação no console, utilize:
```bash
dotnet run
```

### 5. Interação com o Sistema

Ao rodar o programa, o menu principal será exibido no console com as seguintes opções:
- **1**: Cadastro de Pacientes
- **2**: Agenda de Consultas
- **3**: Finalizar o programa

Dependendo da opção escolhida, o sistema exibirá um novo menu com as opções relacionadas (cadastrar, listar, excluir pacientes, agendar, cancelar agendamentos, etc.).

## Validações

### 1. **Validação de Pacientes**
- **Nome**: O nome do paciente deve ter pelo menos 5 caracteres.
- **CPF**: O CPF do paciente deve ser válido e único no sistema.
- **Data de Nascimento**: O paciente deve ter pelo menos 13 anos. A data de nascimento deve estar no formato "dd/MM/yyyy".

### 2. **Validação de Agendamentos**
- **CPF do Paciente**: O CPF informado para agendamento deve corresponder a um paciente cadastrado.
- **Data da Consulta**: A data da consulta deve ser futura seguindo os padrões: "dd/MM/yyyy" , "hhmm".
- **Hora Inicial e Hora Final**: As horas de início e término da consulta devem ser válidas, dentro do horário de funcionamento (08:00 às 19:00) e múltiplas de 15 minutos. A hora final não pode ser antes da hora inicial.
- **Conflito de Horários**: O sistema verifica se já existem consultas agendadas no mesmo horário.
- **Consulta Pendente**: Para cadastrar uma nova consulta o usuário não deve ter nenhuma consulta pendente.

### 3. **Validação de Exclusões**
- **Exclusão de Paciente**: Pacientes com agendamentos futuros não podem ser excluídos.
- **Exclusão de Agendamento**: Apenas agendamentos futuros podem ser excluídos.

## Tecnologias Utilizadas

- **.NET**: Framework utilizado para o desenvolvimento do backend, oferecendo suporte completo para construção de aplicações robustas.
- **C#**: Linguagem de programação utilizada para criar a lógica do sistema.


