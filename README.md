# Agenda_Consult�rio

Este � um sistema de gerenciamento de um consult�rio Odontol�gico desenvolvido em **.NET** para agendar e gerenciar consultas de pacientes. O sistema permite o cadastro de pacientes, o agendamento de consultas e a listagem de agendamentos.

## Funcionalidades

### 1. **Cadastro de Pacientes**
- **Cadastrar paciente**: Permite o cadastro de um novo paciente informando seu nome, CPF e data de nascimento.
- **Excluir paciente**: Exclui um paciente da lista de pacientes cadastrados.
- **Listar pacientes**: Permite listar os pacientes, com op��es de ordena��o:
  - Por **CPF**.
  - Por **nome**.

### 2. **Agenda de Consultas**
- **Agendar consulta**: Permite agendar uma consulta para um paciente, definindo data, hora de in�cio e de t�rmino.
- **Cancelar consulta**: Permite cancelar um agendamento de consulta existente.
- **Listar agendamentos**: Exibe os agendamentos de consulta com detalhes, como data, hora de in�cio, hora de t�rmino e nome do paciente. A lista pode ser filtrada por data.

## Estrutura do Projeto

1. **Models**: Cont�m as classes que representam os dados do sistema.
    - `Paciente.cs`: Representa os pacientes com dados como CPF, nome e data de nascimento.
    - `Agendamento.cs`: Representa os agendamentos de consultas, incluindo data, hora de in�cio, hora de t�rmino e CPF do paciente.
    - `Sistema.cs`: Gerencia a execu��o das funcionalidades principais do sistema (cadastro, agendamento, listagem).

2. **Views**: Cont�m as classes respons�veis por exibir as op��es para o usu�rio no console.
    - `Menus.cs`: Exibe os menus interativos para navega��o entre as funcionalidades.
    - `Respostas.cs`: Exibe as respostas ou resultados das opera��es, como a listagem de pacientes e agendamentos.

3. **Program.cs**: Cont�m o ponto de entrada principal do sistema, onde o m�todo `run()` � chamado para iniciar a execu��o do programa.

## Como Executar

### 1. Clonar o Reposit�rio

Clone o reposit�rio para o seu ambiente local usando o comando:
```bash
git clone https://github.com/billygrahan/Agenda_Consultorio
cd Agenda_Consultorio
```

### 2. Restaurar Depend�ncias

Para restaurar as depend�ncias do projeto, use:
```bash
dotnet restore
```

### 3. Compilar o Projeto

Compile o projeto para verificar se tudo est� correto:
```bash
dotnet build
```

### 4. Executar o Projeto

Para rodar o projeto e iniciar a aplica��o no console, utilize:
```bash
dotnet run
```

### 5. Intera��o com o Sistema

Ao rodar o programa, o menu principal ser� exibido no console com as seguintes op��es:
- **1**: Cadastro de Pacientes
- **2**: Agenda de Consultas
- **3**: Finalizar o programa

Dependendo da op��o escolhida, o sistema exibir� um novo menu com as op��es relacionadas (cadastrar, listar, excluir pacientes, agendar, cancelar agendamentos, etc.).

## Valida��es

### 1. **Valida��o de Pacientes**
- **Nome**: O nome do paciente deve ter pelo menos 5 caracteres.
- **CPF**: O CPF do paciente deve ser v�lido e �nico no sistema.
- **Data de Nascimento**: O paciente deve ter pelo menos 13 anos. A data de nascimento deve estar no formato "dd/MM/yyyy".

### 2. **Valida��o de Agendamentos**
- **CPF do Paciente**: O CPF informado para agendamento deve corresponder a um paciente cadastrado.
- **Data da Consulta**: A data da consulta deve ser futura seguindo os padr�es: "dd/MM/yyyy" , "hhmm".
- **Hora Inicial e Hora Final**: As horas de in�cio e t�rmino da consulta devem ser v�lidas, dentro do hor�rio de funcionamento (08:00 �s 19:00) e m�ltiplas de 15 minutos. A hora final n�o pode ser antes da hora inicial.
- **Conflito de Hor�rios**: O sistema verifica se j� existem consultas agendadas no mesmo hor�rio.
- **Consulta Pendente**: Para cadastrar uma nova consulta o usu�rio n�o deve ter nenhuma consulta pendente.

### 3. **Valida��o de Exclus�es**
- **Exclus�o de Paciente**: Pacientes com agendamentos futuros n�o podem ser exclu�dos.
- **Exclus�o de Agendamento**: Apenas agendamentos futuros podem ser exclu�dos.

## Tecnologias Utilizadas

- **.NET**: Framework utilizado para o desenvolvimento do backend, oferecendo suporte completo para constru��o de aplica��es robustas.
- **C#**: Linguagem de programa��o utilizada para criar a l�gica do sistema.


