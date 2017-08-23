CREATE TABLE Veiculo (
	VeiculoID				INT NOT NULL IDENTITY(1,1),
	Nome					VARCHAR(100) NOT NULL,
	Placa					VARCHAR(8) NOT NULL,
	CONSTRAINT PK_Veiculo PRIMARY KEY (VeiculoID)
)
INSERT INTO Veiculo Values('Veículo de Teste', 'AGP7799');

CREATE TABLE Fabricante (
	FabricanteID			INT NOT NULL IDENTITY(1,1),
	Nome					VARCHAR(100) NOT NULL,
	Email					VARCHAR(100) NULL,
	CONSTRAINT PK_Fabricante PRIMARY KEY (FabricanteID)
)
INSERT INTO Fabricante Values('Fabricante 1', 'wilsondonizetti@hotmail.com');
INSERT INTO Fabricante Values('Fabricante 2', 'wilsondonizetti@hotmail.com');

CREATE TABLE Fornecedor (
	FornecedorID			INT NOT NULL IDENTITY(1,1),
	Nome					VARCHAR(100) NOT NULL,
	Email					VARCHAR(100) NULL,
	CONSTRAINT PK_Fornecedor PRIMARY KEY (FornecedorID)
)
INSERT INTO Fornecedor Values('Fornecedor 1', 'wilsondonizetti@hotmail.com');
INSERT INTO Fornecedor Values('Fornecedor 2', 'wilsondonizetti@hotmail.com');
INSERT INTO Fornecedor Values('Fornecedor 3', 'wilsondonizetti@hotmail.com');

CREATE TABLE Cliente (
	ClienteID				INT NOT NULL IDENTITY(1,1),
	Nome					VARCHAR(100) NOT NULL,
	CPFCNPJ					VARCHAR(20) NULL,
	Email					VARCHAR(100) NULL,
	CONSTRAINT PK_Cliente PRIMARY KEY (ClienteID)
)

INSERT INTO Cliente Values('Zacharias Cristiano Papéis LTDA', '54966897000131', 'wilsondonizetti@hotmail.com');
INSERT INTO Cliente Values('Hans Rogério Manutenções LTDA', '33351701000124','wilsondonizetti@hotmail.com');
INSERT INTO Cliente Values('Marta Bastos Consultoria LTDA', '16306691000196','wilsondonizetti@hotmail.com');

CREATE TABLE Propaganda (
	PropagandaID			INT NOT NULL IDENTITY(1,1),
	Descricao				VARCHAR(100) NOT NULL,
	Observacoes				TEXT NULL,
	DataInicio				DateTime,
	DataFim					DateTime,
	CONSTRAINT PK_Propaganda PRIMARY KEY (PropagandaID)
)

INSERT INTO Propaganda Values('Propaganda de teste', 'Testes','2017-08-01', NULL);

CREATE TABLE Produto (
	ProdutoID				INT NOT NULL IDENTITY(1,1),
	Nome					VARCHAR(100) NOT NULL,
	UN						VARCHAR(3) NOT NULL,
	FabricanteID			INT NOT NULL,
	FornecedorID			INT NULL,
	Preco					DECIMAL(15,2) NOT NULL,
	CONSTRAINT PK_Produto PRIMARY KEY (ProdutoID),
	CONSTRAINT FK_Produto_Fabricante FOREIGN KEY (FabricanteID) REFERENCES Fabricante (FabricanteID),
	CONSTRAINT FK_Produto_Fornecedor FOREIGN KEY (FornecedorID) REFERENCES Fornecedor (FornecedorID)
)
INSERT INTO Produto Values('Produto 1', 'UN', 1,1, 20.00);
INSERT INTO Produto Values('Produto 2', 'KG', 2,2, 15.95);
INSERT INTO Produto Values('Produto 3', 'L', 1,3, 11.21);

CREATE TABLE Lote (
	LoteID					INT NOT NULL IDENTITY(1,1),
	ProdutoID				INT NOT NULL,
	Validade				DATETIME NULL,
	Codigo					VARCHAR(20) NOT NULL,
	FabricanteID			INT NOT NULL,
	Quantidade				DECIMAL(15,4) NOT NULL,
	CONSTRAINT PK_Lote PRIMARY KEY (LoteID),
	CONSTRAINT FK_Lote_Fabricante FOREIGN KEY (FabricanteID) REFERENCES Fabricante (FabricanteID),
	CONSTRAINT FK_Lote_Produto FOREIGN KEY (ProdutoID) REFERENCES Produto (ProdutoID)
)
INSERT INTO Lote Values(1, '2017-08-22','ALKJ2014', 1, 10);
INSERT INTO Lote Values(1, '2018-08-22','ALKJ2017', 1, 5);
INSERT INTO Lote Values(2, '2017-12-22','BBHP2017', 2, 5);
INSERT INTO Lote Values(3, '2018-08-22','LKJG2015', 1, 6);

CREATE TABLE Estoque (
	ProdutoID				INT NOT NULL,
	Quantidade				DECIMAL(15,4) NOT NULL,
	Bloqueado				DECIMAL(15,4) NOT NULL DEFAULT 0.00,
	CONSTRAINT PK_Estoque PRIMARY KEY (ProdutoID),
	CONSTRAINT FK_Estoque_Produto FOREIGN KEY (ProdutoID) REFERENCES Produto (ProdutoID)
)

