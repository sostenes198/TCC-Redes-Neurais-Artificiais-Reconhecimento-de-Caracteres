
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 08/14/2013 21:10:26
-- Generated from EDMX file: E:\TFS\Monografia\Monografia\Executor\Modelo\MonografiaModelo.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Monografia];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Treinamento_Algoritmo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Treinamento] DROP CONSTRAINT [FK_Treinamento_Algoritmo];
GO
IF OBJECT_ID(N'[dbo].[FK_Treinamento_DadosRedeNeural]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Treinamento] DROP CONSTRAINT [FK_Treinamento_DadosRedeNeural];
GO
IF OBJECT_ID(N'[dbo].[FK_DadosTreinamento_Elemento]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DadosTreinamento] DROP CONSTRAINT [FK_DadosTreinamento_Elemento];
GO
IF OBJECT_ID(N'[dbo].[FK_TreinamentoDados_DadosTreinamento]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TreinamentoDados] DROP CONSTRAINT [FK_TreinamentoDados_DadosTreinamento];
GO
IF OBJECT_ID(N'[dbo].[FK_DadosValidacao_Elemento]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DadosValidacao] DROP CONSTRAINT [FK_DadosValidacao_Elemento];
GO
IF OBJECT_ID(N'[dbo].[FK_Validacao_DadosValidacao]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ValidacaoDados] DROP CONSTRAINT [FK_Validacao_DadosValidacao];
GO
IF OBJECT_ID(N'[dbo].[FK_TreinamentoDados_Treinamento]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TreinamentoDados] DROP CONSTRAINT [FK_TreinamentoDados_Treinamento];
GO
IF OBJECT_ID(N'[dbo].[FK_Validacao_Treinamento]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ValidacaoDados] DROP CONSTRAINT [FK_Validacao_Treinamento];
GO
IF OBJECT_ID(N'[dbo].[FK_Treinamento_RedeNeuralResultante]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Treinamento] DROP CONSTRAINT [FK_Treinamento_RedeNeuralResultante];
GO
IF OBJECT_ID(N'[dbo].[FK_LogTreinamento_Treinamento]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LogTreinamento] DROP CONSTRAINT [FK_LogTreinamento_Treinamento];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Algoritmo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Algoritmo];
GO
IF OBJECT_ID(N'[dbo].[DadosRedeNeural]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DadosRedeNeural];
GO
IF OBJECT_ID(N'[dbo].[DadosTreinamento]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DadosTreinamento];
GO
IF OBJECT_ID(N'[dbo].[DadosValidacao]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DadosValidacao];
GO
IF OBJECT_ID(N'[dbo].[Elemento]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Elemento];
GO
IF OBJECT_ID(N'[dbo].[Treinamento]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Treinamento];
GO
IF OBJECT_ID(N'[dbo].[TreinamentoDados]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TreinamentoDados];
GO
IF OBJECT_ID(N'[dbo].[ValidacaoDados]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ValidacaoDados];
GO
IF OBJECT_ID(N'[dbo].[RedeNeuralResultante]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RedeNeuralResultante];
GO
IF OBJECT_ID(N'[dbo].[LogTreinamento]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LogTreinamento];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Algoritmo'
CREATE TABLE [dbo].[Algoritmo] (
    [Codigo] int IDENTITY(1,1) NOT NULL,
    [Descricao] varchar(100)  NOT NULL
);
GO

-- Creating table 'DadosRedeNeural'
CREATE TABLE [dbo].[DadosRedeNeural] (
    [Codigo] int IDENTITY(1,1) NOT NULL,
    [NumeroNeuroniosEntrada] int  NOT NULL,
    [NumeroNeuroniosOculta] int  NOT NULL,
    [NumeroNeuroniosSaida] int  NOT NULL,
    [Pesos] varbinary(max)  NOT NULL
);
GO

-- Creating table 'DadosTreinamento'
CREATE TABLE [dbo].[DadosTreinamento] (
    [Codigo] int IDENTITY(1,1) NOT NULL,
    [Dados] varbinary(5000)  NOT NULL,
    [Elemento] int  NOT NULL,
    [FiguraOriginal] varbinary(max)  NOT NULL
);
GO

-- Creating table 'DadosValidacao'
CREATE TABLE [dbo].[DadosValidacao] (
    [Codigo] int IDENTITY(1,1) NOT NULL,
    [Dados] varbinary(5000)  NOT NULL,
    [Elemento] int  NOT NULL,
    [FiguraOriginal] varbinary(max)  NOT NULL
);
GO

-- Creating table 'Elemento'
CREATE TABLE [dbo].[Elemento] (
    [Codigo] int IDENTITY(1,1) NOT NULL,
    [ValorMinimo] float  NOT NULL,
    [ValorIdeal] float  NOT NULL,
    [ValorMaximo] float  NOT NULL,
    [Descricao] varchar(100)  NOT NULL
);
GO

-- Creating table 'Treinamento'
CREATE TABLE [dbo].[Treinamento] (
    [Codigo] int IDENTITY(1,1) NOT NULL,
    [Algoritmo] int  NOT NULL,
    [NumeroGeracoes] int  NOT NULL,
    [Momentum] float  NOT NULL,
    [TaxaAprendizado] float  NOT NULL,
    [DadosRedeNeural] int  NOT NULL,
    [MinimoLocal] bit  NOT NULL,
    [RedeNeuralResultante] int  NOT NULL,
    [NumeroExemplos] int  NOT NULL
);
GO

-- Creating table 'TreinamentoDados'
CREATE TABLE [dbo].[TreinamentoDados] (
    [Treinamento] int  NOT NULL,
    [DadosTreinamento] int  NOT NULL,
    [Validado] bit  NOT NULL,
    [ValorRetornado] float  NOT NULL,
    [Codigo] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'ValidacaoDados'
CREATE TABLE [dbo].[ValidacaoDados] (
    [Treinamento] int  NOT NULL,
    [DadosValidacao] int  NOT NULL,
    [Validado] bit  NOT NULL,
    [ValorRetornado] float  NOT NULL,
    [Codigo] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'RedeNeuralResultante'
CREATE TABLE [dbo].[RedeNeuralResultante] (
    [Codigo] int IDENTITY(1,1) NOT NULL,
    [PesosResultante] varbinary(max)  NOT NULL
);
GO

-- Creating table 'LogTreinamento'
CREATE TABLE [dbo].[LogTreinamento] (
    [Treinamento] int  NOT NULL,
    [Geracao] int  NOT NULL,
    [MargemErro] float  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Codigo] in table 'Algoritmo'
ALTER TABLE [dbo].[Algoritmo]
ADD CONSTRAINT [PK_Algoritmo]
    PRIMARY KEY CLUSTERED ([Codigo] ASC);
GO

-- Creating primary key on [Codigo] in table 'DadosRedeNeural'
ALTER TABLE [dbo].[DadosRedeNeural]
ADD CONSTRAINT [PK_DadosRedeNeural]
    PRIMARY KEY CLUSTERED ([Codigo] ASC);
GO

-- Creating primary key on [Codigo] in table 'DadosTreinamento'
ALTER TABLE [dbo].[DadosTreinamento]
ADD CONSTRAINT [PK_DadosTreinamento]
    PRIMARY KEY CLUSTERED ([Codigo] ASC);
GO

-- Creating primary key on [Codigo] in table 'DadosValidacao'
ALTER TABLE [dbo].[DadosValidacao]
ADD CONSTRAINT [PK_DadosValidacao]
    PRIMARY KEY CLUSTERED ([Codigo] ASC);
GO

-- Creating primary key on [Codigo] in table 'Elemento'
ALTER TABLE [dbo].[Elemento]
ADD CONSTRAINT [PK_Elemento]
    PRIMARY KEY CLUSTERED ([Codigo] ASC);
GO

-- Creating primary key on [Codigo] in table 'Treinamento'
ALTER TABLE [dbo].[Treinamento]
ADD CONSTRAINT [PK_Treinamento]
    PRIMARY KEY CLUSTERED ([Codigo] ASC);
GO

-- Creating primary key on [Codigo] in table 'TreinamentoDados'
ALTER TABLE [dbo].[TreinamentoDados]
ADD CONSTRAINT [PK_TreinamentoDados]
    PRIMARY KEY CLUSTERED ([Codigo] ASC);
GO

-- Creating primary key on [Codigo] in table 'ValidacaoDados'
ALTER TABLE [dbo].[ValidacaoDados]
ADD CONSTRAINT [PK_ValidacaoDados]
    PRIMARY KEY CLUSTERED ([Codigo] ASC);
GO

-- Creating primary key on [Codigo] in table 'RedeNeuralResultante'
ALTER TABLE [dbo].[RedeNeuralResultante]
ADD CONSTRAINT [PK_RedeNeuralResultante]
    PRIMARY KEY CLUSTERED ([Codigo] ASC);
GO

-- Creating primary key on [Treinamento], [Geracao] in table 'LogTreinamento'
ALTER TABLE [dbo].[LogTreinamento]
ADD CONSTRAINT [PK_LogTreinamento]
    PRIMARY KEY CLUSTERED ([Treinamento], [Geracao] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Algoritmo] in table 'Treinamento'
ALTER TABLE [dbo].[Treinamento]
ADD CONSTRAINT [FK_Treinamento_Algoritmo]
    FOREIGN KEY ([Algoritmo])
    REFERENCES [dbo].[Algoritmo]
        ([Codigo])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Treinamento_Algoritmo'
CREATE INDEX [IX_FK_Treinamento_Algoritmo]
ON [dbo].[Treinamento]
    ([Algoritmo]);
GO

-- Creating foreign key on [DadosRedeNeural] in table 'Treinamento'
ALTER TABLE [dbo].[Treinamento]
ADD CONSTRAINT [FK_Treinamento_DadosRedeNeural]
    FOREIGN KEY ([DadosRedeNeural])
    REFERENCES [dbo].[DadosRedeNeural]
        ([Codigo])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Treinamento_DadosRedeNeural'
CREATE INDEX [IX_FK_Treinamento_DadosRedeNeural]
ON [dbo].[Treinamento]
    ([DadosRedeNeural]);
GO

-- Creating foreign key on [Elemento] in table 'DadosTreinamento'
ALTER TABLE [dbo].[DadosTreinamento]
ADD CONSTRAINT [FK_DadosTreinamento_Elemento]
    FOREIGN KEY ([Elemento])
    REFERENCES [dbo].[Elemento]
        ([Codigo])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DadosTreinamento_Elemento'
CREATE INDEX [IX_FK_DadosTreinamento_Elemento]
ON [dbo].[DadosTreinamento]
    ([Elemento]);
GO

-- Creating foreign key on [DadosTreinamento] in table 'TreinamentoDados'
ALTER TABLE [dbo].[TreinamentoDados]
ADD CONSTRAINT [FK_TreinamentoDados_DadosTreinamento]
    FOREIGN KEY ([DadosTreinamento])
    REFERENCES [dbo].[DadosTreinamento]
        ([Codigo])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TreinamentoDados_DadosTreinamento'
CREATE INDEX [IX_FK_TreinamentoDados_DadosTreinamento]
ON [dbo].[TreinamentoDados]
    ([DadosTreinamento]);
GO

-- Creating foreign key on [Elemento] in table 'DadosValidacao'
ALTER TABLE [dbo].[DadosValidacao]
ADD CONSTRAINT [FK_DadosValidacao_Elemento]
    FOREIGN KEY ([Elemento])
    REFERENCES [dbo].[Elemento]
        ([Codigo])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DadosValidacao_Elemento'
CREATE INDEX [IX_FK_DadosValidacao_Elemento]
ON [dbo].[DadosValidacao]
    ([Elemento]);
GO

-- Creating foreign key on [DadosValidacao] in table 'ValidacaoDados'
ALTER TABLE [dbo].[ValidacaoDados]
ADD CONSTRAINT [FK_Validacao_DadosValidacao]
    FOREIGN KEY ([DadosValidacao])
    REFERENCES [dbo].[DadosValidacao]
        ([Codigo])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Validacao_DadosValidacao'
CREATE INDEX [IX_FK_Validacao_DadosValidacao]
ON [dbo].[ValidacaoDados]
    ([DadosValidacao]);
GO

-- Creating foreign key on [Treinamento] in table 'TreinamentoDados'
ALTER TABLE [dbo].[TreinamentoDados]
ADD CONSTRAINT [FK_TreinamentoDados_Treinamento]
    FOREIGN KEY ([Treinamento])
    REFERENCES [dbo].[Treinamento]
        ([Codigo])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TreinamentoDados_Treinamento'
CREATE INDEX [IX_FK_TreinamentoDados_Treinamento]
ON [dbo].[TreinamentoDados]
    ([Treinamento]);
GO

-- Creating foreign key on [Treinamento] in table 'ValidacaoDados'
ALTER TABLE [dbo].[ValidacaoDados]
ADD CONSTRAINT [FK_Validacao_Treinamento]
    FOREIGN KEY ([Treinamento])
    REFERENCES [dbo].[Treinamento]
        ([Codigo])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Validacao_Treinamento'
CREATE INDEX [IX_FK_Validacao_Treinamento]
ON [dbo].[ValidacaoDados]
    ([Treinamento]);
GO

-- Creating foreign key on [RedeNeuralResultante] in table 'Treinamento'
ALTER TABLE [dbo].[Treinamento]
ADD CONSTRAINT [FK_Treinamento_RedeNeuralResultante]
    FOREIGN KEY ([RedeNeuralResultante])
    REFERENCES [dbo].[RedeNeuralResultante]
        ([Codigo])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Treinamento_RedeNeuralResultante'
CREATE INDEX [IX_FK_Treinamento_RedeNeuralResultante]
ON [dbo].[Treinamento]
    ([RedeNeuralResultante]);
GO

-- Creating foreign key on [Treinamento] in table 'LogTreinamento'
ALTER TABLE [dbo].[LogTreinamento]
ADD CONSTRAINT [FK_LogTreinamento_Treinamento]
    FOREIGN KEY ([Treinamento])
    REFERENCES [dbo].[Treinamento]
        ([Codigo])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------