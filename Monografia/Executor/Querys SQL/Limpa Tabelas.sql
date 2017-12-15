/*Limpa tabelas*/

--Delete From DadosTreinamento
--Delete From DadosValidacao
--Delete From Elemento
Delete From LogTreinamento
Delete From RedeNeuralResultante
Delete From Treinamento
Delete From TreinamentoDados
Delete from ValidacaoDados

--Delete From dadosRedeNeural
--DBCC CHECKIDENT ('dadosRedeNeural', RESEED, 0)
--TRUNCATE TABLE DadosTreinamento
--DBCC CHECKIDENT ('DadosTreinamento', RESEED, 0)

--TRUNCATE TABLE DadosValidacao
--DBCC CHECKIDENT ('DadosValidacao', RESEED, 0)

--TRUNCATE TABLE Elemento
--DBCC CHECKIDENT ('Elemento', RESEED, 0)

--TRUNCATE TABLE LogTreinamento
DBCC CHECKIDENT ('LogTreinamento', RESEED, 0)

--TRUNCATE TABLE RedeNeuralResultante
DBCC CHECKIDENT ('RedeNeuralResultante', RESEED, 0)

--TRUNCATE TABLE Treinamento
DBCC CHECKIDENT ('Treinamento', RESEED, 0)

--TRUNCATE TABLE TreinamentoDados
DBCC CHECKIDENT ('TreinamentoDados', RESEED, 0)

--TRUNCATE TABLE Validacao
DBCC CHECKIDENT ('ValidacaoDados', RESEED, 0)