INSERT INTO Estoque Values(1, 15, 5);
INSERT INTO Estoque Values(2, 5, 5);
INSERT INTO Estoque Values(3, 6, 6);

CREATE TABLE Promocao (
	PromocaoID				INT NOT NULL IDENTITY(1,1),
	ClienteID				INT NULL,
	Descricao				VARCHAR(100) NOT NULL,
	DataInicio				DateTime,
	DataFim					DateTime,
	CONSTRAINT PK_Promocao PRIMARY KEY (PromocaoID),
	CONSTRAINT FK_Promocao_Cliente FOREIGN KEY (ClienteID) REFERENCES Cliente (ClienteID)
)

INSERT INTO Promocao Values(2, 'Promocao por Cliente', '2017-08-01', '2017-12-31');
INSERT INTO Promocao Values(NULL, 'Promocao por Produto', '2017-08-01', '2017-12-31');

CREATE TABLE ItemPromocao (
	ItemPromocaoID			INT NOT NULL IDENTITY(1,1),
	PromocaoID				INT NOT NULL,
	ProdutoID				INT NOT NULL,
	Desconto				DECIMAL(15,4) NULL,
	Valor					DECIMAL(15,4) NULL,
	CONSTRAINT PK_ItemPromocao PRIMARY KEY (ItemPromocaoID),
	CONSTRAINT FK_ItemPromocao_Promocao FOREIGN KEY (PromocaoID) REFERENCES Promocao (PromocaoID),
	CONSTRAINT FK_ItemPromocao_Produto FOREIGN KEY (ProdutoID) REFERENCES Produto (ProdutoID)
)

INSERT INTO ItemPromocao Values(1, 1, 10, NULL);
INSERT INTO ItemPromocao Values(2, 1, NULL, 18.50);
INSERT INTO ItemPromocao Values(2, 2, NULL, 14.00);
INSERT INTO ItemPromocao Values(2, 3, NULL, 9.00);

CREATE TABLE SituacaoPedido (
	SituacaoPedidoID		SMALLINT NOT NULL,
	Descricao				VARCHAR(100) NOT NULL,	
	CONSTRAINT PK_SituacaoPedido PRIMARY KEY (SituacaoPedidoID)	
)

INSERT INTO SituacaoPedido Values(1, 'Aberto');
INSERT INTO SituacaoPedido Values(2, 'Faturado');
INSERT INTO SituacaoPedido Values(3, 'Cancelado');

CREATE TABLE Pedido (
	PedidoID				INT NOT NULL IDENTITY(1,1),
	ClienteID				INT NOT NULL,
	SituacaoPedidoID		SMALLINT NOT NULL,
	DataEmissao				DateTime,
	DataPrevEntrega			DateTime,
	Valor					Decimal(15,2) NOT NULL,
	CONSTRAINT PK_Pedido PRIMARY KEY (PedidoID),
	CONSTRAINT FK_Pedido_Cliente FOREIGN KEY (ClienteID) REFERENCES Cliente (ClienteID),
	CONSTRAINT FK_Pedido_SituacaoPedido FOREIGN KEY (SituacaoPedidoID) REFERENCES SituacaoPedido (SituacaoPedidoID)
)

INSERT INTO Pedido Values(1, 1, '2017-08-01', '2017-12-31', 100);
INSERT INTO Pedido Values(2, 1, '2017-08-01', '2017-12-31', 100);
INSERT INTO Pedido Values(1, 1, '2017-08-01', '2017-12-31', 100);
INSERT INTO Pedido Values(2, 1, '2017-08-01', '2017-12-31', 100);
INSERT INTO Pedido Values(1, 1, '2017-08-01', '2017-12-31', 100);
INSERT INTO Pedido Values(2, 1, '2017-08-01', '2017-12-31', 100);

CREATE TABLE ItemPedido (
	ItemPedidoID			INT NOT NULL IDENTITY(1,1),
	PedidoID				INT NOT NULL,
	ProdutoID				INT NOT NULL,
	Quantidade				DECIMAL(15,4) NOT NULL,
	Valor					DECIMAL(15,4) NOT NULL,
	Desconto				DECIMAL(15,4) NULL,
	LoteID					INT,
	CONSTRAINT PK_ItemPedido PRIMARY KEY (ItemPedidoID),
	CONSTRAINT FK_ItemPedido_Pedido FOREIGN KEY (PedidoID) REFERENCES Pedido (PedidoID),
	CONSTRAINT FK_ItemPedido_Produto FOREIGN KEY (ProdutoID) REFERENCES Produto (ProdutoID),
	CONSTRAINT FK_ItemPedido_Lote FOREIGN KEY (LoteID) REFERENCES Lote (LoteID)
)

INSERT INTO ItemPedido Values(1, 1, 2, 50.00, NULL, 1);
INSERT INTO ItemPedido Values(2, 1, 2, 25.00, NULL, 1);
INSERT INTO ItemPedido Values(2, 2, 2, 25.00, NULL, 3);
INSERT INTO ItemPedido Values(3, 1, 2, 50, NULL, 2);
INSERT INTO ItemPedido Values(4, 1, 2, 50, NULL, 1);
INSERT INTO ItemPedido Values(5, 1, 2, 50, NULL, 2);
INSERT INTO ItemPedido Values(6, 1, 2, 50, NULL, 1);